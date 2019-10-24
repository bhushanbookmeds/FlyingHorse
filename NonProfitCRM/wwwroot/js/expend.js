/// <reference path="jquery.min.js" />
//var formData = new FormData();
//var x = $("#Name").val();
//formData.append("Name", $("#Name").val());
//formData.append("Date", $("#Date").val());
//formData.append("Submitter", $("#Submitter").val());
//formData.append("Amount", $("#Amount").val());
//formData.append("Invoice", "n");

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
        var expenditure = {
            name: $("#Name").val(),
            date: $("#Date").val(),
            submitter: $("#Submitter").val(),
            amount: $("#Amount").val(),
            invoice: "dsfz",
            projectId: $("#ProjectId").val()
        };

        $.ajax({
            url: "/Project/Expenditures",
            type: 'POST',
            data: expenditure,
            //contentType: "application/jason; charset=utf-8",
            //dataType: "json",

            success: function (data) {

                if (data.returnStatus === "success") {

                    var expend = data.returnData;
                    var newdata = "<tr><td>" + expend.name + " </td> <td>" + expend.date + "</td><td>" + expend.submitter + "</td><td>" + expend.amount + "</td><td>" + expend.invoice + "</td></tr>";
                    $('#datatable').append(newdata);
                    debugger
                    var totalAmount = parseInt($("#total_expenditure").html()) + expend.amount;

                    $("#total_expenditure").html(totalAmount);
                }
                else {
                    alert("Expense already exsist");
                }


            }
        });
    });
});
