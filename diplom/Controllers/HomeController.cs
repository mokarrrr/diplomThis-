﻿using diplom.Models;
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
                ProductAverageRates = productAverageRates
            };

            return View(viewModel);
        }

        public PartialViewResult FilteredProductsPartial(int? categoryId, int? secondCategoryId, int? thirdCategoryId, int? fourthCategoryId, int? fifthCategoryId)
        {
            IQueryable<Product> productsQuery = db.Product.Include(p => p.Rates);

            FilterProductsByCategory(ref productsQuery, categoryId);
            FilterProductsByCategory(ref productsQuery, secondCategoryId);
            FilterProductsByCategory(ref productsQuery, thirdCategoryId);
            FilterProductsByCategory(ref productsQuery, fourthCategoryId);
            FilterProductsByCategory(ref productsQuery, fifthCategoryId);

            var model = new ProductViewModel
            {
                HasResults = productsQuery.Any(),
                Products = productsQuery.ToList()
            };

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
        public JsonResult SaveUser(string email, string name, string lastName, string phone)
        {
            try
            {
                // Поиск пользователя по email (вы можете использовать другой уникальный идентификатор)
                var existingUser = db.User.FirstOrDefault(u => u.Email == email);

                if (existingUser != null)
                {
                    // Обновляем данные пользователя
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
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Ошибка: {ex.Message}" });
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
        public ActionResult Register(string Email, string Name, string lastName, string passwordregister, string passwordregistersecond, string phoneRegister)
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
                //ModelState.AddModelError("", "Пароли не совпадают");
                return Json(new { success = false, message = "Пароли не совпадают" });
            }
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(Email);
            }
            catch (FormatException)
            {
                // Ошибка в формате почты (отсутствует знак '@')
                return Json(new { success = false, message = "mailFormatError" });
            }
            bool userExistsByEmail = UserExistsByEmail(Email);
            bool userExistsByPhoneNumber = UserExistsByPhoneNumber(phoneRegister);
            if (userExistsByPhoneNumber || userExistsByEmail)
            {
                if (UserExistsByPhoneNumber(phoneRegister) && userExistsByEmail)
                {
                    // Пользователь с таким номером телефона и с такой почтой уже существует
                    return Json(new { success = false, message = "bothExists" });
                }
                if (userExistsByEmail)
                {
                    //ModelState.AddModelError("", "Пользователь с такой почтой уже зарегистрирован");
                    return Json(new { success = false, message = "emailExists" });
                }

                if (UserExistsByPhoneNumber(phoneRegister))
                {
                    //ModelState.AddModelError("", "Пользователь с таким номером телефона уже зарегистрирован");
                    return Json(new { success = false, message = "phoneExists" });
                }

                // Проверка существования пользователя с такой почтой
                
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
            db.SaveChanges();


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
