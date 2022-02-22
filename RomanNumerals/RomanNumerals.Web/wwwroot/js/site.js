// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var error;
$("#parse").click(function () {
    $("#error").hide();
    var numerals = $("#romanNumerals").val();
    var jqxhr = $.get("http://localhost:49101/parse/" + numerals, function (data) {
        $("#result").text(numerals + " = " + data);
    })
        .done(function () {
            //alert("second success");
        })
        .fail(function (data) {
            error = data.responseJSON.detail;
            $("#error").show();
            $("#error").text(error);
        });
});