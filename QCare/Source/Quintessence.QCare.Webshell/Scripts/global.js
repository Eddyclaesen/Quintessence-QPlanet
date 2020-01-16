(function ($) {
    $.fn.loading = function (message) {
        $(this).html('<p class="loading"><img src="/Images/loading.gif" /><br />' + message + '</p>').fadeIn();
    };

    $.fn.copyToListener = function (selector) {
        $(this).change(function () {
            var value = $(this).val();
            $(selector).val(value);
        });
    };

    $.redirect = function (url) {
        window.location.href = url;
    };
})(jQuery);