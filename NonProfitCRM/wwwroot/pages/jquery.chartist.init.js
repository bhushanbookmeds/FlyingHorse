/**
 * Theme: Metrica - Responsive Bootstrap 4 Admin Dashboard
 * Author: Mannatthemes
 * chartist Js
 */
   

//Simple pie chart
bindPieChart();

function bindPieChart(){

    $.ajax({
        type: "GET",
        url: "/Dashboard/GetDonation?type=YEAR",
        //data: JSON.stringify({
        //    FieldName: name
        //}),
        dataType: "json",
        async: true,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            //console.log(data);

            var labels = [];
            var labelsData = [];

            for (var prop in data) {
                // skip loop if the property is from prototype
                if (!data.hasOwnProperty(prop)) continue;
                // your code
                //alert(prop + " = " + data[prop]);
                labels.push(prop);
                labelsData.push(data[prop]);
            }

            var piedata = {
                series: labelsData
            };

            var sum = function (a, b) { return a + b };

            new Chartist.Pie('#simple-pie', piedata, {
                labelInterpolationFnc: function (value) {
                    return Math.round(value / piedata.series.reduce(sum) * 100) + '%';
                }
            });
        },
        error: function () {
            alert("Error occured!!");
        }
    });

}


getDonationData("WEEK", true);

function getDonationData(name, isPreloaded) {

    $.ajax({
        type: "GET",
        url: "/Dashboard/GetDonation?type=" + name,
        //data: JSON.stringify({
        //    FieldName: name
        //}),
        dataType: "json",
        async: true,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            //console.log(data);

            var labels = [];
            var labelsData = [];

            for (var prop in data) {
                // skip loop if the property is from prototype
                if (!data.hasOwnProperty(prop)) continue;
                // your code
                //alert(prop + " = " + data[prop]);
                labels.push(prop);
                labelsData.push(data[prop]);
            }

            //console.log(JSON.stringify(options));
            document.getElementById('stacked-bar-chart').innerHTML = '';

            new Chartist.Bar('#stacked-bar-chart', {
                labels: labels,
               
                series: [
                    labelsData
                ]
            }, {
                    stackBars: true,
                    axisY: {
                        labelInterpolationFnc: function (value) {
                            return (value / 1000) + 'k';
                        }
                    },
                    plugins: [
                        Chartist.plugins.tooltip()
                    ]
                }).on('draw', function (data) {
                    if (data.type === 'bar') {
                        data.element.attr({
                            style: 'stroke-width: 30px'
                        });
                    }
                });
        },
        error: function () {
            alert("Error occured!!");
        }
    });

}

function getWeekData() {
    getDonationData("WEEK", false);

    $("#btnWeek").addClass('active');
    $("#btnMonth").removeClass('active');
    $("#btnYear").removeClass('active');
}

function getMonthData() {

    getDonationData("MONTH", false);

    $("#btnMonth").addClass('active');
    $("#btnWeek").removeClass('active');
    $("#btnYear").removeClass('active');
}

function getYearData() {
    getDonationData("YEAR", false);

    $("#btnYear").addClass('active');
    $("#btnMonth").removeClass('active');
    $("#btnWeek").removeClass('active');

}