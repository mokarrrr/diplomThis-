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

        public IActionResult UserOrders()
        {
            long userIdCookie = Int64.Parse(Request.Cookies["UserId"]);
            var _Orders = db._orders.Where(o => o._user_id == userIdCookie).Include(o => o.Products).Include(o=>o.Status).ToList();
            return View(_Orders);
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

        public ActionResult MainPage(string searchQuery, int? selectedProductId, string packageNames, string providerNames, int? categoryId, int? secondCategoryId, int? thirdCategoryId, int? fourthCategoryId, int? fifthCategoryId)
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
        public IActionResult GetProduct(int Id)
        {
            
            return StatusCode(200,db.Product.Where(p=>p.IdProduct == Id).Include(p => p.Rates).ThenInclude(r=>r.User).FirstOrDefault());
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
        public ActionResult GetProviderName(int providerId)
        {
            var providerName = db._Provider
                .Where(p => p.IdProvider == providerId)
                .Select(p => p.provider_name)
                .FirstOrDefault();

            return Content(providerName);
        }
        public ActionResult Login(string phoneLogin, string password)
        {
            // Поиск пользователя по номеру телефона
            var user = FindUserByPhone(phoneLogin);

            if (user != null && VerifyPassword(password, user.user_password))
            {
                // Если учетные данные верны, устанавливаем куку
                HttpContext.Response.Cookies.Append("UserId", user.IdUser.ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(400), // Установите желаемое время жизни куки
                    HttpOnly = true,
                    Secure = false, // Устанавливаем Secure в false, чтобы кука отправлялась в любом случае
                    Path = "/" // Устанавливаем путь, чтобы куки были доступны на всех страницах сайта
                });

                return Json(new { success = true, message = "Авторизация успешна.", userName = user.User_name });
            }
            else
            {
                return Json(new { success = false, message = "Неверный номер телефона или пароль." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(string city, string address, string comment, int totalPrice)
        {
            try
            {
                // Получаем идентификатор пользователя из куки
                string userIdCookie = Request.Cookies["UserId"];

                if (string.IsNullOrEmpty(userIdCookie))
                {
                    return StatusCode(401, new { error = "User ID not found in cookies" });
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
        public IActionResult AddItemsToOrderDetails(int orderId)
        {
            try
            {
                // Получаем UserId из куки
                string userIdCookie = Request.Cookies["UserId"];

                // Проверяем, существует ли запись в таблице UserLike
                if (int.TryParse(userIdCookie, out int userId))
                {
                    // Получаем товары в корзине пользователя
                    var userBasketItems = db.User_Baskets
                        .Where(item => item.UserID == userId)
                        .ToList();

                    // Добавляем каждый товар из корзины пользователя в таблицу order_detail
                    foreach (var item in userBasketItems)
                    {
                        db.order_detail.Add(new order_detail
                        {
                            id_product = item.ProductID,
                            id_order = orderId, // Используем переданный идентификатор заказа
                            product_take = 1 // Предположим, что каждый товар взят по одному
                        });
                    }

                    db.SaveChanges();

                    // Удаление товаров из корзины пользователя после добавления в заказ
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
            return db.User.FirstOrDefault(u => u.PhoneNumber == phoneLogin);
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
            if (passwordregister != passwordregistersecond)
            {
                return Json(new { success = false, message = "Пароли не совпадают" });
            }

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
                user_password = HashPassword(passwordregister), // Хеширование пароля

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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
