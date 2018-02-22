$(document).ready(function () {


    'use strict';
    
    var searchOpenBtn       = $('.search-btn'),
        searchCloseBtn      = $('.search-popup .close-btn'),
        searchPopup         = $('.search-popup'),
        formGroup           = $('.search-popup .form-group'),
        searchPopupHeading  = $('.search-popup h2'),
        inputMaterial       = $('.input-material'),
        navbarToggleBtn     = $('.navbar .navbar-toggler'),
        scrollTopBtn        = $('#scrollTop'),
        navbar              = $('.navbar'),
        currentScrollTop    = 0,
        run                 = true,
        c                   = 0;

    // ------------------------------------------------------- //
    // Add Text Background from HTML [data-text] attribute
    // ------------------------------------------------------- //
    $('.has-background-text').each(function () {
        $('<span class="text-bg"></span>').prependTo(this);
        var textBackground = $(this).attr('data-text');
        $(this).find('.text-bg').text(textBackground);
    });

    $('.has-background-text-gray').each(function () {
        $('<span class="text-bg-gray"></span>').prependTo(this);
        var textBackground = $(this).attr('data-text');
        $(this).find('.text-bg-gray').text(textBackground);
    });

    $('.with-bg-text').each(function () {
        $('<span class="heading-bg-text"></span>').prependTo(this);
        var textBackground = $(this).attr('data-text');
        $(this).find('.heading-bg-text').text(textBackground);
    });

    $('.with-bg-text-gray').each(function () {
        $('<span class="heading-bg-text-gray"></span>').prependTo(this);
        var textBackground = $(this).attr('data-text');
        $(this).find('.heading-bg-text-gray').text(textBackground);
    });

    /////-------------- Begin Newsletter Submit-----------------/////////////
    $("#newsletter-form").submit(function (event) {
        event.preventDefault();
        var Url = $(this).attr("action");
        var Data = $(this).serialize();
        $("#subscribe_msj").removeClass("show").addClass("hide");
        $.ajax({
            url: Url,
            type: "Post",
            dataType: "json",
            data: Data,
            success: function (response) {
                $("#subscribe_msj").addClass("show").removeClass("hide");
                if (response.Code == 200) {
                    $("#subscribe_msj .text").text(response.Message)
                    $("#newsletter-form")[0].reset();
                }
                else {
                    $("#subscribe_msj .text").text("Try again");
                }
            }
        });
    });
    /////-------------- End Newsletter Submit-----------------/////////////


    // ------------------------------------------------------------------- //
    // Search Popup
    // ------------------------------------------------------------------ //
    searchOpenBtn.on('click', function (e) { // On search popup Opening
        e.preventDefault();
        searchPopup.show();

        setTimeout(function () { searchPopup.addClass('visible'); }, 100);
        setTimeout(function () { searchPopupHeading.addClass('visible'); }, 600);

        setTimeout(function () {
            formGroup.addClass('visible');
            searchCloseBtn.addClass('visible');
        }, 800);
    });

    searchCloseBtn.on('click', function () { // On search popup closing
        searchPopup.removeClass('visible');

        setTimeout(function () {
            searchPopup.hide();
            formGroup.removeClass('visible');
            searchCloseBtn.removeClass('visible');
            searchPopupHeading.removeClass('visible');
        }, 300);
    });


    // ------------------------------------------------------------------- //
    // Dishes Slider
    // ------------------------------------------------------------------ //
    $('.dishes-slider').owlCarousel({
        loop: true,
        margin: 0,
        dots: true,
        nav: true,
        smartSpeed: 400,
        navText: [
            "<i class='fa fa-long-arrow-left'></i>",
            "<i class='fa fa-long-arrow-right'></i>"
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                nav: false
            },
            600: {
                items: 1,
                nav: false
            },
            1000: {
                items: 3,
                nav: true,
                loop: true
            }
        }
    });

    // ------------------------------------------------------------------- //
    // Chefs Slider
    // ------------------------------------------------------------------ //
    $('.chefs-slider').owlCarousel({
        loop: true,
        margin: 0,
        dots: false,
        nav: true,
        smartSpeed: 400,
        navText: [
            "<i class='fa fa-long-arrow-left'></i>",
            "<i class='fa fa-long-arrow-right'></i>"
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                nav: false,
                dots: true
            },
            600: {
                items: 1,
                nav: false,
                dots: true
            },
            1000: {
                items: 3,
                nav: true,
                loop: true
            }
        }
    });

    // ------------------------------------------------------------------- //
    // Gallery Slider
    // ------------------------------------------------------------------ //
    $('.gallery-slider').owlCarousel({
        loop: true,
        margin: 0,
        dots: true,
        nav: false,
        smartSpeed: 400,
        navText: [
            "<i class='fa fa-long-arrow-left'></i>",
            "<i class='fa fa-long-arrow-right'></i>"
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 2
            },
            600: {
                items: 2
            },
            1000: {
                items: 4
            }
        }
    });

    // ------------------------------------------------------------------- //
    // Events Slider
    // ------------------------------------------------------------------ //
    $('.events-slider').owlCarousel({
        loop: true,
        margin: 0,
        dots: true,
        nav: false,
        smartSpeed: 400,
        items: 1,
        navText: [
            "<i class='fa fa-long-arrow-left'></i>",
            "<i class='fa fa-long-arrow-right'></i>"
        ],
        responsiveClass: true
    });

    // ------------------------------------------------------- //
    // Testimonials Slider
    // ------------------------------------------------------ //
    $('.testimonials-slider').owlCarousel({
        loop: true,
        margin: 20,
        dots: true,
        nav: true,
        smartSpeed: 700,
        navText: [
            "<i class='fa fa-long-arrow-left'></i>",
            "<i class='fa fa-long-arrow-right'></i>"
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                nav: false,
                dots: true,
                margin: 40
            },
            600: {
                items: 1,
                nav: true,
                margin: 40
            },
            1000: {
                items: 1,
                nav: true,
                loop: false
            }
        }
    });


    // ------------------------------------------------------- //
    // Transition Placeholders
    // ------------------------------------------------------ //
    inputMaterial.on('focus', function () {
        $(this).siblings('label').addClass('active');
        $(this).parents('.time_pick').siblings('label').addClass('active');
    });

    inputMaterial.on('blur', function () {
        $(this).siblings('label').removeClass('active');

        if ($(this).val() !== '') {
            $(this).siblings('label').addClass('active');
        } else {
            $(this).siblings('label').removeClass('active');
        }
    });


    // ------------------------------------------------------- //
    // Navbar Toggler Button
    // ------------------------------------------------------- //
    navbarToggleBtn.on('click', function () {
        $(this).toggleClass('active');
    });


    // ------------------------------------------------------- //
    // Scroll Top Button
    // ------------------------------------------------------- //
    scrollTopBtn.on('click', function () {
        $('html, body').animate({ scrollTop: 0}, 1000);
    });

    $(window).on('scroll', function () {

        // Navbar change background on scroll
        if ($(this).scrollTop() >= 5) {
            navbar.addClass('active');
        } else {
            navbar.removeClass('active');
        }

        // Navbar functionality
        var a = $(window).scrollTop(), b = navbar.height();

        currentScrollTop = a;
        if (c < currentScrollTop && a > b + b + 200) {
            navbar.addClass("scrollUp");
        } else if (c > currentScrollTop && !(a <= b)) {
            navbar.removeClass("scrollUp");
        }
        c = currentScrollTop;


        if ($(window).scrollTop() >= 2000) {
            scrollTopBtn.addClass('active');
        } else {
            scrollTopBtn.removeClass('active');
        }
    });

    /////-------------- Begin Count Up-----------------/////////////

    if ($(".counter").length) {
        $(window).scroll(function () {

            var divOffsetTop = $('.vision').offset().top
            if ($(window).scrollTop() > divOffsetTop && run) {
                $('.counter').each(function () {

                    var self = $(this);
                    var dataNum = self.data('count');
                    var dataText = self.data('text');
                    var totalTime = 1000;
                    var intervalTime = 50;
                    var myNumber = dataNum;

                    var dovrCount = totalTime / intervalTime;
                    var step = (myNumber / dovrCount);

                    var a = 0;

                    var myInterval = setInterval(function () {
                        a = a + step;
                        if (Math.round(a) <= myNumber) {
                            self.text(Math.round(a) + dataText);
                        }

                        else {
                            clearInterval(myInterval);
                        }

                    }, intervalTime);
                    run = false;
                });
            }
        });
    }

/////..........End Count Up........./////
});
