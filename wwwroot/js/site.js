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
            $(this).parent().parent().css("border-bottom", "none");
            $(this).text("Hide details");
            $(this).parent().parent().css("font-size", "125%");
            $(this).parent().parent().css("border-top", "1px solid #ccc");
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
            $(".beer-row").css("border-bottom", "1px solid #a8a5a5");
            $(".beer-row a").text("Show details");
            $(".beer-row").css("font-size", "100%");
            $(".beer-row").css("border-top", "none");
        }
    });
})