/***************************************************
==================== JS INDEX ======================
****************************************************
01. PreLoader Js

****************************************************/

(function ($) {
"use strict";

	var windowOn = $(window);
	////////////////////////////////////////////////////
    // 01. PreLoader Js
	$(window).on('load', function(){
	    $('#preloader').fadeOut('slow',function(){$(this).remove();});
	  });
	


	////////////////////////////////////////////////////
    // 02. Mobile Menu Js
	$('#mobile-menu').meanmenu({
		meanMenuContainer: '.mobile-menu',
		meanScreenWidth: "991",
		meanExpand: ['<i class="fal fa-plus"></i>'],
	});

	////////////////////////////////////////////////////
    // 03. Data Background Js
	$("[data-background").each(function () {
		$(this).css("background-image", "url( " + $(this).attr("data-background") + "  )");
	});
	$("[data-bg-color").each(function () {
		$(this).css("background-color",$(this).attr("data-bg-color"));
	});

  	////////////////////////////////////////////////////
    // 04. Nice Select Js
		$('.lv-contact-single-input-wrap-1-5 select, .lv-shop-list-dropdown-1-6 select').niceSelect();

	////////////////////////////////////////////////////
    // 05. slider__active Slider Js
        /* ===============================  Swiper slider  =============================== */
	    
     if (jQuery(".lv-slider-active").length > 0) {
				let sliderActive1 = '.lv-slider-active';
				let sliderInit1 = new Swiper(sliderActive1, {
					// Optional parameters
					slidesPerView: 1,
					slidesPerColumn: 1,
					parallex: false,
					speed: 1000,
					freeMode: false,
					grabCursor: true,
					parallax: true,
					paginationClickable: true,
					loop: false,

					autoplay: {
						delay: 5000,
					},
					pagination: {
          el: ".slider-paginations",
          clickable: true,
          renderBullet: function (index, className) {
            return '<span class="' + className + '">0' + (index + 1) + "</span>";
          }},
					// Navigation arrows
					navigation: {
						nextEl: '.swiper-button-next',
						prevEl: '.swiper-button-prev',
					},

					a11y: false
				});

				function animated_swiper(selector, init) {
					let animated = function animated() {
						$(selector + ' [data-animation]').each(function () {
							let anim = $(this).data('animation');
							let delay = $(this).data('delay');
							let duration = $(this).data('duration');

							$(this).removeClass('anim' + anim)
								.addClass(anim + ' animated')
								.css({
									webkitAnimationDelay: delay,
									animationDelay: delay,
									webkitAnimationDuration: duration,
									animationDuration: duration
								})
								.one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
									$(this).removeClass(anim + ' animated');
								});
						});
					};
					animated();
					// Make animated when slide change
					init.on('slideChange', function () {
						$(sliderActive1 + ' [data-animation]').removeClass('animated');
					});
					init.on('slideChange', animated);
				}

				animated_swiper(sliderActive1, sliderInit1);
			}
	////////////////////////////////////////////////////
    // 06. Masonary Js
	$('.gallery-grid').imagesLoaded( function() {
		// init Isotope
		var $grid = $('.gallery-grid').isotope({
		  itemSelector: '.grid-item',
		  percentPosition: true,
		  masonry: {
			// use outer width of grid-sizer for columnWidth
			columnWidth: '.grid-item',
		  }
		});
	});

	/* magnificPopup img view */
	$('.image-popups').magnificPopup({
		type: 'image',
		gallery: {
			enabled: true
		}
	  });

	/* magnificPopup video view */
	$(".popup-video").magnificPopup({
		type: "iframe",
	});

	////////////////////////////////////////////////////
    // 07. Wow Js
	new WOW().init();

	////////////////////////////////////////////////////
	// 08. Odometer Js
	jQuery('.odometer').appear(function (e) {
		var odo = jQuery(".odometer");
		odo.each(function () {
			var countNumber = jQuery(this).attr("data-count");
			jQuery(this).html(countNumber);
		});
	});
	
	////////////////////////////////////////////////////
    // 05. Testimonial Slider Js
   if (jQuery(".testimonial-active").length > 0) {
			let testimonialActive = '.testimonial-active';
			let testimonialInit = new Swiper(testimonialActive, {
				// Optional parameters
				slidesPerView: 1,
				slidesPerColumn: 1,
				paginationClickable: true,
				loop: false,

				autoplay: {
					delay: 5000,
				},
				pagination: {
        el: ".testimonial-paginations-2",
        clickable: true,
        renderBullet: function (index, className) {
          return '<span class="' + className + '">0' + (index + 1) + "</span>";
        }},
				// Navigation arrows
				navigation: {
					nextEl: '.swiper-button-next-2',
					prevEl: '.swiper-button-prev-2',
				},

				a11y: false
			});
		}
		////////////////////////////////////////////////////
    // 05. Testimonial Slider Js
   if (jQuery(".lv-photography-member-social-active").length > 0) {
			let photographyMemberActive = '.lv-photography-member-social-active';
			let photographyMemberInit = new Swiper(photographyMemberActive, {
				// Optional parameters
				slidesPerView: 1,
				slidesPerColumn: 1,
				paginationClickable: true,
				loop: false,

				autoplay: {
					delay: 5000,
				},
				pagination: {
        el: ".testimonial-paginations-photography",
        clickable: true,
        renderBullet: function (index, className) {
          return '<span class="' + className + '">0' + (index + 1) + "</span>";
        }},
				// Navigation arrows
				navigation: {
					nextEl: '.swiper-button-next-photography',
					prevEl: '.swiper-button-prev-photography',
				},

				a11y: false
			});
		}
		////////////////////////////////////////////////////////
		// 06. Sidebar
		$('.lv-header-bar-1 a, .lv-header-bar-2 a').on('click', function() {
			$('.overlay').addClass('visible-overlay');
			$('.lv-header-sidebar-area').addClass('header-sidebar-visible');
		});
		$('.lv-header-sidebar-action a, .overlay').on('click', function() {
			$('.overlay').removeClass('visible-overlay');
			$('.lv-header-sidebar-area').removeClass('header-sidebar-visible');
		});
		var dateActive = new Swiper(".lv-message-date-box-2-active", {
      spaceBetween: 0,
      slidesPerView: 1,
      effect: 'fade',
       fadeEffect: {
		    crossFade: true
		  },
      freeMode: true,
      watchSlidesProgress: true,
    });
    var MessageContentBox = new Swiper(".lv-message-content-box-2-active", {
      spaceBetween: 0,
      navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
      },
      thumbs: {
        swiper: dateActive,
      },
    });
    // overlay animation
        var fast = 200;
    $(".lv-featured-gallery-item-single-2").hover(function(e) {
        var liPos = $(this).offset();
        var bord = comingMouse(e.pageX - liPos.left, e.pageY - liPos.top, $(this).width(), $(this).height());
        var overlay =  $(this).find($(".lv-featured-gallery-content-2-wrap"));
        switch (bord) {
            case "left":
              overlay.css({"top":"20px","left":"-100%"});
              overlay.stop().animate({"left":"20px"},fast,"linear");
              break;
            case "right":
              overlay.css({"top":"20px","left":"100%"});
              overlay.stop().animate({"left":"20px"},fast,"linear");          
              break;
            case "top":
              overlay.css({"top":"-100%","left":"20px"});
              overlay.stop().animate({"top":"20px"},fast,"linear");
              break;
            case "bottom":
              overlay.css({"top":"100%","left":"20px"});
              overlay.stop().animate({"top":"20px"},fast,"linear");
              break;
        }
    }, function(e) {
        var liPos = $(this).offset();
        var bord = comingMouse(e.pageX - liPos.left, e.pageY - liPos.top, $(this).width(), $(this).height());
        var overlay =  $(this).find($(".lv-featured-gallery-content-2-wrap"));
      switch (bord) {
            case "left":
              overlay.stop().animate({"left":"-100%"},fast);
              break;
            case "right":
              overlay.stop().animate({"left":"100%"},fast);         
              break;
            case "top":
              overlay.stop().animate({"top":"-100%"},fast);
              break;
            case "bottom":
              overlay.stop().animate({"top":"100%"},fast);
              break;
        }
    });
		function comingMouse(hor,vert,larg,haut) {
      var top = Math.abs(vert),
          bottom = Math.abs(vert-haut),
          left = Math.abs(hor),
          right = Math.abs(hor-larg);

      var min = Math.min(top,bottom,left,right);
      switch (min) {
          case left:
              return "left";
          case right:
              return "right";
          case top:
              return "top";
          case bottom:
              return "bottom";
      }
		}
		//range slider activation
	  $("#lv-slider-range").slider({
	    range: true,
	    min: 0,
	    max: 500,
	    values: [75, 300],
	    slide: function (event, ui) {
	      $("#lv-amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
	    },
	  });
	  // Parallax Stuff
	if ($(".stuff").length) {
        var stuff = $('.stuff').get(0);
        var parallaxInstance = new Parallax(stuff);
    }
    if ($(".stuff2").length) {
        var stuff = $('.stuff2').get(0);
        var parallaxInstance = new Parallax(stuff);
    }
    if ($(".stuff3").length) {
        var stuff = $('.stuff3').get(0);
        var parallaxInstance = new Parallax(stuff);
    }
    if ($(".stuff4").length) {
        var stuff = $('.stuff4').get(0);
        var parallaxInstance = new Parallax(stuff);
    }

    $("a.clickup").on('click', function(event) {
        if (this.hash !== "") {
            event.preventDefault();
            var hash = this.hash;
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 500, function() {
                window.location.hash = hash;
            });
        }
    });
	    

})(jQuery);
