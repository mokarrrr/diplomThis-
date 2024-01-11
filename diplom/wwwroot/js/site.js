// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
        $(document).ready(function () {
            $(window).scroll(function () {
                var scroll = $(window).scrollTop();
                var mainBodyOffset = $('.main_body').offset().top;

                if (scroll >= mainBodyOffset) {
                    $('header').addClass('header-scrolled');
                } else {
                    $('header').removeClass('header-scrolled');
                }
            });
        });

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

function showModal() {
    if (!Cookies.get('hasVisited') || Cookies.get('hasVisited') !== 'true') {
        modal.style.display = "block";
    }
}

function setVisitedCookie() {
    Cookies.set('hasVisited', 'true', { expires: 365 }); // Куки на год
}

switchToRegister.onclick = function (event) {
    event.preventDefault(); // Предотвращаем стандартное действие ссылки
    loginContent.style.display = "none";
    registerContent.style.display = "block";
    modal.scrollTop = 0;
}

for (var i = 0; i < closeButtons.length; i++) {
    closeButtons[i].onclick = function () {
        modal.style.display = "none";
        setVisitedCookie();
    }
}

window.onload = function () {
    showModal();
}

// Добавлено событие для закрытия модального окна при клике вне его
window.addEventListener('click', function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
        setVisitedCookie();
    }
});

$(function () {
    $(".btn").click(
        function () {
            var imgtovara = $(this).attr('data-imgtovara');
            var nametitle = $(this).attr('data-nametitle');
            var pricetovar = $(this).attr('data-pricetovar');

            $(".tovarimg").append('<img class="img-fluid" src="' + imgtovara + '" alt="..." />');
            $(".tovarinfo").append('<p class="h3">' + nametitle + '</h1>');
            $(".tovarinfo").append('<p><strong>Цена</strong>:' + pricetovar + '</p>');
            $("#hide1").attr('value', nametitle);
            $("#hide2").attr('value', pricetovar);
        })
});


    
$(document).ready(function () {
    $('.info-button1').on('click', function () {
        var productName = $(this).data('nametitle');
        var description = $(this).data('description');
        var productImageBase64 = $(this).data('img');
        var productPrice = $(this).data('price');
        var productWeight = $(this).data('weight');
        var productMassPerFat = $(this).data('mass_per_fat');
        var productFat = $(this).data('fat');
        var productProtein = $(this).data('protein');
        var productFatty = $(this).data('fatty');
        var productCarb = $(this).data('carb');
        var productEnergyValue = $(this).data('energy_value');
        var productStorageLife = $(this).data('storage_life');
        var productPackageId = $(this).data('package_id');
        var productStorageConditions = $(this).data('storage_conditions');
        var productSale = $(this).data('sale');
        var productRemain = $(this).data('remain');
        var productArticle = $(this).data('article');
        var productSostav = $(this).data('sostav');

        // Проверяем наличие скидки
        if (productSale) {
            // Рассчитываем цену с учетом скидки и округляем до целого числа
            var discountedPrice = Math.round((1 - productSale / 100) * productPrice);

            // Обновляем значения в таблице
            $('#productPrice').html('<del>' + productPrice + ' руб.</del> ' + discountedPrice + ' руб. за');

            // Обновляем значение скидки в div и отображаем его, если скидка больше 0
            $('.sale1').text('-'+productSale + '%');
            $('.sale1').toggle(productSale > 0);
        } else {
            // Если скидки нет, просто обновляем цену и скрываем div
            $('#productPrice').text(productPrice + ' руб. за');
            $('.sale1').hide();
        }

        $('#productNameLabel').text(productName);
        $('#description').text(description);
        $('#productImage').attr('src', '' + productImageBase64);
        $('#productWeight').text(productWeight + ' гр.');
        $('#productMassPerFat').text('Массовая доля жира:' + productMassPerFat + '%');
        $('#productfat').text('Жирность:' + productFat);
        $('#productProtein').text('Белки:' + productProtein + 'г.');
        $('#productFatty').text('Жиры:' + productFatty + 'г.');
        $('#productCarb').text('Углеводы:' + productCarb + 'г.');
        $('#productEnergyValue').text('Энергетическая ценность:' + productEnergyValue + 'ккал');
        $('#productStorageLife').text('Срок хранения:' + productStorageLife + ' дней с момента фасовки');
        $('#productPackageId').text(productPackageId);
        $('#productStorageConditions').text('Условия хранения:' + productStorageConditions);
        $('#productSale').text(productSale);
        $('#productRemain').text(productRemain);
        $('#productArticle').text('Артикул:' + productArticle);
        $('#productSostav').text('Состав:' + productSostav);
    });
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
        var productId = $(this).data("idd");
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
                // Очистить звезды
                // $ratingStars.empty();

                // // Добавить полные и неполные звезды
                // for (var i = 0; i < 5; i++) {
        //     var starHtml = '<svg class="c-icon" width="32" height="32" viewBox="0 0 32 32">';
        //     if (averageRate > i) {
        //         var fillPercentage = (averageRate - i) >= 1 ? 100 : ((averageRate - i) * 100);
        //         starHtml += '<use xlink:href="#star" style="fill: url(#gradient-' + i + ');"/>';
        //         // Добавить градиент для части звезды
        //         starHtml += '<defs><linearGradient id="gradient-' + i + '" x1="0%" y1="0%" x2="100%" y2="0%"><stop offset="' + fillPercentage + '%" style="stop-color: #FFD700; stop-opacity: 1" /><stop offset="' + fillPercentage + '%" style="stop-color: transparent; stop-opacity: 0" /></linearGradient></defs>';
        //     }
        //     starHtml += '</svg>';
        //     $ratingStars.append(starHtml);
        // }
    }
        });



/*    @* табы * @*/

document.addEventListener('DOMContentLoaded', function () {
    const tabs = document.querySelectorAll('#myTabs .tab');
    const tabContents = document.querySelectorAll('#myTabsContent .tab-content');

    tabs.forEach(function (tab, index) {
        tab.addEventListener('click', function () {
            tabs.forEach(function (t) {
                t.classList.remove('active');
            });

            tabContents.forEach(function (content) {
                content.classList.remove('active');
            });

            this.classList.add('active');
            tabContents[index].classList.add('active');

            // Добавленный код для скрытия tab-reviews-content
            if (tabContents[index].id === 'tab-info-content') {
                document.getElementById('tab-reviews-content').style.display = 'none';
            } else {
                document.getElementById('tab-reviews-content').style.display = 'flex';
            }
        });
    });

    // Активировать первую вкладку
    tabs[0].click();
});
 


    /*@* добавление в корзину * @*/
  
        $(document).ready(function () {
            // При нажатии на элемент с классом "send"
            $('.send').on('click', function () {
                // Получите цену товара
                var priceElement = $(this).closest('.card-content').find('.price');
                var saleElement = $(this).closest('.card-content').find('.sale');

                // Получите цену без скидки
                var originalPrice = parseInt(priceElement.text().replace(/[^\d.]/g, ''));

                // Получите сумму с учетом скидки
                var priceWithSale = saleElement.length > 0 ? originalPrice - Math.floor(originalPrice * parseFloat(saleElement.text().replace(/[^\d.]/g, '')) / 100) : originalPrice;

                // Получите текущую сумму в корзине
                var currentTotal = parseInt($('#cartButton').attr('data-total')) || 0;

                // Прибавьте цену товара с учетом скидки к текущей сумме
                var newTotal = currentTotal + priceWithSale;

                // Обновите атрибут data-total у кнопки корзины
                $('#cartButton').attr('data-total', newTotal);

                // Обновите текст суммы в корзине с символом корзины
                $('#cartButton').html('<i class="fas fa-shopping-basket"></i> ' + newTotal + ' руб.');
            });
        });
 

   /* @* фильтры * @*/
    
    document.getElementById('filter').addEventListener('change', function () {
        // Получите выбранное значение фильтра
        var selectedFilter = this.value;

        // Получите все карточки продуктов
        var productCards = document.querySelectorAll('.product-card');

        // Преобразуйте NodeList в Array для удобной манипуляции
        var productArray = Array.from(productCards);

        // Отсортируйте карточки продуктов в зависимости от выбранного фильтра
        switch (selectedFilter) {
            case 'by_popularity':
                // Реализуйте логику сортировки для популярности
                break;
            case 'by_alphabet':
                // Сортируйте карточки продуктов по алфавиту
                productArray.sort(function (a, b) {
                    var titleA = a.querySelector('.card-title').innerText.toUpperCase();
                    var titleB = b.querySelector('.card-title').innerText.toUpperCase();
                    return titleA.localeCompare(titleB);
                });
                break;
            case 'cheaper':
                // Сортируйте карточки продуктов по цене в порядке возрастания
                productArray.sort(function (a, b) {
                    var priceA = getPrice(a);
                    var priceB = getPrice(b);
                    return priceA - priceB;
                });
                break;
            case 'expensive':
                // Сортируйте карточки продуктов по цене в порядке убывания
                productArray.sort(function (a, b) {
                    var priceA = getPrice(a);
                    var priceB = getPrice(b);
                    return priceB - priceA;
                });
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
   


    
        $(document).ready(function () {
            $('#softCheeseCheckbox').change(function () {
                // Получаем значение чекбокса
                var isChecked = $(this).prop('checked');

                // Устанавливаем categoryId в 1, если чекбокс отмечен, иначе в null
                var categoryId = isChecked ? 1 : null;

                // Отправляем запрос на сервер с новым categoryId
                $.ajax({
                    url: '/Home/MainPage',
                    type: 'GET',
                    data: {
                        categoryId: categoryId,
                        // Другие параметры запроса, если нужны
                    },
                    success: function (data) {
                        // Обновляем содержимое страницы, например, заменяем список продуктов
                        $('#container3').html(data);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            });
        });
   

