'use strict';

/* =================================================================== */
/*
 /*      - Template Name: eWedding - Responsive HTML Wedding Template
 /*      - Author: GoNAF
 /*      - Version: 2.00
 /*      - Website: www.zawajco.com
 /*
 /* =================================================================== */

/* -----------------------------------------------
 [Table of contents]
 --------------------------------------------------
 
 1. Preloader
 2. Countdown
 3. Image Hover Effect
 4. Flexslider Plugin Custom
 5. MagnificPopup Plugin
 5.1. MagnificPopup Gallery
 5.2. MagnificPopup Video
 6. Owl Carousel Plugin
 7. Main Slider 
 7.1. Main Slider Video-control
 7.2. Main Slider Audio-control
 7.3. Main Slider Video mobile/tablet
 8. Screen Adapter Function
 8.1. Screen Adapter on Load
 8.2. Screen Adapter on window resize
 9. Waypoints Animated Elements - Waypoint Plugin Custom
 10. Nav Bar
 10.1. Nav Bar Menu
 10.2. Navbar Background Colour Change on Page Scrolls - Waypoint+Sticky Plugins Custom
 10.3. Fixed Top Navbar - Waypoint+Sticky Plugins Custom
 11. Ajax Form Submit
 12. Smouth Scroll
 
 /* ----------------------------------------------- */
/* 1. Preloader
 /* ----------------------------------------------- */

Pace.on('done', function () {
    $('#preloader').hide();
});

/* ----------------------------------------------- */
/* 2. Countdown
 /* ----------------------------------------------- */

$('#clock').countdown('2017/08/16').on('update.countdown', function (event) {
    var $this = $(this).html(event.strftime(''
            + '<span class="wraper-clock"><div class="value">%D</div><div class="value-label">Days</div></span><span class="sep">:</span>'
            + '<span class="wraper-clock"><div class="value">%H</div><div class="value-label">Hours</div></span><span class="sep">:</span>'
            + '<span class="wraper-clock"><div class="value">%M</div><div class="value-label">Minutes</div></span><span class="sep">:</span>'
            + '<span class="wraper-clock"><div class="value">%S</div><div class="value-label">Seconds</div></span>'
            ));
});

/* ----------------------------------------------- */
/* 3. Image Hover Effect
 /* ----------------------------------------------- */

$(".img")
        .on('mouseenter', function () {
            $(this).addClass("hover");
        })
        .on('mouseleave', function () {
            $(this).removeClass("hover");
        });

/* ----------------------------------------------- */
/* 4. Flexslider Plugin Custom
 /* ----------------------------------------------- */

$(window).on('load', function () {
    $('.flexslider').flexslider({
        animation: "fade",
        controlNav: false,
        directionNav: true,
        slideshowSpeed: 5000,
        animationSpeed: 1000,
        pauseOnAction: false,
        animationLoop: true,
        pauseOnHover: true
    });
});

/* ----------------------------------------------- */
/* 5. MagnificPopup Plugin Custom
 /* ----------------------------------------------- */

/* 5.1. MagnificPopup Gallery
 ----------------------------------------- */

$('.popup-gallery').magnificPopup({
    delegate: 'a',
    type: 'image',
    tLoading: 'Loading image #%curr%...',
    mainClass: 'mfp-img-mobile',
    gallery: {
        enabled: true,
        navigateByImgClick: true,
        preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
    },
    image: {
        tError: '<a href="%url%">The image #%curr%</a> could not be loaded.'
    }
});

/* 5.2. MagnificPopup Video
 ----------------------------------------- */

$('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
    disableOn: 700,
    type: 'iframe',
    mainClass: 'mfp-fade',
    removalDelay: 160,
    preloader: false,
    fixedContentPos: false
});

/* ----------------------------------------------- */
/* 6. Owl Carousel Plugin Custom
 /* ----------------------------------------------- */

$("#owl-registry").owlCarousel({
    autoPlay: 3000, //Set AutoPlay to 3 seconds
    items: 3,
    itemsDesktop: [1199, 3],
    itemsDesktopSmall: [979, 3]
});

/* ----------------------------------------------- */
/* 7. Main Slider 
 /* ----------------------------------------------- */

/* 7.1.  Main Slider Video-control
 ------------------------------------ */

var videotoolbar = document.querySelector(".video-control");
if (videotoolbar) {
    videotoolbar.addEventListener("click", function (e) {
        var video = $("#video-slider").data("vide").getVideoObject();
        var button = e.target;
        if (!button.classList.contains("glyphicon-volume-up") && !button.classList.contains("glyphicon-volume-off")) {
            if (!button.classList.contains("glyphicon-pause")) {
                video.play();
                button.classList.add("glyphicon-pause");
                button.classList.remove("glyphicon-play");
            } else {
                video.pause();
                button.classList.add("glyphicon-play");
                button.classList.remove("glyphicon-pause");
            }
            ;
        } else {
            if (!button.classList.contains("glyphicon-volume-up")) {
                button.classList.add("glyphicon-volume-up");
                button.classList.remove("glyphicon-volume-off");
            } else {
                button.classList.add("glyphicon-volume-off");
                button.classList.remove("glyphicon-volume-up");
            }
            ;
            video.muted = !video.muted;
        }
        ;
    });
}
;


/* 7.2. Main Slider Audio-control
 ------------------------------------ */

var audioplayer = document.getElementById("audio-player");
var audiotoolbar = document.querySelector(".audio-control");
if (audiotoolbar) {
    audiotoolbar.addEventListener("click", function (e) {
        var button = e.target;
        if (!button.classList.contains("glyphicon-volume-up")) {
            audioplayer.play();
            button.classList.add("glyphicon-volume-up");
            button.classList.remove("glyphicon-volume-off");
        } else {
            audioplayer.pause();
            button.classList.add("glyphicon-volume-off");
            button.classList.remove("glyphicon-volume-up");
        }
        ;
    });
}
;

/* 7.3. Main Slider Video mobile/tablet
 ------------------------------------ */

if (device.ios() || device.android()) {
    $(".vide-video").addClass("hide");
    $(".vide-image").removeClass("hide");
}
;

/* ----------------------------------------------- */
/* 8. Screen Adapter Function
 /* ----------------------------------------------- */

function screenAdapter() {
    var screenHeight = $(window).height();
    $('.screen-adapter').css("height", screenHeight);
}
;

/* 8.1. Screen Adapter on Load
 ----------------------------------------- */

screenAdapter();

/* 8.2. Screen Adapter on window resize
 ----------------------------------------- */

$(window).resize(function () {
    screenAdapter();
});

/* ----------------------------------------------- */
/* 9. Waypoints Animated Elements - Waypoint Plugin Custom
 /* ----------------------------------------------- */
if (!device.tablet() && !device.mobile()) {
    $('.animated-element').each(function () {
        $(this).waypoint(function () {
            $(this.element).addClass('animated fadeInUp');
        },
                {offset: '90%'});
    });
    $('.animated-element-1').each(function () {
        $(this).waypoint(function () {
            $(this.element).addClass('animated fadeIn');
        },
                {offset: '90%'});
    });
    $('.animated-element-2').each(function () {
        $(this).waypoint(function () {
            $(this.element).addClass('animated zoomIn');
        },
                {offset: '90%'});
    });
    $('.animated-element-3').each(function () {
        $(this).waypoint(function () {
            $(this.element).addClass('animated bounceInRight');
        },
                {offset: '90%'});
    });
    $('.animated-element-4').each(function () {
        $(this).waypoint(function () {
            $(this.element).addClass('animated bounceInLeft');
        },
                {offset: '90%'});
    });
    $('.animated-element-5').each(function () {
        $(this).waypoint(function () {
            $(this.element).addClass('animated fadeInLeft');
        },
                {offset: '90%'});
    });
    $('.animated-element-6').each(function () {
        $(this).waypoint(function () {
            $(this.element).addClass('animated fadeInRight');
        },
                {offset: '90%'});
    });
}
;

/* ----------------------------------------------- */
/* 10. Nav Bar
 * ----------------------------------------------- */

/* 10.1. Nav Bar Menu
 ------------------------------------ */

$('.dropdown')
        .on('mouseenter', function () {
            $('.dropdown-menu', this).stop(true, true).fadeIn("fast");
            $(this).toggleClass('open');
            $('b', this).toggleClass("caret caret-up");
        })
        .on('mouseleave', function () {
            $('.dropdown-menu', this).stop(true, true).fadeOut("fast");
            $(this).toggleClass('open');
            $('b', this).toggleClass("caret caret-up");
        });

/* 10.2. Navbar Background Colour Change When Page Scrolls - Waypoint+Sticky Plugins Custom
 ------------------------------------ */

var navbar_transparent = $('.sticky-wrapper').waypoint(function (direction) {
    if (direction === 'down') {
        $("#navbar-transparent").removeClass("navbar-transparent");
    } else {
        $("#navbar-transparent").addClass("navbar-transparent");
    }
}, {
    offset: '-100%'
});

/* 10.3. Fixed Top Navbar - Waypoint+Sticky Plugins Custom
 ------------------------------------ */

var sticky = new Waypoint.Sticky({
    element: $('.navbar')[0]
});

/* ----------------------------------------------- */
/* 11. Ajax Form Submit
 * ----------------------------------------------- */

$("#block-main-form").validate({
    rules: {
        name: "required",
        subject: "required",
        message: "required",
        email: {
            required: true,
            email: true
        }
    },
    messages: {
        name: "Name Required",
        subject: "Subject Required",
        message: "Message Required",
        email: {
            required: "Email Address Required",
            email: "Please enter a valid email address"
        }
    },
    submitHandler: function (form) {
        $.ajax({
            type: "POST",
            url: "include/contact.php",
            data: $(form).serialize(),
            success: function (response) {
                $('#success').html(response);
                var $this = $('.btn');
                $this.button('loading');
                setTimeout(function () {
                    $this.button('reset');
                }, 4000);
            }
        });
        return false;
    }
});

/* ----------------------------------------------- */
/* 12. Smouth Scroll
 * ----------------------------------------------- */

$('.navbar-scroll a').smoothScroll({
    speed: 1000,
    offset: -100
});
