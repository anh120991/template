jQuery.validator.addMethod("specialfullChar", function (value, element) {

    if ((/[!"#$%&'()*+.\/:;<=>?@\[\\\]^_`{|}~-]/gi.test(value))) {
        return false
    }
    return true;
}, "không được chứa các ký tự đặc biệt!");

jQuery.validator.setDefaults({
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {

            error.insertAfter(element.closest('.input-group'));
        } else if (element.parent('.checkbox-inline').length) {
            error.addClass('error-checkbox').insertAfter(element.closest('.row'));     //
        }
        else if (element.hasClass('select2-hidden-accessible')) {
            error.insertAfter(element.next('span'));  // select2
            element.next('span').addClass('error').removeClass('valid');
        } else {
            error.insertAfter(element);               // default
        }
    }
});



$.validator.addMethod('customphone', function (value, element) {
    return this.optional(element) || /^(\+91-|\+91|0)?\d{10}$/.test(value);
}, "Please enter a valid phone number");
