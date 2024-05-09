// This is the Slick.js function for the top slider
// It defines the settings and calls the slick function
$(this).ready(function () {
    if ($.fn.slick) {
        // Have dots, with a fade speed
        $('.slider-image').slick({
            dots: true,
            arrows: false,
            autoplay: false,
            fade: true,
            fadeSpeed: 1000,
            slidesToShow: 1,
            slidesToScroll: 1
        });

        // Default slick options for product categories
        var slickOptions = { 
            dots: false,
            arrows: true,
            slidesToShow: slideCounter(), // Check against screen size (to only show 2 for tablet and mobile)
            slidesToScroll: 1
        }

        $('#productCategories').slick(slickOptions); // Create the slider for product categories

        // Checks whether the sliders should change amount of slides shown depending on screen size
        $(window).resize(function () { // Check the screen dimensions (using slideCounter) and change for tablet and mobile
            if (slickOptions.slidesToShow != slideCounter()) { // Only change if the slider count has changed (is a smaller screen)
                slickOptions.slidesToShow = slideCounter();
                $('#productCategories').slick('unslick').slick(slickOptions); // Set the new slider
            }
        });

        // This function returns how many sliders should be visibile for the product categories section 
        // Dependant on screen size - mobile and tablet users will only see 2 of the product categories compared to 4
        function slideCounter() {
            var screenWidth = $(window).width(); // Get the screen width
            if (screenWidth <= 1013) return 2; // Change number of slides to show for smaller screens
            else return 4; // Default
        };
    } else {
        console.error('Slick carousel plugin is not loaded properly.'); // error logging
    }

});


// Add event listener to the menu toggle button
document.querySelector(".menu-toggle").addEventListener('click', function () {
    // Toggle the active class on the menu toggle button
    this.classList.toggle('active');

    // Check if the menu toggle button is active
    if (this.classList.contains('active')) {
        // If active, show the top-bar dropdown menu
        document.querySelector(".top-bar .dropdown").style.display = "contents";
    } else if (!(this.classList.contains('active'))) {
        document.querySelector(".top-bar .dropdown").style.display = "none";
    }
});

// Checks the window size and change what is shown on header depending on size
if ($(window).width() >= 1013) {
    document.querySelector(".top-bar .dropdown").style.display = "contents";
} else document.querySelector(".top-bar .dropdown").style.display = "none";
// Checks the window when the window is resized to change what is shown on the header
$(window).resize(function () {
    if ($(window).width() >= 1013) {
        document.querySelector(".top-bar .dropdown").style.display = "contents";
    }
    else document.querySelector(".top-bar .dropdown").style.display = "none";
});
