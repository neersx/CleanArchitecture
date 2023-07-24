$(function() {
	"use strict";

	/*==============================================
	PRELOADER
	===============================================*/
	$(window).on('load', function() {
		$("#loading").fadeOut(500);
	});

	/*==============================================
	NAVIGATION
	===============================================*/
	$('a.page-scroll').on('click', function(e){
		var anchor = $(this);
		$('html, body').stop().animate({
			scrollTop: $(anchor.attr('href')).offset().top - 50
		}, 1500);
		e.preventDefault();
	});		

	$(window).on('scroll', function() {
		var menuTop = $('.menu-top');
		if ($(this).scrollTop() > 100) {
			menuTop.addClass('menu-shrink');
		} else {
			menuTop.removeClass('menu-shrink');
		}
	});			
	
	$(document).on('click','.navbar-collapse.in',function(e) {
		if( $(e.target).is('a') && $(e.target).attr('class') != 'dropdown-toggle' ) {
			$(this).collapse('hide');
		}
	});

	/*==============================================
	COUNTDOWN // set your wedding date here
	===============================================*/
	if ($("#countdown").length) {
		$("#countdown").countdown({
			until : new Date(2023, 12-1, 31)
			//http://keith-wood.name/countdown.html
		});
	}

	/*==============================================
	MIXITUP
	===============================================*/
	jQuery('.grid').mixitup({
			targetSelector: '.mix',
		});
	$('.image-popup').magnificPopup({
		type: 'image',
		closeOnContentClick: true,
		mainClass: 'mfp-img-mobile',
		image: {
			verticalFit: true
		}
	});

	/*==============================================
	INTRO VIDEO
	===============================================*/
	$('.control').on('click', function() {
		$(this).remove();
		var video = '<iframe src="' + $('.video img').attr('data-video') + '"></iframe>'
		$('.video img').after(video);
		return false;
	});

	/*==============================================
	COUNTER UP
	===============================================*/
	$('.counter').counterUp({
		delay: 2,
		time: 1000
	});

	/*==============================================
	WISHED SLIDER MOVE WITH MOUSEL WHEEL
	===============================================*/
	$('#wishedSlider').bind('mousewheel', function(e){
		$(this).carousel('next');
	});

	/*==============================================
	BACK TO TOP
	===============================================*/
	var backToTp = '#back-to-top';
    if ($(backToTp).length) {
	    var scrollTrigger = 100, // px
	        backToTop = function () {
	            var scrollTop = $(window).scrollTop();
	            if (scrollTop > scrollTrigger) {
	                $(backToTp).addClass('show');
	            } else {
	                $(backToTp).removeClass('show');
	            }
	        };
	    backToTop();
	    $(window).on('scroll', function () {
	        backToTop();
	    });
	    $(backToTp).on('click', function (e) {
	        e.preventDefault();
	        $('html,body').animate({
	            scrollTop: 0
	        }, 700);
	    });
	}

	/*==============================================
	BACKGROUND IMAGE PARALLAX
	===============================================*/
	$(window).on('scroll', function () {
	    $("#page-bg-image").css("background-position","50% " + ($(this).scrollTop() / 2) + "px");
	});
	
});