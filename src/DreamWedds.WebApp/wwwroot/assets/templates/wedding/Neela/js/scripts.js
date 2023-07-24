/*
* Author: Wisely Themes
* Author URI: http://www.wiselythemes.com
* Theme Name: Neela
* Version: 1.0.1
*/

/*jslint browser:true, devel: true, this: true, long: true, unordered: true */
/*global google, window, RichMarker, jQuery, mobile_menu_title, hero_full_screen, slidehow_images, timeline_parallax, map_color, map_initial_zoom, map_initial_latitude, map_initial_longitude, use_default_map_style, contact_form_success_msg, contact_form_error_msg, contact_form_recaptcha_error_msg, c_days, c_hours, c_minutes, c_seconds, countdown_end_msg, Waypoint, Freewall, map_markers, lightbox, onepage_nav, rtl, grecaptcha  */

var Neela;

(function ($) {
    "use strict";

    Neela = {

        initialized: false,
        cDays: c_days,
        cHours: c_hours,
        cMinutes: c_minutes,
        cSeconds: c_seconds,
        countdownEndMsg: countdown_end_msg,
        contactFormErrorMsg: contact_form_error_msg,
        contactFormRecaptchaErrorMsg: contact_form_recaptcha_error_msg,
        contactFormSuccessMsg: contact_form_success_msg,
        sendingMail: false,
        heroFullScreen: hero_full_screen,
        mapColor: map_color,
        mapInitialLatitude: map_initial_latitude,
        mapInitialLongitude: map_initial_longitude,
        mapInitialZoom: map_initial_zoom,
        mapMarkers: map_markers,
        useDefaultMapStyle: use_default_map_style,
        mobMenuFlag: false,
        mobileMenuTitle: mobile_menu_title,
        onepageNav: onepage_nav,
        rtlFlag: rtl,
        slidehowImages: slidehow_images,
        timelineParallax: timeline_parallax,

        init: function () {

            var $_self = this;

            if ($_self.initialized) {
                return;
            }

            $_self.initialized = true;
            $_self.build();
            $_self.events();
        },

        build: function () {

            var $_self = this;

            /**
            * Create Neela Style elements
            */
            $_self.neelaStyle();

            /**
            * Preloader
            */
            $_self.preloader();

            /**
            * Navigation
            */
            $_self.navigation();

            /**
            * Dinamically create the menu for mobile devices
            */
            $_self.createMobileMenu();

            /**
            * Fit hero to screen size
            */
            $_self.heroHeight();

            /**
            * Activate placeholder in older browsers
            */
            $("input, textarea").placeholder();

            /**
            * Initialize Google Maps and populate with pin locations
            */
            $_self.googleMap();

            /**
            * Create Lightbox Gallery links
            */
            $_self.createLightboxGallery();

            /**
            * Create Hero Background Slideshow
            */
            $_self.createBackgroundSlideshow();

            /**
            * Create Owl Sliders
            */
            $_self.createOwlSliders();

            /**
            * Create Gallery
            */
            $_self.createGallery();

            /**
            * Create the Hero background image grid
            */
            $_self.bgImageGrid();

            /**
            * Create Countdown
            */
            $_self.countdown();

            /**
            * Initiate Parallax
            */
            $_self.parallaxBg();
        },

        events: function () {

            var $_self = this;
            var isLoadedVid;
            var isLoadedAnim;

            /**
            * Functions called on window resize
            */
            $_self.windowResize();

            /**
            * Resize embed videos
            */
            isLoadedVid = setInterval(function () {
                if (/loaded|complete/.test(document.readyState)) {
                    clearInterval(isLoadedVid);
                    $_self.resizeVideos();
                }
            }, 10);

            /**
            * Contact form submit
            */
            $_self.contactForm();

            /**
            * Capture object events
            */
            $_self.objEvents();

            /**
            * Timeline Parallax Effect
            */
            $_self.parallaxTimeline();

            /**
            * Animate elements on scrolling
            */
            isLoadedAnim = setInterval(function () {
                if (/loaded|complete/.test(document.readyState)) {
                    clearInterval(isLoadedAnim);
                    $_self.animateElems();
                }
            }, 10);
        },

        neelaStyle: function () {
            $(".neela-style, .btn.btn-primary, .btn.btn-light, .btn.btn-dark").prepend("<span class=\"h-lines\"></span><span class=\"v-lines\"></span>");
        },

        preloader: function () {
            var isLoaded = setInterval(function () {
                if (/loaded|complete/.test(document.readyState)) {
                    clearInterval(isLoaded);
                    $("#preloader").fadeOut(1000);
                }
            }, 10);
        },

        navigation: function () {

            var $_self = this;
            var sticky;

            $(".nav li a").on("click", function (event) {
                var navActive = $(this);
                var scroll = 0;

                if ($.browser.mobile && (!navActive.closest(".dropdown").hasClass("open") || navActive.closest(".dropdown-menu").css("display") === "block" || !navActive.parent().parent().hasClass("nav"))) {
                    event.preventDefault();
                    return false;
                }

                if (navActive.attr("href").charAt(0) === "#" && $(navActive.attr("href")).length) {
                    event.preventDefault();

                    if (navActive.attr("href") !== "#hero" && $(navActive.attr("href")).offset() !== null) {
                        scroll = $(navActive.attr("href")).offset().top - 55;
                    }

                    $("html, body").stop().animate({
                        scrollTop: scroll
                    }, 1500, "easeOutExpo", function () {
                        navActive.blur();
                    });
                } else {
                    window.open(navActive.attr("href"), "_self");
                }
            });

            if (window.Waypoint !== undefined) {
                sticky = new Waypoint.Sticky({
                    element: $(".nav-section"),
                    offset: -1
                });

                sticky = sticky;
            }

            if ($(".nav-section.light").length) {
                $(window).on("scroll load", function () {
                    var s = $(window).scrollTop();

                    if (s > 180) {
                        $(".nav-section.light").addClass("sticky");
                    } else {
                        $(".nav-section.light").removeClass("sticky");
                    }
                });
            }

            if ($("#wrapper > section, #wrapper > div#hero").length !== 0 && $_self.onepageNav && jQuery().waypoint) {
                $("#wrapper > section, #wrapper > div#hero").waypoint({
                    element: $("#wrapper > section"),
                    handler: function (direction) {
                        var $elem = $(this);
                        var id = $elem[0].element.id;

                        if (direction === "up") {
                            id = $elem[0].element.previousElementSibling.id;
                        }

                        $(".nav a").removeClass("active");

                        if ($(window).scrollTop() < 100) {
                            $(".nav a[href=\"#hero\"]").addClass("active");
                        } else {
                            $(".nav a[href=\"#" + id + "\"]").addClass("active");
                        }
                    },
                    offset: "50%"
                });
            }

            $(window).on("load", function () {
                var hash = location.hash.replace("#", "");

                if (hash !== "") {
                    location.hash = "";
                    $("html, body").stop().animate({
                        scrollTop: $("#" + hash).offset().top - 65
                    }, 1500, "easeInOutExpo");
                }

                if (window.Waypoint !== undefined) {
                    sticky = new Waypoint.Sticky({
                        element: $(".nav-section")
                    });
                }
            });
        },

        createMobileMenu: function (w) {

            var $_self = this;
            var $wrapper = $("#wrapper");
            var $navMobile;
            var etype;

            if ($.browser.mobile) {
                etype = "touchstart";
            } else {
                etype = "click";
            }

            if (w !== null) {
                w = $(window).innerWidth();
            }

            if (w <= 975 && !$_self.mobMenuFlag) {

                $("body").prepend("<nav class=\"nav-mobile\"><i class=\"fa fa-times\"></i><h2>" + $_self.mobileMenuTitle + "</h2><ul></ul></nav>");

                $(".nav-mobile > ul").html($(".nav").html());

                $(".nav-mobile b, .nav-mobile .nav-logo").remove();

                $(".nav-mobile ul.dropdown-menu").removeClass().addClass("dropdown-mobile");

                if ($(".navbar > a.btn").length) {
                    $(".navbar > a.btn").each(function () {
                        $(".nav-mobile").append($(this).clone());
                    });

                    $(".nav-mobile > a.btn").removeClass("btn-light").addClass("btn-primary btn-sm");
                }

                $navMobile = $(".nav-mobile");

                $("#nav-mobile-btn").on(etype, function (e) {
                    e.stopPropagation();
                    e.preventDefault();

                    setTimeout(function () {
                        $wrapper.addClass("open");
                        $navMobile.addClass("open");
                    }, 25);

                    $(document).on(etype, function (e) {
                        if (!$(e.target).hasClass("nav-mobile") && !$(e.target).parents(".nav-mobile").length) {
                            $wrapper.removeClass("open");
                            $navMobile.removeClass("open");
                            $(document).off(etype);
                        }
                    });

                    $(">i", $navMobile).on(etype, function () {
                        $wrapper.removeClass("open");
                        $navMobile.removeClass("open");
                        $(document).off(etype);
                    });
                });

                $_self.mobMenuFlag = true;

                $(".nav-mobile li a").on("click", function (event) {
                    var navActive = $(this);
                    var scroll = 0;

                    if (navActive.attr("href") !== "#hero") {
                        scroll = $(navActive.attr("href")).offset().top - 65;
                    }

                    $("html, body").stop().animate({
                        scrollTop: scroll
                    }, 1500, "easeInOutExpo", function () {
                        navActive.blur();
                    });

                    $wrapper.removeClass("open");
                    $navMobile.removeClass("open");
                    $(document).off(etype);

                    event.preventDefault();
                });
            }
        },

        heroHeight: function () {

            var $_self = this;

            if ($_self.heroFullScreen) {
                $("#hero").css({minHeight: $(window).innerHeight() + "px"});

                $(window).resize(function () {
                    var padding = parseInt($("#hero").css("padding-bottom")) + 70;
                    var nextMargin = parseInt($("#hero").next("section").css("margin-top"));
                    var wHeight = $(window).innerHeight() - padding;
                    var cHeight = $("#hero >.container").height();
                    var dif;
                    var xtraMT = -10;

                    if (nextMargin < 0 && !Number.isNaN(nextMargin)) {
                        wHeight += nextMargin + padding;
                    }

                    dif = wHeight - cHeight;

                    if ($(".nav-section.light").length) {
                        xtraMT = 10;
                    }

                    if (dif > 0 && $(".v-center").length) {
                        $("#hero >.container").css({"margin-top": (((dif) / 2) + xtraMT) + "px"});
                    }

                    $("#hero").css({minHeight: $(window).innerHeight() + "px"});
                });
            }
        },

        bgImageGrid: function () {

            if ($("#freewall").length) {
                $("#freewall .item").each(function () {
                    var $item = $(this);
                    $item.width(Math.floor(260 + 200 * Math.random()));
                    $item.css({"background-image": "url(" + $(">img", $item).attr("src") + ")"});
                    $(">img", $item).remove();
                });

                $("#freewall").appendTo("#wrapper");

                $(document).ready(function () {
                    var wall = new Freewall("#freewall");
                    wall.reset({
                        selector: ".item",
                        animate: false,
                        cellW: 20,
                        cellH: 320,
                        gutterX: 1,
                        gutterY: 1,
                        onResize: function () {
                            wall.fitWidth();
                        }
                    });
                    wall.fitWidth();
                });
            }
        },

        googleMap: function () {

            var $_self = this;
            var styles = [];
            var styledMap;
            var myLatlng;
            var mapOptions;
            var gmap;
            var mapCanvasId;
            var createMarker;
            var i = 0;
            var zoomCtrlPos;
            var stViewCtrlPos;
            var fullscreenCtrlPos;

            if ($(".gmap").length === 0 || $_self.mapMarkers === "undefined" || $_self.mapMarkers.length === 0 || window.google === undefined) {
                return false;
            }

            if (!(/^\d|\.|-$/.test($_self.mapInitialLatitude)) || !(/^\d|\.|-$/.test($_self.mapInitialLongitude))) {
                $_self.mapInitialLatitude = $_self.mapMarkers[0].latitude;
                $_self.mapInitialLongitude = $_self.mapMarkers[0].longitude;
            }

            myLatlng = new google.maps.LatLng($_self.mapInitialLatitude, $_self.mapInitialLongitude);

            if (!$_self.useDefaultMapStyle) {
                styles = [
                    {
                        stylers: [
                            {hue: $_self.mapColor},
                            {saturation: -75},
                            {lightness: 5}
                        ]
                    },
                    {
                        featureType: "administrative",
                        elementType: "labels.text.fill",
                        stylers: [
                            {saturation: 20},
                            {lightness: -70}
                        ]
                    },
                    {
                        featureType: "water",
                        elementType: "geometry",
                        stylers: [
                            {saturation: -50},
                            {lightness: 40}
                        ]
                    },
                    {
                        featureType: "road",
                        elementType: "geometry",
                        stylers: [
                            {hue: $_self.mapColor},
                            {saturation: -100},
                            {lightness: 0}
                        ]
                    },
                    {
                        featureType: "road.highway",
                        elementType: "geometry",
                        stylers: [
                            {hue: $_self.mapColor},
                            {saturation: 5},
                            {lightness: 5}
                        ]
                    },
                    {
                        featureType: "road",
                        elementType: "geometry.stroke",
                        stylers: [
                            {saturation: 10},
                            {lightness: 0}
                        ]
                    },
                    {
                        featureType: "road.highway",
                        elementType: "geometry.stroke",
                        stylers: [
                            {saturation: 0},
                            {lightness: 20}
                        ]
                    },
                    {
                        featureType: "transit",
                        elementType: "geometry",
                        stylers: [
                            {hue: $_self.mapColor},
                            {saturation: 30},
                            {lightness: -30}
                        ]
                    }
                ];
            }

            styledMap = new google.maps.StyledMapType(styles, {name: "Neela"});

            zoomCtrlPos = google.maps.ControlPosition.RIGHT_CENTER;
            stViewCtrlPos = google.maps.ControlPosition.RIGHT_BOTTOM;
            fullscreenCtrlPos = google.maps.ControlPosition.RIGHT_TOP;

            if ($_self.rtlFlag) {
                zoomCtrlPos = google.maps.ControlPosition.LEFT_CENTER;
                stViewCtrlPos = google.maps.ControlPosition.LEFT_BOTTOM;
                fullscreenCtrlPos = google.maps.ControlPosition.LEFT_TOP;
            }

            mapOptions = {
                center: myLatlng,
                zoom: $_self.mapInitialZoom,
                scrollwheel: false,
                panControl: false,
                mapTypeControl: false,
                zoomControl: true,
                zoomControlOptions: {
                    position: zoomCtrlPos
                },
                streetViewControlOptions: {
                    position: stViewCtrlPos
                },
                fullscreenControlOptions: {
                    position: fullscreenCtrlPos
                }
            };

            $(".gmap").each(function () {
                mapCanvasId = $(this).attr("id");

                gmap = new google.maps.Map(document.getElementById(mapCanvasId), mapOptions);
                gmap.mapTypes.set("map_style", styledMap);
                gmap.setMapTypeId("map_style");

                createMarker = function (obj) {
                    var lat = obj.latitude;
                    var lng = obj.longitude;
                    var icon = obj.icon;
                    var info = obj.infoWindow;

                    var infowindow = new google.maps.InfoWindow({
                        content: "<div class=\"infoWindow\">" + info + "</div>"
                    });

                    var marker = new RichMarker({
                        position: new google.maps.LatLng(lat, lng),
                        map: gmap,
                        anchor: 8,
                        anchorPoint: new google.maps.Point(0, -50),
                        shadow: "none",
                        content: "<div class=\"marker\"><i class=\"fa " + icon + "\"></i></div>"
                    });

                    google.maps.event.addListener(marker, "click", function () {
                        infowindow.open(gmap, marker);
                    });
                };

                while (i < $_self.mapMarkers.length) {
                    createMarker($_self.mapMarkers[i]);
                    i += 1;
                }
            });
        },

        createBackgroundSlideshow: function () {
            var $_self = this;

            if ($(".bg-slideshow").length && $_self.slidehowImages.length) {
                $(".bg-slideshow").zoomSlider({
                    src: $_self.slidehowImages,
                    bullets: false,
                    speed: 10000,
                    switchSpeed: 1000,
                    interval: 6000
                });
            }
        },

        createLightboxGallery: function () {

            if (window.lightbox !== undefined) {
                lightbox.option({
                    "resizeDuration": 200,
                    "wrapAround": true,
                    "disableScrolling": true,
                    "showImageNumberLabel": false,
                    "positionFromBottom": 150
                });
            }
        },

        createOwlSliders: function () {
            var $_self = this;

            if ($(".timeline-gallery").length) {
                $(".timeline-gallery").owlCarousel({
                    nav: true,
                    dots: false,
                    responsive: {
                        "0": {
                            items: 1
                        }
                    },
                    rtl: $_self.rtlFlag
                });
            }

            if ($(".testimonials").length) {
                $(".testimonials").owlCarousel({
                    nav: false,
                    dots: true,
                    responsive: {
                        "0": {
                            items: 1
                        }
                    },
                    rtl: $_self.rtlFlag
                });
            }
        },

        createGallery: function () {

            var $gallery = $(".gallery-scroller");
            var scrolling = false;

            if ($(".gallery-scroller").length) {

                $(".gallery-right").on("click", function () {
                    if (scrolling) {
                        return false;
                    }

                    scrolling = true;
                    $gallery.animate({scrollLeft: $gallery.scrollLeft() + 380}, function () {
                        scrolling = false;
                    });
                });

                $(".gallery-left").on("click", function () {
                    if (scrolling) {
                        return false;
                    }

                    scrolling = true;
                    $gallery.animate({scrollLeft: $gallery.scrollLeft() - 380}, function () {
                        scrolling = false;
                    });
                });

                $(document).ready(function () {
                    $(".gallery-scroller").niceScroll({
                        cursorcolor: "#fff",
                        cursorwidth: "0px",
                        background: "#fff",
                        cursorborder: "0px solid #1F2326",
                        zindex: "999",
                        autohidemode: false,
                        enablemousewheel: false,
                        touchbehavior: true
                    });
                });
            }
        },

        countdown: function () {
            var $_self = this;
            var changeTime;

            if ($(".countdown").length) {

                changeTime = function (counter, $parent, future) {
                    var today = new Date();
                    var _dd = future - today;
                    var _dday;
                    var _dhour;
                    var _dmin;
                    var _dsec;

                    if (_dd < 0) {
                        $parent.html("<div class=\"end\">" + $_self.countdownEndMsg + "</div>");
                        clearInterval(counter);

                        return false;
                    }

                    _dday = Math.floor(_dd / (60 * 60 * 1000 * 24) * 1);
                    _dhour = Math.floor((_dd % (60 * 60 * 1000 * 24)) / (60 * 60 * 1000) * 1);
                    _dmin = Math.floor(((_dd % (60 * 60 * 1000 * 24)) % (60 * 60 * 1000)) / (60 * 1000) * 1);
                    _dsec = Math.floor((((_dd % (60 * 60 * 1000 * 24)) % (60 * 60 * 1000)) % (60 * 1000)) / 1000 * 1);

                    $(".days > div", $parent).html(_dday);
                    $(".hours > div", $parent).html(_dhour);
                    $(".minutes > div", $parent).html(_dmin);
                    $(".seconds > div", $parent).html(_dsec);
                };

                $(".countdown").each(function () {
                    var counter;
                    var $parent = $(this);
                    var future = new Date($parent.data("date"));
                    var html = "";

                    if (future && Object.prototype.toString.call(future) === "[object Date]" && !Number.isNaN(future)) {
                        html = "<div class=\"days\"><div></div><span>" + $_self.cDays + "</span></div>" +
                        "<div class=\"hours\"><div></div><span>" + $_self.cHours + "</span></div>" +
                        "<div class=\"minutes\"><div></div><span>" + $_self.cMinutes + "</span></div>" +
                        "<div class=\"seconds\"><div></div><span>" + $_self.cSeconds + "</span></div>";

                        $parent.html(html);
                    }

                    counter = setInterval(function () {
                        changeTime(counter, $parent, future);
                    }, 1000);
                });
            }
        },

        parallaxBg: function () {

            var $_self = this;

            if (!$.browser.mobile && $(window).innerWidth() > 992) {

                $(window).on("scroll", function () {
                    var scrolled = $(window).scrollTop();

                    $(".parallax-background").each(function () {
                        var $elem = $(this);
                        var initY = $elem.offset().top;
                        var height = $elem.outerHeight();
                        var visible = $_self.isInViewport(this);
                        var diff;
                        var ratio;

                        if (visible) {
                            diff = scrolled - initY;
                            ratio = Math.round((diff / height) * 100);
                            $elem.css("background-position", "center " + parseInt(-(ratio * (height / 250))) + "px");
                        }
                    });
                });

            } else {
                $(".parallax-background").css({"background-position": "50% 50%", "background-size": "cover", "background-attachment": "scroll"});
            }
        },

        isInViewport: function (elem) {
            var obj = elem.getBoundingClientRect();

            return (
                (obj.height > 0 || obj.width > 0) &&
                obj.bottom >= 0 &&
                obj.right >= 0 &&
                obj.top <= (window.innerHeight || document.documentElement.clientHeight) &&
                obj.left <= (window.innerWidth || document.documentElement.clientWidth)
            );
        },

        windowResize: function () {

            var $_self = this;

            $(window).resize(function () {
                var w = $(window).innerWidth();

                $_self.createMobileMenu(w);

                $_self.blogMetas();

                if ($(window).innerWidth() < 751) {
                    $(".navbar > a.btn").addClass("btn-sm");
                    $(".navbar > a.btn").width("auto");
                }
            });
        },

        resizeVideos: function () {

            var $allVideos = $("iframe[src^=\"http://player.vimeo.com\"], iframe[src^=\"https://player.vimeo.com\"], iframe[src^=\"http://www.youtube.com\"], iframe[src^=\"https://www.youtube.com\"], object, embed");

            $allVideos.each(function () {
                var $el = $(this);
                var aspRatio = $el.attr("height") / $el.attr("width");

                if (aspRatio < 0.3 || aspRatio > 0.8 || Number.isNaN(aspRatio)) {
                    aspRatio = 0.559;
                }

                $el.attr("data-aspectRatio", aspRatio).removeAttr("height").removeAttr("width");
            });

            $(window).resize(function () {

                $allVideos.each(function () {
                    var $el = $(this);
                    var newWidth = $el.parent().width();

                    $el.width(newWidth).height(newWidth * $el.attr("data-aspectRatio"));
                });

            }).resize();
        },

        contactForm: function () {

            var $_self = this;

            $(".submit_form").on("click", function (e) {

                var $submit_btn = $(this);
                var $form = $submit_btn.closest("form");
                var $fields = $("input, textarea, select, fieldset", $form);
                var len = 0;
                var re = /\S+@\S+\.\S+/;
                var html = "contact";
                var error = false;
                var showSuccess;
                var stopSpin;
                var encodeHtml;

                e.preventDefault();

                encodeHtml = function (str) {
                    return encodeURIComponent(str);
                };

                $submit_btn.width("auto");

                $(".form_status_message").html("");

                $fields.each(function () {
                    var $field = $(this);

                    if ($field.attr("type") === "hidden") {
                        if ($field.hasClass("subject")) {
                            html += "&subject=" + encodeHtml($field.val());
                        } else if ($field.hasClass("fromName") || $field.hasClass("fromname")) {
                            html += "&fromname=" + encodeHtml($field.val());
                        } else if ($field.hasClass("fromEmail") || $field.hasClass("fromemail")) {
                            html += "&fromemail=" + encodeHtml($field.val());
                        } else if ($field.hasClass("emailTo") || $field.hasClass("emailto")) {
                            html += "&emailto=" + encodeHtml($field.val());
                        }
                    } else {
                        if ($field.attr("type") === "checkbox" && $field.parents("fieldset").length === 1 && $field.parents("fieldset").hasClass("required")) {
                            return;
                        }

                        if ($field.is("fieldset") && $field.hasClass("required") && $("#" + $field.attr("id") + " input:checkbox:checked").length === 0) {
                            $("input", $field).addClass("is-invalid");
                            error = true;
                        } else if ($field.hasClass("required") && $field.attr("type") === "checkbox" && !$("input[id='" + $field.attr("id") + "']").is(":checked")) {
                            $field.addClass("is-invalid");
                            error = true;
                        } else if ($field.hasClass("required") && $field.val() === "" && $field.attr("type") !== "checkbox" && !$field.is("fieldset")) {
                            $field.addClass("is-invalid");
                            error = true;
                        } else if ($field.hasClass("required") && $field.attr("type") === "radio" && !$("input[name='" + $field.attr("name") + "']").is(":checked")) {
                            $field.addClass("is-invalid");
                            error = true;
                        } else if ($field.attr("type") === "email" && $field.val() !== "" && re.test($field.val()) === false) {
                            $field.addClass("is-invalid");
                            error = true;
                        } else if ($field.attr("id") !== "g-recaptcha-response" && $field.attr("id") !== "recaptcha-token") {
                            $field.removeClass("is-invalid");
                            $("input", $field).removeClass("is-invalid");

                            if ($field.hasClass("subject")) {
                                html += "&subject=" + encodeHtml($field.val());
                                html += "&subject_label=" + encodeHtml($field.attr("name"));
                            } else if ($field.hasClass("fromName") || $field.hasClass("fromname")) {
                                html += "&fromname=" + encodeHtml($field.val());
                                html += "&fromname_label=" + encodeHtml($field.attr("name"));
                            } else if ($field.hasClass("fromEmail") || $field.hasClass("fromemail")) {
                                html += "&fromemail=" + encodeHtml($field.val());
                                html += "&fromemail_label=" + encodeHtml($field.attr("name"));
                            } else {
                                if ($field.attr("type") === "radio") {
                                    if ($("input[id='" + $field.attr("id") + "']").is(":checked")) {
                                        html += "&field" + len + "_label=" + encodeHtml($field.attr("name"));
                                        html += "&field" + len + "_value=" + encodeHtml($.trim($("label[for='" + $field.attr("id") + "']").text()));
                                    }
                                } else if ($field.is("fieldset")) {
                                    html += "&field" + len + "_label=" + encodeHtml($field.attr("name"));

                                    $("#" + $field.attr("id") + " input:checkbox:checked").each(function (index) {
                                        if (index === 0) {
                                            html += "&field" + len + "_value=";
                                            html += encodeHtml($.trim($("label[for='" + $(this).attr("id") + "']").text()));
                                        } else {
                                            html += ", " + encodeHtml($.trim($("label[for='" + $(this).attr("id") + "']").text()));
                                        }
                                    });
                                } else {
                                    html += "&field" + len + "_label=" + encodeHtml($field.attr("name"));
                                    html += "&field" + len + "_value=" + encodeHtml($field.val());
                                }

                                len += 1;
                            }
                        }
                    }
                });

                html += "&len=" + len;

                if ($(".g-recaptcha").length) {
                    html += "&recaptcha=" + grecaptcha.getResponse();
                }

                showSuccess = function () {
                    $(".form_status_message").html("<div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\">" + $_self.contactFormSuccessMsg + "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button></div>");
                };

                stopSpin = function () {
                    $(".fa-spinner", $submit_btn).remove();
                    $submit_btn.removeClass("disabled");
                };

                if (!error && !$_self.sendingMail) {
                    $_self.sendingMail = true;

                    $submit_btn.append("<i class=\"fas fa-spinner fa-spin after\"></i>");
                    $submit_btn.addClass("disabled");

                    $.ajax({
                        type: "POST",
                        url: "contact.php",
                        data: html,
                        success: function (msg) {
                            stopSpin();

                            if (msg === "ok") {
                                showSuccess();
                                $form[0].reset();
                            } else {
                                $_self.showError($_self.contactFormRecaptchaErrorMsg);
                            }

                            $_self.sendingMail = false;

                            if ($(".g-recaptcha").length) {
                                grecaptcha.reset();
                            }
                        },
                        error: function () {
                            stopSpin();

                            $_self.showError();
                            $_self.sendingMail = false;
                        }
                    });

                } else {
                    $_self.showError();
                }

                return false;
            });
        },

        showError: function (err = "") {
            var $_self = this;

            if (err === "") {
                err = $_self.contactFormErrorMsg;
            }

            $(".form_status_message").html("<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">" + err + "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button></div>");
        },

        objEvents: function () {

            //Fix buttons width (round to integer)
            $(".btn").each(function () {
                var $btn = $(this);
                var w = $btn.width();
                var xtr = 2;
                var len = $btn.text().split(" ").length;

                if (len > 2 || $btn.find("i").length !== 0) {
                    xtr = 15;
                } else if (len > 1) {
                    xtr = 8;
                }

                $btn.width(Math.round(w) + xtr);
            });

            //Rotate heart divider on mouseenter about element
            $("#about-us .element .image").on("mouseenter", function () {
                var $elem = $(this);

                if ($elem.parent().is(":first-child") && !$(">.divider-about-us", $elem.closest(".row")).hasClass("flip")) {
                    return false;
                }

                if (!$elem.hasClass("flip")) {
                    if ($("#about-us .element .image.flip").length) {
                        $("#about-us .element .image").toggleClass("flip");
                    } else {
                        $elem.toggleClass("flip");
                    }

                    $(">.divider-about-us", $elem.closest(".row")).toggleClass("flip");
                }
            });

            //Slide map info on mouseenter map_canvas
            $("#map_canvas").on("mouseenter", function () {
                $(".location-info").addClass("open");
            }).on("mouseleave", function () {
                $(".location-info").removeClass("open");
            });

            // Scroll effect of navigation logo and .scrollto buttons
            $(".nav-logo, .scrollto").on("click", function (event) {
                var $navActive = $(this);
                var scroll = 0;
                var href = $navActive.attr("href");

                if (/#/.test(href) && $(href).length) {

                    event.preventDefault();

                    if (href !== "#hero") {
                        scroll = $(href).offset().top - 65;
                    }

                    $("html, body").stop().animate({
                        scrollTop: scroll
                    }, 1500, "easeInOutExpo", function () {
                        $navActive.blur();
                    });
                }
            });

            //Element V2 image handler
            if ($(".element-v2").length) {
                $(".element-v2").each(function () {
                    var $elem = $(">.image", $(this));
                    $elem.css({"background-image": "url(" + $(">img", $elem).attr("src") + ")"});
                    $(">img", $elem).hide();
                });
            }

            if ($(".overflow-image").length) {
                $(".overflow-image").each(function () {
                    var $elem = $(this);
                    $elem.css({"background-image": "url(" + $(">img", $elem).attr("src") + ")"});
                });
            }

            if ($(".progress").length) {
                $(".progress").waypoint(function () {
                    $(".progress").each(function () {
                        $("> .progress-bar", $(this)).delay(300).queue(function (next) {
                            var $elem = $(this);
                            $elem.css("width", $elem.attr("aria-valuenow") + "%");
                            next();
                        });
                    });
                }, {
                    triggerOnce: true,
                    offset: "bottom-in-view"
                });
            }
        },

        parallaxTimeline: function () {

            var $_self = this;
            var tp;

            if ($_self.timelineParallax) {
                tp = function (e) {
                    $("> div", this).each(function () {

                        var $obj = $(this);
                        var val = $obj.attr("data-parallax");
                        var x = (e.clientX * val) / 300;
                        var y = (e.clientY * val) / 300;

                        $obj.css({
                            "-webkit-transform": "translateX(" + x + "px) translateY(" + y + "px)",
                            "-moz-transform": "translateX(" + x + "px) translateY(" + y + "px)",
                            "-ms-transform": "translateX(" + x + "px) translateY(" + y + "px)",
                            "-o-transform": "translateX(" + x + "px) translateY(" + y + "px)",
                            "transform": "translateX(" + x + "px) translateY(" + y + "px)"
                        });
                    });
                };

                if ($(window).innerWidth() > 992) {
                    $(window).scroll(function () {
                        var s = $(window).scrollTop();
                        var h = $(window).height();

                        $(".timeline [class^=\"template-\"]").each(function () {
                            var $obj = $(this);
                            var offset = $obj.offset().top;
                            var timelineH = $obj.height();

                            if ((s + h) >= offset && s <= (offset + timelineH)) {
                                $obj.on("mousemove", tp);
                            } else {
                                $obj.off("mousemove", tp);
                            }

                        });
                    });
                }
            }
        },

        blogMetas: function () {
            var $elems = $(".info-blog .bottom-info, .post-content .bottom-info");

            if ($elems.length) {
                $elems.each(function () {
                    var $obj = $(this);

                    if ($obj.height() > 35) {
                        $obj.addClass("center");
                    } else {
                        $obj.removeClass("center");
                    }
                });
            }
        },

        animateElems: function () {

            var animate = function () {
                $("[data-animation-delay]").each(function () {
                    var $this = $(this);
                    var s = $(window).scrollTop();
                    var h = $(window).height();
                    var d = parseInt($this.attr("data-animation-delay"), 10);
                    var dir = $this.data("animation-direction");

                    if (dir === undefined) {
                        return false;
                    }

                    $this.addClass("animate-" + dir);

                    $(document).ready(function () {
                        if ((s + h) >= $this.offset().top) {
                            if (Number.isNaN(d) || d === 0) {
                                $this.removeClass("animate-" + dir).addClass("animation-" + dir);
                            } else {
                                setTimeout(function () {
                                    $this.removeClass("animate-me").addClass("animation-" + dir);
                                }, d);
                            }
                        }
                    });
                });
            };

            if ($(window).innerWidth() >= 751) {
                $(window).scroll(function () {
                    animate();
                });

                animate();
            } else {
                $("[data-animation-delay]").addClass("visible");
            }
        }
    };

    Neela.init();

}(jQuery));
