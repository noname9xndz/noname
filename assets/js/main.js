/*******************************************************
    Template Name    : Maslin - Personal Portfolio HTML Template
    Author           : aam-developer
    Version          : 1.0
    Created          : 2020
    File Description : Main Js file of the template
*******************************************************/
(function ($) {
	"use strict";

	// START PRELOADED
	$(window).on('load', function() {
		function preLoader() {
            setTimeout(function () {
                $('#preloader-wapper .loader-middle').addClass('loaded');
                setTimeout(function () {
                    $('#preloader-wapper').addClass('loaded');
                    setTimeout(function () {
                        $('#preloader-wapper').remove();
                    }, 500);
                }, 700);
            }, 1200);
        };
        preLoader();
	});
	
	//  Porfolio isotope and filter
    $(window).on('load', function() {
		var projectIsotope = $('.project-container').isotope({
			itemSelector: '.project-grid-item'
		});

		$('#project-flters li').on('click', function () {
			$("#project-flters li").removeClass('filter-active');
			$(this).addClass('filter-active');

			projectIsotope.isotope({
				filter: $(this).data('filter')
			});
		});
    });
	
	
   
	// Progress bar animation with Waypoint JS
    if ($('.skill-item').length > 0) { 
      var waypoint = new Waypoint({
        element: document.getElementsByClassName('skill-item'),
        handler: function(direction) {
          
          $('.progress-bar').each(function() {
            var bar_value = $(this).attr('aria-valuenow') + '%';                
            $(this).animate({ width: bar_value }, { easing: 'linear' });
          });

          this.destroy()
        },
        offset: '50%'
      });
    }
	
		// Odometer JS
        $('.odometer').appear(function() {
			var odo = $(".odometer");
			odo.each(function() {
				var countNumber = $(this).attr("data-count");
				$(this).html(countNumber);
			});
        });
	
	// Testimonials owl
	$('#testimonial-slide').owlCarousel({
		margin: 5,
		autoplay: true,
		center: true,
		autoplayTimeout: 4000,
		nav: false,
		smartSpeed: 1000,
		dots: false,
		autoplayHoverPause: true,
		loop: true,
        responsiveClass:true,
		responsive: {
			0: {
				items: 1
			},
			600: {
				items: 2
			},
			1000: {
				items: 3
			}
		}
	});
	
	
	//  magnificPopup
	var magnifPopup = function () {
		$('.popup-img').magnificPopup({
			type: 'image',
			removalDelay: 300,
			mainClass: 'mfp-with-zoom',
			gallery: {
				enabled: true
			},
			zoom: {
				enabled: true, // By default it's false, so don't forget to enable it

				duration: 700, // duration of the effect, in milliseconds
				easing: 'ease-in-out', // CSS transition easing function

				// The "opener" function should return the element from which popup will be zoomed in
				// and to which popup will be scaled down
				// By defailt it looks for an image tag:
				opener: function (openerElement) {
					// openerElement is the element on which popup was initialized, in this case its <a> tag
					// you don't need to add "opener" option if this code matches your needs, it's defailt one.
					return openerElement.is('img') ? openerElement : openerElement.find('img');
				}
			}
		});
	};

	// Call the functions
	magnifPopup();
	
})(jQuery);