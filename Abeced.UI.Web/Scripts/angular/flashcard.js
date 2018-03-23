(function () {

    var flashCardApp = angular.module('FlashCardApp', ['ngSanitize']);

    flashCardApp.controller("flashController", FlashController);

    function FlashController(FactListService) {

        var ViewModel = this;
        ViewModel.ActiveCard = 0;
        ViewModel.setActiveCard = setActiveCard;
        ViewModel.RightAnswer = RightAnswer;
        ViewModel.WrongAnswer = WrongAnswer;
        ViewModel.NumRightAnswers = 0;
        ViewModel.NumWrongAnswers = 0;
        ViewModel.Questions = [];
        ViewModel.Answers = [];
        ViewModel.RightAnswers = [];
        var answeredQuestions;

        FactListService.GetFactList().then(function (response) {
            ViewModel.data = response.data;// setting the ViewModel.data to our factList json File
            ViewModel.NumFacts = response.data.factList.length;
            answeredQuestions[ViewModel.NumFacts];
            for (var i = 0; i < ViewModel.NumFacts; i++) {

                ViewModel.Questions[i].push(ViewModel.data.factList[i].question);
                ViewModel.Answers[i].push(ViewModel.data.factList[i].answer)
            }

        });

        function setActiveCard(index) {
            if (index === undefined) { // if index is not passed then the following will be executed
                var stopLoop = false;
                var factsLength = ViewModel.data.factList.length - 1; // remember computers starts at zero
                // while stopLoop == false
                while (!stopLoop) {
                    // if  activeCard is equal to that active Card but less than facts then increment active card with one else set activeCard to zero and start all over to search for a wrong card clicked in the list
                    ViewModel.ActiveCard = ViewModel.ActiveCard < factsLength ? ++ViewModel.ActiveCard : 0;
                    if (ViewModel.ActiveQuestion === 0) {

                        ViewModel.UnCompletedError = true;
                    }

                    if (ViewModel.data.factList[ViewModel.ActiveCard].Outcome === null || ViewModel.data.factList[ViewModel.ActiveCard].Outcome === false) {
                        stopLoop = true; // if an fact in the loop is equal to false or equal to null then the loop stops at that point
                    }

                }

            } else {

                ViewModel.ActiveCard = index;
            }


        }
        var TimesRightAnswerClicked = 0
        function RightAnswer() {
            TimesRightAnswerClicked ++;
            ViewModel.data.factList[ViewModel.ActiveCard].Outcome = true;

            for (var i = 0; i < TimesRightAnswerClicked; i++) {

                ViewModel.RightAnswers[i].push(ViewModel.data.factList[ViewModel.ActiveCard].Outcome)

            }



        }

       //function RightAnswer() {

       //     ViewModel.data.factList[ViewModel.ActiveCard].Outcome = true;
       //     if (ViewModel.data.factList[ViewModel.ActiveCard].Outcome == true ) {
       //         ViewModel.NumRightAnswers++;


       //         if (ViewModel.NumRightAnswers >= ViewModel.NumFacts) {
       //             //finalise App
       //             //However we have to be very sure that all the questions have been answered by implementing this for loop which checks if there is a question which is still equal to null
       //             for (var i = 0; i < ViewModel.NumFacts; i++) {

       //                 if (ViewModel.data.factList[i].Outcome === false || ViewModel.data.factList[i].Outcome === null ) {
       //                     setActiveCard(i) // call this function if there is unaswered question 
       //                     return; // stops the loop to stop further looping
       //                 }

       //             }
       //             ViewModel.UnCompletedError = false;
       //             ViewModel.finalise = true; // this means all questions have been answered the user is ready to go the next step
       //             return; // stop executing further so that the below setActiveQuestion function won't be executed.
       //         }
               
       //     }
       //     ViewModel.setActiveCard();
       // }

        //function WrongAnswer() {

        //    ViewModel.data.factList[ViewModel.ActiveCard].Outcome = false;
        //    if (ViewModel.data.factList[ViewModel.ActiveQuestion].Outcome == false) {
        //        ViewModel.NumWrongAnswers++;
        //        ViewModel.NumRightAnswers--;

               
        //            //finalise App
        //            //However we have to be very sure that all the questions have been answered by implementing this for loop which checks if there is a question which is still equal to null
        //            for (var i = 0; i < ViewModel.NumFacts; i++) {

        //                if (ViewModel.data.factList[i].Outcome === false || ViewModel.data.factList[i].Outcome === null) {
        //                    setActiveCard(i) // call this function if there is unaswered question 
        //                    return; // stops the loop to stop further looping
        //                }

        //            }
                

        //            ViewModel.setActiveCard();

        //    }

        //}

    }


    // factory to get data from a Json return Action method defined in the flashcard controller.
    flashCardApp.factory("FactListService", ['$http', function ($http) {
        var result = {};
        result.GetFactList = function () {
            return $http.get("/FlashCard/flashCardsJSONData");

        }

        return result;

    }

    ]);

})();