//Use Strict Mode
(function($) {
  "use strict";

  //Remove loading-wrapper class before window load
  var loadingWrapper = $('.loading-wrapper');
  loadingWrapper.css({'visibility': 'visible'}).animate({opacity: '1'}, 600);

  //Begin - Window Load
  $(window).on('load', function(){

    //loader and Intro Animations
    var pageLoader = $('#page-loader');
    pageLoader.delay(1000).fadeOut(400, function(){});    

    //Page Loader 
    var introItem1 = $('#intro-item1');
    setTimeout(function(){
      introItem1.addClass('active');
      return false;    
    }, 1100);  

    //back to top
    var toTop =$('html, body');
    function backToTop() {
      toTop.animate({
        scrollTop: 0
      }, 800);
    }

    //Isotope
    var $isotopeContainer = $('#isotope-filter'),
    $isotopeOptionContainer = $('#options'),
    $options = $isotopeOptionContainer.find('a[href^="#"]').not('a[href="#"]'),
    isOptionLinkClicked = false;

    $isotopeContainer.imagesLoaded( function() {
      $isotopeContainer.isotope({
        itemSelector : '.element',
        resizable: false,
        //filter: '*',
        transitionDuration: '0.8s',
        layoutMode: 'packery',
        packery: {
          
        }
      });
    });

    function isotopeGO() {
        var isotopeItem = $(this),
        href = isotopeItem.attr('href');
          
        if ( isotopeItem.hasClass('selected') ) {
          return;
        } else {
          $options.removeClass('selected');
          isotopeItem.addClass('selected');
        }

        jQuery.bbq.pushState( '#' + href );
        isOptionLinkClicked = true;
        return false;
    }

    $options.on('click', function () {       
        isotopeGO();
    });

    var isotopeLink = $('.isotope-link');
    isotopeLink.on('click', function () { 
        backToTop();
        isotopeGO();       
    });
    

    $(window).on( 'hashchange', function( event ){
      var isotopeFilter = window.location.hash.replace( /^#/, '');
      
      if( isotopeFilter == false )
        isotopeFilter = 'home';
        
      $isotopeContainer.imagesLoaded( function() {
        $isotopeContainer.isotope({
          filter: '.' + isotopeFilter
        });
      });
      
      if ( isOptionLinkClicked == false ){
        $options.removeClass('selected');
        $isotopeOptionContainer.find('a[href="#'+ isotopeFilter +'"]').addClass('selected');      
      }    
      
      isOptionLinkClicked = false;

    }).trigger('hashchange');

    var linkMenu = $('.navbar-nav li a');
    linkMenu.on('click', function(){
      linkMenu.removeClass('activeMenu');
      $(this).addClass('activeMenu');
    });

    //Masonry Layout on Blog
    var $isotopeContainerBlog = $('#blog-posts-masonry')

    $isotopeContainerBlog.imagesLoaded( function() {
      $isotopeContainerBlog.isotope({
        itemSelector : '.blog-item',
        resizable: false,
        //filter: '*',
        transitionDuration: '0.8s',
        layoutMode: 'packery'
      });
    });  

  });

  //Begin - Document Ready
  $(document).on('ready', function(){


    // Double Tap to Go - Mobile Friendly SubMenus
    var liHasUl = $('.navbar-nav li:has(ul)');
    liHasUl.doubleTapToGo();

    // Maps iframe Overlay
    var map = $('#map');
    var mapIframe = $('#map iframe');
    map.on('click', function () {
        mapIframe.css("pointer-events", "auto");
        return false;
    });

    map.on('mouseleave', function () {
        mapIframe.css("pointer-events", "none");
        return false;
    });

    //Form Validator and Ajax Sender
    var contactForm = $("#contactForm");

    var contactWait = $('#contactWait');
    var contactSuccess = $("#contactSuccess");
    var contactError = $("#contactError");

    contactForm.validate({
      submitHandler: function(form) {
        $.ajax({
          type: "POST",
          url: "php/contact-form.php",
          data: {
            "name": $("#contactForm #name").val(),
            "email": $("#contactForm #email").val(),
            "guests": $("#contactForm #guests").val(),
            "attending": $("#contactForm #attending").val(),
            "message": $("#contactForm #message").val()
          },
          dataType: "json",
          success: function (data) {
            if (data.response == "success") {
              contactWait.hide();
              contactSuccess.fadeIn(300).addClass('modal-show');
              contactError.addClass("hidden");   
            } else {
              contactWait.hide();
              contactError.fadeIn(300).addClass('modal-show');
              contactSuccess.addClass("hidden");
            }
          },
          beforeSend: function() {
            contactWait.fadeIn(200);
          }
        });
      }
    });


    //Modal for Contact Form
    var modalWrap = $('.modal-wrap');
    modalWrap.on('click', function () {
      modalWrap.fadeOut(300);
    }); 

    //Modal for Forms
    function hideModal() {
      modalWrap = $('.modal-wrap');
      modalWrap.fadeOut(300);
      return false;
    }

    modalWrap.on('click', function () {
      hideModal();
    });   

    modalWrap.on('click', function () {
      hideModal();
    }); 

    //bootstrap tooltips
    var Tooltips = $('[data-toggle="too;ltip"]')
    Tooltips.tooltip();

    //Nivo Lightbox
    var nivoLightbox = $('a.nivobox');
    nivoLightbox.nivoLightbox({ effect: 'fade' });

  //End - Document Ready
  });

//End - Use Strict mode
})(jQuery);