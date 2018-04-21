//$(categoryTabClick = function (categoryName) {
//    if (categoryName == 'Java') {
//        $('#areaSubmitted').css("color", "red");
//        $('#areaStart').css("visibility", "collapse");
//        $('#areaSubmitted').css("visibility", "visible");
//        //$('#areaStart').addClass('isHidden');
//        //$('#areaSubmitted').removeClass('isHidden');
//    }
//    //node = $('#DashboardContent')[0].firstChild;
//    //if (node) {
//    //    node.remove();
//    //}
//    //$('#DashboardContent').append(categoryName);
//})



const categoryTabClickStart = (function (testName) {

    node = $('#DashboardContent')[0].firstChild;
    if (node) {
        node.remove();
    }


    let row = $(`
            <div id="areaStart">
                <span id="testName"></span>
                <button style="float:right">Start</button>
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
                <button style="float:right">Submitted</button>
            </div>
        `);

    $('#DashboardContent').append(row);
    $('#testName').html(testName);
});

