(function () {

    var flashCardApp = angular.module('FlashCardApp', ['ngSanitize']);

    flashCardApp.controller("flashController", FlashController);

    function FlashController(FactListService) {

        var ViewModel = this;
        ViewModel.ActiveCard = 0;
        ViewModel.Outcomes = 0;
        ViewModel.setActiveCard = setActiveCard;
        ViewModel.NumRightAnswers = 0;
        ViewModel.NumWrongAnswers = 0;
        ViewModel.RightAnswer = RightAnswer;
        ViewModel.RightAnswers = [];
        ViewModel.List = [];
        

        FactListService.GetFactList().then(function (response) {
            ViewModel.data= response.data;// setting the ViewModel.FlashcardData to our factList json File
            ViewModel.FlashcardData = response.data;
            ViewModel.NumFacts = response.data.factList.length;
           
            for (var i = 0; i < ViewModel.NumFacts; i++) {
                ViewModel.List.push(ViewModel.FlashcardData.factList[i])
            }

        });
        ViewModel.FlashcardData.factList[ViewModel.ActiveCard].trials = 1;
        function setActiveCard() {
          
                var stopLoop = false;
                var factsLength = ViewModel.data.factList.length - 1; // remember computers starts at zero
                // while stopLoop == false
                while (!stopLoop) {
                    // if  activeCard is equal to that active Card but less than facts then increment active card with one else set activeCard to zero and start all over to search for a wrong card clicked in the list
                    //ViewModel.ActiveCard = ViewModel.ActiveCard < factsLength ? ++ViewModel.ActiveCard : 0;
                   

                    if (ViewModel.ActiveCard < factsLength) {
                        ViewModel.ActiveCard++;
                    } else {
                        ViewModel.ActiveCard = 0;
                    }
                  
                  
                        if (ViewModel.FlashcardData.factList[ViewModel.ActiveCard].Outcome === null || ViewModel.FlashcardData.factList[ViewModel.ActiveCard].Outcome === false) {
                            stopLoop = true; // if an fact in the loop is equal to false or equal to null then the loop stops at that point
                        }
                    
                  

                }

          


        }
       
        function RightAnswer() {

            ViewModel.RightAnswers.push(ViewModel.FlashcardData.factList[ViewModel.ActiveCard])
            if (ViewModel.RightAnswers.length > 0) {
                for (var i = 0; i < ViewModel.RightAnswers.length; i++) {
                    if (ViewModel.FlashcardData.factList[ViewModel.ActiveCard] == ViewModel.RightAnswers[i] ) {
                        setActiveCard();
                        return;
                    } else if (ViewModel.FlashcardData.factList[ViewModel.ActiveCard] == ViewModel.RightAnswers[i]){

                        setActiveCard();
                        return;
                    } else if (ViewModel.FlashcardData.factList[ViewModel.ActiveCard - 2] == ViewModel.RightAnswers[i]){
                        ViewModel.ActiveCard = ViewModel.ActiveCard - 2;
                        ViewModel.FlashcardData.factList[ViewModel.ActiveCard].trials = 2;
                        return;

                    }
                }


            }


           

         


        }

       //function RightAnswer() {

       //     ViewModel.FlashcardData.factList[ViewModel.ActiveCard].Outcome = true;
       //     if (ViewModel.FlashcardData.factList[ViewModel.ActiveCard].Outcome == true ) {
       //         ViewModel.NumRightAnswers++;


       //         if (ViewModel.NumRightAnswers >= ViewModel.NumFacts) {
       //             //finalise App
       //             //However we have to be very sure that all the questions have been answered by implementing this for loop which checks if there is a question which is still equal to null
       //             for (var i = 0; i < ViewModel.NumFacts; i++) {

       //                 if (ViewModel.FlashcardData.factList[i].Outcome === false || ViewModel.FlashcardData.factList[i].Outcome === null ) {
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

        //    ViewModel.FlashcardData.factList[ViewModel.ActiveCard].Outcome = false;
        //    if (ViewModel.FlashcardData.factList[ViewModel.ActiveQuestion].Outcome == false) {
        //        ViewModel.NumWrongAnswers++;
        //        ViewModel.NumRightAnswers--;

               
        //            //finalise App
        //            //However we have to be very sure that all the questions have been answered by implementing this for loop which checks if there is a question which is still equal to null
        //            for (var i = 0; i < ViewModel.NumFacts; i++) {

        //                if (ViewModel.FlashcardData.factList[i].Outcome === false || ViewModel.FlashcardData.factList[i].Outcome === null) {
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