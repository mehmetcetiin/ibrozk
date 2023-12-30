document.body.className = document.body.className.replace('no-js', '')

document.oncontextmenu = function () { return false }
if (document.layers) {
    window.captureEvents(Event.MOUSEDOWN);
    window.onmousedown = function (e) {
        if (e.target == document) return false;
    }
}
else {
    document.onmousedown = function () { return false }
}


var modal = document.getElementById('myModal');

// Resmi alın ve modale ekleyin
var modalImg = document.getElementById('modalImg');

// Tüm resimleri döngü yapın ve onclick etkinliğini ekleyin
var photos = document.querySelectorAll('.photo img');
photos.forEach(function (photo) {
    photo.onclick = function () {
        modal.style.display = 'block';
        modalImg.src = this.src;
    };
});

// Modali kapatan <span> öğesini alın
var span = document.getElementsByClassName('close')[0];

// Kullanıcı <span> (x) üzerine tıkladığında modali kapatın
span.onclick = function () {
    modal.style.display = 'none';
};

// Kullanıcı modalin dışına tıkladığında modali kapatın
window.onclick = function (event) {
    if (event.target === modal) {
        modal.style.display = 'none';
    }
};


var modal = document.getElementById('myModal');
var modalImg = document.getElementById('modalImg');
var photos = document.querySelectorAll('.photo img');
var currentIndex = 0;

// Fotoğrafları açan fonksiyon
function openModal(index) {
    modal.style.display = 'block';
    modalImg.src = photos[index].src;
    currentIndex = index;
    document.querySelector('.menu').classList.add('lightbox-open');

}

// Fotoğrafı kapatma fonksiyonu
function closeModal() {
    modal.style.display = 'none';
    document.querySelector('.menu').classList.remove('lightbox-open');

}

// Fotoğraf geçişini yöneten fonksiyon
function navigate(direction) {
    var newIndex = currentIndex + direction;
    if (newIndex >= 0 && newIndex < photos.length) {
        openModal(newIndex);
    }
}

// Tüm resimlere onclick olayını ekle
photos.forEach(function (photo, index) {
    photo.onclick = function () {
        openModal(index);
    };
});

// Kapatma düğmesine onclick olayını ekle
var span = document.getElementsByClassName('close')[0];
span.onclick = closeModal;

// Klavye olaylarını dinle
document.addEventListener('keydown', function (event) {
    if (modal.style.display === 'block') {
        switch (event.key) {
            case 'ArrowLeft':
                navigate(-1); // Sol ok tuşu
                break;
            case 'ArrowRight':
                navigate(1); // Sağ ok tuşu
                break;
            case 'Escape':
                closeModal(); // Esc tuşu
                break;
        }
    }
});


document.addEventListener("DOMContentLoaded", function () {
    var menuIcon = document.querySelector('.menu-icon');
    var nav = document.querySelector('.nav');

    menuIcon.addEventListener('click', function () {
        nav.classList.toggle('active');
    });
});