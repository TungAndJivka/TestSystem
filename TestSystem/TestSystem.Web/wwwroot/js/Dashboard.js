
$(document).ready(function () {
    $('#Java').click();
});


const categoryTabClickStart = (function (categoryName) {        
    node = $('#DashboardContent')[0].firstChild;
    if (node) {
        node.remove();
    }

    var url = window.location.protocol + '/TakeTest/Index/' + categoryName;

    let row = $(`
            <div class="dash-area" id="areaStart">
                <span id="categoryName" style="color: black"></span>
                <a href="${url}" class="StartTestBtn DashBtn">Start</a>
            </div>
        `);

    let testName = categoryName + ' Test';

    $('#DashboardContent').append(row);
    $('#categoryName').html(testName);
});


const categoryTabClickSubmitted = (function (categoryName) {
    node = $('#DashboardContent')[0].firstChild;
    if (node) {
        node.remove();
    }

    let row = $(`
            <div class="dash-area" id="areaSubmitted">
                <span id="testName"></span>
                <a class="SubmittedTestBtn DashBtn">Submitted</a>
            </div>
        `);

    let testName = categoryName + ' Test';
    //let testName = categoryName ;

    $('#DashboardContent').append(row);
    $('#testName').html(testName);
});
var ctx = document.getElementById("myChart").getContext('2d');

var teststatistics;

$(function () {
    var $this = $(this);
    var frmValues = $this.serialize();
    $.ajax({
        type: $this.attr('get'),
        url: $this.attr('/Dashboard/GetAA'),
    })
        .done(function (Json) {
            teststatistics = Json;
        })
        .fail(function () {
            $("#para").text("An error occured");
        });
})



var myChart = new Chart(ctx, {
    type: 'pie',
    data: {
        labels: ["Java", "SQL", ".NET", "JavaScript"],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5],
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',   
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)'
            ],
            borderWidth: 1
        }]
    },
    options: {
        
    }
});