﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//для карточки продукта
$(function (maam) {
    $('.product-card').hover(function () {
        $(this).find('.description').animate({
            height: "toggle",
            opacity: "toggle"
        }, 300);
    });
});

/*переключение на регистрацию*/
//document.getElementById('switchToRegister').addEventListener('click', function () {
//    document.getElementById('loginContent').style.display = 'none'; // Скрыть контент страницы входа
//    document.getElementById('registerContent').style.display = 'block'; // Показать контент страницы регистрации
//});
/*кнопка вверх*/
        

/*        .......прокрутка хедер*/
        $(document).ready(function () {
            $(window).scroll(function () {
                if ($(this).scrollTop() > 50) {
                    $('#backToTop').fadeIn();
                } else {
                    $('#backToTop').fadeOut();
                }
            });

            $('#backToTop').click(function () {
                $('body, html').animate({
                    scrollTop: 0
                }, 400);
                return false;
            });
        });

    
        //модальное окно
        //var modal = document.getElementById("modal");
        //var loginContent = document.getElementById("loginContent");
        //var registerContent = document.getElementById("registerContent");
        //var switchToRegister = document.getElementById("switchToRegister");
        //var closeButtons = document.getElementsByClassName("close");

        //function showModal() {
        //    if (!localStorage.getItem('hasVisited')) {
        //        modal.style.display = "block";
        //        localStorage.setItem('hasVisited', 'true');
        //    }
        //}

        //switchToRegister.onclick = function () {
        //    loginContent.style.display = "none";
        //    registerContent.style.display = "block";
        //    modalContent.style.transform = "translateY(-100px)"; 
        //}
        //for (var i = 0; i < closeButtons.length; i++) {
        //    closeButtons[i].onclick = function () {
        //        modal.style.display = "none";
        //    }
        //}
        //window.onclick = function (event) {
        //    if (event.target == modal) {
        //        modal.style.display = "none";
        //    }
        //}
        //window.onload = function () {
        //    showModal();
        //}


        //ограничение вводимых символов
        document.getElementById('phoneLogin', 'phoneRegister').addEventListener('input', function (e) {
            this.value = this.value.replace(/[^\d]/g, '');
        });
        document.getElementById('phoneRegister').addEventListener('input', function (e) {
            this.value = this.value.replace(/[^\d]/g, '');
        });


//$(document).ready(function () {
//    $('#loginButton').click(function (e) {
//        e.preventDefault();
//        var phoneLogin = $('#phoneLogin').val();
//        var password = $('#password').val(); // Убедитесь, что у вашего поля ввода пароля есть id="password"

//        $.post('/', { phoneLogin: phoneLogin, password: password }, function (data) {
//            if (data.success) {
//                $('#userNameDisplay').text(data.userName); // Предполагается, что у вас есть элемент <p id="userNameDisplay"></p>
//            } else {
//                alert(data.success);
//            }
//        });
//    });

//    // Обработка клика вне формы
//    $(window).click(function (event) {
//        if ($(event.target).is('.close, #loginContent')) {
//            // Закрыть модальное окно
//        }
//    });
//});


var modal = document.getElementById("modal");
var loginContent = document.getElementById("loginContent");
var registerContent = document.getElementById("registerContent");
var switchToRegister = document.getElementById("switchToRegister");
var closeButtons = document.getElementsByClassName("close");

//function showModal() {
//    if (!Cookies.get('hasVisited') || Cookies.get('hasVisited') !== 'true') {
//        modal.style.display = "block";
//    }
//}

//function setVisitedCookie() {
//    Cookies.set('hasVisited', 'true', { expires: 365 }); // Куки на год
//}

switchToRegister.onclick = function (event) {
    event.preventDefault(); // Предотвращаем стандартное действие ссылки
    loginContent.style.display = "none";
    registerContent.style.display = "block";
    modal.scrollTop = 0;
}
var returnToLoginButton = document.getElementById("returnToLoginButton");
returnToLoginButton.onclick = function (event) {
    event.preventDefault(); // Предотвращаем стандартное действие кнопки
    document.getElementById("loginContent").style.display = "block";
    document.getElementById("registerContent").style.display = "none";
    document.getElementById("modal").scrollTop = 0;
}

for (var i = 0; i < closeButtons.length; i++) {
    closeButtons[i].onclick = function () {
        modal.style.display = "none";
        /*setVisitedCookie();*/
    }
}

//window.onload = function () {
//    showModal();
//}

// Добавлено событие для закрытия модального окна при клике вне его
window.addEventListener('click', function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
        setVisitedCookie();
    }
});

//$(function () {
//    $(".info-button1").click(function () {
//        var imgtovara = $(this).attr('data-imgtovara');
//        var nametitle = $(this).attr('data-nametitle');
//        var pricetovar = $(this).attr('data-pricetovar');

//        $(".tovarimg").html('<img class="img-fluid" src="' + imgtovara + '" alt="..." />');
//        $(".tovarinfo").html('<p class="h3">' + nametitle + '</p>');
//        $(".tovarinfo").append('<p><strong>Цена</strong>: ' + pricetovar + '</p>');
//        $("#hide1").val(nametitle);
//        $("#hide2").val(pricetovar);
//    });
//});


    
$(document).ready(function () {
    $('.info-button1').on('click', function () {
        //var productName = $(this).data('nametitle');
        //var description = $(this).data('description');
        //var productImageBase64 = $(this).data('img');
        //var productPrice = $(this).data('price');
        //var productWeight = $(this).data('weight');
        //var productMassPerFat = $(this).data('mass_per_fat');
        //var productFat = $(this).data('fat');
        //var productProtein = $(this).data('protein');
        //var productFatty = $(this).data('fatty');
        //var productCarb = $(this).data('carb');
        //var productEnergyValue = $(this).data('energy_value');
        //var productStorageLife = $(this).data('storage_life');
        //var productPackageId = $(this).data('package_id');
        //var productStorageConditions = $(this).data('storage_conditions');
        //var productSale = $(this).data('sale');
        //var productRemain = $(this).data('remain');
        //var productArticle = $(this).data('article');
        //var productSostav = $(this).data('sostav');
        var productId = $(this).data('idproduct');


        $.ajax({
            type: "GET",
            url: "/Home/GetProduct", // Замените на ваш реальный URL
            data: { Id: productId },
            success: function (Product) {
                if (Product["product_sale"]) {
                    // Рассчитываем цену с учетом скидки и округляем до целого числа
                    var discountedPrice = Math.round((1 - Product["product_sale"] / 100) * Product["product_price"]);

                    // Обновляем значения в таблице
                    $('#productPrice').html('<del>' + Product["product_price"] + ' руб.</del> ' + discountedPrice + ' руб. за');

                    // Обновляем значение скидки в div и отображаем его, если скидка больше 0
                    $('.sale1').text('-' + Product["product_sale"] + '%');
                    $('.sale1').toggle(Product["product_sale"] > 0);
                } else {
                    // Если скидки нет, просто обновляем цену и скрываем div
                    $('#productPrice').text(Product["product_price"] + ' руб. за');
                    $('.sale1').hide();
                }

                $('#productNameLabel').text(Product["name_product"]);
                /*$('#productPrice').text(Product["product_price"] + " рублей за");*/
                $('#description').text(Product["product_description_"]);
                $('#productImage').attr('src', '' + Product["product_img"]);
                $('#productWeight').text(Product["product_weight"] + ' гр.');
                $('#productMassPerFat').text('Массовая доля жира:' + Product["product_mass_per_fat"] + '%');
                $('#productfat').text('Жирность:' + Product["product_fat"] + '%');
                $('#productProtein').text('Белки:' + Product["product_protein"] + 'г.');
                $('#productFatty').text('Жиры:' + Product["product_fatty"] + 'г.');
                $('#productCarb').text('Углеводы:' + Product["product_carb"] + 'г.');
                $('#productEnergyValue').text('Энергетическая ценность:' + Product["product_energy_value"] + 'ккал');
                $('#productStorageLife').text('Срок хранения:' + Product["product_storage_life"] + ' дней с момента фасовки');
                $('#productPackageId').text(Product["product_package_id"]);
                $('#productStorageConditions').text('Условия хранения:' + Product["product_storage_conditions"]);
                $('#productSale').text(Product["product_sale"]);
                $('#productRemain').text(Product["product_remain"]);
                $('#productArticle').text('Артикул:' + Product["product_article"]);
                $('#productSostav').text('Состав:' + Product["product_sostav"]);
                $('#prodid').text(Product["idProduct"]);
                Product["rates"].forEach(function (comment) {
                    var initials = comment["user"]["user_name"].charAt(0) + comment["user"]["surname"].charAt(0);
                    var starsHTML = '';
                    // Определяем количество звезд в зависимости от оценки
                    var rating = parseInt(comment["_Rate"]);
                    for (var i = 0; i < rating; i++) {
                        starsHTML += '★'; // Звезда
                    }
                    for (var j = rating; j < 5; j++) {  
                        starsHTML += '<span style=" font-size: 30px;">☆</span>'; 
                    }

                    var commentHTML = '<div style="width:97%;border: 3px solid black;margin-bottom:10px; border-radius: 10px;max-height:105px;margin-left:30px">';
                    commentHTML += '<div style="display: flex; align-items: flex-start; padding: 20px;">'; // Изменено здесь
                    commentHTML += '<div class="user-initials" style="width: 50px; height: 50px; background-color: gray; border-radius: 50%; text-align: center; line-height: 31px; color: white; font-weight: bold; margin-right: -9px;padding:10px;line-width:20px">' + initials + '</div>';
                    commentHTML += '<div style="flex-grow: 1;">';
                    commentHTML += '<p style="margin-top: -0.2%; margin-bottom: 0;text-align:left;font-size:20px;margin-left:15px">' + comment["user"]["user_name"] + " " + comment["user"]["surname"] + '</p>';

                    commentHTML += '<div style="display: flex; flex-direction: row;">'; // Обертка для комментария и звезд
                    if (comment["rate_comment"] !== null) {
                        commentHTML += '<p style="margin-top: 5px; margin-bottom: 0; font-size: 20px;width:795px;text-align:left;margin-left:15px">' + comment["rate_comment"] + '</p>';
                    }
                    commentHTML += '<p style="margin-top: -16px; margin-bottom: 0; font-size: 30px; margin-left: auto;color:#FFD700">' + starsHTML + '</p>';
                    commentHTML += '</div>';

                    commentHTML += '</div>';
                    commentHTML += '</div>';                    
                    commentHTML += '</div>';
                    commentHTML += '</div>';

                    $('#rewey').append(commentHTML);
                });

                $('#exampleModal').on('hidden.bs.modal', function (e) {
                    $('#rewey').empty(); // Очистка содержимого элемента #rewey
                });
                console.log(Product);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });


    });
});


$(document).ready(function () {
    // Получаем значение айди продукта из элемента <p id="prodid"></p>
    var productId = $('#prodid').text();

    // Передаем значение айди продукта в представление через модель
    // Я предполагаю, что у вас есть модель с именем Model, которая имеет свойство SelectedProductId
    Model.SelectedProductId = productId;
});
        function getPackageNameById(packageId) {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    type: "GET",
                    url: "/Home/GetPackageName", // Замените на ваш реальный URL
                    data: { packageId: packageId },
                    success: function (packageName) {
                        resolve(packageName);
                    },
                    error: function (xhr, status, error) {
                        reject(error);
                    }
                });
            });
        }

        $(document).ready(function () {
            $(".info-button1").on("click", function () {
                var packageId = $(this).data("package_id");
                var $productSup = $("#productSupplier");

                // Вызов функции с использованием Promise
                getPackageNameById(packageId)
                    .then(function (packageName) {
                        // Обновить текст в элементе <p>
                        $productSup.text("Упаковка: " + packageName);
                    })
                    .catch(function (error) {
                        console.error("Ошибка при получении имени пакета:", error);
                    });
            });
        });

        function getProviderNameById(providerId) {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    type: "GET",
                    url: "/Home/GetProviderName", // Замените на ваш реальный URL
                    data: { providerId: providerId },
                    success: function (providerName) {
                        resolve(providerName);
                    },
                    error: function (xhr, status, error) {
                        reject(error);
                    }
                });
            });
        }

        $(document).ready(function () {
            $(".info-button1").on("click", function () {
                var providerId = $(this).data("provider_id");
                var $productPa = $("#productpa");

                // Вызов функции с использованием Promise
                getProviderNameById(providerId)
                    .then(function (providerName) {
                        // Обновить текст в элементе <p>
                        $productPa.text("Поставщик: " + providerName);
                    })
                    .catch(function (error) {
                        console.error("Ошибка при получении имени поставщика:", error);
                    });
            });
        });


    function scrollToElement(elementId) {
            var element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
            }
        }


$(document).ready(function () {
    $(".info-button1").on("click", function () {
        var productId = $(this).data('idproduct');
        var $productRate = $("#productrate");
        var $productRate2 = $("#productrate2");
        var $ratingStars = $("#rating-stars");

        getProductRatingById(productId)
            .then(function (averageRate) {
                updateProductRating($productRate, $ratingStars, averageRate);

                // Добавьте класс для изменения цвета текста, включая звезду
                $productRate2.html('<span class="star-rating">' + averageRate + '<span class="star">★</span></span>');

                // Переопределите цвет только для звезды и добавьте стили
                $productRate2.find('.star')
                    .css('color', '#FFD700')
                    
            })
            .catch(function (error) {
                console.error("Ошибка при получении рейтинга продукта:", error);
            });
    });

    // Функция для выполнения асинхронного запроса к серверу и получения рейтинга продукта
    function getProductRatingById(productId) {
                return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Home/GetProductRating',
            data: { productId: productId },
            type: 'GET',
            success: function (result) {
                resolve(result);
            },
            error: function (error) {
                reject(error);
            }
        });
                });
            }

    // Функция для обновления текста и звезд
    function updateProductRating($productRate, $ratingStars, averageRate) {
        $productRate.text("Рейтинг продукта: " + averageRate + '★');

    }
        });



/*    @* табы * @*/


 


    /*@* добавление в корзину * @*/
  
$(document).ready(function () {
    // При нажатии на элемент с классом "send"
    $('.send').on('click', function () {
        // Проверяем значение AuthenticationSuccess в Local Storage
        var authenticationSuccess = localStorage.getItem("AuthenticationSuccess");

        // Если пользователь не авторизован (AuthenticationSuccess=0), открываем модальное окно
        if (authenticationSuccess === null || authenticationSuccess === "0") {
            $('#modal').show();
            return;
        }

        // Получите цену товара
        //var priceElement = $(this).closest('.card-content').find('.price');
        //var saleElement = $(this).closest('.card-content').find('.sale');

        //// Получите цену без скидки
        //var originalPrice = parseInt(priceElement.text().replace(/[^\d.]/g, ''));

        //// Получите сумму с учетом скидки
        //var priceWithSale = saleElement.length > 0 ? originalPrice - Math.floor(originalPrice * parseFloat(saleElement.text().replace(/[^\d.]/g, '')) / 100) : originalPrice;

        //// Получите текущую сумму в корзине
        //var currentTotal = parseInt($('#cartButton').attr('data-total')) || 0;

        //// Прибавьте цену товара с учетом скидки к текущей сумме
        //var newTotal = currentTotal + priceWithSale;

        //// Обновите атрибут data-total у кнопки корзины
        //$('#cartButton').attr('data-total', newTotal);

        //// Обновите текст суммы в корзине с символом корзины
        //$('#cartButton').html('<i class="fas fa-shopping-basket"></i> ' + newTotal + ' руб.');
    });
});
 

   /* @* фильтры * @*/




document.addEventListener('DOMContentLoaded', function () {
    // Получите изначальные карточки продуктов
    var productCards = document.querySelectorAll('.product-card');

    // Сохраните изначальный порядок карточек продуктов
    var initialOrder = Array.from(productCards);

    // Получите контейнер для карточек продуктов
    var container = document.querySelector('.container3');

    // Функция для очистки и добавления карточек в контейнер
    function renderProductCards(cards) {
        // Очистите контейнер
        container.innerHTML = '';

        // Добавьте карточки в контейнер в нужном порядке
        cards.forEach(function (card) {
            container.appendChild(card);
        });
    }

    // Обработчик события изменения фильтра
    document.getElementById('filter').addEventListener('change', function () {
        // Получите выбранное значение фильтра
        var selectedFilter = this.value;

        // Создайте копию изначального порядка карточек продуктов
        var productArray = Array.from(initialOrder);

        // Отсортируйте карточки продуктов в зависимости от выбранного фильтра
        switch (selectedFilter) {
            case 'by_popularity':
                // При выборе 'by_popularity' просто отрисуйте изначальный порядок
                renderProductCards(productArray);
                break;
            case 'by_alphabet':
                // Сортируйте карточки продуктов по алфавиту
                productArray.sort(function (a, b) {
                    var titleA = a.querySelector('.card-title').innerText.toUpperCase();
                    var titleB = b.querySelector('.card-title').innerText.toUpperCase();
                    return titleA.localeCompare(titleB);
                });
                renderProductCards(productArray);
                break;
            case 'cheaper':
                // Сортируйте карточки продуктов по цене в порядке возрастания
                productArray.sort(function (a, b) {
                    var priceA = getPrice(a);
                    var priceB = getPrice(b);
                    return priceA - priceB;
                });
                renderProductCards(productArray);
                break;
            case 'expensive':
                // Сортируйте карточки продуктов по цене в порядке убывания
                productArray.sort(function (a, b) {
                    var priceA = getPrice(a);
                    var priceB = getPrice(b);
                    return priceB - priceA;
                });
                renderProductCards(productArray);
                break;
            default:
                break;
        }
    

        // Обновите контейнер отсортированными карточками продуктов
        var container = document.querySelector('.container3');
        container.innerHTML = '';
        productArray.forEach(function (card) {
            container.appendChild(card);
        });
    });
    });


function getPrice(card) {
    var saleElement = card.querySelector('.sale');
    var priceElement = card.querySelector('.price');

    if (saleElement && parseInt(saleElement.innerText.replace('-', '')) > 0) {
        // Если есть скидка, учитываем цену со скидкой
        return parseFloat(priceElement.innerText.split(' ')[2]);
    } else {
        // В противном случае учитываем обычную цену
        return parseFloat(priceElement.innerText.split(' ')[1]);
    }
}
   


    
        //$(document).ready(function () {
        //    $('#softCheeseCheckbox').change(function () {
        //        // Получаем значение чекбокса
        //        var isChecked = $(this).prop('checked');

        //        // Устанавливаем categoryId в 1, если чекбокс отмечен, иначе в null
        //        var categoryId = isChecked ? 1 : null;

        //        // Отправляем запрос на сервер с новым categoryId
        //        $.ajax({
        //            url: '/Home/MainPage',
        //            type: 'GET',
        //            data: {
        //                categoryId: categoryId,
        //                // Другие параметры запроса, если нужны
        //            },
        //            success: function (data) {
        //                // Обновляем содержимое страницы, например, заменяем список продуктов
        //                $('#container3').html(data);
        //            },
        //            error: function (error) {
        //                console.log(error);
        //            }
        //        });
        //    });
        //});

    function scrollToElement(elementId) {
            var element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
            }
        }



    $(document).ready(function () {
        // Сохраняем начальное состояние чекбоксов
        var isChecked = $('#softCheeseCheckbox').prop('checked');
        var isSecondChecked = $('#secondCheckbox').prop('checked');
        var isThirdChecked = $('#thirdCheckbox').prop('checked');
        var isFourthChecked = $('#fourthCheckbox').prop('checked');
        var isFifthChecked = $('#fifthCheckbox').prop('checked');


        // Обработчик для первого чекбокса
        $('#softCheeseCheckbox').change(function () {
            updateProducts();
        });

        // Обработчик для второго чекбокса
        $('#secondCheckbox').change(function () {
            updateProducts();
        });

        // Обработчик для третьего чекбокса
        $('#thirdCheckbox').change(function () {
            updateProducts();
        });

        // Обработчик для четвертого чекбокса
        $('#fourthCheckbox').change(function () {
            updateProducts();
        });

        // Обработчик для пятого чекбокса
        $('#fifthCheckbox').change(function () {
            updateProducts();
        });

        function updateProducts() {
            // Получаем значения всех чекбоксов
            var isChecked = $('#softCheeseCheckbox').prop('checked');
            var isSecondChecked = $('#secondCheckbox').prop('checked');
            var isThirdChecked = $('#thirdCheckbox').prop('checked');
            var isFourthChecked = $('#fourthCheckbox').prop('checked');
            var isFifthChecked = $('#fifthCheckbox').prop('checked');

            // Отправляем запрос на сервер с обновленными значениями категорий
            $.ajax({
                url: '/Home/FilteredProductsPartial',
                type: 'GET',
                data: {
                    categoryId: isChecked ? 1 : null,
                    secondCategoryId: isSecondChecked ? 2 : null,
                    thirdCategoryId: isThirdChecked ? 3 : null,
                    fourthCategoryId: isFourthChecked ? 4 : null,
                    fifthCategoryId: isFifthChecked ? 5 : null,
                    // Другие параметры запроса, если нужны
                },
                timeout: 30000,
                success: function (data) {
                    // Скрываем модальное окно modaluserr перед обновлением страницы
                    $('#modaluserr').css('display', 'none');

                    // Обновляем содержимое всей страницы
                    $('.container3').html(data);

                    // Восстанавливаем состояние чекбоксов

                    AddCardListeners();
                    
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

    });

        // Добавляем обработчик события клика к кнопке
        $('.info-button1').on('click', function () {
            // Получаем ID продукта из атрибута data-idd
            var productId = $(this).data('idproduct');

            // Выполняем AJAX-запрос для получения количества оценок
            $.ajax({
                url: '/Home/CountRatings', // Замените ControllerName на ваш фактический контроллер
                type: 'GET',
                data: { productId: productId },
                success: function (data) {
                    // Обновляем содержимое элементов <p> с id "five", "four", "three", "two", "one"
                    var count5 = data[5] || 0;
                    $('#five').text(count5);

                    var count4 = data[4] || 0;
                    $('#four').text(count4);

                    var count3 = data[3] || 0;
                    $('#three').text(count3);

                    var count2 = data[2] || 0;
                    $('#two').text(count2);

                    var count1 = data[1] || 0;
                    $('#one').text(count1);
                },
                error: function (error) {
                    console.error('Ошибка при получении количества оценок:', error);
                }
            });
        });

        // Добавляем обработчик события клика к кнопке
        $('.info-button1').on('click', function () {
            // Получаем ID продукта из атрибута data-idd
            var productId = $(this).data('idproduct');

            // Выполняем AJAX-запрос для получения количества оценок
            $.ajax({
                url: '/Home/CountRatings', // Замените ControllerName на ваш фактический контроллер
                type: 'GET',
                data: { productId: productId },
                success: function (data) {
                    // Обновляем содержимое элементов <p> с id "five", "four", "three", "two", "one"
                    var count5 = data[5] || 0;
                    var count4 = data[4] || 0;
                    var count3 = data[3] || 0;
                    var count2 = data[2] || 0;
                    var count1 = data[1] || 0;

                    $('#five').text(count5);
                    $('#four').text(count4);
                    $('#three').text(count3);
                    $('#two').text(count2);
                    $('#one').text(count1);

                    // Подсчитываем общее количество оценок
                    var totalRatings = count5 + count4 + count3 + count2 + count1;

                    // Рассчитываем процентные доли для оценок 5, 4, 3, 2 и 1
                    var percentage5 = totalRatings > 0 ? (count5 / totalRatings) * 100 : 0;
                    var percentage4 = totalRatings > 0 ? (count4 / totalRatings) * 100 : 0;
                    var percentage3 = totalRatings > 0 ? (count3 / totalRatings) * 100 : 0;
                    var percentage2 = totalRatings > 0 ? (count2 / totalRatings) * 100 : 0;
                    var percentage1 = totalRatings > 0 ? (count1 / totalRatings) * 100 : 0;

                    // Обновляем ширину линий заполнения
                    $('#progress-bar').css('width', percentage5 + '%');
                    $('#progress-bar1').css('width', percentage4 + '%');
                    $('#progress-bar2').css('width', percentage3 + '%');
                    $('#progress-bar3').css('width', percentage2 + '%');
                    $('#progress-bar4').css('width', percentage1 + '%');
                },
                error: function (error) {
                    console.error('Ошибка при получении количества оценок:', error);
                }
            });
        });

        $(document).ready(function () {
            // Устанавливаем значение в Local Storage при загрузке страницы
            var authenticationSuccess = localStorage.getItem("AuthenticationSuccess");

            if (authenticationSuccess === null) {
                // Если значение не установлено, устанавливаем в "0"
                localStorage.setItem("AuthenticationSuccess", "0");
            } else if (authenticationSuccess === "1") {
                // Если пользователь успешно авторизован, скрываем модальное окно и выходим
                $('#modal').hide();
                enableSendButton();
                return;
            }

            // Проверяем, есть ли кука "FirstVisit"
            var firstVisitCookie = getCookie("FirstVisit");

            if (!firstVisitCookie) {
                // Если кука не установлена, отображаем модальное окно
                $('#modal').show();

                // Устанавливаем куку "FirstVisit"
                document.cookie = "FirstVisit=true; expires=" + new Date(Date.now() + 365 * 24 * 60 * 60 * 1000).toUTCString() + "; path=/";
            }

            // Закрытие модального окна при клике на крестик
            $('#modal .close').click(function () {
                $('#modal').hide();

                // Устанавливаем значение "AuthenticationSuccess" в "0" при закрытии окна
                localStorage.setItem("AuthenticationSuccess", "0");
            });

            // Закрытие модального окна при клике вне его области
            $(window).click(function (event) {
                if (event.target == $('#modal')[0]) {
                    $('#modal').hide();

                    // Устанавливаем значение "AuthenticationSuccess" в "0" при закрытии окна
                    localStorage.setItem("AuthenticationSuccess", "0");
                }
            });

            // ... (остальной код скрипта)


            




            $('#loginButton').click(function () {
                // Получаем значение поля phoneLogin и удаляем лишние пробелы, символы () и -
                var phoneLogin = $('#phoneLogin').val().replace(/\s+/g, '').replace(/[()+-]/g, '').replace(/^7/, '');

                var password = $('#password').val();

                var formData = {
                    phoneLogin: phoneLogin,
                    password: password
                };

                $.ajax({
                    type: 'POST',
                    url: '/Home/Login',
                    data: formData,
                    dataType: 'json',
                    success: function (data) {
                        console.log(data); // Вывод данных, полученных с сервера
                        if (data.success) {
                            // Успешная авторизация

                            // Закрытие модального окна
                            $('#modal').modal('hide');

                            // Скрытие сообщения об ошибке (если видимо)
                            $('#errorMessage').hide();

                            // Установка значения в Local Storage
                            localStorage.setItem("AuthenticationSuccess", "1");

                            // Проверка роли пользователя
                            var role = data.role.trim(); // Удаляем пробелы в значении роли
                            if (role === "admin") {
                                console.log("User is an admin. Redirecting to AdminPage...");
                                // Перенаправление на страницу администратора
                                window.location.href = '/Home/AdminPage';
                            } else {
                                console.log("User is not an admin. Reloading current page...");
                                // Перезагрузка текущей страницы
                                location.reload();
                            }

                        } else {
                            // Показ сообщения об ошибке
                            $('#errorMessage').show();
                        }
                    },
                    error: function () {
                        alert('Произошла ошибка при выполнении запроса.');
                    }
                });
            });

            function getCookie(name) {
                var value = "; " + document.cookie;
                var parts = value.split("; " + name + "=");
                if (parts.length == 2) return parts.pop().split(";").shift();
                return null;
            }
        });

$(document).ready(function () {
    $('#registerButton').click(function () {
        console.log('Кнопка регистрации нажата');

        $('#Email').css('border-color', '');
        $('#passwordregister').css('border-color', '');
        $('#passwordregistersecond').css('border-color', '');
        $('#phoneRegister').css('border-color', '');
        $('#lastName').css('border-color', '');
        $('#Name').css('border-color', '');

        var email = $('#Email').val();
        var name = $('#Name').val();
        var lastName = $('#lastName').val();
        var password = $('#passwordregister').val();
        var passwordConfirmation = $('#passwordregistersecond').val();
        var phoneLogin = $('#phoneRegister').val().replace(/_/g, '').replace(/\s+/g, '').replace(/[()+-]/g, '').replace(/^7/, '');
        var checkbox = $('#givetrue');

        // Проверяем, активен ли чекбокс
        var consentChecked = checkbox.is(':checked');

        // Проверка длины номера телефона
        //if (phoneLogin.length !== 10) {
        //    $('#phoneErrorMessage').text('Номер телефона должен содержать ровно 10 цифр').show();
        //    return; // Прекращаем выполнение функции, если длина номера телефона неверна
        //} else {
        //    $('#phoneErrorMessage').hide(); // Если длина номера телефона правильная, скрываем сообщение об ошибке
        //}

        // Удаление всех символов форматирования из номера телефона
        phoneLogin = phoneLogin.replace(/\s+/g, '').replace(/[()+-]/g, '').replace(/^7/, '');

        // Отправка данных на сервер
        var formData = {
            Email: email,
            Name: name,
            lastName: lastName,
            passwordregister: password,
            passwordregistersecond: passwordConfirmation,
            phoneRegister: phoneLogin,
            consentChecked: consentChecked  // Добавляем состояние чекбокса в данные формы
        };

        if (!consentChecked) {
            $('#consentLink').css('color', 'red');
        } else {
            $('#consentLink').css('color', ''); // Возвращаем цвет ссылки к исходному состоянию
        }

        $.ajax({
            type: 'POST',
            url: '/Home/Register',
            data: formData,
            dataType: 'json',

            success: function (data) {
                if (data.success) {
                    // Регистрация успешна, теперь пытаемся войти
                    var loginData = {
                        phoneLogin: phoneLogin,
                        password: password
                    };
                    console.log('Номер телефона для входа:', phoneLogin);
                    $.ajax({
                        type: 'POST',
                        url: '/Home/Login',
                        data: loginData,
                        dataType: 'json',
                        success: function (loginResult) {
                            if (loginResult.success) {
                                localStorage.setItem("AuthenticationSuccess", "1");
                                $('#modal').hide();
                                location.reload();
                            } else {
                                // Обработка ошибок авторизации
                            }
                        },

                        error: function () {
                            alert('Произошла ошибка при выполнении запроса авторизации.');
                            console.log('Номер телефона для входа:', phoneLogin);
                        }
                    });
                } else {
                    // Регистрация не успешна
                    if (data.message === "Не все поля заполнены") {                        
                        showNotification('Не все поля заполнены', 'warning');
                        $('#Email').css('border-color', 'red');
                        $('#passwordregister').css('border-color', 'red');
                        $('#passwordregistersecond').css('border-color', 'red');
                        $('#phoneRegister').css('border-color', 'red');
                        $('#lastName').css('border-color', 'red');
                        $('#Name').css('border-color', 'red');
                    } else if (data.message === "Пароли не совпадают") {                        
                        showNotification('Пароли не совпадают', 'warning');
                        $('#passwordregister').css('border-color', 'red');
                        $('#passwordregistersecond').css('border-color', 'red');
                    } else if (data.message === "Номер телефона должен содержать ровно 10 символов") {
                        showNotification('Номер телефона должен содержать ровно 10 символов', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                    } else if (data.message === "Неверный формат адреса электронной почты") {
                        showNotification('Неверный формат адреса электронной почты', 'warning');
                        $('#Email').css('border-color', 'red');
                    } else if (data.message === "Пользователь с таким номером телефона и адресом электронной почты уже существует") {
                        showNotification('Пользователь с таким номером телефона и адресом электронной почты уже существует', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                        $('#Email').css('border-color', 'red');
                    } else if (data.message === "Номер телефона содержит четыре подряд нуля") {
                        showNotification('Номер телефона содержит четыре подряд нуля', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                    } else if (data.message === "Номер телефона не может состоять только из нулей") {
                        showNotification('Номер телефона не может состоять только из нулей', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                    } else if (data.message === "Необходимо согласиться с условиями") {
                        if (!consentChecked) {
                            showNotification2('Необходимо согласиться с условиями');
                            $('#consentLink').css('color', 'red');
                        }
                    } else if (data.message === "Пользователь с таким адресом электронной почты уже зарегистрирован") {
                        showNotification('Пользователь с таким адресом электронной почты уже зарегистрирован', 'warning');
                        $('#Email').css('border-color', 'red');
                    } else if (data.message === "Пользователь с таким номером телефона уже зарегистрирован") {
                        showNotification('Пользователь с таким номером телефона уже зарегистрирован', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                    } else {
                        $('#errorDiv').text('Произошла ошибка при регистрации: ' + data.message).show();
                    }
                }

                // Добавим обработку ошибок с !consentChecked
                if (!consentChecked) {
                    if (data.message === "Не все поля заполнены") {                        
                        showNotification('Не все поля заполнены', 'warning');
                        $('#Email').css('border-color', 'red');
                        $('#passwordregister').css('border-color', 'red');
                        $('#passwordregistersecond').css('border-color', 'red');
                        $('#phoneRegister').css('border-color', 'red');
                        $('#lastName').css('border-color', 'red');
                        $('#Name').css('border-color', 'red');
                    } else if (data.message === "Пароли не совпадают") {                        
                        showNotification('Пароли не совпадают', 'warning');
                        $('#passwordregister').css('border-color', 'red');
                        $('#passwordregistersecond').css('border-color', 'red');
                    } else if (data.message === "Номер телефона должен содержать ровно 10 символов") {
                        showNotification('Номер телефона должен содержать ровно 10 символов', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                    } else if (data.message === "Неверный формат адреса электронной почты") {
                        showNotification('Неверный формат адреса электронной почты', 'warning');
                        $('#Email').css('border-color', 'red');
                    } else if (data.message === "Пользователь с таким номером телефона и адресом электронной почты уже существует") {
                        showNotification('Пользователь с таким номером телефона и адресом электронной почты уже существует', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                        $('#Email').css('border-color', 'red');
                    } else if (data.message === "Номер телефона содержит четыре подряд нуля") {
                        showNotification('Номер телефона содержит четыре подряд нуля', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                    } else if (data.message === "Номер телефона не может состоять только из нулей") {
                        showNotification('Номер телефона не может состоять только из нулей', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                    }  else if (data.message === "Пользователь с таким адресом электронной почты уже зарегистрирован") {
                        showNotification('Пользователь с таким адресом электронной почты уже зарегистрирован', 'warning');
                        $('#Email').css('border-color', 'red');
                    } else if (data.message === "Пользователь с таким номером телефона уже зарегистрирован") {
                        showNotification('Пользователь с таким номером телефона уже зарегистрирован', 'warning');
                        $('#phoneRegister').css('border-color', 'red');
                    } else {
                        $('#errorDiv').text('Произошла ошибка при регистрации: ' + data.message).show();
                    }
                }
            },
            error: function () {
                // Обработка ошибки запроса регистрации
                $('#errorDiv').text('Произошла ошибка при выполнении запроса регистрации.').show();
            }
        });
    });
});







        $(document).ready(function () {
            $('#showPasswordButton3').click(function () {
                var passwordField = $('#passwordregistersecond');

                // Изменяем тип поля ввода в зависимости от его текущего типа
                if (passwordField.attr('type') === 'password') {
                    passwordField.attr('type', 'text');
                } else {
                    passwordField.attr('type', 'password');
                }
            });
        });

        $(document).ready(function () {
            $('#showPasswordButton2').click(function () {
                var passwordField = $('#passwordregister');

                // Изменяем тип поля ввода в зависимости от его текущего типа
                if (passwordField.attr('type') === 'password') {
                    passwordField.attr('type', 'text');
                } else {
                    passwordField.attr('type', 'password');
                }
            });
        });

        $(document).ready(function () {
            $('#showPasswordButton').click(function () {
                var passwordField = $('#password');

                // Изменяем тип поля ввода в зависимости от его текущего типа
                if (passwordField.attr('type') === 'password') {
                    passwordField.attr('type', 'text');
                } else {
                    passwordField.attr('type', 'password');
                }
            });
        });



        $(document).ready(function () {
            // Добавьте обработчик события для кнопки с классом "btn user-button"
            $('.btn.user-button').click(function () {
                // Проверяем значение в Local Storage перед открытием модального окна
                if (localStorage.getItem("AuthenticationSuccess") === "0") {
                    $('#modal').show();
                    
                }
            });
        });



$(document).ready(function () {
    // Функция для форматирования номера телефона
    function formatPhoneNumber(phoneNumber) {
        return '+7 (' + phoneNumber.slice(0, 3) + ') ' + phoneNumber.slice(3, 6) + '-' + phoneNumber.slice(6, 8) + '-' + phoneNumber.slice(8);
    }

    // Функция для проверки и восстановления начальных символов "+7" при изменении значения поля ввода
    $('#phoneRegisteruser').on('input', function () {
        var phoneNumber = $(this).val();
        if (!phoneNumber.startsWith('+7')) {
            // Если номер не начинается с "+7", восстанавливаем символы "+7"
            $(this).val('+7' + phoneNumber);
        }
    });

    // Проверяем значение в Local Storage при загрузке страницы
    var authenticationSuccess = localStorage.getItem("AuthenticationSuccess");

    if (authenticationSuccess === "1") {
        // Если AuthenticationSuccess равно 1, открываем модальное окно
        $('#openAuthModalButton').click(function () {
            // Получение данных о пользователе
            $.ajax({
                url: '/Home/UserProfile', // Замените на путь к вашему методу UserProfile
                type: 'GET',
                success: function (data) {
                    if (data.success) {
                        // Заполнение полей формы данными о пользователе
                        $('#Emailuser').val(data.email);
                        $('#Nameuser').val(data.name);
                        $('#lastNameuser').val(data.surname);

                        // Форматируем и устанавливаем номер телефона
                        $('#phoneRegisteruser').val(formatPhoneNumber(data.phoneNumber));

                        // Вставка первой буквы имени и фамилии в элемент с идентификатором nameuserphoto
                        var initials = (data.name.charAt(0) + data.surname.charAt(0)).toUpperCase();
                        $('#nameuserphoto').text(initials);

                        // Отображение модального окна
                        $('#modaluserr').show();
                    } else {
                        // Обработка ошибки, если не удалось получить данные о пользователе
                        console.error('Ошибка при получении данных о пользователе.');
                    }
                },
                error: function () {
                    // Обработка ошибки AJAX-запроса
                    console.error('Ошибка при выполнении AJAX-запроса.');
                }
            });
        });
    }

    // Проверка наличия символов "+7" в начале номера телефона при нажатии клавиши "Backspace"
    $('#phoneRegisteruser').on('keydown', function (event) {
        var phoneNumber = $(this).val();
        if (event.which === 8 && $(this).get(0).selectionStart <= 4 && phoneNumber.startsWith('+7')) {
            event.preventDefault();
        }
        if (event.which === 8 && $(this).get(0).selectionStart == 8 && phoneNumber.startsWith('+7 (')) {
            event.preventDefault();
        }
        if (event.which === 8 && $(this).get(0).selectionStart == 9 && phoneNumber.startsWith('+7 (')) {
            event.preventDefault();
        }
        if (event.which === 8 && $(this).get(0).selectionStart == 13 && phoneNumber.startsWith('+7 (')) {
            event.preventDefault();
        }
        if (event.which === 8 && $(this).get(0).selectionStart == 16 && phoneNumber.startsWith('+7 (')) {
            event.preventDefault();
        }
    });

    // Закрытие модального окна при клике на крестик
    $('#modaluserr .close').click(function () {
        $('#modaluserr').hide();
    });

    // Закрытие модального окна при клике вне его области
    $(window).click(function (event) {
        if (event.target == $('#modaluserr')[0]) {
            $('#modaluserr').hide();
        }
    });
});
   


$(document).ready(function () {
    // Флаг, указывающий на изменение номера телефона
    var phoneChanged = false;
    // Флаг, указывающий на изменение email
    var emailChanged = false;

    // Вызываем функцию для активации или деактивации кнопки сохранения при первой загрузке модального окна
    toggleSaveButton();

    // Следим за изменениями в полях ввода email
    $("#Emailuser").on("input", function () {
        emailChanged = true; // Установка флага изменения email
        toggleSaveButton();
        checkAllFieldsFilled(); // Проверка заполнения всех полей
    });

    // Следим за изменениями в полях ввода имени и фамилии
    $("#Nameuser, #lastNameuser").on("input", function () {
        toggleSaveButton();
        checkAllFieldsFilled(); // Проверка заполнения всех полей
    });

    // Следим за изменением номера телефона
    $("#phoneRegisteruser").on("input", function () {
        phoneChanged = true;
        toggleSaveButton();
        checkAllFieldsFilled(); // Проверка заполнения всех полей
    });

    // Функция для активации или деактивации кнопки сохранения в зависимости от наличия изменений в полях
    function toggleSaveButton() {
        var email = $("#Emailuser").val();
        var name = $("#Nameuser").val();
        var lastName = $("#lastNameuser").val();
        var phone = $('#phoneRegisteruser').val().replace(/_/g, '').replace(/\s+/g, '').replace(/[()+-]/g, '').replace(/^7/, '');

        // Проверяем, есть ли в полях ввода изменения, кроме email
        var hasChanges = phoneChanged || emailChanged || name.trim() !== "" || lastName.trim() !== "";

        // Активируем или деактивируем кнопку сохранения
        $("#savebutton").prop("disabled", !hasChanges);
    }

    // Функция для проверки заполнения всех полей и отображения сообщения об ошибке при необходимости
    function checkAllFieldsFilled() {
        var email = $("#Emailuser").val();
        var name = $("#Nameuser").val();
        var lastName = $("#lastNameuser").val();
        var phone = $("#phoneRegisteruser").val();

        // Проверка наличия пустых полей
        if (email.trim() === "" || name.trim() === "" || lastName.trim() === "" || phone.trim() === "") {
            $("#errorMessage5").show();
        } else {
            $("#errorMessage5").hide();
        }
    }

    // Отправка данных на сервер при редактировании пользователя
    $("#savebutton").click(function () {
        var email = $("#Emailuser").val();
        var name = $("#Nameuser").val();
        var lastName = $("#lastNameuser").val();
        var phone = $('#phoneRegisteruser').val().replace(/_/g, '').replace(/\s+/g, '').replace(/[()+-]/g, '').replace(/^7/, '');

        // Показываем сообщение об ошибке
        $("#phoneErrorMessage").hide();
        $("#phoneMail10").hide();

        // Если номер телефона не изменился и не было изменений в email, отправляем данные без проверки на сервер
        if (!phoneChanged && !emailChanged) {
            updateUser(email, name, lastName, phone);
        } else {
            // Проверяем длину номера телефона
            if (phone.trim() !== "") {
                // Проверяем, изменился ли номер телефона
                if (phoneChanged) {
                    $.ajax({
                        type: "POST",
                        url: "/Home/CheckPhoneNumber",
                        data: { phone: phone },
                        success: function (result) {
                            if (result.exists) {
                               
                                showNotification('Номер телефона уже используется.', 'error');
                            } else {
                                // Проверяем, изменился ли email
                                if (emailChanged) {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Home/CheckEmail",
                                        data: { email: email },
                                        success: function (emailResult) {
                                            if (emailResult.exists) {
                                                showNotification('Пользователь с таким email уже существует.', 'error');
                                            } else {
                                                updateUser(email, name, lastName, phone);
                                            }
                                        },
                                        error: function () {
                                            // Обработка ошибки
                                            alert("Произошла ошибка при проверке email");
                                        }
                                    });
                                } else {
                                    updateUser(email, name, lastName, phone);
                                }
                            }
                        },
                        error: function () {
                            // Обработка ошибки
                            alert("Произошла ошибка при проверке номера телефона");
                        }
                    });
                } else {
                    // Номер телефона не изменился, отправляем данные без проверки на сервер
                    // Проверяем, изменился ли email
                    if (emailChanged) {
                        $.ajax({
                            type: "POST",
                            url: "/Home/CheckEmail",
                            data: { email: email },
                            success: function (emailResult) {
                                if (emailResult.exists) {
                                    showNotification('Пользователь с таким email уже существует.', 'error');
                                } else {
                                    updateUser(email, name, lastName, phone);
                                }
                            },
                            error: function () {
                                // Обработка ошибки
                                alert("Произошла ошибка при проверке email");
                            }
                        });
                    } else {
                        updateUser(email, name, lastName, phone);
                    }
                }
            } else {
                // Если номер телефона не указан, просто отправляем данные без проверки на сервер
                // Проверяем, изменился ли email
                if (emailChanged) {
                    $.ajax({
                        type: "POST",
                        url: "/Home/CheckEmail",
                        data: { email: email },
                        success: function (emailResult) {
                            if (emailResult.exists) {
                                
                                showNotification('Пользователь с таким email уже существует.', 'error');
                            } else {
                                updateUser(email, name, lastName, phone);
                            }
                        },
                        error: function () {
                            // Обработка ошибки
                            alert("Произошла ошибка при проверке email");
                        }
                    });
                } else {
                    updateUser(email, name, lastName, phone);
                }
            }
        }
    });

    // Функция для обновления данных пользователя
    function updateUser(email, name, lastName, phone) {
        $.ajax({
            type: "POST",
            url: "/Home/SaveUser",
            data: { email: email, name: name, lastName: lastName, phone: phone },
            success: function (result) {
                if (result.success) {
                    // Обработка успешного сохранения данных
                    $('#modaluserr').hide();
                } else {
                    // Обработка ошибок при сохранении данных
                    if (result.message === "Номер телефона содержит четыре подряд нуля") {
                        showNotification('Номер телефона содержит четыре подряд нуля.', 'error');
                    } else if (result.message === "Номер телефона должен содержать не менее 10 символов") {

                        showNotification('Номер телефона должен содержать не менее 10 символов', 'error');
                    } else if (result.message === "Номер телефона не может состоять только из нулей") {

                        showNotification('Номер телефона не может состоять только из нулей.', 'error');
                    } else if (result.message === "Номер телефона должен содержать не более 10 символов") {

                        showNotification('"Номер телефона должен содержать не более 10 символов.', 'error');
                    } else if (result.message === "Некорректный адрес электронной почты") {

                        showNotification('Некорректный адрес электронной почты.', 'error');
                    } else if (result.message === "Пользователь с таким email уже существует") {

                        showNotification('Пользователь с таким email уже существует.', 'error');
                    } else {
                        alert("Ошибка: " + result.message);
                    }
                }
            },
            error: function () {
                // Обработка ошибки
                alert("Произошла ошибка при отправке данных");
            }
        });
    }

    // Закрытие модального окна при клике на кнопку закрытия ("close")
    $('#modaluserr .close').click(function () {
        $('#modaluserr').hide();
        $("#phoneErrorMessage10").hide();
        $("#phoneErrorMessage20").hide(); // Скрыть сообщение об ошибке
        $("#phoneMailMessage20").hide(); // Скрыть сообщение о некорректном адресе электронной почты
        $("#phoneMail10").hide();
        $("#errorMessage5").hide();// Скрыть сообщение о существующем email
        $("#Emailuser, #Nameuser, #lastNameuser, #phoneRegisteruser").val(""); // Сброс значений полей ввода
        toggleSaveButton(); // Обновить состояние кнопки сохранения
        phoneChanged = false; // Сбрасываем флаг изменения номера телефона
        emailChanged = false; // Сбрасываем флаг изменения email
    });

    // Закрытие модального окна при клике вне его области
    $(window).click(function (event) {
        if (event.target == $('#modaluserr')[0]) {
            $('#modaluserr').hide();
            $("#phoneErrorMessage10").hide();
            $("#phoneErrorMessage20").hide(); // Скрыть сообщение об ошибке
            $("#phoneMailMessage20").hide(); // Скрыть сообщение о некорректном адресе электронной почты
            $("#phoneMail10").hide();
            $("#errorMessage5").hide();// Скрыть сообщение о существующем email
            $("#Emailuser, #Nameuser, #lastNameuser, #phoneRegisteruser").val(""); // Сброс значений полей ввода
            toggleSaveButton(); // Обновить состояние кнопки сохранения
            phoneChanged = false; // Сбрасываем флаг изменения номера телефона
            emailChanged = false; // Сбрасываем флаг изменения email
        }
    });
});

   

        $(document).ready(function () {
            $('#logoutButton').click(function () {
                $.ajax({
                    url: '/Home/Logout', // Замените на реальный URL вашего контроллера
                    type: 'POST', // или 'GET', в зависимости от вашего контроллера
                    success: function (response) {
                        // Успешное выполнение запроса
                        console.log(response);
                        localStorage.setItem('AuthenticationSuccess', '0');
                         window.location.reload(); // Пример обновления страницы
                    },
                    error: function (error) {
                        // Ошибка выполнения запроса
                        console.error(error);
                    }
                });
            });
        });
    

function AddCardListeners() {

    $('.info-button1').on('click', function () {
        var productId = $(this).data('idproduct');

        $.ajax({
            type: "GET",
            url: "/Home/GetPackageName",
            data: { packageId: packageId },
            success: function (packageName) {
                resolve(packageName);
            },
            error: function (xhr, status, error) {
                reject(error);
            }
        });



        $.ajax({
            type: "GET",
            url: "/Home/GetProviderName",
            data: { providerId: providerId },
            success: function (providerName) {
                resolve(providerName);
            },
            error: function (xhr, status, error) {
                reject(error);
            }
        });

        $.ajax({
            url: '/Home/GetProduct',
            type: 'GET',
            data: { Id: productId },
            success: function (Product) {
                if (Product["product_sale"]) {
                    var discountedPrice = Math.round((1 - Product["product_sale"] / 100) * Product["product_price"]);
                    $('#productPrice').html('<del>' + Product["product_price"] + ' руб.</del> ' + discountedPrice + ' руб. за');
                    $('.sale1').text('-' + Product["product_sale"] + '%');
                    $('.sale1').toggle(Product["product_sale"] > 0);
                } else {
                    $('#productPrice').text(Product["product_price"] + ' руб. за');
                    $('.sale1').hide();
                }

                $('#productNameLabel').text(Product["name_product"]);
                $('#description').text(Product["product_description_"]);
                $('#productImage').attr('src', '' + Product["product_img"]);
                $('#productWeight').text(Product["product_weight"] + ' гр.');
                $('#productMassPerFat').text('Массовая доля жира:' + Product["product_mass_per_fat"] + '%');
                $('#productfat').text('Жирность:' + Product["product_fat"] + '%');
                $('#productProtein').text('Белки:' + Product["product_protein"] + 'г.');
                $('#productFatty').text('Жиры:' + Product["product_fatty"] + 'г.');
                $('#productCarb').text('Углеводы:' + Product["product_carb"] + 'г.');
                $('#productEnergyValue').text('Энергетическая ценность:' + Product["product_energy_value"] + 'ккал');
                $('#productStorageLife').text('Срок хранения:' + Product["product_storage_life"] + ' дней с момента фасовки');
                $('#productPackageId').text(Product["product_package_id"]);
                $('#productStorageConditions').text('Условия хранения:' + Product["product_storage_conditions"]);
                $('#productSale').text(Product["product_sale"]);
                $('#productRemain').text(Product["product_remain"]);
                $('#productArticle').text('Артикул:' + Product["product_article"]);
                $('#productSostav').text('Состав:' + Product["product_sostav"]);
                $('#prodid').text(Product["idProduct"]);

                Product["rates"].forEach(function (comment) {
                    var initials = comment["user"]["user_name"].charAt(0) + comment["user"]["surname"].charAt(0);
                    var starsHTML = '';
                    var rating = parseInt(comment["_Rate"]);
                    for (var i = 0; i < rating; i++) {
                        starsHTML += '★';
                    }
                    for (var j = rating; j < 5; j++) {
                        starsHTML += '<span style=" font-size: 30px;">☆</span>';
                    }

                    var commentHTML = '<div style="width:97%;border: 3px solid black;margin-bottom:10px; border-radius: 10px;max-height:105px;margin-left:30px">';
                    commentHTML += '<div style="display: flex; align-items: flex-start; padding: 20px;">';
                    commentHTML += '<div class="user-initials" style="width: 50px; height: 50px; background-color: gray; border-radius: 50%; text-align: center; line-height: 31px; color: white; font-weight: bold; margin-right: 10px;padding:10px;line-width:20px">' + initials + '</div>';
                    commentHTML += '<div style="flex-grow: 1;">';
                    commentHTML += '<p style="margin-top: -0.2%; margin-bottom: 0;text-align:left;font-size:20px">' + comment["user"]["user_name"] + " " + comment["user"]["surname"] + '</p>';

                    commentHTML += '<div style="display: flex; flex-direction: row;">';
                    if (comment["rate_comment"] !== null) {
                        commentHTML += '<p style="margin-top: 5px; margin-bottom: 0;text-align:left; font-size: 20px;width:850px">' + comment["rate_comment"] + '</p>';
                    }
                    commentHTML += '<p style="margin-top: -16px; margin-bottom: 0; font-size: 30px; margin-left: auto;color:#FFD700">' + starsHTML + '</p>';
                    commentHTML += '</div>';

                    commentHTML += '</div>';
                    commentHTML += '</div>';
                    commentHTML += '</div>';
                    commentHTML += '</div>';

                    $('#rewey').append(commentHTML);
                });

                $('#exampleModal').on('hidden.bs.modal', function (e) {
                    $('#rewey').empty();
                });

                console.log(Product);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

        $.ajax({
            url: '/Home/CountRatings',
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                var count5 = data[5] || 0;
                var count4 = data[4] || 0;
                var count3 = data[3] || 0;
                var count2 = data[2] || 0;
                var count1 = data[1] || 0;

                $('#five').text(count5);
                $('#four').text(count4);
                $('#three').text(count3);
                $('#two').text(count2);
                $('#one').text(count1);

                var totalRatings = count5 + count4 + count3 + count2 + count1;
                var percentage5 = totalRatings > 0 ? (count5 / totalRatings) * 100 : 0;
                var percentage4 = totalRatings > 0 ? (count4 / totalRatings) * 100 : 0;
                var percentage3 = totalRatings > 0 ? (count3 / totalRatings) * 100 : 0;
                var percentage2 = totalRatings > 0 ? (count2 / totalRatings) * 100 : 0;
                var percentage1 = totalRatings > 0 ? (count1 / totalRatings) * 100 : 0;

                $('#progress-bar').css('width', percentage5 + '%');
                $('#progress-bar1').css('width', percentage4 + '%');
                $('#progress-bar2').css('width', percentage3 + '%');
                $('#progress-bar3').css('width', percentage2 + '%');
                $('#progress-bar4').css('width', percentage1 + '%');
            },
            error: function (error) {
                console.error('Ошибка при получении количества оценок:', error);
            }
        });

        var packageId = $(this).data("package_id");
        var providerId = $(this).data("provider_id");

        getPackageNameById(packageId)
            .then(function (packageName) {
                $('#productSupplier').text("Упаковка: " + packageName);
            })
            .catch(function (error) {
                console.error("Ошибка при получении имени пакета:", error);
            });

        getProviderNameById(providerId)
            .then(function (providerName) {
                $('#productpa').text("Поставщик: " + providerName);
            })
            .catch(function (error) {
                console.error("Ошибка при получении имени поставщика:", error);
            });
    });
    $(document).ready(function () {
        $(".info-button1").on("click", function () {
            var productId = $(this).data('idproduct');
            var $productRate = $("#productrate");
            var $productRate2 = $("#productrate2");
            var $ratingStars = $("#rating-stars");

            getProductRatingById(productId)
                .then(function (averageRate) {
                    updateProductRating($productRate, $ratingStars, averageRate);

                    // Добавьте класс для изменения цвета текста, включая звезду
                    $productRate2.html('<span class="star-rating">' + averageRate + '<span class="star">★</span></span>');

                    // Переопределите цвет только для звезды и добавьте стили
                    $productRate2.find('.star')
                        .css('color', '#FFD700');
                })
                .catch(function (error) {
                    console.error("Ошибка при получении рейтинга продукта:", error);
                });
        });

        // Функция для выполнения асинхронного запроса к серверу и получения рейтинга продукта
        function getProductRatingById(productId) {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    url: '/Home/GetProductRating',
                    data: { productId: productId },
                    type: 'GET',
                    success: function (result) {
                        resolve(result);
                    },
                    error: function (error) {
                        reject(error);
                    }
                });
            });
        }
    });
    function updateProductRating($productRate, $ratingStars, averageRate) {
        $productRate.text("Рейтинг продукта: " + averageRate + '★');

    }

    $(document).ready(function () {
        $('.fav-button').click(function () {
            // Получаем айди продукта из атрибута data-idd кнопки
            var productId = $(this).data('idproduct');
            var authenticationSuccess = localStorage.getItem("AuthenticationSuccess");

            if (authenticationSuccess === null || authenticationSuccess === "0") {
                $('#modal').show();
                return;
            }

            // Проверяем наличие записи в таблице UserLike
            $.ajax({
                type: 'POST',
                url: '/Home/CheckUserLike',
                data: { productId: productId },
                dataType: 'json',
                success: function (data) {
                    // Получаем userId из ответа
                    var userId = data.userId;

                    if (data.exists) {
                        // Запись уже существует, не делаем ничего
                        console.log('Запись уже существует');
                    } else {
                        // Записи нет, добавляем новую запись
                        $.ajax({
                            type: 'POST',
                            url: '/Home/AddUserLike',
                            data: { userId: userId, productId: productId },
                            dataType: 'json',
                            success: function (response) {
                                console.log('Запись успешно добавлена');
                            },
                            error: function () {
                                console.log('Произошла ошибка при добавлении записи');
                            }
                        });
                    }
                },
                error: function () {
                    console.log('Произошла ошибка при проверке наличия записи');
                }
            });
        });
    });
    $(document).ready(function () {
        $('.fav-button1').click(function () {
            var button = $(this);
            // Получаем айди продукта из атрибута data-idd кнопки
            var productId = $(this).data('idproduct');
            var authenticationSuccess = localStorage.getItem("AuthenticationSuccess");

            if (authenticationSuccess === null || authenticationSuccess === "0") {
                $('#modal').show();
                return;
            }
            // Проверяем наличие записи в таблице UserLike
            $.ajax({
                type: 'POST',
                url: '/Home/CheckUserLike',
                data: { productId: productId },
                dataType: 'json',
                success: function (data) {
                    // Получаем userId из ответа
                    var userId = data.userId;

                    if (data.exists) {
                        $.ajax({
                            type: 'POST',
                            url: '/Home/RemoveUserLike',
                            data: { userId: userId, productId: productId },
                            dataType: 'json',
                            success: function (response) {
                                if (response.success) {
                                    console.log('Товар успешно удален из избранного');
                                    button.css({ 'background-color': 'white', 'color': 'black' });
                                    showNotification('Товар удален из избранного', 'error');
                                } else {
                                    console.log('Произошла ошибка: ' + response.message);
                                }
                            },
                            error: function () {
                                console.log('Произошла ошибка при удалении товара из избранного');
                            }
                        });
                        return;
                    } else {
                        // Записи нет, добавляем новую запись
                        $.ajax({
                            type: 'POST',
                            url: '/Home/AddUserLike',
                            data: { userId: userId, productId: productId },
                            dataType: 'json',
                            success: function (response) {
                                console.log('Запись успешно добавлена');
                                button.css({ 'background-color': 'red', 'color': 'white' });
                                button.attr('title', 'Удалить из избранного');
                                showNotification('Товар добавлен в избранное', 'success');
                            },
                            error: function () {
                                console.log('Произошла ошибка при добавлении записи');
                            }
                        });
                    }
                },
                error: function () {
                    console.log('Произошла ошибка при проверке наличия записи');
                }
            });
        });
    });
    $(document).ready(function () {
        $('.send').click(function () {
            // Получаем айди продукта из атрибута data-idd кнопки
            var productId = $(this).data('idd');
            var authenticationSuccess = localStorage.getItem("AuthenticationSuccess");

            if (authenticationSuccess === null || authenticationSuccess === "0") {
                $('#modal').show();
                return;
            }
            // Проверяем наличие записи в таблице UserLike
            $.ajax({
                type: 'POST',
                url: '/Home/CheckUserBasket',
                data: { productId: productId },
                dataType: 'json',
                success: function (data) {
                    // Получаем userId из ответа
                    var userId = data.userId;

                    if (data.exists) {
                        console.log('я твой рот ебал');
                        showNotification('Товар уже в корзине', 'error');
                    } else {
                        // Записи нет, добавляем новую запись
                        $.ajax({
                            type: 'POST',
                            url: '/Home/AddUserBasket',
                            data: { userId: userId, productId: productId },
                            dataType: 'json',
                            success: function (response) {
                                console.log('Запись успешно добавлена');
                                console.log('ID пользователя:', userId);
                                console.log('ID товара:', productId);
                                showNotification('Товар добавлен в корзину', 'success');
                            },
                            error: function () {
                                console.log('Произошла ошибка при добавлении записи');
                            }
                        });
                    }
                },
                error: function () {
                    console.log('Произошла ошибка при проверке наличия записи');
                }
            });
        });
    });
    $(document).ready(function () {
        var productId; // Переменная для хранения айди продукта

        // Обработчик события клика для кнопки info-button1
        $('.info-button1').click(function () {
            // Получаем айди продукта из атрибута data-idd кнопки
            productId = $(this).data('idproduct');
        });


        // Обработчик события клика для кнопки fav-button2
        $('#cardfav').click(function (event) {
            var button = $(this);
            event.preventDefault(); // Предотвращаем стандартное действие кнопки (перенаправление)

            var authenticationSuccess = localStorage.getItem("AuthenticationSuccess");

            if (authenticationSuccess === null || authenticationSuccess === "0") {
                $('#modal').show();
                $('#exampleModal').css('display', 'none');  // Скрыть exampleModal
                $('#modal').css('z-index', '1051');  // Устанавливаем z-index для modal выше
                return;
            }

            // Проверяем, был ли установлен productId
            if (productId !== undefined) {
                // productId был установлен, можем его использовать
                // Проверяем наличие записи в таблице UserLike
                $.ajax({
                    type: 'POST',
                    url: '/Home/CheckUserLike',
                    data: { productId: productId },
                    dataType: 'json',
                    success: function (data) {
                        // Получаем userId из ответа
                        var userId = data.userId;

                        if (data.exists) {
                            // Запись уже существует, не делаем ничего

                            $.ajax({
                                type: 'POST',
                                url: '/Home/RemoveUserLike',
                                data: { userId: userId, productId: productId },
                                dataType: 'json',
                                success: function (response) {
                                    if (response.success) {
                                        console.log('Товар успешно удален из избранного');
                                        console.log('Товар успешно удален из избранного');
                                        button.css({ 'background-color': 'transparent', 'color': 'black' });
                                        showNotification('Товар удален из избранного', 'error');
                                        var favButton = $('.fav-button1[data-idproduct="' + productId + '"]');

                                        // Изменяем стили кнопки .fav-button1
                                        favButton.css({ 'background-color': 'transparent', 'color': 'black' });
                                        favButton.attr('title', 'Добавить в избранное');
                                    } else {
                                        console.log('Произошла ошибка: ' + response.message);
                                    }
                                },
                                error: function () {
                                    console.log('Произошла ошибка при удалении товара из избранного');
                                }
                            });
                            return;
                        } else {
                            // Записи нет, добавляем новую запись
                            $.ajax({
                                type: 'POST',
                                url: '/Home/AddUserLike',
                                data: { userId: userId, productId: productId },
                                dataType: 'json',
                                success: function (response) {
                                    console.log('Запись успешно добавлена');
                                    button.css({ 'background-color': 'red', 'color': 'white' });
                                    button.attr('title', 'Удалить из избранного');
                                    var favButton = $('.fav-button1[data-idproduct="' + productId + '"]');

                                    // Изменяем стили кнопки .fav-button1
                                    favButton.css({ 'background-color': 'red', 'color': 'white' });
                                    favButton.attr('title', 'Удалить из избранного');
                                    showNotification('Товар добавлен в избранное', 'success');
                                },
                                error: function () {
                                    console.log('Произошла ошибка при добавлении записи');

                                }
                            });
                        }
                    },
                    error: function () {
                        console.log('Произошла ошибка при проверке наличия записи');
                    }
                });
            } else {
                console.log('Айди продукта не определено');
            }
        });
    });
    $(document).ready(function () {
        var productId; // Переменная для хранения айди продукта

        // Обработчик события клика для кнопки info-button1
        $('.info-button1').click(function () {
            // Получаем айди продукта из атрибута data-idd кнопки
            productId = $(this).data('idproduct');
        });

        // Обработчик события клика для кнопки senddd
        $('#senddd').click(function (event) {
            event.preventDefault(); // Предотвращаем стандартное действие кнопки (перенаправление)

            var authenticationSuccess = localStorage.getItem("AuthenticationSuccess");

            if (authenticationSuccess === null || authenticationSuccess === "0") {
                $('#modal').show();
                $('#exampleModal').css('display', 'none');  // Скрыть exampleModal
                $('#modal').css('z-index', '1051');  // Устанавливаем z-index для modal выше
                return;
            }

            // Проверяем, был ли установлен productId
            if (productId !== undefined) {
                // productId был установлен, можем его использовать
                // Проверяем наличие записи в таблице UserLike
                $.ajax({
                    type: 'POST',
                    url: '/Home/CheckUserBasket',
                    data: { productId: productId },
                    dataType: 'json',
                    success: function (data) {
                        // Получаем userId из ответа
                        var userId = data.userId;

                        if (data.exists) {
                            // Запись уже существует, удаляем из корзины
                            console.log('Товар успешно удален из корзины');
                            showNotification('Товар удален из корзины', 'error');
                            $.ajax({
                                type: 'POST',
                                url: '/Home/RemoveUserBasket',
                                data: { userId: userId, productId: productId },
                                dataType: 'json',
                                success: function (response) {
                                    if (response.success) {
                                        console.log('Товар успешно удален из корзины');
                                        showNotification('Товар удален из корзины', 'error');
                                        // Здесь можно выполнить другие действия по успешному удалению
                                    } else {
                                        console.log('Произошла ошибка: ' + response.message);
                                    }
                                },
                                error: function () {
                                    console.log('Произошла ошибка при удалении товара из корзины');
                                }
                            });
                            
                        } else {
                            // Записи нет, добавляем новую запись
                            $.ajax({
                                type: 'POST',
                                url: '/Home/AddUserBasket',
                                data: { userId: userId, productId: productId },
                                dataType: 'json',
                                success: function (response) {
                                    console.log('Запись успешно добавлена');
                                    showNotification('Товар добавлен в корзину', 'success');
                                    // Скрываем модальное окно
                                },
                                error: function () {
                                    console.log('Произошла ошибка при добавлении записи');

                                }
                            });
                        }
                    },
                    error: function () {
                        console.log('Произошла ошибка при проверке наличия записи');
                    }
                });
            } else {
                console.log('Айди продукта не определено');
            }
        });
    });
    function checkUserLike(productId, button) {
        $.ajax({
            type: "POST",
            url: "/Home/CheckUserLike",
            data: { productId: productId },
            success: function (response) {
                if (response.exists) {
                    // Если товар в избранном, добавляем красный фон кнопке
                    button.css('background-color', 'red');
                    button.css('color', 'white');
                    button.attr('title', 'Удалить из избранного');
                } else {
                    // Если товар не в избранном, убираем красный фон кнопки
                    button.css('background-color', 'transparent');
                }
            },
            error: function (xhr, status, error) {
                // Обработка ошибки, если не удалось выполнить запрос
                console.error(error);
            }
        });
    }

    // При загрузке страницы выполним проверку наличия товара в избранном для каждой кнопки
    $(document).ready(function () {
        $('.fav-button1').each(function () {
            var button = $(this);
            var productId = button.data('idproduct');
            checkUserLike(productId, button);
        });
    });
    function showNotification2(message, type) {
        Swal.fire({
            position: 'bottom-end',
            icon: type,
            title: message,
            showConfirmButton: false,
            timer: 5000, // Закрыть уведомление через 5 секунд
            toast: true,
            timerProgressBar: true,
            didOpen: (toast) => {
                // Убрать закрытие при наведении мыши
                toast.addEventListener('mouseenter', Swal.stopTimer);
                toast.addEventListener('mouseleave', Swal.resumeTimer);
            }
        });
    }
    function showNotification2(message, type) {
        Swal.fire({
            position: 'bottom-end',
            icon: type,
            title: message,
            showConfirmButton: false,
            timer: 5000, // Закрыть уведомление через 5 секунд
            toast: true,
            timerProgressBar: true,
            didOpen: (toast) => {
                // Убрать закрытие при наведении мыши
                toast.addEventListener('mouseenter', Swal.stopTimer);
                toast.addEventListener('mouseleave', Swal.resumeTimer);
            }
        });
    }
    $(document).ready(function () {
        // Отслеживаем клик по кнопке "info-button1"
        $('.info-button1').on('click', function () {
            var productId = $(this).data('idproduct'); // Получаем айди товара из кнопки

            // Отправляем запрос на сервер для проверки наличия товара в избранном
            $.ajax({
                type: "POST",
                url: "/Home/CheckUserLike",
                data: { productId: productId },
                success: function (response) {
                    var modalButton = $('#exampleModal').find('.fav-button2');

                    // Если товар в избранном, добавляем красный фон кнопке в модальном окне
                    if (response.exists) {
                        modalButton.css('background-color', 'red');
                        modalButton.css('color', 'white');
                    } else {
                        // Если товар не в избранном, убираем красный фон кнопки в модальном окне
                        modalButton.css('background-color', 'transparent');
                    }
                },
                error: function (xhr, status, error) {
                    // Обработка ошибки, если запрос не удался
                    console.error(error);
                }
            });
        });

        // После закрытия модального окна
        $('#exampleModal').on('hidden.bs.modal', function (e) {
            // Сбрасываем стили для modalButton
            var modalButton = $('#exampleModal').find('.fav-button2');
            modalButton.css('background-color', '');
            modalButton.css('color', ''); // сброс цвета
        });
    });
}

