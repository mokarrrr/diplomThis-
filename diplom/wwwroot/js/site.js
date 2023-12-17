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



