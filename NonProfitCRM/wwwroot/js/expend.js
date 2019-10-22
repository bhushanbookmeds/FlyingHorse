/// <reference path="jquery.min.js" />
//$(document).ready(function () {
//    $("#btnsubmit").click(function () {
//        debugger
//        var myformdata = $("#myform");
//        $.ajax({
//            type: "POST",
//            url:"/Project/Expenditures",
//            data: myformdata,
//            success: function (data) {
//                $("#myLargeModalLabel").modal("hide");

//            }
//        })

//    })
//})
$(function () {
    $("#btnSubmit").click(function () {
        debugger
        var formData = new FormData();
        formData.append("Title", $("#Title").html());
        formData.append("Date", $("#Date").html());
        formData.append("Description", $("#Description").html());
        formData.append("Amount", $("#Amount").html());
        formData.append("Invoice", $("#Invoice").html());
        $.ajax({
            url: "/Project/Expenditures",
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            data: formData,
            contentType:"application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response) {
                    $('#title').html(response.title)
                    $('#date').html(response.date)
                    $('#description').html(response.description)
                    $('#amount').html(response.amount)
                    $('#invoive').html(response.invoive)

                }
                else {
                    alert("Somehting went wrong");
                }
                return response;
            }
        });
    });
});