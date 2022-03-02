// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    $("a.details-link").click(function () {
        var id = $(this).attr('id');

        if ($(this).text() == "Show details") {
            reset();
            $("#description-header-row-" + id).css("display", "inline-block");
            $("#description-row-" + id).slideDown("slow");
            $("#food-header-" + id).css("display", "inline-block");
            $(".foods-wrapper-row-" + id).slideDown("slow");
            $(".food-list-" + id).css("display", "inline");
            $(this).text("Hide details");

            // All .beer-row(s)
            $(".beer-row .beer-name, .beer-row .beer-apv").css("opacity", "40%");

            // The the previous .beer
            $(this).parent().parent().parent().prev().find(".beer-row").css("border-bottom", "none");

            // Current .beer
            $(this).parent().parent().parent().css("background", "lightgray");
            $(this).parent().parent().parent().css("-webkit-box-shadow", "1px 1px 3px 1px #000000");
            $(this).parent().parent().parent().css("box-shadow", "1px 1px 3px 1px #000000");
            $(this).parent().parent().parent().css("margin-top", "2em");

            // Current .beer-row
            $(this).parent().parent().css("font-size", "125%");
            $(this).parent().parent().css("background", "#b3d8f9");
            $(this).parent().parent().children().css("opacity", "100%");
        } else {
            reset();
        }

        function reset() {
            $(".description-header-row").css("display", "none");
            $(".description-row").slideUp("fast");
            $(".food-header").css("display", "none");
            $(".foods-wrapper-row").slideUp("fast");
            $(".food-list").css("display", "none");
            $(".foods-wrapper-row").slideUp("fast");
            $(".food-list").css("display", "none");
            $(".beer-row a").text("Show details");
            $(".beer-row").css("font-size", "100%");
            $(".beer-row").css("border-bottom", "1px solid #a8a5a5");
            $(".beer-row .beer-name, .beer-row .beer-apv").css("opacity", "100%");
            $(".beer-row, .beer").css("background", "none");
            $(".beer").css("-webkit-box-shadow", "none");
            $(".beer").css("box-shadow", "none");
            $(".beer").css("margin-top", "0");
        }
    });
})