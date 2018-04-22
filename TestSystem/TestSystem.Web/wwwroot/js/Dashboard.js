const categoryTabClickStart = (function (testName) {

    node = $('#DashboardContent')[0].firstChild;
    if (node) {
        node.remove();
    }


    let row = $(`
            <div id="areaStart">
                <span id="testName"></span>
                <a href="#" class="StartTestBtn">Start</a>
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
            <div id="areaStart">
                <span id="testName"></span>
                <a class="SubmittedTestBtn">Submitted</a>
            </div>
        `);

    $('#DashboardContent').append(row);
    $('#testName').html(testName);
});

