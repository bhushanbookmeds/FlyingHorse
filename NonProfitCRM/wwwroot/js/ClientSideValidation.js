/// <reference path="jquery.min.js" />

$(function () {
    $("#Name").keydown(function (e) {

        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode >= 35 && e.keyCode <= 40)) { return; }
        if (e.keyCode < 65 || e.keyCode > 90) {
            //$("#txtName").html("Please use Alphabets only.");
            e.preventDefault();


        }

    });

    $("#PhoneNumber").keydown(function (e) {
        if (e.which >= 48 && e.which <= 57  || e.which === 8) {

            var phone = $("#PhoneNumber").val();
            var phregex =^\+[1 - 9]{ 1 } [0 - 9]{ 3, 14 } $
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
    $("#Age").keydown(function (e) {
        if (e.which >= 48 && e.which <= 57 && $("#Age").val().length < 3 || e.which === 8) {

            //$("#txtAge").empty();
            //$("#txtAge").html("Please provide a valid phone number");
            return;
        }
        else
            e.preventDefault();
    });


    //$("#Age").addMethod("minAge", "maxAge", function (value, element, min, max)){
    //    var age = $("#Age").val();
    //    if (age > min + 1 && age <= max) {
    //        return true;
    //    }
    //    $("#myform".validate({
    //        rules: {
    //            Age: {
    //                required: true,
    //                minAge: 0
    //            maxAge: 100

    //            }
    //        },
    //        messages: {
    //            Age: {
    //                required: "Please enter your age."
    //                minAge: "Age should be between  1 to 100"
    //                maxAge: "Age should be between  1 to 100"
    //            }

    //        }
    //    }
    //});
    
    $("#myform").validate({
        rules: {
            Age: {
                required: true,
                range: [13,100]
            }
        },
        messages: {
            Age: {
                required: "Please enter your age.",
                range:"Age should be between 13 to 100"
            }
        }
    });
    

});
function changetextbox() {
    debugger
    if (document.getElementById("ContactTypeId").value === "2") {
        document.getElementById("Age").disable = 'true';

    } else {
        document.getElementById("Age").disable = 'false';
    }
}

function CheckMandateValidate() {
   
    var count = 0, message = '';
    if ($.trim($('#Name').val()) === '') { message = "Name,"; count++; }
    if ($.trim($('#Type').val()) === 0) { message = message + "Type,"; count++; }
    if ($.trim($('#PhoneNumber').val()) === '') { message = message + "PhoneNumber,"; count++; }

    if ($.trim($('#Email').val()) === '') { message = message + "Email,"; count++; }
    if ($.trim($('#AddressLine1').val()) === '') { message = message + "AddressLine1,"; count++; }
    if ($.trim($('#AddressStreet').val()) === '') { message = message + "AddressStreet,"; count++; }
    if ($.trim($('#AddressCity').val()) === '') { message = message + "AddressCity,"; count++; }
    if ($.trim($('#AddressState').val()) === '') { message = message + "AddressState,"; count++; }
    if ($.trim($('#AddressZipCode').val()) === '') { message = message + "AddressZipcode,"; count++; }
    if ($.trim($('#AddressCountry').val()) === 0) { message = message + "AddressCountry,"; count++; }
    if (count === 0) {
        $("#myform").submit();
    }
    else { alert(message + " Invalid Data or Empty or Not Selected "); }
}
