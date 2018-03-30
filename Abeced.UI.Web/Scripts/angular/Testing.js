(function () {
    var TestsApp = angular.module("TestsApp", []);
    TestsApp.controller("TestsController", testsController);


    function testsController() {

                var ViewModel = this;
                ViewModel.ActiveQuestion = 0;
                ViewModel.questionAnswered = questionAnswered;
                ViewModel.setActiveQuestion = setActiveQuestion;
                ViewModel.ProgressIncrement = 0;
                ViewModel.NumFacts;
                ViewModel.InputedAnswer = "";// its gonna take the value the user types as their answer
                ViewModel.Test = "how are you everypone";

                var numQuestionsAnswered = 0;
                FactListService.GetFactList().then(function (response) {
                    ViewModel.data = response.data;// setting the ViewModel.data to our factList json File
                    ViewModel.NumFacts = response.data.factList.length;

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


            }





            // factory to get data from a Json return Action method defined in the flashcard controller.
            //TestsApp.factory("FactListService", ['$http', function ($http) {
            //    var result = {};
            //    result.GetFactList = function () {
            //        return $http.get("/FlashCard/QuizesJSONData");

            //    }

            //    return result;

            //}
            //]);
        
});