const categoryTabClickStart = (function (categoryName) {        
    node = $('#DashboardContent')[0].firstChild;
    if (node) {
        node.remove();
    }

    var url = window.location.protocol + '/TakeTest/Index/' + categoryName;

    let row = $(`
            <div class="dash-area" id="areaStart">
                <span id="categoryName"></span>
                <a href="${url}" class="StartTestBtn DashBtn">Start</a>
            </div>
        `);

    $('#DashboardContent').append(row);
    $('#categoryName').html(categoryName);
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

