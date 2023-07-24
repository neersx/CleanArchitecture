$('.partners').slick({
    slidesToShow: 6,
    slidesToScroll: 1,
    autoplay: true,
    arrows: false,
    autoplaySpeed: 2000,
    responsive: [{
            breakpoint: 1100,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
                infinite: true,
                dots: true
            }
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                infinite: true,
                dots: true
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
                infinite: true,
                dots: true
            }
        }
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
    ]
});
$('.thank-you-slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    arrows: false,
    autoplaySpeed: 2000,
});

$('.mobile-gallery').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    arrows: false,
    autoplaySpeed: 2000,
    responsive: [{
            breakpoint: 992,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                infinite: true,
                dots: true
            }
        },
        {
            breakpoint: 768,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
                infinite: true,
                dots: true
            }
        },
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
    ]
});
// Menu
function myFunction(x) {
    x.classList.toggle("change");
}
$(document).ready(function() {
    // $(".menu-toggle").fadeToggle();
    $(".menu-toggle").click(function() {
        $(".header-mob-wp").toggleClass('menu-active');
    });
});
$(document).ready(function() {
    // $(".menu-toggle").fadeToggle();
    $(".menu-toggle").click(function() {
        $("body").toggleClass('scrolltop-none');
    });
});
new WOW().init();

const second = 1000,
    minute = second * 60,
    hour = minute * 60,
    day = hour * 24;

let countDown = new Date('May 29, 2020 00:00:00').getTime(),
    x = setInterval(function() {

        let now = new Date().getTime(),
            distance = countDown - now;

        document.getElementById('days').innerText = Math.floor(distance / (day)),
            document.getElementById('hours').innerText = Math.floor((distance % (day)) / (hour)),
            document.getElementById('minutes').innerText = Math.floor((distance % (hour)) / (minute)),
            document.getElementById('seconds').innerText = Math.floor((distance % (minute)) / second);

        //do something later when date is reached
        //if (distance < 0) {
        //  clearInterval(x);
        //  'IT'S MY BIRTHDAY!;
        //}

    }, second)



// pagescoll
jQuery(".menu-menu-1-container li a").click(function() {
    jQuery(".header-mob-wp").removeClass("menu-active");
    jQuery(".menu-toggle").removeClass("change");
});

$("a[href^='#']").click(function(e) {
    e.preventDefault();

    var position = $($(this).attr("href")).offset().top - 100;

    $("body, html").animate({
        scrollTop: position
    }, 1500);
});


// Scroll-to-Top
$(document).ready(function() {
    var scrollTop = $(".scrollTop");

    $(window).scroll(function() {
        var topPos = $(this).scrollTop();
        if (topPos > 100) {
            $(scrollTop).css("opacity", "1");

        } else {
            $(scrollTop).css("opacity", "0");
        }
        if ($(window).scrollTop() >= 900) {
            $('header').addClass('fixed-header');
            $('header div').addClass('visible-title');
        } else {
            $('header').removeClass('fixed-header');
            $('header div').removeClass('visible-title');
        }

    });
    $(scrollTop).click(function() {
        $('html, body').animate({
            scrollTop: 0
        }, 800);
        return false;
    });


});
$('[data-fancybox="preview"]').fancybox({
    thumbs: {
        autoStart: true
    }
});
//# sourceMappingURL=custom.js.map