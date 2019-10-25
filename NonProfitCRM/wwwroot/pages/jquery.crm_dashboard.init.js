var options = {
    chart: {
        height: 365,
        type: 'line',
        stacked: false,
        toolbar: {
            show: false
        },
    },
    stroke: {
        width: [0, 2, 5],
        curve: 'smooth'
    },
    plotOptions: {
        bar: {
            columnWidth: '20%',
            endingShape: 'rounded',
        },

    },
    colors: ["#4d79f6", "#eef1f5", "#4ac7ec"],
    series: [{
        name: 'Leads',
        type: 'column',
        data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30]
    }, {
        name: 'Vendors',
        type: 'area',
        data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43]
    }, {
        name: 'Invoice Generate',
        type: 'line',
        data: [130, 125, 36, 30, 45, 35, 64, 52, 59, 36, 39]
    }],
    fill: {
        type: 'gradient',
        gradient: {
            inverseColors: true,
            shade: 'light',
            type: "horizontal",
            shadeIntensity: 0.25,
            gradientToColors: undefined,
            opacityFrom: 1,
            opacityTo: 1,
            stops: [0, 100, 100, 100]
        }
    },
    labels: ['01/01/2003', '02/01/2003', '03/01/2003', '04/01/2003', '05/01/2003', '06/01/2003', '07/01/2003', '08/01/2003', '09/01/2003', '10/01/2003', '11/01/2003'],
    markers: {
        size: 0
    },
    xaxis: {
        type: 'datetime',
        axisBorder: {
            show: true,
            color: '#bec7e0',
        },
        axisTicks: {
            show: true,
            color: '#bec7e0',
        },
    },
    yaxis: {
        min: 0
    },
    tooltip: {
        shared: true,
        intersect: false,
        y: {
            formatter: function (y) {
                if (typeof y !== "undefined") {
                    return y.toFixed(0) + "%";
                }
                return y;

            }
        }
    },
    legend: {
        labels: {
            useSeriesColors: true
        },
        markers: {
            customHTML: [
                function () {
                    return ''
                }, function () {
                    return ''
                }, function () {
                    return ''
                }
            ]
        }
    }
}

var chart = new ApexCharts(
    document.querySelector("#crm_dash_2"),
    options
);

chart.render();


getDonationData("week");

function getDonationData(name) {
    $.ajax({
        type: "GET",
        url: "/Dashboard/GetDonation?type=" + name,
        //data: JSON.stringify({
        //    FieldName: name
        //}),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data);
        },
        error: function () {
            alert("Error occured!!");
        }
    });

}
