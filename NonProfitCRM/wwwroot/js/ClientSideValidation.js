﻿/// <reference path="jquery.min.js" />$(function () {    $("#Name").keydown(function (e) {        //if (typeof e === 'string') {        //    return e.charAt(0).toUpperCase() + e.slice(1);        //}        //return null;        // var Name = this.val().toLowerCase().split('#Name');        //e = e.toLowerCase().split('#Name');        //for (var i = 0; i < e.length; i++) {        //    e[i] = e[i].charAt(0).toUpperCase() + e[i].slice(1);        //return e..toLowerCase().split(' ').map(function (word) {        //    return (word.charAt(0).toUpperCase() + word.slice(1));        //}).join(' ');        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 32, 110, 190]) !== -1 || (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||            (e.keyCode >= 35 && e.keyCode <= 40)) { return; }        if (e.keyCode < 65 || e.keyCode > 90) {            e.preventDefault();        }        //return;    });    //$(function capital_letter(Name)    //{    //    Name = Name.split(" ");    //    for (let i = 0, x = Name.length; i < x; i++) {    //        Name[i] = Name[i][0].toUpperCase() + Name[i].substr(1);    //    }    //    return str.join(" ");    //};    //$("#Name").keydown(function check(a) {    //    var a = document.getElementById("Name").value;    //    document.getElementById("Name").value = a.toUpperCase();    //});    $("#PhoneNumber").keydown(function (e) {        if ((e.which >= 48 && e.which <= 57 || e.which >= 96 && e.which <= 105) && $("#PhoneNumber").val().length < 10 || e.which === 8) {            $("#MobileNumber").empty();            $("#MobileNumber").html("Please provide a valid phone number");            return;        }        else            e.preventDefault();    });    $("#Email").blur(function (e) {        var email = $("#Email").val();        var re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;        if (re.test(email)) { return true; }        else {            $("#mail").empty();            $("#mail").html("Please enter a valid Email address");            // $.alert("Invalid Email");            e.preventDefault();        }    });    $("#AddressZipcode").keydown(function (e) {        if ((e.which >= 48 && e.which <= 57 || e.which >= 96 && e.which <= 105) && $("#AddressZipcode").val().length < 6 || e.which === 8) {            return;        }        else            e.preventDefault();    });    //debugger;    //var zipcode = $("#AddressZipcode").val;    //var zipregex = /^\d{6}$/;    ////var zipregex = /^\d{3}\s?\d{3}$/;    ////var zippergex = /^\d{5}$|^\d{5}-\d{4}$/;    //if (zipregex.test(zipcode)) { return true; }    //else {    //    $("#zipcode").empty();    //    $("#zipcode").html("Please provide a valid zipcode");    //    e.preventDefault();    //}    //});    $("#Age").keydown(function (e) {        if ((e.which >= 48 && e.which <= 57 || e.which >= 96 && e.which <= 105) && $("#Age").val().length < 3 || e.which === 8) {            return;        }        else            e.preventDefault();    });    $("#myform").validate({        rules: {            Age: {                required: true,                range: [13, 100]            }        },        messages: {            Age: {                required: "Please enter your age.",                range: "Age should be between 13 to 100"            }        }    });    $('#AddressCity').keydown(function (e) {        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 32, 110, 190]) !== -1 || (e.keyCode === 65 && e.which === 8 && (e.ctrlKey === true || e.metaKey === true)) ||            (e.keyCode >= 35 && e.keyCode <= 40)) {            return;        }        if (e.keyCode < 65 || e.keyCode > 90) {            e.preventDefault();        }    });    //$('#PhoneCodeCountry').change(function () {    //    var PhoneCodeCountry = $(this).val();    //    if (PhoneCodeCountry) {    //        $('#PhoneCode').val(PhoneCodeCountry);    //        $('#CountryId').val(PhoneCodeCountry);    $.ajax({        url: "/Volunteers/State",        type: 'GET',        data: { CountryId: PhoneCodeCountry },        success: function (data) {            $("#StateAddress").find('option').remove();            $.each(data, function (key, value) {                $("#StateAddress").append($("<option></option>").val(value.id).html(value.name));            });        }    });    function CheckMandateValidate() {        var count = 0, message = '';        if ($.trim($('#Name').val()) === '') { message = "Name,"; count++; }        //if ($.trim($('#Type').val()) === 0) { message = message + "Type,"; count++; }        if ($.trim($('#PhoneCode').val()) === '') { message = message + "PhoneCode,"; count++; }        if ($.trim($('#PhoneNumber').val()) === '') { message = message + "PhoneNumber,"; count++; }        if ($.trim($('#Email').val()) === '') { message = message + "Email,"; count++; }        if ($.trim($('#Age').val()) === '') { message = message + "Age,"; count++; }        if ($.trim($('#Gender').val()) === 0) { message = message + "Gender,"; count++; }        if ($.trim($('#AddressLine1').val()) === '') { message = message + "AddressLine1,"; count++; }        if ($.trim($('#AddressStreet').val()) === '') { message = message + "AddressStreet,"; count++; }        if ($.trim($('#AddressCity').val()) === '') { message = message + "AddressCity,"; count++; }        if ($.trim($('#AddressState').val()) === '') { message = message + "AddressState,"; count++; }        if ($.trim($('#AddressZipCode').val()) === 0) { message = message + "AddressZipcode,"; count++; }        if ($.trim($('#AddressCountry').val()) === 0) { message = message + "AddressCountry,"; count++; }        // if ($.trim($('#facebook').val())=== 0) { message = message + "facebook,"; count++;}        if (count === 0) {            $("#myform").submit();        }        else { alert(message + " Invalid Data or Empty or Not Selected "); }    }    $("#CountryId").change(function () {        var statevar = $(this).val();    })
});