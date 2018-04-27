$(function () {
    $('#test-submit-btn').on('click', function () {
        $('.radio-btn-to-change')
            .toArray()
            .forEach(function (rButton) {
                var inputId = $(rButton).closest('.funkyradio').find('> input')[0].id;

                var separators = ['__', '_'];
                var params = inputId.split(new RegExp(separators.join('|'), 'g'));

                var questionId = params[1];
                var answerNumber = params[3];

                $(rButton).attr('id', `Questions_${questionId}__Answers_${answerNumber}_SelectedAnswerId`)
                $(rButton).attr('name', `Questions[${questionId}].Answers[${answerNumber}].SelectedAnswerId`)
            })
    })

    //window.onbeforeunload = function () {
    //    return ""
    //}

    if (window.performance) {
        console.info("window.performance works fine on this browser");
    }
});

$(function () {

    let time = ((parseFloat($('#time').html().trim())) * 60 * 1000.0);

    setTimeout(
        function () {
            $('#submit-test-btn').click();
        }, time);
});

$(function () {
    let mins = parseFloat($('#time').html().trim());

    var target_date = new Date().getTime() + (1000 * 3600 * mins / 60.0);
    //var target_date = new Date().getTime() + (1000 * 3600 * 48); // set the countdown date
    var days, hours, minutes, seconds; // variables for time units

    var countdown = document.getElementById("tiles"); // get tag element

    getCountdown();

    setInterval(function () { getCountdown(); }, 1000);

    function getCountdown() {

        // find the amount of "seconds" between now and target
        var current_date = new Date().getTime();
        var seconds_left = (target_date - current_date) / 1000;

        days = pad(parseInt(seconds_left / 86400));
        seconds_left = seconds_left % 86400;

        hours = pad(parseInt(seconds_left / 3600));
        seconds_left = seconds_left % 3600;

        minutes = pad(parseInt(seconds_left / 60));
        seconds = pad(parseInt(seconds_left % 60));

        // format countdown string + set tag value
        countdown.innerHTML = "</span><span>" + hours + "</span><span>" + minutes + "</span><span>" + seconds + "</span>";
    }

    function pad(n) {
        return (n < 10 ? '0' : '') + n;
    }
});





