//validate common

var validatetype = { number: '[data-required="true"]' }
var objAjax;
function Global() { }
Global.prototype.callBack = '';
Global.prototype.isHandlerCallBack = false;

Global.prototype.aJaxCall = function (url, type, dataIni, beginFunc, funcCallBack, funcError) {
    objGlobal = new Global();
    var bgFunction;
    if (objAjax != null)
        objAjax.abort();
    if (beginFunc == undefined || beginFunc == null) {
        bgFunction = showLoading
    } else {
        bgFunction = beginFunc
    };
    if (funcCallBack == undefined || funcCallBack == null)
        funcCallBack = function () {
            hideLoading();
            return objGlobal.closeMess();
        };

    objAjax = $.ajax({
        type: type,
        url: url,
        cache: false,
        traditional: true,
        beginSend: bgFunction(),
        //contentType: "application/json; charset=utf-8",
        //datatype: 'json',
        data: dataIni,
        success: function (arg) { funcCallBack(arg); },
        error: function (e) {
            if (funcError != undefined && funcError != null)
                funcError(e);
            hideLoading();
        },
        complete: function (e, xhr, settings) {
            if (e.status === 302) {
                reSetLogin();
            }
            hideLoading();
        }
    });
}



function showLoading() {
    if ($('.loadicon')) {
        $('.loadicon').show();
        $('.loadicon').addClass('loader');
    }
}
function hideLoading() {
    Done();
}

function setActiveMenu() {
    var currenUrl = location.pathname;
    $('#menu-bar').find('a').each(function () {
        var url = $(this).attr('href');
        if (currenUrl.indexOf(url) > -1) {
            $(this).closest('.rootLi').addClass('active');
        }
    })
}
function getFromData(f) {
    var formArray = $(f).serializeArray();
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        var oldVal = returnArray[formArray[i]['name']]; // get gia tri cu
        if (oldVal != undefined) {
            if (oldVal.constructor !== Array) { //khoi tao mang cho gia tri
                var tempArr = [];
                tempArr.push(oldVal);
                tempArr.push($.trim(formArray[i]['value']));

            }
            else {
                var tempArr = oldVal; //set gia tri vao mang
                tempArr.push($.trim(formArray[i]['value']));
            }
            returnArray[formArray[i]['name']] = tempArr;
        }
        else {
            var _chkElement = $(f).find('input[name=' + formArray[i]['name'] + ']');
            if (_chkElement.length == 1 && _chkElement.attr('type').toUpperCase() == 'CHECKBOX') {
                returnArray[formArray[i]['name']] = _chkElement.is(':checked');
            }
            else {
                returnArray[formArray[i]['name']] = $.trim(formArray[i]['value']);
            }
        }

    }
    return returnArray;
}


function loadFormatDate() {
    var datepickers; // declare this as global variable
    datepickers = $('body').find('.datepicker');
    for (var i = 0; i < datepickers.length; i++) {
        var currenItem = datepickers.eq(i);
        loadSingleFormatDate(currenItem);
    }
}


function loadSingleFormatDate(o){
    var currenItem = o;
    if (currenItem.attr('compairDate') != undefined && currenItem.attr('equalDate') != undefined) {
        var compairItem = $('#' + currenItem.attr('compairDate'));
        if (compairItem) {
            if (currenItem.attr('equalDate') == 'small') {
                jQuery(currenItem).datetimepicker({
                    format: 'd/m/Y',
                    timepicker: false,
                    onShow: function (ct) {
                        this.setOptions({
                            minDate: new Date(2020, 08, 02) //jQuery(compairItem).val() ? jQuery(compairItem).val() : false
                        })
                    },
                    scrollMonth: false,
                    scrollInput: false
                });
            }
            else {
                jQuery(currenItem).datetimepicker({
                    format: 'd/m/Y',
                    timepicker: false,
                    onShow: function (ct) {
                        this.setOptions({
                            minDate: jQuery(compairItem).val() ? jQuery(compairItem).val() : false
                        })
                    },
                    scrollMonth: false,
                    scrollInput: false
                });
            }
        }
    }
    else {
        jQuery('.datepicker').datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            formatDate: 'd/m/Y',
            scrollMonth: false,
            scrollInput: false
        });
    }
}


var mybutton = document.getElementById("scrollTop");
function scrollToTop() {
    window.onscroll = function () { scrollFunction() };
}
function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

function parseDecimal(number) {
    if (number != null && number != undefined && number != '') {
        if (number.indexOf(',') > -1) {
            number = number.replace(/,/g, '');
        }
        if (number.indexOf('.') > -1) {
            number = number.replace(/\./g, ',');
        }
        return number;
    }
    return 0;
}

function parsePercent(number) {
    if (number != null && number != undefined && number != '') {
        if (number.indexOf(',') > -1) {
            number = number.replace(/,/g, '');
        }
        if (number.indexOf('.') > -1) {
            number = number.replace(/\./g, ',');
        }
        return number.replace(/%/g, '');;
    }
    return 0;
}


function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat)
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/')
}

function formatDateExcel(date) {
    debugger;
    var dt = date.split(/\-|\s|\//); // allows for 01-05-2011 also
    return new Date(dt[2], dt[1], dt[0]);
}

