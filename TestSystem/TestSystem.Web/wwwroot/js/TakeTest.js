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
    $(".example").TimeCircles({
        time: {
            Days: { color: "#FA8258", show: false },
            Hours: { color: "#FA8258" },
            Minutes: { color: "#FA8258" },
            Seconds: { color: "#FA8258" }
        }
    }).addListener(countdownComplete);

    function countdownComplete(unit, value, total) {
        if (total <= 0) {
            $(this).fadeOut('slow').replaceWith("<h2>Time's Up!</h2>");
            $('#submit-test-btn').click()
        }
    }
});



const showModal = (function () {
    $('#exampleModalCenter').on('shown.bs.modal', function () {
        $('#myInput').trigger('focus')
    })
});





