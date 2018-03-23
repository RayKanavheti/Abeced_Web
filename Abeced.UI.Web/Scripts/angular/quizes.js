(function () {


    var QuizApp = angular.module('QuizApp', ['ngSanitize']);
    QuizApp.controller("testsController", testsController);
    QuizApp.controller("ListController", ListController);
    
    



    function ListController(FactListService) {
        var ViewModel = this;
        ViewModel.ActiveQuestion = 0;
        ViewModel.questionAnswered = questionAnswered;
        ViewModel.setActiveQuestion = setActiveQuestion;
        ViewModel.SubmitAnswer = SubmitAnswer; // executed after the user clicks View result button
        ViewModel.InputedAnswer ="";// its gonna take the value the user types as their answer
        ViewModel.SuccessPercentage; // the score in percentage a user will get after submitting an answer
        ViewModel.DisplayAnswer = false;
        ViewModel.WrongWords = "";
        ViewModel.CorrectWords = "";
        ViewModel.UnCompletedError = false;
        ViewModel.finalise = false;
        ViewModel.progress = "";
        ViewModel.progressTop = "";
        ViewModel.GoToReview = GoToReview;
        ViewModel.highlightedAnswer;
        ViewModel.Review = false;
        ViewModel.InputedAnswerArray;
        ViewModel.NumFacts;
        ViewModel.AverageScore = 0;
        ViewModel.ProgressIncrement = 0;
        ViewModel.CourseName = document.getElementById("CourseName").innerText;
        var numQuestionsAnswered = 0;
        FactListService.GetFactList().then(function (response) {
            ViewModel.data = response.data;// setting the ViewModel.data to our factList json File
            ViewModel.highlightedAnswer = [response.data.factList.length];
            ViewModel.SuccessPercentage = [response.data.factList.length];
            ViewModel.InputedAnswerArray = [response.data.factList.length];
            ViewModel.NumFacts = response.data.factList.length;
          
        });
       
        // also rep
        function setActiveQuestion(index) {
            if (index === undefined) { // if index is not passed then the following will be executed
                var stopLoop = false;
                var quizLength = ViewModel.data.factList.length - 1; // remember computers starts at zero
                // while stopLoop == false
                while (!stopLoop) {
                    // if  activeQuestion is equal to that active question but less than quizlength then increment active question with one else set active to zero and start all over to search for an unanswered question in the list
                    ViewModel.ActiveQuestion = ViewModel.ActiveQuestion < quizLength ? ++ViewModel.ActiveQuestion : 0;
                    if (ViewModel.ActiveQuestion === 0) {

                        ViewModel.UnCompletedError = true;
                    }

                    if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered === null) {
                        stopLoop = true; // if an unanswered question in the loop then the loops stops at this point
                    }

                }

            } else {

                ViewModel.ActiveQuestion = index;
            }
            

        }

        function questionAnswered() {
            //when the user press continue these values should be initialised
            
            ViewModel.WrongWords = "";
            ViewModel.CorrectWords = "";
            ViewModel.DisplayAnswer = false;
            ViewModel.InputedAnswer = "";

           
            
            var quizLength = ViewModel.data.factList.length; // finding the number of facts the user want to be tested upon
            
            // checking if the field for the inputed answer is no longer null 
            if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered !== null) {
                numQuestionsAnswered++; // pressing the continue button means a question has been answered since it checked if Answer is not equal to null hence incrementing the number of questions answered
                ViewModel.ProgressIncrement++;
                //if num of questions is greater or equal quizlength then it means all the questions have been answered
                if (numQuestionsAnswered >= quizLength) {
                    //finalise App
                    //However we have to be very sure that all the questions have been answered by implementing this for loop which checks if there is a question which is still equal to null
                    for (var i = 0; i < quizLength; i++) {

                        if (ViewModel.data.factList[i].Answered === null) {
                            setActiveQuestion(i) // call this function if there is unaswered question 
                            return; // stops the loop to stop further looping
                        }

                    }
                    ViewModel.UnCompletedError = false;
                    ViewModel.finalise = true; // this means all questions have been answered the user is ready to go the next step
                    return; // stop executing further so that the below setActiveQuestion function won't be executed.
                }

            }
            ViewModel.setActiveQuestion();

        }
        function SubmitAnswer() {
            ViewModel.DisplayAnswer = true;
            ViewModel.data.factList[ViewModel.ActiveQuestion].Answered = ViewModel.InputedAnswer; // keeping track of the inputed answer

            var Isnan = false;
            var answers = parseInt(ViewModel.data.factList[ViewModel.ActiveQuestion].answer);

            if (isNaN(answers)) {
                Isnan = true;
            } else {
                Isnan = false;
            }
            if (Isnan == true){
                    var correctAnswerLength = ViewModel.data.factList[ViewModel.ActiveQuestion].answer.length;
                    var InputedAnswerLength = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered.length;

                    var InputedAnswerSplit = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered.split(' ');
                    var correctAnswerSplit = ViewModel.data.factList[ViewModel.ActiveQuestion].answer.split(' ');

                    var wrongString = new Array();
                    var correctString = new Array();
                    var highlightedText = correctAnswerSplit;
                    // checking if the inputed answer is equal to the stipulated answer
                    if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered == ViewModel.data.factList[ViewModel.ActiveQuestion].answer || ViewModel.data.factList[ViewModel.ActiveQuestion].Answered !== ViewModel.data.factList[ViewModel.ActiveQuestion].answer) {
                        //ViewModel.InputedAnswer = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered;

                        for (var i = 0; i < correctAnswerSplit.length; i++) {

                            for (var j = 0; j < InputedAnswerSplit.length; j++) {
                                if (correctAnswerSplit[i] == InputedAnswerSplit[j]) {
                                    correctString.push(correctAnswerSplit[i]);

                                }

                            }
                        }



                        ViewModel.CorrectWords = correctString.join(' ');
                        wrongString = correctAnswerSplit.difference(ViewModel.CorrectWords);

                        for (var i = 0; i < highlightedText.length; i++) {
                            for (var j = 0; j < correctString.length; j++) {
                                if (highlightedText[i] == correctString[j]) {
                                    highlightedText[i] = highlightedText[i].replace(highlightedText[i], '<span class="correct">' + highlightedText[i] + '</span>');
                                }
                            }

                        }
                        var distance = levenshtein(ViewModel.data.factList[ViewModel.ActiveQuestion].Answered, ViewModel.data.factList[ViewModel.ActiveQuestion].answer);
                        var failurePercentage = Math.round((distance / correctAnswerLength) * 100);
                        ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = (100 - failurePercentage);
                        if (ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] < 0) {
                            if (ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] < -100) {
                                ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = 0;
                            } else {
                                ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = 100 + ViewModel.SuccessPercentage[ViewModel.ActiveQuestion];
                            }

                        }
                        ViewModel.InputedAnswerArray[ViewModel.ActiveQuestion] = ViewModel.InputedAnswer;

                        ViewModel.highlightedAnswer[ViewModel.ActiveQuestion] = highlightedText.join(' ');
                        ViewModel.WrongWords = wrongString.join(' ');
                    }
    
            } 
            // check if the inputed number is a number for a numeric question
            //first check if the answer for the question is a numeric
            if (Number.isInteger(answers) && Isnan == false) {
                var str = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered;
                var RemoveWhiteSpacesInputed = str.replace(/\s/g, '');
                var RemoveWhiteSpacesCorrectAnswer = ViewModel.data.factList[ViewModel.ActiveQuestion].answer.replace(/\s/g, '');
                if (RemoveWhiteSpacesInputed == RemoveWhiteSpacesCorrectAnswer) {
                    ViewModel.highlightedAnswer[ViewModel.ActiveQuestion] = '<span class="correctInt">' + ViewModel.data.factList[ViewModel.ActiveQuestion].answer + '<span>';
                    ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = 100;

                } else {
                    ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = 0;
                    ViewModel.highlightedAnswer[ViewModel.ActiveQuestion] = '<span class="correctInt">' + ViewModel.data.factList[ViewModel.ActiveQuestion].answer + '<span>';
                }


            }

            ViewModel.progress = {
                "width": ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] + "%",
            }
            ViewModel.progressTop = {
                "width": ViewModel.NumFacts,
            }

                
          
          
        }

        //function to compare between two arrays and filter out what is not found in the other array and creates another array for these new values.
        Array.prototype.difference = function (e) {
            return this.filter(function (i) { return e.indexOf(i) < 0; });
        };

        function GoToReview() {
            ViewModel.Review = true;
            var totalPercentage = 0;
            for (var i = 0; i < ViewModel.NumFacts; i++) {
                totalPercentage = totalPercentage + ViewModel.SuccessPercentage[i];
            }
            ViewModel.AverageScore = (totalPercentage / ViewModel.NumFacts);
            var CourseTitle; 

        }
    }


    // factory to get data from a Json return Action method defined in the flashcard controller.
      QuizApp.factory("FactListService", ['$http', function ($http) {
        var result = {};
        result.GetFactList = function () {
            return $http.get("/FlashCard/QuizesJSONData");

        }

        return result;

      }

    ]);



      function testsController(FactListService) {

          var ViewModel = this;
          ViewModel.ActiveQuestion = 0;
          ViewModel.questionAnswered = questionAnswered;
          ViewModel.setActiveQuestion = setActiveQuestion;
          ViewModel.ProgressIncrement = 0;
          ViewModel.NumFacts;
          ViewModel.InputedAnswer = "";// its gonna take the value the user types as their answer
          ViewModel.GoToReview = GoToReview;
          ViewModel.highlightedAnswer;
          ViewModel.Review = false;
          ViewModel.finalise = false;
          ViewModel.InputedAnswerArray;
          ViewModel.SuccessPercentage; // the score in percentage a user will get after submitting an answer
          ViewModel.WrongWords = "";
          ViewModel.CorrectWords = "";
          ViewModel.UnCompletedError = false;
          ViewModel.CourseName = document.getElementById("CourseName").innerText;


          var numQuestionsAnswered = 0;
          FactListService.GetFactList().then(function (response) {
              ViewModel.data = response.data;// setting the ViewModel.data to our factList json File
              ViewModel.NumFacts = response.data.factList.length;
              ViewModel.highlightedAnswer = [response.data.factList.length];
              ViewModel.InputedAnswerArray = [response.data.factList.length];
              ViewModel.SuccessPercentage = [response.data.factList.length];

          });


          function setActiveQuestion(index) {
              if (index === undefined) { // if index is not passed then the following will be executed
                  var stopLoop = false;
                  var quizLength = ViewModel.data.factList.length - 1; // remember computers starts at zero
                  // while stopLoop == false
                  while (!stopLoop) {
                      // if  activeQuestion is equal to that active question but less than quizlength then increment active question with one else set active to zero and start all over to search for an unanswered question in the list
                      ViewModel.ActiveQuestion = ViewModel.ActiveQuestion < quizLength ? ++ViewModel.ActiveQuestion : 0;
                      if (ViewModel.ActiveQuestion === 0) {

                          ViewModel.UnCompletedError = true;
                      }

                      if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered === null) {
                          stopLoop = true; // if an unanswered question in the loop then the loops stops at this point
                      }

                  }

              } else {

                  ViewModel.ActiveQuestion = index;
              }


          }


          function questionAnswered() {
              //when the user press continue these values should be initialised

              ViewModel.WrongWords = "";
              ViewModel.CorrectWords = "";
              ViewModel.DisplayAnswer = false;
              











              ViewModel.data.factList[ViewModel.ActiveQuestion].Answered = ViewModel.InputedAnswer; // keeping track of the inputed answer
              ViewModel.InputedAnswerArray[ViewModel.ActiveQuestion] = ViewModel.InputedAnswer;

              var Isnan = false;
              var answers = parseInt(ViewModel.data.factList[ViewModel.ActiveQuestion].answer);

              if (isNaN(answers)) {
                  Isnan = true;
              } else {
                  Isnan = false;
              }
              if (Isnan == true) {
                  var correctAnswerLength = ViewModel.data.factList[ViewModel.ActiveQuestion].answer.length;
                  var InputedAnswerLength = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered.length;

                  var InputedAnswerSplit = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered.split(' ');
                  var correctAnswerSplit = ViewModel.data.factList[ViewModel.ActiveQuestion].answer.split(' ');

                  var wrongString = new Array();
                  var correctString = new Array();
                  var highlightedText = correctAnswerSplit;
                  // checking if the inputed answer is equal to the stipulated answer
                  if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered == ViewModel.data.factList[ViewModel.ActiveQuestion].answer || ViewModel.data.factList[ViewModel.ActiveQuestion].Answered !== ViewModel.data.factList[ViewModel.ActiveQuestion].answer) {
                      //ViewModel.InputedAnswer = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered;

                      for (var i = 0; i < correctAnswerSplit.length; i++) {

                          for (var j = 0; j < InputedAnswerSplit.length; j++) {
                              if (correctAnswerSplit[i] == InputedAnswerSplit[j]) {
                                  correctString.push(correctAnswerSplit[i]);

                              }

                          }
                      }



                      ViewModel.CorrectWords = correctString.join(' ');
                      wrongString = correctAnswerSplit.difference(ViewModel.CorrectWords);

                      for (var i = 0; i < highlightedText.length; i++) {
                          for (var j = 0; j < correctString.length; j++) {
                              if (highlightedText[i] == correctString[j]) {
                                  highlightedText[i] = highlightedText[i].replace(highlightedText[i], '<span class="correct">' + highlightedText[i] + '</span>');
                              }
                          }

                      }
                      var distance = levenshtein(ViewModel.data.factList[ViewModel.ActiveQuestion].Answered, ViewModel.data.factList[ViewModel.ActiveQuestion].answer);
                      var failurePercentage = Math.round((distance / correctAnswerLength) * 100);
                      ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = (100 - failurePercentage);
                      if (ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] < 0) {
                          if (ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] < -100) {
                              ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = 0;
                          } else {
                              ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = 100 + ViewModel.SuccessPercentage[ViewModel.ActiveQuestion];
                          }

                      }
                      
                      ViewModel.highlightedAnswer[ViewModel.ActiveQuestion] = highlightedText.join(' ');
                      ViewModel.WrongWords = wrongString.join(' ');
                  }

              }
              // check if the inputed number is a number for a numeric question
              //first check if the answer for the question is a numeric
              if (Number.isInteger(answers) && Isnan == false) {
                  var str = ViewModel.data.factList[ViewModel.ActiveQuestion].Answered;
                  var RemoveWhiteSpacesInputed = str.replace(/\s/g, '');
                  var RemoveWhiteSpacesCorrectAnswer = ViewModel.data.factList[ViewModel.ActiveQuestion].answer.replace(/\s/g, '');
                  if (RemoveWhiteSpacesInputed == RemoveWhiteSpacesCorrectAnswer) {
                      ViewModel.highlightedAnswer[ViewModel.ActiveQuestion] = '<span class="correctInt">' + ViewModel.data.factList[ViewModel.ActiveQuestion].answer + '<span>';
                      ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = 100;

                  } else {
                      ViewModel.SuccessPercentage[ViewModel.ActiveQuestion] = 0;
                      ViewModel.highlightedAnswer[ViewModel.ActiveQuestion] = '<span class="correctInt">' + ViewModel.data.factList[ViewModel.ActiveQuestion].answer + '<span>';
                  }


              }


              ViewModel.progressTop = {
                  "width": ViewModel.NumFacts,
              }










              var quizLength = ViewModel.data.factList.length; // finding the number of facts the user want to be tested upon

              // checking if the field for the inputed answer is no longer null 
              if (ViewModel.data.factList[ViewModel.ActiveQuestion].Answered !== null) {
                  numQuestionsAnswered++; // pressing the continue button means a question has been answered since it checked if Answer is not equal to null hence incrementing the number of questions answered
                  ViewModel.ProgressIncrement++;
                  //if num of questions is greater or equal quizlength then it means all the questions have been answered
                  if (numQuestionsAnswered >= quizLength) {
                      //finalise App
                      //However we have to be very sure that all the questions have been answered by implementing this for loop which checks if there is a question which is still equal to null
                      for (var i = 0; i < quizLength; i++) {

                          if (ViewModel.data.factList[i].Answered === null) {
                              setActiveQuestion(i) // call this function if there is unaswered question 
                              return; // stops the loop to stop further looping
                          }

                      }
                      ViewModel.UnCompletedError = false;
                      ViewModel.finalise = true; // this means all questions have been answered the user is ready to go the next step
                      return; // stop executing further so that the below setActiveQuestion function won't be executed.
                  }

              }
              ViewModel.setActiveQuestion();
              ViewModel.InputedAnswer = "";
          }

         



          function GoToReview() {
              ViewModel.Review = true;
              var totalPercentage = 0;
              for (var i = 0; i < ViewModel.NumFacts; i++) {
                  totalPercentage = totalPercentage + ViewModel.SuccessPercentage[i];
              }
              ViewModel.AverageScore = (totalPercentage / ViewModel.NumFacts);
              var CourseTitle;

          }
          //function to compare between two arrays and filter out what is not found in the other array and creates another array for these new values.
          Array.prototype.difference = function (e) {
              return this.filter(function (i) { return e.indexOf(i) < 0; });
          };

      }


})();
