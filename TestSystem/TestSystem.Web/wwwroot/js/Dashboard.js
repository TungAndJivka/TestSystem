const categoryTabClickStart = (function (testName) {

    node = $('#DashboardContent')[0].firstChild;
    if (node) {
        node.remove();
    }


    let row = $(`
            <div id="areaStart">
                <span id="testName"></span>
                <button class="buttonhover">Start</button>
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
                <button class="buttonhover">Submitted</button>
            </div>
        `);

    $('#DashboardContent').append(row);
    $('#testName').html(testName);
});

