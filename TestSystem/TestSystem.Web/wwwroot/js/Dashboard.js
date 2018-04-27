$(function () {
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

    let testName = categoryName ;

    $('#DashboardContent').append(row);
    $('#testName').html(testName);
});

