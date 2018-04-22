﻿const categoryTabClickStart = (function (testName) {

    node = $('#DashboardContent')[0].firstChild;
    if (node) {
        node.remove();
    }


    let row = $(`
            <div class="dash-area" id="areaStart">
                <span id="testName"></span>
                <a href="#" class="StartTestBtn DashBtn">Start</a>
            </div>
        `);

    $('#DashboardContent').append(row);
    $('#testName').html(testName);
});


const categoryTabClickSubmitted = (function (testName) {

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

    $('#DashboardContent').append(row);
    $('#testName').html(testName);
});

