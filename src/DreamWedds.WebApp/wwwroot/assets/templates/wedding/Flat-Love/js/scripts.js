/*
 Name: 		FlatLove Html Template
 Author: 	http://themeforest.net/user/eriktailor
 Version: 	1.0.0
--------------------------------------------------------------------- */

$(document).ready(function(){

/* ---------------------------------------------------------------------

 # JS PLUGINS

--------------------------------------------------------------------- */

/* --------------------------------------------------
 ## Counterup init
-------------------------------------------------- */
if (jQuery().counterUp) {
	$('.counter-value').counterUp({
    	delay: 10,
    	time: 1500
	});
}

/* --------------------------------------------------
 ## Countdown Init
-------------------------------------------------- */
if (jQuery().countdown) {
	$('[data-countdown]').countdown();
}

/* --------------------------------------------------
 ## Wow init
-------------------------------------------------- */
new WOW().init();

/* --------------------------------------------------
 ## Owlcarousel init
-------------------------------------------------- */
if (jQuery().owlCarousel) {
	$('#photo-carousel').owlCarousel({
	    itemsCustom : [
	      [0   , 1],
	      [768 , 3],
	      [992 , 3],
	      [1200, 4],
	    ],
	    navigation  : false,
	    pagination  : false,
	    autoPlay    : 3000
	});
    $('#quote-carousel').owlCarousel({
		navigation : false,
		slideSpeed : 300,
		paginationSpeed : 400,
		singleItem: true
    });	
    $('#sponsor-carousel').owlCarousel({
		navigation : false,
		slideSpeed : 300,
		paginationSpeed : 400,
		autoPlay    : 2000
    });
}

/* --------------------------------------------------
 ## Parallax resize fix
-------------------------------------------------- */
if (jQuery().parallax) {
	$(window).trigger('resize').trigger('scroll');
}

/* ---------------------------------------------------------------------

 # THEME ELEMENTS

--------------------------------------------------------------------- */

/* --------------------------------------------------
 ## Preloader
-------------------------------------------------- */
function preLoader() {
	$(window).on('load', function() {
	    var $preloader = $('#preloader'),
	        $spinner   = $preloader.find('.spinner');
	    $spinner.fadeOut();
	    $preloader.delay(250).fadeOut(800);
	});
} 
preLoader();

/* --------------------------------------------------
 ## Scrolltop
-------------------------------------------------- */
function scrollTop() {
	var scrollTopBtn = $('#scrollTop');
	$(window).on('load resize scroll', function () {
		if ($(this).scrollTop() > 600) {
			scrollTopBtn.addClass('upscaled');
		} else {
			scrollTopBtn.removeClass('upscaled');
		}
	});	
	scrollTopBtn.click(function(){
		$('body,html').animate({ 
			scrollTop: 0 
		}, 800);
		$('.navbar-nav li').removeClass('active');
		return false;
	});
}
scrollTop();

/* --------------------------------------------------
 ## Navigation
-------------------------------------------------- */
function onepageNav() {
	var $body     = $(document.body);
	var navHeight = $('.navbar').outerHeight(true) + 10;
	$body.scrollspy({
		target: '#onepage-nav',
		offset: navHeight
	});
	$('.navbar a[href*=#]:not([href=#])').click(function() {
		$('#onepage-nav').removeClass('in');
		if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
			var target = $(this.hash);
			target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
		    if (target.length) {
				$('html,body').animate({
				scrollTop: target.offset().top - 50
				}, 1000);
				return false;
		    }
	  	}
	});
}
onepageNav();

/* --------------------------------------------------
 ## Form Validate
-------------------------------------------------- */
function formValidate() {
	var $form = $("#rsvpForm");
    $form.on("submit", function(e){
      if( $form[0].checkValidity() ) {
        $.post("php/rsvp.php", $form.serialize(), function(response) {
        	$('.submit-wrap').fadeOut(300);
          	$('#successMsg').removeClass('hidden').fadeIn(500);
          	e.preventDefault();
        });
      } else console.log("invalid");
      e.preventDefault();
    });
}
formValidate();

/* --------------------------------------------------
 ## Select Fix
-------------------------------------------------- */
function selectFix() {
	$(document.body).on('click', '.select-fix .dropdown-menu li', function(event) {
		var $target = $(event.currentTarget);
		$target.closest('.btn-group').find('[data-bind="label"]').text($target.text()).end().children('.dropdown-toggle').dropdown('toggle');
		return false;
	});
}
selectFix();
 
/* --------------------------------------------------
 ## Carousel Controls
-------------------------------------------------- */
function carouselControl() {
	var $prev = $('.carousel a[data-slide="prev"]'),
		$next = $('.carousel a[data-slide="next"]');		
	$prev.click(function() {
		$('.carousel').carousel('prev');
	});
	$next.click(function() {
		$('.carousel').carousel('next');
	});	
}
carouselControl();

/* --------------------------------------------------
 ## Tooltips
-------------------------------------------------- */
function toolTips() {
	$('[data-toggle="tooltip"]').tooltip();
}
toolTips();

/* --------------------------------------------------
 ## Tabs
-------------------------------------------------- */
$('.tabs-nav a').click(function (e) {
  e.preventDefault()
  $(this).tab('show')
})


});
