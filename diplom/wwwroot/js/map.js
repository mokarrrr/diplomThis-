var mybutton = document.getElementById("backToTop");

window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

function scrollToTop() {
    var currentPosition = document.documentElement.scrollTop || document.body.scrollTop;

    if (currentPosition > 0) {
        window.requestAnimationFrame(scrollToTop);
        window.scrollTo(0, currentPosition - currentPosition / 10); // Здесь можно регулировать скорость поднятия
    }
}

mybutton.addEventListener("click", function () {
    scrollToTop();
});

var policyModal = document.getElementById("myModal1"); // Убедитесь, что идентификатор уникален
var policyBtn = document.getElementById("modalOpen"); // Идентификатор для кнопки открытия
var policyClose = policyModal.getElementsByClassName("close")[0]; // Предполагая, что у вас есть элемент с классом "close" внутри модального окна

policyBtn.onclick = function (event) {
    event.preventDefault();
    policyModal.style.display = "block";
    document.body.style.overflow = "hidden";
}

policyClose.onclick = function () {
    policyModal.style.display = "none";
    document.body.style.overflow = "";
}

window.onclick = function (event) {
    if (event.target == policyModal) {
        policyModal.style.display = "none";
        document.body.style.overflow = "";
    }
}


