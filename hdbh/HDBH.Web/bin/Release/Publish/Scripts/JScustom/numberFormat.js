$(document).ready(function () {
    loadformatnumber();
    numerictypeformat('numericformat');
});



function loadformatnumber(o) {
    autonumericformat("numberinput", o);
    autonumericformat("numberinput0", o);
    autonumericformat("numberinput1", o);
    autonumericformat("numberinput2", o);
    autonumericformat("numberinput3", o);
    autonumericformat("numberinput4", o);
    autonumericformat("numberinput5", o);
    autonumericformat("numberinput6", o);
    autonumericformat("numberinput7", o);
    autonumericformat("numberinput8", o);
    autonumericformat("numberinput9", o);
    autonumericformat("numbernosign", o);
    autonumericformat("numberpercent", o);
    autonumericformat("numberpercentnosign", o);
    autonumericformat("numberpercentnosign3dec", o);
    autonumericformat("numberpercentnosign4dec", o);
    autonumericformat("numberpercent4dec", o);
}


function numerictypeformat(c) {
    var items = $("." + c);
    if (items.length == 0) return;

    for (var i = 0; i < items.length; i++) {
        var max = '999999999999999';
        var min = '-99999999999999';
        var decimal = 2;

        var item = $(items[i]);
        if (item.attr('nummax')) {
            max = item.attr('nummax');
        }
        if (item.attr('nummin')) {
            min = item.attr('nummin');
        }
        if (item.attr('decimal')) {
            decimal = parseInt(item.attr('decimal'));
        }
        var customNumeric = {};
        customNumeric = { aPad: true, vMax: max, vMin: min, mDec: decimal, aForm: true };
        item.autoNumeric('destroy');
        item.autoNumeric('init', customNumeric);
    }

    items.click(function () {
        if ($(this).is('input')) this.select();
    });
    for (var i = 0; i < items.length; i++) {
        var item = $(items[i]);
        if (item) {
            if (!item.is('input')) continue;
            //item.unbind('keypress');
            //item.on('keypress', function (e) {
            //    if (['b', 'm', 'k'].indexOf(e.key.toLowerCase()) >= 0) {

            //        var x = 0;
            //        if (e.key.toLowerCase() == 'b') x = 1000000000;
            //        else if (e.key.toLowerCase() == 'm') x = 1000000;
            //        else if (e.key.toLowerCase() == 'k') x = 1000;
            //        var v = x * $(this).autoNumeric('get');
            //        if (v > 999999999999999 || v < -999999999999999) $(this).val($(this).autoNumeric('get'));
            //        else $(this).val(v);
            //    }
            //});

            if (!(item.attr('iscustevt'))) {

                item.attr('iscustevt', true);
                item.keypress(function (e) {
                    if (['b', 'm', 'k'].indexOf(e.key.toLowerCase()) >= 0) {

                        var x = 0;
                        if (e.key.toLowerCase() == 'b') x = 1000000000;
                        else if (e.key.toLowerCase() == 'm') x = 1000000;
                        else if (e.key.toLowerCase() == 'k') x = 1000;
                        var v = x * $(this).autoNumeric('get');
                        if (v > 999999999999999 || v < -999999999999999) $(this).val($(this).autoNumeric('get'));
                        else $(this).val(v);

                    }
                });
            }



            var currentID = item.attr('id');
            if (!currentID) continue;
            if (currentID.indexOf('_regex') != -1) {
                var newID = currentID.split("_regex")[0];
                if (item.parent().find("#" + newID).length == 0) {
                    item.parent().append('<input type="number" style="display:none;" name="' + newID + '" id="' + newID + '" value="' + item.val() + '" />');
                    item.change(function (e) {
                        var newElement = $(this).parent().find('#' + $(this).attr('id').split("_regex")[0]);
                        if (newElement.length == 1) {
                            var v = $(this).autoNumeric('get');
                            if (typeof (v) == "undefined") {
                                v = $(this).val();
                            }
                            newElement.val(v);
                        }
                    });

                }
            }

        }

    }
}

function autonumericformat(c, o) {
    if (o == null) o = { aPad: true };
    var items = $("." + c);
    if (items.length == 0) return;
    for (var i = 0; i < items.length; i++) {
        var item = $(items[i]);
        item.val(item.val().trim());
    }
    var customNumeric = {};
    var defaultmaxmin = { vMax: '999999999999999999999999999999', vMin: '-999999999999999999999999999999', };
    if (c == "numberinput") customNumeric = $.extend(o, defaultmaxmin, { mDec: 0, aForm: true });
    else if (c == "numberinput0") customNumeric = $.extend(o, defaultmaxmin, { mDec: 0, aForm: true });
    else if (c == "numberinput1") customNumeric = $.extend(o, defaultmaxmin, { mDec: 1, aForm: true });
    else if (c == "numberinput2") customNumeric = $.extend(o, defaultmaxmin, { mDec: 2, aForm: true });
    else if (c == "numberinput3") customNumeric = $.extend(o, defaultmaxmin, { mDec: 3, aForm: true });
    else if (c == "numberinput4") customNumeric = $.extend(o, defaultmaxmin, { mDec: 4, aForm: true });
    else if (c == "numberinput5") customNumeric = $.extend(o, defaultmaxmin, { mDec: 5, aForm: true });
    else if (c == "numberinput6") customNumeric = $.extend(o, defaultmaxmin, { mDec: 6, aForm: true });
    else if (c == "numberinput7") customNumeric = $.extend(o, defaultmaxmin, { mDec: 7, aForm: true });
    else if (c == "numberinput8") customNumeric = $.extend(o, defaultmaxmin, { mDec: 8, aForm: true });
    else if (c == "numberinput9") customNumeric = $.extend(o, defaultmaxmin, { mDec: 9, aForm: true });
   
    else if (c == "numbernosign") customNumeric = $.extend(o, { vMax: '999999999999999999999999999999', vMin: '0', mDec: 0, aForm: true });
    else if (c == "numberpercent") customNumeric = $.extend(o, { vMax: '100', vMin: '-100', mDec: 2, aSign: '%', pSign: 's', aForm: true });
    else if (c == "numberpercentnosign") customNumeric = $.extend(o, { vMax: '100', vMin: '0', mDec: 2, aSign: '%', pSign: 's', aForm: true });
    else if (c == "numberpercentnosign3dec") customNumeric = $.extend(o, { vMax: '100', vMin: '0', mDec: 3, aSign: '%', pSign: 's', aForm: true });
    else if (c == "numberpercentnosign4dec") customNumeric = $.extend(o, { vMax: '100', vMin: '0', mDec: 4, aSign: '%', pSign: 's', aForm: true });
    else if (c == "numberpercent4dec") customNumeric = $.extend(o, { vMax: '100', vMin: '-100', mDec: 4, aSign: '%', pSign: 's', aForm: true });

    //
    //items.autoNumeric('destroy');
    //if (items.autoNumeric('getSettings')) {
    //}
    //else { items.autoNumeric('init', customNumeric); }

    for (var i = 0; i < items.length; i++) {
        var item = $(items[i]);
        if (item.is("th")) continue;

        // huyln fix
        if (!item.autoNumeric('getSettings'))
            item.autoNumeric('init', customNumeric);
        else
            item.autoNumeric('update', customNumeric);
        item.attr("numberclassname", c);

        //
        //item.autoNumeric('update', customNumeric);
    }

    items.click(function () {
        if ($(this).is('input')) this.select();
    });

    for (var i = 0; i < items.length; i++) {
        var item = $(items[i]);
        if (!item.is('input')) continue;
        //item.unbind('keypress');
        if (!(item.attr('iscustevt'))) {
            item.attr('iscustevt', true);
            item.keypress(function (e) {
                if (['b', 'm', 'k'].indexOf(e.key.toLowerCase()) >= 0) {

                    var x = 0;
                    if (e.key.toLowerCase() == 'b') x = 1000000000;
                    else if (e.key.toLowerCase() == 'm') x = 1000000;
                    else if (e.key.toLowerCase() == 'k') x = 1000;
                    var v = x * $(this).autoNumeric('get');
                    if (v > 999999999999999 || v < -999999999999999) $(this).val($(this).autoNumeric('get'));
                    else $(this).val(v);

                }
            });
        }
        var currentID = item.attr('id');
        if (!currentID) continue;
        if (currentID.indexOf('_regex') != -1) {
            var newID = currentID.split("_regex")[0];
            if (item.parent().find("#" + newID).length == 0) {
                item.parent().append('<input style="display:none;" name="' + newID + '" id="' + newID + '" value="' + item.autoNumeric('get') + '" />');
                item.change(function (e) {
                    var newElement = $(this).parent().find('#' + $(this).attr('id').split("_regex")[0]);
                    if (newElement.length == 1) {
                        var v = $(this).autoNumeric('get');
                        if (typeof (v) == "undefined") {
                            v = $(this).val();
                        }
                        newElement.val(v);
                    }
                });

            }

        }
    }
}
