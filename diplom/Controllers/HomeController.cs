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

namespace diplom.Controllers
{
    public class HomeController : Controller
    {
        private MainContext db = new MainContext();
        private readonly ILogger<HomeController> _logger;
        //List<Product> Products = MainContext.Instantce.Products.ToList();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult HomePage()
        {
           return View("HomePage", "~/css/site.css"); 
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

        public ActionResult MainPage(string searchQuery)
        {
            IQueryable<Product> productsQuery = db.Product;
            bool hasResults = true;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                productsQuery = productsQuery.Where(p => p.Name_product.Contains(searchQuery)
                                                          || p.product_article.Contains(searchQuery));
                hasResults = productsQuery.Any();
            }
            else
            {
                hasResults = productsQuery.Any();
            }           
            var productsList = productsQuery.ToList();
            ProductViewModel viewModel = new ProductViewModel
            {
                Products = productsList,
                HasResults = hasResults,
                ProductCount = productsList.Count 
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string phoneLogin, string password)
        {
            // Хеширование пароля перед его сравнением с хешированным паролем в базе данных
            //string hashedPassword = HashPassword(password);
            string hashedPassword = password;


            var user = FindUser(phoneLogin, hashedPassword);

            if (user != null)
            {
                // Успешная авторизация                
                HttpContext.Session.SetString("UserName", user.User_name);
                System.Diagnostics.Debug.WriteLine(user.User_name);
                
                return View(user);
                return Json(new { success = true, message = "Авторизация успешна.", userName = user.User_name });
            }
            else
            {
                // Авторизация не удалась
                System.Diagnostics.Debug.WriteLine("что-то не так");
                return View(user);
                return Json(new { success = false, message = "Неверный номер телефона или пароль." });
            }
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

        // Пример метода для поиска пользователя в базе данных
        private _User FindUser(string phoneLogin, string hashedPassword)
        {
            var user = db._User.FirstOrDefault(u => u.PhoneNumber == phoneLogin && u.user_password == hashedPassword);
            return user;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
