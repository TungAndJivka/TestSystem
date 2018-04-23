var questionTemplate = `
<div id="question-{{q_id}}" class="question-container">
    <div class="panel-heading">
        <a data-toggle="collapse" href="#collapse-{{q_id}}">Question {{q_number}}</a>
        <button type="button" class="delete-question">X</button>
    </div>

    <div id="collapse-{{q_id}}" class="panel-collapse collapse">
        <div class="panel-body">
            <div>Description</div>
            <input type="text" id="Questions_{{q_id}}__Body" name="Questions[{{q_id}}].Body" class="form-control input-lg value=""></input>
        </div>
        <div class="answers-container"></div>
        <div class="panel-body">
            <button class="add-answer" name="collapse-{{q_id}}" type="button">Add New Answer</button>
        </div>
    </div>
</div>`;
var answerTemplate = `
<div id="question-{{q_id}}-answer-{{a_id}}">
    <div class="answer-heading">
        <div>Answer {{a_number}}</div>
        <input id="Questions_{{q_id}}__Answers_{{a_id}}__IsCorrect" name="radio-{{q_id}}" type="radio" value="true" autocomplete="off">
        <button class="delete-answer" type="button">X</button>        
    </div>
    <div class="answer-body">        
        <input id="Questions_{{q_id}}__Answers_{{a_id}}__Content" name="Questions[{{q_id}}].Answers[{{a_id}}].Content" class="answer-content summernote form-control"></input>
    </div>    
</div>`;

$(function () {

    var deleteQuestionClickEvent = $('#questions-container #questions-body').on('click', '.delete-question', function () {
        var questionId = parseInt(this.parentNode.parentNode.id.split('-')[1]);

        $(this.parentNode.parentNode).remove();

        var nextQuestions = $('#questions-body .question-container')
            .filter(function () {
                var nextQuestionId = parseInt(this.id.split('-')[1]);
                return nextQuestionId > questionId;
            })
            .toArray();

        nextQuestions.forEach(function (question) {
            var newQuestionId = parseInt(question.id.split('-')[1]) - 1;

            $(question).attr('id', `question-${newQuestionId}`);
            $(`#question-${newQuestionId} a`).attr('href', `#collapse-${newQuestionId}`);
            $(`#question-${newQuestionId} a h4`).text(`Question ${newQuestionId + 1}`);
            $(`#question-${newQuestionId} .panel-collapse`).attr('id', `collapse-${newQuestionId}`);
            $(`#question-${newQuestionId} .question-description input`).attr('id', `Questions_${newQuestionId}__Body`);
            $(`#question-${newQuestionId} .question-description input`).attr('name', `Questions[${newQuestionId}].Body`);
            $(`#question-${newQuestionId} .add-answer`).attr('name', `collapse-${newQuestionId}`);

            var answers = $(`#Question-${newQuestionId} .answer-container`).toArray();

            answers.forEach(function (answer) {
                var answerId = parseInt(answer.id.split('-')[3]) - 1;
                $(`#question-${newQuestionId} .answer-container .answer-content`).attr('id', `Questions_${newQuestionId}__Answers_${answerId}__Content`);

                $(`#question-${newQuestionId} .answer-container .answer-content`).attr('id', `Questions_${newQuestionId}__Answers_${answerId}__Content`);
                $(`#question-${newQuestionId} .answer-container .answer-content`).attr('name', `Questions[${newQuestionId}].Answers[${answerId}].Content`);
            });

        });
    });

    var addQuestionClickEvent = $('#questions-container #add-question').on('click', function () {
        var newQuestionId = $('#questions-body .panel-heading').length;

        $('#questions-container #questions-body')
            .append(questionTemplate
                .replace(/\{\{\q_id\}\}/g, newQuestionId)
                .replace(/\{\{\q_number\}\}/g, newQuestionId + 1));
    });


    var addAnswerClickEvent = $('#questions-container #questions-body').on('click', '.add-answer', function () {
        var questionId = this.name;
        var questionNumber = questionId.split('-')[1];
        var newAnswerNumber = $(`#${questionId} .answers-container .answer-container`).length;

        $(`#${questionId} .answers-container`)
            .append(answerTemplate
                .replace(/\{\{\q_id\}\}/g, questionNumber)
                .replace(/\{\{\a_id\}\}/g, newAnswerNumber)
                .replace(/\{\{\a_number\}\}/g, newAnswerNumber + 1));


        var answerRadioButtons = $(`#question-${questionNumber} .answers-container .answer-container input`)
            .filter(function () {
                return this.type === 'radio';
            });

        var hasCheckedRadioButton = answerRadioButtons
            .is(function () {
                return $(this).prop('checked') === true;
            });

        if (!hasCheckedRadioButton) {
            answerRadioButtons.first().prop('checked', true);
        }
    });



    var deleteAnswerClickEvent = $('#questions-container #questions-body').on('click', '.delete-answer', function () {
        var answerId = this.parentNode.id.split('-');
        var questionId = parseInt(answerId[1]);
        var answerNumber = parseInt(answerId[3]);

        $(this.parentNode).remove();

        var nextAnswers = $(`#questions-body #question-${questionId} .answer-container`)
            .filter(function () {
                var nextAnswerNumber = parseInt(this.id.split('-')[3]);
                return nextAnswerNumber > answerNumber;
            })
            .toArray();

        nextAnswers.forEach(function (a) {
            var newAnswerNumber = parseInt(a.id.split('-')[3]) - 1;

            $(a).attr('id', `question-${questionId}-answer-${newAnswerNumber}`);
            $(`#question-${questionId}-answer-${newAnswerNumber} h3`).text(`Answer ${newAnswerNumber + 1}`);
            $(`#question-${questionId}-answer-${newAnswerNumber} .answer-content`).attr('id', `Questions_${questionId}__Answers_${newAnswerNumber}__Content`);
            $(`#question-${questionId}-answer-${newAnswerNumber} .answer-content`).attr('name', `Questions[${questionId}].Answers[${newAnswerNumber}].Content`);
        });

        var answerRadioButtons = $(`#question-${questionId} .answers-container .answer-container input`)
            .filter(function () {
                return this.type === 'radio';
            });

        var hasCheckedRadioButton = answerRadioButtons
            .is(function () {
                return $(this).prop('checked') === true;
            });

        if (!hasCheckedRadioButton && answerRadioButtons.first() !== undefined) {
            answerRadioButtons.first().prop('checked', true);
        }
    });

});