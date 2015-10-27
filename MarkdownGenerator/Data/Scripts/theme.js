$(document).ready(function () {
    $('#themeABtn').click(function () {
        $('body').removeClass().addClass("themeA");
    });

    $('#themeBBtn').click(function () {
        $('body').removeClass().addClass("themeB");
    });

    $('#themeCBtn').click(function () {
        $('body').removeClass().addClass("themeC");
    });
});
