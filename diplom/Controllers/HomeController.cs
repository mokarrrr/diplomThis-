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
hg
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

        public IActionResult HomePage()
        {
           return View("HomePage", "~/css/site.css"); 
        }
        public IActionResult AboutUsPage()
        {
            return View();
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


        public ActionResult GetProductRating(int productId) /* Никита это расчет среднего рейтинга */
        {
            // Получите рейтинг для выбранного продукта и верните его в виде строки
            var ratesForProduct = db.Rate.Where(r => r.Productid == productId).ToList();
            double averageRate = ratesForProduct.Any() ? ratesForProduct.Average(r => r._Rate) : 0;

            // Округлить до одного знака после запятой
            averageRate = Math.Round(averageRate, 1);

            return Content(averageRate.ToString());
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(string phoneLogin, string password)
        //{
        //    // Хеширование пароля перед его сравнением с хешированным паролем в базе данных
        //    //string hashedPassword = HashPassword(password);
        //    string hashedPassword = password;


        //    var user = FindUser(phoneLogin, hashedPassword);

        //    if (user != null)
        //    {
        //        // Успешная авторизация                
        //        HttpContext.Session.SetString("UserName", user.User_name);
        //        System.Diagnostics.Debug.WriteLine(user.User_name);

        //        return View(user);
        //        return Json(new { success = true, message = "Авторизация успешна.", userName = user.User_name });
        //    }
        //    else
        //    {
        //        // Авторизация не удалась
        //        System.Diagnostics.Debug.WriteLine("что-то не так");
        //        return View(user);
        //        return Json(new { success = false, message = "Неверный номер телефона или пароль." });
        //    }
        //}

        //// Метод для хеширования пароля
        //private string HashPassword(string password)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        //    }
        //}

        //// Пример метода для поиска пользователя в базе данных
        //private _User FindUser(string phoneLogin, string hashedPassword)
        //{
        //    var user = db._User.FirstOrDefault(u => u.PhoneNumber == phoneLogin && u.user_password == hashedPassword);
        //    return user;
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
