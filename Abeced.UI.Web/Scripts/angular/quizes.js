(function () {
    var QuizApp = angular.module("QuizApp",['ngSanitize']);
    QuizApp.controller("ListController", ListController);


    function ListController(FactListService) {
        var ViewModel = this;
        ViewModel.ActiveQuestion = 0;
        ViewModel.questionAnswered = questionAnswered;
        ViewModel.setActiveQuestion = setActiveQuestion;
        ViewModel.SubmitAnswer = SubmitAnswer;
        ViewModel.InputedAnswer = "";
        ViewModel.SuccessPercentage = 0;
        ViewModel.DisplayAnswer = false;
        ViewModel.WrongWords = "";
        ViewModel.CorrectWords = ""
        ViewModel.highlightedAnswer = "";
        ViewModel.UnCompletedError = false;
        ViewModel.finalise = false;
        ViewModel.progress = "";


        var numQuestionsAnswered = 0;
        FactListService.GetFactList().then(function (response) {
            ViewModel.data = response.data;

        });

        function setActiveQuestion(index) {
            if (index === undefined) {
                var stopLoop = false;
                var quizLength = ViewModel.data.factList.length - 1;
                while (!stopLoop) {
                    ViewModel.ActiveQuestion = ViewModel.ActiveQuestion < quizLength ? ++ViewModel.ActiveQuestion : 0;
                    if (ViewModel.ActiveQuestion === 0) {

                        ViewModel.UnCompletedError = true;
                    }

                    if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered === null) {
                        stopLoop = true;
                    }

                }

            } else {

                ViewModel.ActiveQuestion = index;
            }
            

        }

        function questionAnswered() {
            //when the user press continue these values should be initialised
            ViewModel.InputedAnswer = "";
            ViewModel.SuccessPercentage = 0;
            ViewModel.WrongWords = "";
            ViewModel.CorrectWords = "";
            ViewModel.DisplayAnswer = false;

            var quizLength = ViewModel.data.factList.length;
            if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered !== null) {
                numQuestionsAnswered++;
                if (numQuestionsAnswered >= quizLength) {
                    //finalise App
                    for (var i = 0; i < quizLength; i++) {
                        if (ViewModel.data.factList[i].Answered === null) {
                            setActiveQuestion(i)
                            return;
                        }

                    }
                    ViewModel.UnCompletedError = false;
                    ViewModel.finalise = true;
                    return;
                }

            }
            ViewModel.setActiveQuestion();

        }
        function SubmitAnswer() {
            ViewModel.DisplayAnswer = true;
            ViewModel.data.factList[ViewModel.ActiveQuestion].Answered = ViewModel.InputedAnswer; // keeping track of the inputed answer

            var correctAnswerLength = ViewModel.data.factList[ViewModel.ActiveQuestion].answer.length;
            var InputedAnswerLength = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered.length;

            var InputedAnswerSplit = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered.split(' ');
            var correctAnswerSplit = ViewModel.data.factList[ViewModel.ActiveQuestion].answer.split(' ');

            var wrongString = new Array();
            var correctString = new Array();
            var highlightedText = correctAnswerSplit;
            // checking if the inputed answer is equal to the stipulated answer
            if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered == ViewModel.data.factList[ViewModel.ActiveQuestion].answer || ViewModel.data.factList[ViewModel.ActiveQuestion].Answered !== ViewModel.data.factList[ViewModel.ActiveQuestion].answer ) {
                //ViewModel.InputedAnswer = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered;
                
                for (var i = 0; i < correctAnswerSplit.length; i++) {
                   
                    for (var j = 0; j < InputedAnswerSplit.length; j++) {
                        if (correctAnswerSplit[i] == InputedAnswerSplit[j] ) {
                            correctString.push(correctAnswerSplit[i]); 

                        }
                       
                    }
                }


                var distance = levenshtein(ViewModel.data.factList[ViewModel.ActiveQuestion].Answered, ViewModel.data.factList[ViewModel.ActiveQuestion].answer);
                var failurePercentage = Math.round((distance / correctAnswerLength) * 100);
                ViewModel.SuccessPercentage = (100 - failurePercentage);
                ViewModel.progress = {
                    "width": ViewModel.SuccessPercentage +"%",
                }

               

                ViewModel.CorrectWords = correctString.join(' ');
                wrongString = correctAnswerSplit.difference(ViewModel.CorrectWords);

                for (var i = 0; i < highlightedText.length; i++) {
                    for (var j = 0; j < correctString.length; j++) {
                        if (highlightedText[i] == correctString[j]) {
                            highlightedText[i] = highlightedText[i].replace(highlightedText[i], '<span class="correct">' + highlightedText[i] +'</span>');
                        }
                    }

                }
                ViewModel.highlightedAnswer = highlightedText.join(' ');
                ViewModel.WrongWords = wrongString.join(' ');

                
                

            } //else {
                //ViewModel.InputedAnswer = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered;


                //levenstein distance

                //alertify.alert("Great !!! You got it correct");
           // }


        }

        Array.prototype.difference = function (e) {
            return this.filter(function (i) { return e.indexOf(i) < 0; });
        };
        

    }

    QuizApp.factory("FactListService", ['$http', function ($http) {
        var result = {};
        result.GetFactList = function () {
            return $http.get("/FlashCard/QuizesJSONData");

        }

        return result;

    }
    ]);

})();
