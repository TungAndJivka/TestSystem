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
            <textarea id="Questions_{{q_id}}__Description" name="Questions[{{q_id}}].Description" class="summernote form-control input-lg" value=""></textarea>
        </div>
        <div class="answers-container">
            <div id="question-{{q_id}}-answer-0" class="answer-container">
                <div class="answer-heading">
                    <div class="answerNumber" id="answerNumber">Answer 1</div>
                    <input clas id="Questions_{{q_id}}__Answers_0__IsCorrect" class="answer-is-correct create-test-radio" name="radio-{{q_id}}" type="radio" value="true" autocomplete="off">
                    <button class="delete-answer create-test-btn-red" type="button">
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>        
                </div>
                <div class="answer-body">        
                    <textarea id="Questions_{{q_id}}__Answers_0__Content" name="Questions[{{q_id}}].Answers[0].Content" class="summernote answer-content"></textarea>
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
                    <textarea id="Questions_{{q_id}}__Answers_1__Content" name="Questions[{{q_id}}].Answers[1].Content" class="summernote answer-content"></textarea>
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
                    <textarea id="Questions_{{q_id}}__Answers_2__Content" name="Questions[{{q_id}}].Answers[2].Content" class="summernote answer-content"></textarea>
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
        <h4 class="w-100 p-3">You need to add Questions to your Test</h4>
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

        if ($('#questions-body .question-container').length === 0) {
            $('#questions-body').html(noQuestionFrame);
        }
        else {
            nextQuestions.forEach(function (question) {
                var newQuestionId = parseInt(question.id.split('-')[1]) - 1;

                $(question).attr('id', `question-${newQuestionId}`);
                $(`#question-${newQuestionId} a`).attr('href', `#collapse-${newQuestionId}`);
                $(`#question-${newQuestionId} #questionNumber`).text(`Question ${newQuestionId + 1}`);
                $(`#questi-on-${newQuestionId} .panel-collapse`).attr('id', `collapse-${newQuestionId}`);
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
        }
        
    });


   

    var addQuestionClickEvent = $('#questions-container #add-question').on('click', function () {

        var newQuestionId = $('#questions-body .question-container').length;

        collapseQuestions();

        var questionHtml = questionTemplate
                .replace(/\{\{\q_id\}\}/g, newQuestionId)
                .replace(/\{\{\q_number\}\}/g, newQuestionId + 1);

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

        $(`#Questions_${questionNumber}__Answers_${newAnswerNumber}__Content`).summernote(answerSummernoteConfig);
    });



    var deleteAnswerClickEvent = $('#questions-container #questions-body').on('click', '.delete-answer', function () {
        var questionName = this.name;
        var answerId = this.parentNode.parentNode.id.split('-');
        var questionId = parseInt(answerId[1]);
        var answerNumber = parseInt(answerId[3]);
        var answerCount = $(`#question-${questionId} .answers-container .answer-container`).length
        if (answerCount > 2) {
            $(this.parentNode.parentNode).remove();

            var nextAnswers = $(`#questions-body #question-${questionId} .answer-container`)
                .filter(function () {
                    var nextAnswerNumber = parseInt(this.id.split('-')[3]);
                    return nextAnswerNumber > answerNumber;
                })
                .toArray();

            nextAnswers.forEach(function (a) {
                var newAnswerNumber = parseInt(a.id.split('-')[3]) - 1;

                $(a).attr('id', `question-${questionId}-answer-${newAnswerNumber}`);
                $(`#question-${questionId}-answer-${newAnswerNumber} #answerNumber`).text(`Answer ${newAnswerNumber + 1}`);
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
        }
        else {
            alert("A question can't have less than 2 answers")
        }
        
    });

    var createTestClickEvent = $('.create-test').on('click', function (event) {
        //event.preventDefault();
        //var form = $(this).closest('form').serialize();

        //console.log(form);

        //debugger;
        $('#questions-container #questions-body .answer-is-correct')
            .toArray()
            .forEach(function (rButton) {
                var params = $(rButton).closest('.answer-container')[0].id.split('-');
                var questionId = params[1];
                var answerNumber = params[3];

                $(rButton).attr('name', `Questions[${questionId}].Answers[${answerNumber}].IsCorrect`);
            });
    });


    var questionSummernoteConfig = {
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

    var answerSummernoteConfig = {
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

    var initializeSummernote = function () {
        $('.summernote')
            .toArray()
            .forEach(function (textarea) {
                var text = textarea.textContent;
                $(textarea).summernote(answerSummernoteConfig);
                textarea.textContent = text;
            });
    };

    initializeSummernote();

    var collapseQuestions = function collapse() {
        $('.collapse').collapse('hide');
    }
});




$(document).ready(function () {
    $('#summernote').summernote();
});

