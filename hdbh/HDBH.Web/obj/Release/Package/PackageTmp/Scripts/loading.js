var ua = navigator.userAgent;

var IEMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);

var isIE9 = /MSIE 9/i.test(ua);
var isIE10 = /MSIE 10/i.test(ua);
var isIE11 = /rv:11.0/i.test(ua) && !IEMobile ? true : false;

//var isIE9 = false;
//var isIE10 = false;
//var isIE11 = false;

//if (ua.indexOf("Trident/7.0") > -1)
//    return isIE11=true;
//else if (ua.indexOf("Trident/6.0") > -1)
//    return isIE10 = true;
//else if (ua.indexOf("Trident/5.0") > -1)
//    return isIE9 = true;
//else
//    return 0; 

var isEge = /Edge\/12./i.test(navigator.userAgent)
var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || ua.indexOf(' OPR/') >= 0;
var isFirefox = typeof InstallTrigger !== 'undefined';
var isIE = false || !!document.documentMode;
var isEdge = !isIE && !!window.StyleMedia && !isIE11;

var Loadx = 0;







function DrawLoad() {
    var Stroke = $('.load-present');
    var Paths = $(Stroke).find('path');

    $(Paths).each(function (index, element) {
        var totalLength = this.getTotalLength();

        if (isIE9 || isIE10 || isIE11 || isEdge) {
            $(this).css({ 'stroke-dasharray': totalLength + ' ' + totalLength });
            $(this).css({ 'stroke-dashoffset': totalLength, 'stroke-dasharray': totalLength + ' ' + totalLength });
            $(this).stop().animate({ 'stroke-dashoffset': 0 }, 1000, 'linear', function () {
                $(this).stop().animate({ 'stroke-dashoffset': totalLength }, 1000, 'linear');
            });
            setTimeout(function () { $('.loadicon').addClass('show') }, 900);
        } else {
            setTimeout(function () { $('.loadicon').addClass('show') }, 800);
        }
    });



}



function Done() {





    $('.loadicon').fadeOut(500, function () {
        $('.loadicon').removeClass('loader');
        $('.loadicon').removeClass('show');
    });





}

//LOAD WEATHER


//$(document).ready(function () {




//    if (!$('.loadicon').hasClass('loader')) {
//        $('.loadicon').show();
//        $('.loadicon').addClass('loader');
//        DrawLoad();
//    }





//    setTimeout(function () { if (Loadx == 0) { Loadx = 1; Done(); } }, 1500);




//});




