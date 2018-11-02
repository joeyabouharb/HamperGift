$(document).ready(function () {

    $('.navbar').on('click', function (e) {
        
        $(this).parent("li").toggleClass('open');
        
            e.preventDefault();
            e.stopPropagation();
    
    });

});

