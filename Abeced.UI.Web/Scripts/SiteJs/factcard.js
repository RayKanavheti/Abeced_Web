
var showingAns = false;
var numQuestions = 0;
var numCorrect = 0;
var steppp = null;
var current_fact = 0;
var timer;
var playduration = 
$(function () {
    
    $('#btnPause').hide();
    $('#btnPlay').hide();
    $('#amazingaudioplayer-1').hide();
});

interval = 10000; //initial interval

jQuery(document).ready(function () {

    jQuery("#amazingaudioplayer-1").bind("amazingaudioplayer.playprogress", function (event, data) {
        //console.log(data);
        //console.log(data.current);
        //console.log(data.duration);
        //interval = data.duration;
    });

});

$('#btnPlay').click(function () {
     timer = setInterval(request, interval); // start setInterval as "run"
    clearInterval(timer);    // stop the setInterval()

    //change time interval  here.. 
    var tt = getAudioTime();
    if (tt != 0 && tt != NaN) {
        interval = tt;
    }
    //console.log("am here  ---" + interval);
        if (steppp == null) {
            $(".wizard-step:first").fadeIn();
        }

        setCardAudio();

        $('#btnPlay').hide();
        $('#btnPause').show();
        $('#amazingaudioplayer-1').show();
        $('#introbox').fadeOut();

       
       

        function request() {

            //console.log(interval); // firebug or chrome log
            //debugger;

            
            // show first step
            var firsst = $(".wizard-step:first");
            var $step = $(".wizard-step:visible"); // get current step

            if ($step.next().hasClass("confirm")) { // is it last? - lets loop to the begining

                //console.log($('.wizard-step:first'));
                $step.hide();
                firsst.show();
                $step = $(".wizard-step:visible"); // get current step
                
            }
            else {

                $step.hide().next().fadeIn();  // show it and hide current step
            }

            setCardAudio();
            clearInterval(timer);    // stop the setInterval()

            //change time interval  here.. 
            var tt = getAudioTime();
            if (tt != 0 && tt != NaN) {
                interval = tt;
            }
            //console.log(interval);
        
            timer = setInterval(request, interval); // start the setInterval()


    }

    ////change time interval  here.. 
        clearInterval(timer);

        var tt = getAudioTime();
        if (tt != 0 && tt != NaN) {
            interval = tt;
        }

        console.log(interval);
        timer = setInterval(function () {
    //        // show first step
            var firsst = $(".wizard-step:first");
            var $step = $(".wizard-step:visible"); // get current step

            if ($step.next().hasClass("confirm")) { // is it last? - lets loop to the begining

                //console.log($('.wizard-step:first'));
                $step.hide();
                firsst.show();
                //current_fact = 0;    //reset to first
                $step = $(".wizard-step:visible"); // get current step
            }
            else {

                $step.hide().next().fadeIn();  // show it and hide current step
            }

            setCardAudio();
            //console.log(jQuery("#amazingaudioplayer-1").data("object"));
            //console.log(jQuery("#amazingaudioplayer-1"));


        }, interval);

});

$('#btnPause').click(function () {
    steppp = $(".wizard-step:visible");
    window.clearInterval(timer);
    $('#btnPause').fadeOut();
    $('#btnPlay').fadeIn();
});

$('#btnClose').click(function () {
    if (confirm("Are you sure You want to EXIT")) {
        window.location.href = "/Flash/GetTopics";
    }
});

$('#btnStart').click(function () {
    $('#btnStart').hide();
    $('#btnPause').hide();
    //$('#btnPlay').fadeIn();
    $('#btnPlay').click();

});

function getAudioTime()
{
    if ($(".wizard-step:visible").hasClass("silence"))
    {
        return 4000;
    }
    return 10000; //parseInt(($(".amazingaudioplayer-time").text().split("/")[1]).split(":")[1].trim()) * 1000;
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function animateCard() {
    document.getElementById('spinner').classList.remove("spinAnimation");
    document.getElementById('spinner').offsetWidth = document.getElementById('spinner').offsetWidth;
    document.getElementById('spinner').classList.add("spinAnimation");
    //$("#fldcardCol2").height($("#fldcardCol1").height());
}

function setCardAudio() {
    var source = document.getElementById('ctlbgmusic');
    source.pause();
    source.currentTime = 0;
    var ansaudio = $('.wizard-step:visible > .frontCard').find('input.factAudio[type="hidden"]').val();
    source.src = ansaudio ;//audio/ogg/' + this.parentElement.getAttribute('data-value');
    
    //audio.load(); //call this to just preload the audio without playing
    $(".ctlbgmusic").trigger('play');
    //audio.play(); //call this to play the song right away
    //console.log(source);
}

function setCardAudio2() {

    jQuery("#amazingaudioplayer-1").data("object").stopAudio();
    var ansaudio = $('.wizard-step:visible > .frontCard').find('input.factAudio[type="hidden"]').val();
    var data_fact = $(".wizard-step:visible").attr("data-fact");

    if (data_fact) {
        //console.log("id is " + ansaudio + "  .. " + data_fact);
        jQuery("#amazingaudioplayer-1").data("object").audioRun(parseInt(data_fact), true);
    }
    else { //silence
        jQuery("#amazingaudioplayer-1").data("object").audioRun(1, true);
    }
    //$('.amazingaudioplayer-source').attr("data-src", ansaudio);

    //$('#amazingaudioplayer-1').find('audio').find('source').attr('src', ansaudio);
    //jQuery("#amazingaudioplayer-1").data("object").playAudio();
    

    //$('#amazingaudioplayer-1').find('audio').trigger('pause');
    //$('#amazingaudioplayer-1').find('audio').currentTime = 0; //prop("currentTime", 0);

}

$('#txtFlip2').click(function () {

    if (!$('.cardAnswerArea').is(":visible")) {
        $('.wizard-step:visible > .frontCard').slideToggle();
        $('.wizard-step:visible > .backCard').slideToggle();
        $('.cardAnswerArea').toggle();
        $('.cssFlip').hide();

        animateCard();
        setTimeout(function () { $(".headerOptions").css("background-color", "#636363"); }, 250);
        setTimeout(function () { $("#card_name").css("color", "#F7F7F7"); }, 250);

        //set audio to answer audio on flip
        var ansaudio = $('.wizard-step:visible > .frontCard').find('input.factAudio[type="hidden"]').val();
        $('.amazingaudioplayer-source').attr("data-src", ansaudio);
        $('#amazingaudioplayer-1').find('audio').find('source').attr('src', ansaudio);
        $('#amazingaudioplayer-1').find('audio').trigger('pause');
        //$('#amazingaudioplayer-1').find('audio').stopAudio();//prop("currentTime", 0);


        //$('#txtFlip').attr("disabled", true);

    }
    else {
        alert("Please indicate whether you got it right or not ?");
    }
});

$(this).keypress(function (key) {
    if (key.which == 13) {
        $('#btnAnswer').click();
    }
});

$('#userAnswer').keypress(function (key) {
    if (key.which == 13) {
    }
});

$('#restart-step2').click(function () {

    var cid = getUrlVars()["cid"];
    //var sid = getUrlVars()["sid"];
    var sub = getUrlVars()["sub"];

    if (cid == "")
        window.location.href = "/Flash";
    else
        window.location.href = "/Flash/ReviseCard?cid=" + cid + "&sub=" + sub;;
});

$('#restart-step').click(function () {

    //var cardid = $('.wizard-step:visible > .frontCard').find('input[type="hidden"]').val();
    var sesid = $('#SessionId').val();
    var cid = getUrlVars()["cid"];
    var sub = getUrlVars()["sub"];

    window.location.href = "/Flash/ReviseCard?sid=" + sesid + "&cid=" + cid + "&rp=1";
});

$('#next-step3').click(function () {

    window.location.href = "/Flash/GetTopics";
});


//Toggle background music..
$('#bgmusic-toggle-switch').on('switch-change', function (e, data) {
    var stats = $('#bgmusic-toggle-switch').bootstrapSwitch('status');
    if (stats == false) {
        document.getElementById('ctlbgmusic').muted = true;
        //$(".ctlbgmusic").trigger('play');
    }
    else {
        document.getElementById('ctlbgmusic').muted = false;
    }
    //var $el = $(data.el)
    // , value = data.value;
    //console.log(e, $el, value);    
});

var au = document.createElement('ctlbgmusic');
au.addEventListener('loadedmetadata', function () {
    au.setAttribute('data-time', au.duration);
}, false);

au.onloadedmetadata = function () {
    alert(audio.duration);
};



