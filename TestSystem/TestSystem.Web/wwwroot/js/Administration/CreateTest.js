$(function () {

    let addQuestionClickEvent = $('#questions-container #add-question').on('click', function () {
        let newQuestionId = $('#questions-body .question-container').length;
        collapseQuestions();

        let questionHtml = createQuestionTemplate(newQuestionId)

        if (newQuestionId === 0) {
            $('#questions-container #questions-body')
                .html(questionHtml);
        }
        else {
            $('#questions-container #questions-body')
                .append(questionHtml);
        }
        $(`#Questions_${newQuestionId}__Description`).summernote(questionSummernoteConfig);
        $(`#question-${newQuestionId} .answer-content`).summernote(answerSummernoteConfig);
    });

    let deleteQuestionClickEvent = $('#questions-container #questions-body').on('click', '.delete-question', function () {
        let questionId = parseInt(this.parentNode.parentNode.id.split('-')[1]);

        $(this.parentNode.parentNode).remove();

        let nextQuestions = $('#questions-body .question-container')
            .filter(function () {
                let nextQuestionId = parseInt(this.id.split('-')[1]);
                return nextQuestionId > questionId;
            })
            .toArray();

        if ($('#questions-body .question-container').length === 0) {
            $('#questions-body').html(noQuestionFrame);
        }
        else {
            nextQuestions.forEach(function (question) {
                let newQuestionId = parseInt(question.id.split('-')[1]) - 1;
                let nextQuestion = $(question);

                $(question).attr('id', `question-${newQuestionId}`);
                nextQuestion.find('a').attr('href', `#collapse-${newQuestionId}`);
                nextQuestion.find(' .panel-collapse').attr('id', `collapse-${newQuestionId}`);
                nextQuestion.find(' .question-description').attr('id', `Questions_${newQuestionId}__Description`).attr('name', `Questions[${newQuestionId}].Description`);
                nextQuestion.find('.add-answer').attr('name', `collapse-${newQuestionId}`);
                nextQuestion.find(' #questionNumber').text(`Question ${ newQuestionId + 1 }`);

                let answers = $(`#question-${newQuestionId} .answer-container`).toArray();

                answers.forEach(function (answer) {
                    let answerId = parseInt(answer.id.split('-')[3]);

                    let nextQuestionAnswer = $(answer)                    

                    nextQuestionAnswer.attr('id', `question-${newQuestionId}-answer-${answerId}`)

                    nextQuestionAnswer.find('.answer-is-correct').attr('id', `Questions_${newQuestionId}__Answers_${answerId}__IsCorrect`);

                    nextQuestionAnswer.find('.answer-content').attr('id', `Questions_${newQuestionId}__Answers_${answerId}__Content`);
                    nextQuestionAnswer.find('.answer-content').attr('name', `Questions[${newQuestionId}].Answers[${answerId}].Content`);
                });

            });
        }

    });
    let addAnswerClickEvent = $('#questions-container #questions-body').on('click', '.add-answer', function () {
        let questionId = this.name;
        let questionNumber = questionId.split('-')[1];
        let newAnswerNumber = $(`#${questionId} .answers-container .answer-container`).length;

        $(`#${questionId} .answers-container`)
            .append(answerTemplate
                .replace(/\{\{\q_id\}\}/g, questionNumber)
                .replace(/\{\{\a_id\}\}/g, newAnswerNumber)
                .replace(/\{\{\a_number\}\}/g, newAnswerNumber + 1));


        let answerRadioButtons = $(`#question-${questionNumber} .answers-container .answer-container input`)
            .filter(function () {
                return this.type === 'radio';
            });

        let hasCheckedRadioButton = answerRadioButtons
            .is(function () {
                return $(this).prop('checked') === true;
            });

        if (!hasCheckedRadioButton) {
            answerRadioButtons.first().prop('checked', true);
        }

        $(`#Questions_${questionNumber}__Answers_${newAnswerNumber}__Content`).summernote(answerSummernoteConfig);
    });



    let deleteAnswerClickEvent = $('#questions-container #questions-body').on('click', '.delete-answer', function () {
        let questionName = this.name;
        let answerId = this.parentNode.parentNode.id.split('-');
        let questionId = parseInt(answerId[1]);
        let answerNumber = parseInt(answerId[3]);
        let answerCount = $(`#question-${questionId} .answers-container .answer-container`).length
        if (answerCount > 2) {
            $(this.parentNode.parentNode).remove();

            let nextAnswers = $(`#questions-body #question-${questionId} .answer-container`)
                .filter(function () {
                    let nextAnswerNumber = parseInt(this.id.split('-')[3]);
                    return nextAnswerNumber > answerNumber;
                })
                .toArray();

            nextAnswers.forEach(function (a) {
                let newAnswerNumber = parseInt(a.id.split('-')[3]) - 1;

                $(a).attr('id', `question-${questionId}-answer-${newAnswerNumber}`);
                $(`#question-${questionId}-answer-${newAnswerNumber} #answerNumber`).text(`Answer ${newAnswerNumber + 1}`);
                $(`#question-${questionId}-answer-${newAnswerNumber} .answer-content`).attr('id', `Questions_${questionId}__Answers_${newAnswerNumber}__Content`);
                $(`#question-${questionId}-answer-${newAnswerNumber} .answer-content`).attr('name', `Questions[${questionId}].Answers[${newAnswerNumber}].Content`);
            });

            let answerRadioButtons = $(`#question-${questionId} .answers-container .answer-container input`)
                .filter(function () {
                    return this.type === 'radio';
                });

            let hasCheckedRadioButton = answerRadioButtons
                .is(function () {
                    return $(this).prop('checked') === true;
                });

            if (!hasCheckedRadioButton && answerRadioButtons.first() !== undefined) {
                answerRadioButtons.first().prop('checked', true);
            }
        }
        else {
            alert("A question can't have less than 2 answers")
        }

    });

    let createTestClickEvent = $('.create-test').on('click', function (event) {
        //event.preventDefault();
        //let form = $(this).closest('form').serialize();

        //console.log(form);

        //debugger;
        $('#questions-container #questions-body .answer-is-correct')
            .toArray()
            .forEach(function (rButton) {
                let params = $(rButton).closest('.answer-container')[0].id.split('-');
                let questionId = params[1];
                let answerNumber = params[3];

                $(rButton).attr('name', `Questions[${questionId}].Answers[${answerNumber}].IsCorrect`);
            });
    });


    let questionSummernoteConfig = {
        placeholder: 'Add question description here...',
        height: 200,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            ['para', ['ul', 'ol', 'paragraph']]
        ],
        disableResizeEditor: true
    };

    let answerSummernoteConfig = {
        placeholder: 'Add answer content here...',
        height: 150,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            ['para', ['ul', 'ol', 'paragraph']]
        ],
        disableResizeEditor: true
    };

    let initializeSummernote = function () {
        $('.summernote')
            .toArray()
            .forEach(function (textarea) {
                let text = textarea.textContent;
                $(textarea).summernote(answerSummernoteConfig);
                textarea.textContent = text;
            });
    };

    initializeSummernote();

    let collapseQuestions = function collapse() {
        $('.collapse').collapse('hide');

    }
    function createQuestionTemplate(questionId) {
        return questionTemplate
            .replace(/\{\{\q_id\}\}/g, questionId)
            .replace(/\{\{\q_number\}\}/g, questionId + 1)
    }



    let questionTemplate = `
<div id="question-{{q_id}}" class="question-container">
    <div class="panel-heading">
        <a class="questionNumber" id="questionNumber" data-toggle="collapse" href="#collapse-{{q_id}}">Question {{q_number}}</a>
        <button type="button" class="delete-question create-test-btn-red">
            <span class="glyphicon glyphicon-remove"></span>
        </button>
    </div>

    <div id="collapse-{{q_id}}" class="panel-collapse collapse in">
        <div class="panel-body">
            <div>Description</div>
            <textarea id="Questions_{{q_id}}__Description" name="Questions[{{q_id}}].Description" class="question-description summernote form-control input-lg" value=""></textarea>
        </div>
        <div class="answers-container">
            <div id="question-{{q_id}}-answer-0" class="answer-container">
                <div class="answer-heading">
                    <div class="answerNumber" id="answerNumber">Answer 1</div>
                    <input id="Questions_{{q_id}}__Answers_0__IsCorrect" class="answer-is-correct create-test-radio" name="radio-{{q_id}}" type="radio" value="true" autocomplete="off">
                    <button class="delete-answer create-test-btn-red" type="button">
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>        
                </div>
                <div class="answer-body">        
                    <textarea id="Questions_{{q_id}}__Answers_0__Content" name="Questions[{{q_id}}].Answers[0].Content" class="answer-content summernote "></textarea>
                </div>    
            </div>

            <div id="question-{{q_id}}-answer-1" class="answer-container">
                <div class="answer-heading">
                    <div class="answerNumber" id="answerNumber">Answer 2</div>
                    <input clas id="Questions_{{q_id}}__Answers_1__IsCorrect" class="answer-is-correct create-test-radio" name="radio-{{q_id}}" type="radio" value="true" autocomplete="off">
                    <button class="delete-answer create-test-btn-red" type="button">
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>        
                </div>
                <div class="answer-body">        
                    <textarea id="Questions_{{q_id}}__Answers_1__Content" name="Questions[{{q_id}}].Answers[1].Content" class="answer-content summernote "></textarea>
                </div>    
            </div>

            <div id="question-{{q_id}}-answer-2" class="answer-container">
                <div class="answer-heading">
                    <div class="answerNumber" id="answerNumber">Answer 3</div>
                    <input clas id="Questions_{{q_id}}__Answers_2__IsCorrect" class="answer-is-correct create-test-radio" name="radio-{{q_id}}" type="radio" value="true" autocomplete="off">
                    <button class="delete-answer create-test-btn-red" type="button">
                        <span class="glyphicon glyphicon-remove"></span> 
                    </button>        
                </div>
                <div class="answer-body">        
                    <textarea id="Questions_{{q_id}}__Answers_2__Content" name="Questions[{{q_id}}].Answers[2].Content" class="answer-content summernote"></textarea>
                </div>    
            </div>

        </div>
        <div class="panel-body">
            <button class="add-answer create-test-btn-green" name="collapse-{{q_id}}" type="button">
                <span class="glyphicon glyphicon-plus"></span>
                Answer
            </button>
        </div>
    </div>
</div>`;

    let answerTemplate = `
<div id="question-{{q_id}}-answer-{{a_id}}" class="answer-container">
    <div class="answer-heading">
        <div class="answerNumber" id="answerNumber">Answer {{a_number}}</div>
        <input id="Questions_{{q_id}}__Answers_{{a_id}}__IsCorrect" class="answer-is-correct create-test-radio" name="radio-{{q_id}}" type="radio" value="true" autocomplete="off">
        <button class="delete-answer create-test-btn-red" type="button">
            <span class="glyphicon glyphicon-remove"></span>
        </button>        
    </div>
    <div class="answer-body">        
        <textarea id="Questions_{{q_id}}__Answers_{{a_id}}__Content" name="Questions[{{q_id}}].Answers[{{a_id}}].Content" class="summernote answer-content"></textarea>
    </div>    
</div>`;

    let noQuestionFrame =
        `<div>
        <h4 class="w-100 p-3">Please add queations to your test.</h4>
     </div>`;

});




$(document).ready(function () {
    $('#summernote').summernote();
});

