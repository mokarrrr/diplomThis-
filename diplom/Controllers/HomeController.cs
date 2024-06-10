using diplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using diplom.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Data;

namespace diplom.Controllers
{
    public class ChangePassRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
    public class HomeController : Controller
    {
        private MainContext db = new MainContext();
        private readonly ILogger<HomeController> _logger;


        public int IdProduct1 { get; private set; }

        //List<Product> Products = MainContext.Instantce.Products.ToList();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult AboutUsPage()
        {
            return View();
        }
        public IActionResult Supplies(string searchQuery)
        {
            IQueryable<_Provider> suppliersQuery = db._Provider.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Фильтруем поставщиков на основе поискового запроса (например, по имени поставщика)
                suppliersQuery = suppliersQuery.Where(p => p.provider_name.ToLower().Contains(searchQuery.ToLower()));
            }

            // Преобразуем запрос в список (выполняем запрос к базе данных и получаем отфильтрованных поставщиков)
            var suppliersList = suppliersQuery.ToList();

            return View(suppliersList);
        }

        public IActionResult Package(string searchQuery)
        {
            IQueryable<Package> packagesQuery = db.Package.AsQueryable();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Фильтруем пакеты на основе поискового запроса (например, по названию или идентификатору)
                packagesQuery = packagesQuery.Where(p => p.Package_name.ToLower().Contains(searchQuery.ToLower()) || p.Package_id.ToString().Contains(searchQuery.ToLower()));
            }

            // Преобразуем запрос в список (выполняем запрос к базе данных и получаем отфильтрованные пакеты)
            var packagesList = packagesQuery.ToList();

            return View(packagesList);
        }
        //public IActionResult AdminPage()
        //{
        //    return View();
        //}
        public ActionResult AdminPage(string searchQuery, int? selectedProductId, string packageNames, string providerNames, int? categoryId, int? secondCategoryId, int? thirdCategoryId, int? fourthCategoryId, int? fifthCategoryId)
        {
            IQueryable<Product> productsQuery = db.Product.Include(p => p.Rates);
            bool hasResults = true;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                productsQuery = productsQuery
                    .Where(p => p.Name_product.Contains(searchQuery) || p.product_article.Contains(searchQuery));
                hasResults = productsQuery.Any();
            }
            else
            {
                hasResults = productsQuery.Any();
            }

            // Фильтруем продукты по каждой категории, если соответствующий categoryId не равен null
            FilterProductsByCategory(ref productsQuery, categoryId);
            FilterProductsByCategory(ref productsQuery, secondCategoryId);
            FilterProductsByCategory(ref productsQuery, thirdCategoryId);
            FilterProductsByCategory(ref productsQuery, fourthCategoryId);
            FilterProductsByCategory(ref productsQuery, fifthCategoryId);

            var productsList = productsQuery.ToList();

            Dictionary<int, double> productAverageRates = productsList
                .ToDictionary(p => p.IdProduct, p => p.Rates.Any() ? p.Rates.Average(r => r._Rate) : 0);
            ProductViewModel viewModel = new ProductViewModel
            {
                Products = productsList,
                HasResults = hasResults,
                ProductCount = productsList.Count,
                PackageNames = packageNames,
                ProviderNames = providerNames,
                SelectedProductId = selectedProductId ?? 0,
                ProductAverageRates = productAverageRates,

            };

            return View(viewModel);
        }

        public IActionResult UserOrders(string searchQuery, int pageNumber = 1, int pageSize = 5)
        {
            long userIdCookie = Int64.Parse(Request.Cookies["UserId"]);
            var userOrdersQuery = db._orders.Where(o => o._user_id == userIdCookie)
                                            .Include(o => o.Products)
                                            .Include(o => o.Status)
                                            .OrderByDescending(o => o.order_date)
                                            .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Фильтруем заказы по айди заказа
                userOrdersQuery = userOrdersQuery.Where(o => o.Idorder.ToString().Contains(searchQuery));
            }

            int totalOrders = userOrdersQuery.Count();
            var userOrdersList = userOrdersQuery.Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToList();

            int totalPages = (int)Math.Ceiling(totalOrders / (double)pageSize);

            var viewModel = new ULVM
            {
                _orders = userOrdersList,
                OrderPageNumber = pageNumber,
                OrderTotalPages = totalPages,
                OrderPageSize = pageSize,
                HasOrders = userOrdersList.Any()
            };

            return View(viewModel);
        }
        public IActionResult AllOrders(string searchQuery, int pageNumber = 1, int pageSize = 5)
        {
            IQueryable<_order> ordersQuery = db._orders
                .Include(o => o.Products)
                .Include(o => o.Status)
                .Include(o => o.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                ordersQuery = ordersQuery.Where(o => o.User.Surname.ToLower().Contains(searchQuery.ToLower()) || o.User.IdUser.ToString().Contains(searchQuery.ToLower()));
            }

            int totalOrders = ordersQuery.Count();
            var ordersList = ordersQuery
                .OrderByDescending(o => o.order_date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalPages = (int)Math.Ceiling(totalOrders / (double)pageSize);

            var viewModel = new ULVM
            {
                _orders = ordersList,
                OrderPageNumber = pageNumber,
                OrderTotalPages = totalPages,
                OrderPageSize = pageSize,
                HasOrders = ordersList.Any()
            };

            ViewBag.SearchQuery = searchQuery;
            ViewBag.PageSize = pageSize;

            var statuses = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Принят" },
        new SelectListItem { Value = "2", Text = "Идет отправка" },
        new SelectListItem { Value = "3", Text = "В доставке" },
        new SelectListItem { Value = "4", Text = "Ожидает получения" },
        new SelectListItem { Value = "5", Text = "Завершён" },
        new SelectListItem { Value = "6", Text = "Отменен" }
    };

            ViewBag.Statuses = statuses;

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, int newStatusId)
        {
            var order = db._orders.FirstOrDefault(o => o.Idorder == orderId);
            if (order == null)
            {
                return NotFound(new { success = false, message = "Order not found" });
            }

            order.status_id = newStatusId;
            db.SaveChanges();

            var statusName = db._statuses
                .Where(s => s.Idstatus == newStatusId)
                .Select(s => s.status_name)
                .FirstOrDefault();

            return Json(new { success = true, statusName = statusName });
        }


        public IActionResult AllUsers(string searchQuery, int pageNumber = 1, int pageSize = 16)
        {
            IQueryable<User> usersQuery = db.User.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                usersQuery = usersQuery.Where(u =>
                    u.User_name.ToLower().Contains(searchQuery.ToLower()) ||
                    u.Surname.ToLower().Contains(searchQuery.ToLower()) ||
                    u.IdUser.ToString().Contains(searchQuery.ToLower())
                );
            }

            int totalUsers = usersQuery.Count();
            var usersList = usersQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            var viewModel = new ULVM
            {
                Users = usersList,
                UserPageNumber = pageNumber,
                UserTotalPages = totalPages,
                UserPageSize = pageSize,
                HasUsers = usersList.Any()
            };

            ViewBag.PageSize = pageSize;

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUser(int userId, string userName, string surname, string email, string phoneNumber)
        {
            var user = await db.User.FirstOrDefaultAsync(u => u.IdUser == userId);

            if (user == null)
            {
                return NotFound(); // Если пользователь не найден, возвращаем ошибку 404
            }

            // Обновляем данные пользователя
            user.User_name = userName;
            user.Surname = surname;
            user.Email = email;
            user.PhoneNumber = phoneNumber;

            db.Update(user);
            await db.SaveChangesAsync();

            return Ok(); // Возвращаем успешный результат
        }
        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            // Находим пользователя по Id
            var user = db.User.FirstOrDefault(u => u.IdUser == userId);

            if (user == null)
            {
                return NotFound(); // Если пользователь не найден, вернуть 404
            }

            try
            {
                var ordersCount = db._orders.Count(o => o._user_id == userId);

                if (ordersCount > 0)
                {
                    // Если у пользователя есть связанные заказы, возвращаем сообщение об ошибке
                    return StatusCode(500, "Невозможно удалить пользователя, так как у нео есть связанные заказы.");
                }

                // Находим и удаляем все связанные записи в таблице Rate, где client_id равен userId
                var ratesToDelete = db.Rate.Where(r => r.client_id == userId);
                db.Rate.RemoveRange(ratesToDelete);

                // Удаляем пользователя
                db.User.Remove(user);

                // Сохраняем изменения в базе данных
                db.SaveChanges();

                // Возвращаем успешный результат клиенту
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при удалении пользователя, возвращаем соответствующий статус код
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpGet]
        //public IActionResult GetStatusName(int statusId)
        //{
        //    var status = db._statuses.FirstOrDefault(s => s.Id == statusId);

        //    if (status != null)
        //    {
        //        return Ok(status.status_name);
        //    }
        //    else
        //    {
        //        return NotFound("Статус не найден");
        //    }
        //}
        public IActionResult User_like()
        {
            string userIdCookie = Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            {
                // Запрос к базе данных для получения избранных продуктов пользователя
                var liked = db.UserLike.Where(cart => cart.UserID == userId).Include(cart => cart.Product).ToList();
                ULVM VM = new ULVM
                {
                    User_Likes = liked
                };
                return View(VM);
            }

            // В случае, если IdUser отсутствует в куках, можно реализовать другую логику
            return Json(new { success = false, message = "Не удалось получить информацию о пользователе." });
        }
        public IActionResult User_basket()
        {
            string userIdCookie = Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            {
                // Запрос к базе данных для получения избранных продуктов пользователя
                var liked = db.User_Baskets.Where(cart => cart.UserID == userId).Include(cart => cart.Product).ToList();
                Cart VM = new Cart
                {
                    Users_Baskets = liked
                };
                return View(VM);
            }

            // В случае, если IdUser отсутствует в куках, можно реализовать другую логику
            return Json(new { success = false, message = "Не удалось получить информацию о пользователе." });
        }
        //public ActionResult MainPage(string searchQuery)
        //{
        //    IQueryable<Product> products = db.Product;
        //    if (!string.IsNullOrEmpty(searchQuery))
        //    {
        //        products = products.Where(x => x.Name_product == searchQuery || x.product_article == searchQuery);
        //    }
        //    ProductViewModel PVM = new ProductViewModel
        //    {
        //        Products = products.ToList(),
        //    };
        //    return View(PVM);
        //}

        public ActionResult MainPage(string searchQuery, int? selectedProductId, string packageNames, string providerNames, int? categoryId, int? secondCategoryId, int? thirdCategoryId, int? fourthCategoryId, int? fifthCategoryId, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Product> productsQuery = db.Product.Include(p => p.Rates);
            bool hasResults = true;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                productsQuery = productsQuery
                    .Where(p => p.Name_product.Contains(searchQuery) || p.product_article.Contains(searchQuery));
                hasResults = productsQuery.Any();
            }
            else
            {
                hasResults = productsQuery.Any();
            }

            // Фильтруем продукты по каждой категории, если соответствующий categoryId не равен null
            FilterProductsByCategory(ref productsQuery, categoryId);
            FilterProductsByCategory(ref productsQuery, secondCategoryId);
            FilterProductsByCategory(ref productsQuery, thirdCategoryId);
            FilterProductsByCategory(ref productsQuery, fourthCategoryId);
            FilterProductsByCategory(ref productsQuery, fifthCategoryId);

            productsQuery = productsQuery.Where(p => !p.Ishidden);

            int totalProducts = productsQuery.Count();
            var productsList = productsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            Dictionary<int, double> productAverageRates = productsList
                .ToDictionary(p => p.IdProduct, p => p.Rates.Any() ? p.Rates.Average(r => r._Rate) : 0);

            ProductViewModel viewModel = new ProductViewModel
            {
                Products = productsList,
                HasResults = hasResults,
                ProductCount = totalProducts,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
                PackageNames = packageNames,
                ProviderNames = providerNames,
                SelectedProductId = selectedProductId ?? 0,
                ProductAverageRates = productAverageRates,
            };

            ViewBag.PageSize = pageSize;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetProviderName(int providerId)
        {
            var providerName = db._Provider
                .Where(p => p.IdProvider == providerId)
                .Select(p => p.provider_name)
                .FirstOrDefault();

            return Content(providerName);
        }
        [HttpGet]
        public PartialViewResult FilteredProductsPartial(int? categoryId, int? secondCategoryId, int? thirdCategoryId, int? fourthCategoryId, int? fifthCategoryId)
        {
            IQueryable<Product> productsQuery = db.Product.Include(p => p.Rates);

            FilterProductsByCategory(ref productsQuery, categoryId);
            FilterProductsByCategory(ref productsQuery, secondCategoryId);
            FilterProductsByCategory(ref productsQuery, thirdCategoryId);
            FilterProductsByCategory(ref productsQuery, fourthCategoryId);
            FilterProductsByCategory(ref productsQuery, fifthCategoryId);

            var model = productsQuery.ToList();


            return PartialView("_ProductPartial", model);
        }
        private void FilterProductsByCategory(ref IQueryable<Product> productsQuery, int? categoryId)
        {
            if (categoryId.HasValue)
            {
                var productIdsInCategory = db.catalog_Product
                    .Where(pc => pc.Id_catalog == categoryId.Value)
                    .Select(pc => pc.id_product_catalog)
                    .ToList();

                productsQuery = productsQuery
                    .Where(p => productIdsInCategory.Contains(p.IdProduct));

            }
        }


        //private IQueryable<Product> ApplyFilters(IQueryable<Product> productsQuery, string searchQuery, int? categoryId)
        //{
        //    if (!string.IsNullOrEmpty(searchQuery))
        //    {
        //        productsQuery = productsQuery
        //            .Where(p => p.Name_product.Contains(searchQuery) || p.product_article.Contains(searchQuery));
        //    }

        //    if (categoryId.HasValue)
        //    {
        //        var productIdsInCategory = db.catalog_Product
        //            .Where(pc => pc.Id_catalog == categoryId.Value)
        //            .Select(pc => pc.id_product_catalog)
        //            .ToList();

        //        productsQuery = productsQuery
        //            .Where(p => productIdsInCategory.Contains(p.IdProduct));
        //    }

        //    return productsQuery;
        //}


        public ActionResult GetProductRating(int productId)
        {
            // Получаю рейтинг для выбранного продукта и верните его в виде строки
            var ratesForProduct = db.Rate.Where(r => r.Productid == productId).ToList();
            double averageRate = ratesForProduct.Any() ? ratesForProduct.Average(r => r._Rate) : 0;


            averageRate = Math.Round(averageRate, 1);

            return Content(averageRate.ToString());
        }

        public Dictionary<int, int> CountRatings(int productId)
        {
            // Получение оценок для выбранного продукта
            var ratesForProduct = db.Rate.Where(r => r.Productid == productId).ToList();

            // Создание словаря для хранения количества каждой оценки
            Dictionary<int, int> ratingCounts = new Dictionary<int, int>();

            // Подсчет количества каждой оценки
            foreach (var rate in ratesForProduct)
            {
                int convertedRate = (int)rate._Rate; // Преобразование в int
                if (ratingCounts.ContainsKey(convertedRate))
                {
                    ratingCounts[convertedRate]++;
                }
                else
                {
                    ratingCounts[convertedRate] = 1;
                }
            }

            return ratingCounts;
        }
        [HttpGet]
        public IActionResult GetProductCategory(int idProduct)
        {
            var catalogProduct = db.catalog_Product.FirstOrDefault(cp => cp.id_product_catalog == idProduct);
            if (catalogProduct == null)
            {
                return NotFound();
            }
            return Json(catalogProduct);
        }
        [HttpPost]
        [ActionName("UpdateProductCategory")]
        public IActionResult UpdateProductCategory(int productId, int categoryId)
        {
            try
            {
                // Найдите продукт по его идентификатору
                var product = db.catalog_Product.FirstOrDefault(p => p.id_product_catalog == productId);

                if (product == null)
                {
                    return NotFound(); // Если продукт не найден, вернуть ошибку 404
                }

                // Выполните сырой SQL-запрос для принудительного изменения значения внешнего ключа
                db.Database.ExecuteSqlRaw("UPDATE catalog_Product SET Id_catalog = {0} WHERE id_product_catalog = {1}", categoryId, productId);

                return Ok(); // Вернуть успешный статус 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка при обновлении категории продукта: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetProduct(int Id)
        {
            try
            {
                // Получаем продукт из базы данных с загрузкой связанных сущностей (Rates и User)
                var product = db.Product
                    .Include(p => p.Rates)
                        .ThenInclude(r => r.User)
                    .FirstOrDefault(p => p.IdProduct == Id);

                if (product == null)
                {
                    // Если продукт не найден, возвращаем NotFound
                    return NotFound();
                }

                // Возвращаем успешный ответ с кодом 200 и объектом продукта в формате JSON
                return Ok(product);
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем код 500 с сообщением об ошибке
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult GetPackageName(int packageId)
        {
            var packageName = db.Package
                .Where(p => p.Package_id == packageId)
                .Select(p => p.Package_name)
                .FirstOrDefault();

            return Content(packageName);
        }
        [HttpGet]
        public ActionResult GetStatusName(int statusId)
        {
            var statName = db._statuses
                .Where(p => p.Idstatus == statusId)
                .Select(p => p.status_name)
                .FirstOrDefault();

            return Content(statName);
        }
        public ActionResult Login(string phoneLogin, string password)
        {
            // Поиск пользователя по номеру телефона
           var user = FindUserByPhone(phoneLogin);

            if (user != null && VerifyPassword(password, user.user_password))
            {
                // Если учетные данные верны
                HttpContext.Response.Cookies.Append("UserId", user.IdUser.ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(400),
                    HttpOnly = true,
                    Secure = false,
                    Path = "/"
                });

                // Удаляем лишние пробелы из строк "admin" и роли пользователя
                string adminRole = "admin".Trim();
                string userRole = user.role.Trim();

                // Включаем информацию о роли пользователя в JSON ответ
                if (userRole == adminRole)
                {
                    // Установка куки с ролью администратора
                    HttpContext.Response.Cookies.Append("UserRole", userRole, new CookieOptions
                    {
                        Expires = DateTime.Now.AddHours(400),
                        HttpOnly = false,
                        Secure = false,
                        Path = "/"
                    });
                }

                var responseData = new
                {
                    success = true,
                    message = "Авторизация успешна.",
                    userName = user.User_name,
                    role = userRole
                };

                return Json(responseData);
            }
            else
            {
                return Json(new { success = false, message = "Неверный номер телефона или пароль." });
            }
        }

        [HttpGet]
        public IActionResult CheckUserRoleCookie()
        {
            if (HttpContext.Request.Cookies.TryGetValue("UserRole", out string userRole) )
            {
                return Ok(new { isAdmin = true });
            }

            return Ok(new { isAdmin = false });
        }

        [HttpPost]
        public IActionResult CancelOrder(int id)
        {
            try
            {
                // Найдите заказ по его идентификатору в базе данных и установите статус 6 (Отменен)
                var order = db._orders.FirstOrDefault(o => o.Idorder == id);

                if (order != null)
                {
                    order.status_id = 6; // Устанавливаем статус "Отменен"

                    // Сохраняем изменения в базе данных
                    db.SaveChanges();

                    return Ok(); // Возвращаем успешный результат
                }
                else
                {
                    return NotFound(); // Заказ не найден
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при отмене заказа: {ex.Message}"); // Возвращаем ошибку сервера
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(string city, string address, string comment, int totalPrice, Dictionary<int, int> productQuantities, string paymentMethod)
        {
            try
            {
                string userIdCookie = Request.Cookies["UserId"];

                if (string.IsNullOrEmpty(userIdCookie))
                {
                    return StatusCode(401, new { error = "User ID not found in cookies" });
                }

                List<int> insufficientProducts = new List<int>();

                // Проверка наличия достаточного количества товаров
                foreach (var kvp in productQuantities)
                {
                    int productId = kvp.Key;
                    int quantity = kvp.Value;

                    // Получаем продукт из базы данных
                    var product = await db.Product.FirstOrDefaultAsync(p => p.IdProduct == productId);

                    if (product == null || quantity < 0)
                    {
                        return StatusCode(400, new { error = $"Invalid product ID or quantity: {productId}" });
                    }

                    // Проверяем, достаточное ли количество товара на складе
                    if (product.product_remain < quantity)
                    {
                        insufficientProducts.Add(productId);
                    }
                }

                if (insufficientProducts.Any())
                {
                    var insufficientProductsInfo = insufficientProducts.Select(id =>
                    {
                        var product = db.Product.FirstOrDefault(p => p.IdProduct == id);
                        return new
                        {
                            ProductId = id,
                            ProductName = product.Name_product ?? "Unknown Product",
                            AvailableQuantity = product.product_remain ?? 0
                        };
                    }).ToList();

                    // Возвращаем ответ с информацией о недостающих товарах
                    return StatusCode(400, new
                    {
                        error = "Insufficient stock for some products",
                        insufficientProducts = insufficientProductsInfo
                    });
                }

                // Создаем новый заказ
                var order = new _order
                {
                    status_id = 1, // Например, устанавливаем начальный статус заказа
                    order_city = city,
                    order_date = DateTime.Now,
                    order_address = address,
                    order_commentary = comment,
                    OrderSum = totalPrice,
                    Payment = paymentMethod, // Сохраняем способ оплаты
                    _user_id = int.Parse(userIdCookie) // Преобразуем строку с идентификатором пользователя в целое число
                };

                // Добавляем заказ в контекст базы данных
                db._orders.Add(order);

                // Сохраняем изменения в базе данных
                await db.SaveChangesAsync();

                // Возвращаем успешный ответ с сообщением о создании заказа
                return Ok(new { message = "Order created successfully", orderId = order.Idorder });
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем соответствующий статус код и сообщение об ошибке
                return StatusCode(500, new { error = $"Failed to create order: {ex.Message}" });
            }
        }



        [HttpPost]
        public async Task<IActionResult> SaveComment(int productId, string comment, int rating)
        {
            // Получаем ID пользователя из куки
            string userIdCookie = Request.Cookies["UserId"];

            // Проверяем, что ID пользователя из куки представляет собой целое число
            if (!int.TryParse(userIdCookie, out int userId))
            {
                // Если не удалось преобразовать ID пользователя, вернем ошибку
                return BadRequest("Неверный формат ID пользователя.");
            }

            // Проверяем, существует ли уже комментарий от данного пользователя для данного товара
            var existingComment = db.Rate.FirstOrDefault(c => c.Productid == productId && c.client_id == userId);

            if (existingComment != null)
            {
                // Если комментарий существует, обновляем его
                existingComment._Rate = rating;
                existingComment.Rate_comment = comment;
            }
            else
            {
                // Создаем новый комментарий
                var newComment = new Rate
                {
                    _Rate = rating, // Используем переданный рейтинг
                    Rate_comment = comment,
                    client_id = userId, // Используем ID пользователя из куки
                    Productid = productId
                };

                // Добавляем комментарий в контекст базы данных
                db.Rate.Add(newComment);
            }

            // Сохраняем изменения в базе данных
            db.SaveChanges();

            // Формируем JSON объект с сообщением об успехе
            var response = new { message = "Комментарий успешно сохранен!" };

            // Возвращаем успешный результат с JSON объектом
            return Ok(response);
        }


        [HttpPost]
        public IActionResult AddItemsToOrderDetails(int orderId, Dictionary<int, int> productQuantities)
        {
            try
            {
                // Получаем UserId из куки
                string userIdCookie = Request.Cookies["UserId"];

                // Проверяем, существует ли запись в таблице UserLike
                if (int.TryParse(userIdCookie, out int userId))
                {
                    // Добавляем каждый товар из корзины пользователя в таблицу order_detail
                    foreach (var kvp in productQuantities)
                    {
                        int productId = kvp.Key;
                        int quantity = kvp.Value;

                        // Получаем продукт по его идентификатору
                        var product = db.Product.FirstOrDefault(p => p.IdProduct == productId);

                        if (product != null && quantity > 0)
                        {
                            // Проверяем, достаточное ли количество товара на складе
                            if (product.product_remain >= quantity)
                            {
                                // Вычитаем quantity из product_remain
                                product.product_remain -= quantity;

                                // Создаем запись в таблице order_detail
                                db.order_detail.Add(new order_detail
                                {
                                    id_product = productId,
                                    id_order = orderId,
                                    product_take = quantity
                                });
                            }
                            else
                            {
                                // Если на складе не достаточно товара, возвращаем ошибку
                                return Json(new { error = $"Недостаточно товара с идентификатором {productId}" });
                            }
                        }
                    }

                    // Удаление товаров из корзины пользователя после добавления в заказ
                    var productIdsToRemove = productQuantities.Where(kvp => kvp.Value >= 1).Select(kvp => kvp.Key).ToList();
                    var userBasketItems = db.User_Baskets
                        .Where(item => item.UserID == userId && productIdsToRemove.Contains(item.ProductID))
                        .ToList();
                    db.User_Baskets.RemoveRange(userBasketItems);
                    db.SaveChanges();

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { error = "UserId не найден в куках" });
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем сообщение об ошибке
                return Json(new { error = ex.Message });
            }
        }






        [HttpGet]
        public IActionResult CheckComment(int productId)
        {
            // Получаем ID пользователя из куки
            string userIdCookie = Request.Cookies["UserId"];

            // Проверяем, что ID пользователя из куки представляет собой целое число
            if (!int.TryParse(userIdCookie, out int userId))
            {
                // Если не удалось преобразовать ID пользователя, возвращаем ошибку
                return BadRequest("Неверный формат ID пользователя.");
            }

            // Ищем комментарий для данного пользователя и товара
            var comment = db.Rate.FirstOrDefault(c => c.Productid == productId && c.client_id == userId);

            if (comment != null)
            {
                // Если комментарий найден, возвращаем его
                return Json(new { success = true, comment = comment.Rate_comment });
            }
            else
            {
                // Если комментарий не найден, возвращаем сообщение об отсутствии комментария
                return Json(new { success = false });
            }
        }
        public ActionResult UserProfile()
        {
            // Получение значения из куки
            string userIdCookie = Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie))
            {
                // Преобразование значения куки в int (предполагается, что UserId - это целочисленное значение)
                if (int.TryParse(userIdCookie, out int userId))
                {
                    // Поиск пользователя в БД по UserId
                    var user = FindUserById(userId);

                    if (user != null)
                    {
                        // Возвращаем информацию о пользователе
                        return Json(new { success = true,
                            email = user.Email,
                            phoneNumber = user.PhoneNumber,
                            name = user.User_name,
                            surname = user.Surname
                        });
                    }
                }
            }

            // Если что-то пошло не так или пользователь не найден, возвращаем сообщение об ошибке
            return Json(new { success = false, message = "Не удалось получить информацию о пользователе." });
        }

        [HttpPost]

        public JsonResult SaveUser(string email, string name, string lastName, string phone, string originalPhone, string originalEmail)
        {
            try
            {
                // Проверка наличия символов "@" и "."
                if (!email.Contains("@") || !email.Contains("."))
                {
                    return Json(new { success = false, message = "Некорректный адрес электронной почты" });
                }

                // Проверка наличия четырех нулей подряд в номере телефона
                if (phone.Contains("0000"))
                {
                    return Json(new { success = false, message = "Номер телефона содержит четыре подряд нуля" });
                }
                if (phone.All(c => c == '0'))
                {
                    return Json(new { success = false, message = "Номер телефона не может состоять только из нулей" });
                }

                // Проверка минимальной длины номера телефона
                if (phone.Length < 10)
                {
                    return Json(new { success = false, message = "Номер телефона должен содержать не более 10 символов" });
                }
                if (phone.Length > 10)
                {
                    return Json(new { success = false, message = "Номер телефона должен содержать не менее 10 символов" });
                }
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(phone))
                {
                    return Json(new { success = false, message = "Все поля должны быть заполнены" });
                }


                // Проверка существования пользователя по адресу электронной почты, только если email изменился


                // Получение идентификатора пользователя из cookie
                string userIdCookie = Request.Cookies["UserId"];

                // Поиск пользователя по идентификатору из cookie
                int userId;
                if (int.TryParse(userIdCookie, out userId))
                {
                    // Поиск пользователя по идентификатору из cookie
                    var existingUser = db.User.FirstOrDefault(u => u.IdUser == userId);

                    if (existingUser != null)
                    {
                        existingUser.Email = email;
                        existingUser.User_name = name;
                        existingUser.Surname = lastName;
                        existingUser.PhoneNumber = phone;

                        // Сохраняем изменения
                        db.SaveChanges();

                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Пользователь не найден" });
                    }
                }
                else
                {
                    // Обработка ошибки преобразования
                    return Json(new { success = false, message = "Ошибка при получении идентификатора пользователя" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Ошибка: {ex.Message}" });
            }
        }


        [HttpPost]
        public JsonResult CheckPhoneNumber(string phone, string originalPhone)
        {
            try
            {
                // Проверка существования номера телефона в базе данных
                if (phone != originalPhone)
                {
                    var existingUser = db.User.FirstOrDefault(u => u.PhoneNumber == phone);
                    return Json(new { exists = existingUser != null });
                }
                else
                {
                    // Номер телефона не был изменен, поэтому считаем, что такой номер уже существует
                    return Json(new { exists = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Ошибка: {ex.Message}" });
            }
        }
        [HttpPost]
        public JsonResult CheckEmail(string email, string originalEmail)
        {
            try
            {
                // Проверка существования email в базе данных
                if (email != originalEmail)
                {
                    var existingUser = db.User.FirstOrDefault(u => u.Email == email);
                    return Json(new { exists = existingUser != null });
                }
                else
                {
                    // Email не был изменен, поэтому считаем, что такой email уже существует
                    return Json(new { exists = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Ошибка: {ex.Message}" });
            }
        }

        private User FindUserById(int userId)
        {
            // Подключение к вашему контексту базы данных

                try
                {
                    // Поиск пользователя по Id
                    var user = db.User.FirstOrDefault(u => u.IdUser == userId);

                    // Вернуть найденного пользователя или null, если пользователь не найден
                    return user;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок, если необходимо
                    // Логирование ошибок, вывод в консоль, и т.д.
                    Console.WriteLine($"Ошибка при поиске пользователя: {ex.Message}");
                    return null;
                }
            
        }

        private User FindUserByPhone(string phoneLogin)
        {
            // Ваш код для поиска пользователя по номеру телефона в базе данных
            // Пример:
            return db.User.FirstOrDefault(u => u.PhoneNumber.Equals(phoneLogin) );
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                var enteredPasswordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // Сравниваем хеши введенного пароля и хешированного пароля из базы данных
                return string.Equals(enteredPasswordHash, hashedPassword, StringComparison.OrdinalIgnoreCase);
            }
        }
        private string HashPasswordSHA256(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool IsPasswordValid(string enteredPasswordHash, string hashedPassword)
        {
            // Сравниваем хеши введенного пароля и хешированного пароля из базы данных
            return string.Equals(enteredPasswordHash, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        [HttpPost]
        public IActionResult UpdatePassword([FromBody] ChangePassRequest changePassRequest)
        {
            // Получаем ID пользователя из куки
            string userIdCookie = Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(userIdCookie))
            {
                return Unauthorized(); // Куки с идентификатором пользователя отсутствуют или некорректны
            }

            if (!int.TryParse(userIdCookie, out int userId))
            {
                return Unauthorized(); // Некорректный формат идентификатора пользователя
            }

            // Получаем пользователя из базы данных по его ID
            var user = db.User.Find(userId);

            if (user == null)
            {
                return NotFound(); // Пользователь не найден
            }

            // Хешируем введенный старый пароль и сравниваем его с хешем пароля из базы данных
            var sha256 = SHA256.Create();
            var oldPasswordHash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(changePassRequest.oldPassword))).Replace("-", "").ToLower();
            if (!IsPasswordValid(oldPasswordHash, user.user_password))
            {
                return BadRequest(new { error = "Неверный старый пароль" }); // Неправильный старый пароль
            }

            // Хешируем новый пароль и обновляем в базе данных
            var newPasswordHash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(changePassRequest.newPassword))).Replace("-", "").ToLower();
            user.user_password = newPasswordHash;
            db.SaveChanges();

            return Ok(); // Пароль успешно изменен
        }


        private User FindUser(string phoneLogin, string password)
        {
            try
            {
                var user = db.User.FirstOrDefault(u => u.PhoneNumber == phoneLogin && u.user_password == password);
                return user;
            }
            catch (Exception ex)
            {
                // Вывести информацию об ошибке в консоль или логи
                Console.WriteLine($"Ошибка при поиске пользователя: {ex.Message}");
                throw; // Пробросить исключение для обработки в вызывающем коде
            }
        }


        [HttpPost]
        public ActionResult Register(string Email, string Name, string lastName, string passwordregister, string passwordregistersecond, string phoneRegister, bool consentChecked)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(passwordregister) || string.IsNullOrEmpty(passwordregistersecond) || string.IsNullOrEmpty(phoneRegister))
            {
                // Возвращаем сообщение об ошибке
                return Json(new { success = false, message = "Не все поля заполнены" });
            }

            // Проверка пароля и его подтверждения
            

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(Email);
            }
            catch (FormatException)
            {
                // Ошибка в формате почты (отсутствует знак '@')
                return Json(new { success = false, message = "Неверный формат адреса электронной почты" });
            }

            // Проверка наличия трех подряд нулей в номере телефона
            if (phoneRegister.Contains("0000"))
            {
                // Номер телефона содержит четыре подряд нуля
                return Json(new { success = false, message = "Номер телефона содержит четыре подряд нуля" });
            }

            if (passwordregister != passwordregistersecond)
            {
                return Json(new { success = false, message = "Пароли не совпадают" });
            }
            // Проверка, что номер телефона не состоит из всех нулей
            if (phoneRegister.All(c => c == '0'))
            {
                return Json(new { success = false, message = "Номер телефона не может состоять только из нулей" });
            }
            if (phoneRegister.Length != 10)
            {
                // Номер телефона должен содержать ровно 10 символов
                return Json(new { success = false, message = "Номер телефона должен содержать ровно 10 символов" });
            }

            // Проверка состояния чекбокса


            if (!consentChecked)
            {
                // Чекбокс не отмечен
                return Json(new { success = false, message = "Необходимо согласиться с условиями" });
            }

            // Проверка существования пользователя с такой почтой или номером телефона
            bool userExistsByEmail = UserExistsByEmail(Email);
            bool userExistsByPhoneNumber = UserExistsByPhoneNumber(phoneRegister);

            if (userExistsByPhoneNumber || userExistsByEmail)
            {
                if (userExistsByPhoneNumber && userExistsByEmail)
                {
                    // Пользователь с таким номером телефона и с такой почтой уже существует
                    return Json(new { success = false, message = "Пользователь с таким номером телефона и адресом электронной почты уже существует" });
                }

                if (userExistsByEmail)
                {
                    // Пользователь с такой почтой уже зарегистрирован
                    return Json(new { success = false, message = "Пользователь с таким адресом электронной почты уже зарегистрирован" });
                }

                if (userExistsByPhoneNumber)
                {
                    // Пользователь с таким номером телефона уже зарегистрирован
                    return Json(new { success = false, message = "Пользователь с таким номером телефона уже зарегистрирован" });
                }
            }

            // Создание нового пользователя
            var newUser = new User
            {
                PhoneNumber = phoneRegister,
                Email = Email,
                User_name = Name,
                Surname = lastName,
                user_password = HashPassword(passwordregister),
                role = "user"
                // Хеширование пароля

                // Дополнительные поля, если есть
            };

            // Добавление пользователя в базу данных
            db.User.Add(newUser);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Произошла ошибка при добавлении пользователя
                return Json(new { success = false, message = "Произошла ошибка при регистрации пользователя: " + ex.Message });
            }

            // Возвращаем успешный результат, если все проверки прошли успешно
            return Json(new { success = true, message = "Регистрация успешна" });
        }

        // Метод для проверки существования пользователя по номеру телефона
        private bool UserExistsByPhoneNumber(string phoneNumber)
        {
            return db.User.Any(u => u.PhoneNumber == phoneNumber);
        }

        // Метод для проверки существования пользователя по адресу электронной почты
        private bool UserExistsByEmail(string email)
        {
            return db.User.Any(u => u.Email == email);
        }

        // Метод для хеширования пароля
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public IActionResult Logout()
        {
            // Удаление куки 'UserId'
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserRole");
            return Ok(); // Пример возвращения успешного ответа
        }

        public IActionResult LikedProducts()
        {
            // Получаем IdUser из кук
            string userIdCookie = Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            {
                // Запрос к базе данных для получения избранных продуктов пользователя
                var liked = db.UserLike.Where(cart => cart.UserID == userId).Include(cart => cart.Product).ToList();
                ULVM VM = new ULVM
                {
                    User_Likes = liked
                };
                return View(VM);
            }

            // В случае, если IdUser отсутствует в куках, можно реализовать другую логику
            return RedirectToAction("Index", "Home"); // Например, перенаправление на главную страницу
        }

        [HttpPost]
        public IActionResult CheckUserLike(int productId)
        {
            try
            {
                // Получаем UserId из куки
                string userIdCookie = Request.Cookies["UserId"];

                // Проверяем, существует ли запись в таблице UserLike
                if (int.TryParse(userIdCookie, out int userId))
                {
                    var existingLike = db.UserLike.FirstOrDefault(like => like.UserID == userId && like.ProductID == productId);
                    return Json(new { exists = existingLike != null, userId = userId });
                }
                else
                {
                    return Json(new { error = "UserId не найден в куках" });
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем сообщение об ошибке
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddUserLike(int userId, int productId)
        {
            try
            {
                // Создаем новую запись в таблице UserLike
                var newUserLike = new UserLikes
                {
                    UserID = userId,
                    ProductID = productId
                };

                // Добавляем запись в контекст базы данных и сохраняем изменения
                db.UserLike.Add(newUserLike);
                db.SaveChanges();

                return Json(new { success = true, message = "Запись успешно добавлена" });
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем сообщение об ошибке
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RemoveUserLike(int productId)
        {
            try
            {
                // Получаем значение куки "UserId"
                string userIdCookie = HttpContext.Request.Cookies["UserId"];

                // Если куки не содержат значения, вы можете вернуть ошибку или принять другие меры
                if (string.IsNullOrEmpty(userIdCookie))
                {
                    return Json(new { success = false, message = "Куки с идентификатором пользователя не найдены" });
                }

                int userId = int.Parse(userIdCookie);

                // Находим запись в таблице UserLike для данного пользователя и продукта
                var userLike = db.UserLike.FirstOrDefault(ul => ul.UserID == userId && ul.ProductID == productId);

                // Если запись найдена, удаляем ее из базы данных
                if (userLike != null)
                {
                    db.UserLike.Remove(userLike);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Товар успешно удален из избранного" });
                }
                else
                {
                    return Json(new { success = false, message = "Запись о лайке не найдена" });
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем сообщение об ошибке
                return Json(new { error = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult CheckUserBasket(int productId)
        {
            try
            {
                // Получаем UserId из куки
                string userIdCookie = Request.Cookies["UserId"];

                // Проверяем, существует ли запись в таблице UserLike
                if (int.TryParse(userIdCookie, out int userId))
                {
                    var existingLike = db.User_Baskets.FirstOrDefault(like => like.UserID == userId && like.ProductID == productId);
                    return Json(new { exists = existingLike != null, userId = userId });
                }
                else
                {
                    return Json(new { error = "UserId не найден в куках" });
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем сообщение об ошибке
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddUserBasket(int userId, int productId)
        {
            try
            {
                // Создаем новую запись в таблице UserLike
                var newUserBasket = new UserBasket
                {
                    UserID = userId,
                    ProductID = productId
                };

                // Добавляем запись в контекст базы данных и сохраняем изменения
                db.User_Baskets.Add(newUserBasket);
                db.SaveChanges();

                return Json(new { success = true, message = "Запись успешно добавлена" });
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем сообщение об ошибке
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RemoveUserBasket(int productId)
        {
            try
            {
                // Получаем значение куки "UserId"
                string userIdCookie = HttpContext.Request.Cookies["UserId"];

                // Если куки не содержат значения, вы можете вернуть ошибку или принять другие меры
                if (string.IsNullOrEmpty(userIdCookie))
                {
                    return Json(new { success = false, message = "Куки с идентификатором пользователя не найдены" });
                }

                int userId = int.Parse(userIdCookie);

                // Находим запись в таблице UserLike для данного пользователя и продукта
                var userLike = db.User_Baskets.FirstOrDefault(ul => ul.UserID == userId && ul.ProductID == productId);

                // Если запись найдена, удаляем ее из базы данных
                if (userLike != null)
                {
                    db.User_Baskets.Remove(userLike);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Товар успешно удален из избранного" });
                }
                else
                {
                    return Json(new { success = false, message = "Запись о лайке не найдена" });
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем сообщение об ошибке
                return Json(new { error = ex.Message });
            }
        }

         public IActionResult GetProviders()
    {
        var providers = db._Provider.Select(s => new
        {
            id = s.IdProvider,
            name = s.provider_name
        }).ToList();

        return Json(providers); // Вернуть список поставщиков в формате JSON
    }

    // Метод для получения списка упаковок в формате JSON
    public IActionResult GetPackages()
    {
        var packages = db.Package.Select(p => new
        {
            id = p.Package_id,
            name = p.Package_name
        }).ToList();

        return Json(packages); // Вернуть список упаковок в формате JSON
    }
        [HttpPost]
        public IActionResult UpdateProductHiddenStatus(int productId, bool ishidden)
        {
            try
            {
                // Находим продукт по Id
                var product = db.Product.FirstOrDefault(p => p.IdProduct == productId);

                if (product == null)
                {
                    return NotFound(); // Если продукт не найден, возвращаем 404 Not Found
                }

                // Обновляем свойство ishidden и сохраняем изменения в базе данных
                product.Ishidden = ishidden;
                db.SaveChanges();

                return Ok(); // Возвращаем успешный ответ с кодом 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // В случае ошибки возвращаем 500 Internal Server Error
            }
        }

        [HttpPost]
        public IActionResult UpdateProductHiddenStatus2(int productId, bool ishidden)
        {
            try
            {
                // Находим продукт по Id
                var product = db.Product.FirstOrDefault(p => p.IdProduct == productId);

                if (product == null)
                {
                    return NotFound(); // Если продукт не найден, возвращаем 404 Not Found
                }

                // Обновляем свойство ishidden и сохраняем изменения в базе данных
                product.Ishidden = ishidden;
                db.SaveChanges();

                return Ok(); // Возвращаем успешный ответ с кодом 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // В случае ошибки возвращаем 500 Internal Server Error
            }
        }

        [HttpPost]
        public IActionResult UpdateProductData(int idProduct, string storageConditions, int storageLife, int energyValue, int carb, int fatty, int protein, int fat, string massPerFat, string article, string sostav, int sale, int remain, string description, int price, string productName)
        {
            try
            {
                // Находим продукт по идентификатору
                var product = db.Product.FirstOrDefault(p => p.IdProduct == idProduct);

                if (product == null)
                {
                    return NotFound(); // Если продукт не найден, возвращаем 404 Not Found
                }

                // Обновляем данные продукта
                product.product_storage_conditions = storageConditions;
                product.product_storage_life = storageLife;
                product.product_energy_value = energyValue;
                product.product_carb = carb;
                product.product_fatty = fatty;
                product.product_protein = protein;
                product.product_fat = fat;
                product.product_mass_per_fat = massPerFat;
                product.product_article = article;
                product.product_sostav = sostav;
                product.product_sale = sale;
                product.product_remain = remain;
                product.product_weight = 500;
                product.product_description_ = description;
                product.product_price = price;
                product.Name_product = productName;


                // Сохраняем изменения в базе данных
                db.SaveChanges();

                // Возвращаем успешный результат
                return Ok();
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем ошибку сервера с сообщением
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult UpdateProductProvider(int idProduct, int providerId)
        {
            var product = db.Product.Find(idProduct);

            if (product != null)
            {
                product.Provider_id = providerId;

                db.SaveChanges();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = "Продукт не найден" });
            }
        }

        [HttpPost]
        public IActionResult UpdateProductPackage(int idProduct, int packageId)
        {
            var product = db.Product.Find(idProduct);

            if (product != null)
            {
                product.product_package_id = packageId;

                db.SaveChanges();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = "Продукт не найден" });
            }
        }

        [HttpPost]
        public IActionResult DeleteComment(int commentId)
        {
            // Находим комментарий по его идентификатору
            var comment = db.Rate.FirstOrDefault(c => c.Rate_id == commentId);

            if (comment == null)
            {
                return NotFound(); // Если комментарий не найден, вернуть 404
            }

            try
            {

                // Удаляем комментарий из таблицы Comments
                db.Rate.Remove(comment);

                // Сохраняем изменения в базе данных
                db.SaveChanges();

                // Возвращаем успешный результат клиенту
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при удалении комментария, возвращаем соответствующий статус код
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(string imageData, int productId)
        {
            if (string.IsNullOrEmpty(imageData))
            {
                return BadRequest("Пустые данные изображения");
            }

            try
            {
                // Получение продукта по его идентификатору (productId) из базы данных
                var product = await db.Product.FindAsync(productId);

                if (product != null)
                {
                    // Обновление поля product_img у продукта
                    product.product_img = imageData; // Сохраняем строку Base64 в поле product_img

                    // Сохранение изменений в базе данных
                    await db.SaveChangesAsync();

                    // Возвращаем успешный результат клиенту
                    return Ok(new { message = "Изображение успешно сохранено" });
                }
                else
                {
                    return NotFound("Продукт не найден");
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки при сохранении данных изображения
                return StatusCode(500, $"Ошибка при сохранении изображения: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateSup(string SupName, string SupPhone)
        {
            if (string.IsNullOrWhiteSpace(SupName) || string.IsNullOrWhiteSpace(SupPhone))
            {
                return StatusCode(500, "Ошибка: не заполнены все поля");
            }

            var newSupplier = new _Provider
            {
                provider_name = SupName,
                provider_phone = SupPhone
            };

            db._Provider.Add(newSupplier);
            db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult CreatePack(string PackName)
        {
            if (string.IsNullOrWhiteSpace(PackName) )
            {
                return StatusCode(500, "Ошибка: не заполнены все поля");
            }

            var newSupplier = new Package
            {
                Package_name = PackName,
                
            };

            db.Package.Add(newSupplier);
            db.SaveChanges();

            return Json(new { success = true });
        }



        [HttpPost]
        public IActionResult DeleteSup(int userId)
        {
            // Находим пользователя по Id
            var user = db._Provider.FirstOrDefault(u => u.IdProvider == userId);

            if (user == null)
            {
                return NotFound(); // Если пользователь не найден, вернуть 404
            }

            try
            {
                // Удаляем пользователя
                db._Provider.Remove(user);

                // Сохраняем изменения в базе данных
                db.SaveChanges();

                // Возвращаем успешный результат клиенту
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при удалении пользователя, возвращаем соответствующий статус код
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult DeletePack(int userId)
        {
            // Находим пользователя по Id
            var user = db.Package.FirstOrDefault(u => u.Package_id == userId);

            if (user == null)
            {
                return NotFound(); // Если пользователь не найден, вернуть 404
            }

            try
            {
                // Удаляем пользователя
                db.Package.Remove(user);

                // Сохраняем изменения в базе данных
                db.SaveChanges();

                // Возвращаем успешный результат клиенту
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при удалении пользователя, возвращаем соответствующий статус код
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddProduct(string img, int packageId, int providerId, string storageConditions, int storageLife, int energyValue, int carb, int fatty, int protein, int fat, string massPerFat, string article, string sostav, int sale, int remain, string description, int price, string productName, int categoryId)
        {
            try
            {
                // Проверяем, существует ли продукт с таким же именем
                var existingProduct = db.Product.FirstOrDefault(p => p.Name_product == productName);
                if (existingProduct != null)
                {
                    // Если продукт с таким именем уже существует, возвращаем ошибку
                    return BadRequest($"Продукт с именем '{productName}' уже существует.");
                }

                // Создаем новый объект Product для добавления в базу данных
                var newProduct = new Product
                {
                    product_storage_conditions = storageConditions,
                    product_storage_life = storageLife,
                    product_energy_value = energyValue,
                    product_carb = carb,
                    product_fatty = fatty,
                    product_protein = protein,
                    product_fat = fat,
                    product_mass_per_fat = massPerFat,
                    product_article = article,
                    product_sostav = sostav,
                    product_sale = sale,
                    product_remain = remain,
                    product_weight = 500, // Пример значения
                    product_description_ = description,
                    product_price = price,
                    Name_product = productName,
                    product_package_id = packageId,
                    Provider_id = providerId,
                    product_img = img
                };

                // Добавляем новый продукт в базу данных
                db.Product.Add(newProduct);
                db.SaveChanges(); // Сохраняем изменения, чтобы получить ID нового продукта

                // Создаем новую запись в таблице catalog_Product для связи с категорией
                var catalogProduct = new catalog_product
                {
                    id_product_catalog = newProduct.IdProduct, // Предполагается, что ID продукта генерируется после SaveChanges
                    Id_catalog = categoryId
                };

                // Добавляем новую запись в таблицу catalog_Product
                db.catalog_Product.Add(catalogProduct);
                db.SaveChanges(); // Сохраняем изменения

                // Возвращаем успешный результат
                return Ok();
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем ошибку сервера с сообщением
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
