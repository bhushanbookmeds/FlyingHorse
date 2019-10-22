/// <reference path="jquery.min.js" />

$(function () {

    $("#dtpDob").datepicker({
        dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, buttonImage: '../images/cal.png',
        buttonImageOnly: true, showOn: 'both',
    });
  
    $("#NewName").keydown(function (e) {
        debugger
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode >= 35 && e.keyCode <= 40)) { return; }
        if (e.keyCode < 65 || e.keyCode > 90) {
            e.preventDefault();

        }
    });

    $("#PhoneNumber").keydown(function (e) {
        if (e.which >= 48 && e.which <= 57 && $("#PhoneNumber").val().length < 10 || e.which === 8) {

            $("#MobileNumber").empty();
            $("#MobileNumber").html("Please provide a valid phone number");
            return;
        }
        else
            e.preventDefault();
    });

    $("#Email").blur(function (e) {
        var email = $("#Email").val();
        var re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (re.test(email)) { return true; }
        else {
            $("#mail").empty();
            $("#mail").html("Please enter a valid Email address");
            // $.alert("Invalid Email");
            e.preventDefault();
        }
    });

    $("#AddressZipcode").blur(function (e) {
        var zipcode = $("#AddressZipcode").val();
        var zipregex = /^\d{5}$/;

        if (zipregex.test(zipcode)) { return true; }
        else {
            $("#zipcode").empty();
            $("#zipcode").html("Please provide a valid zipcode");
            //$.alert("Invalid Email");
            e.preventDefault();
        }
    });
});

function CheckMandateValidate() {

    var count = 0, message = '';
    if ($.trim($('#NewName').val()) === '') { message = "Name,"; count++; }
    if ($.trim($('#ProjectType').val()) === 0) { message = message + "ProjectType,"; count++; }
    if ($.trim($('#PhoneNumber').val()) === '') { message = message + "PhoneNumber,"; count++; }
    if ($.trim($('#dtpDob').val()) == '') { message = message + "Date,"; count++; }
    if ($.trim($('#AddressLine1').val()) === '') { message = message + "AddressLine1,"; count++; }
    if ($.trim($('#AddressStreet').val()) === '') { message = message + "AddressStreet,"; count++; }
    if ($.trim($('#AddressCity').val()) === '') { message = message + "AddressCity,"; count++; }
    if ($.trim($('#AddressState').val()) === '') { message = message + "AddressState,"; count++; }
    if ($.trim($('#AddressZipCode').val()) === '') { message = message + "AddressZipcode,"; count++; }
    if ($.trim($('#AddressCountry').val()) === '') { message = message + "AddressCountry,"; count++; }
    if (count === 0) {
        $("#myNewForm").submit();
    }
    else {$.alert(message + " Invalid Data or Empty or Not Selected "); }
}



