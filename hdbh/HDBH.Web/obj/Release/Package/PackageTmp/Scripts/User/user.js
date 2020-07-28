$form = $('form');
var user = {
    Init: function () {
        this.validateUser();
    },
    validateUser: function () {
        if ($form) {
            $form.validate({
                rules: {
                    userName: "required"
                },
                submitHandler: function () {
                    var url = form.attr('action');
                    var data = getFromData(form);
                    data.counterPartyName = form.find('#counterPartyName').val();
                    data.counterPartyGroup = form.find('#counterPartyGroup option:selected').val();
                    data.counterPartyStatus = form.find('#counterPartyStatus').is(':checked') ? "1" : "0";
                    $.ajax({
                        type: "POST",
                        url: url,
                        dataType: "json",
                        data: data, // serializes the form's elements.
                        success: function (data) {
                            if (data.errorCode == 0) {
                                Notification.Success("", data.errorMessage);
                                location.href = "/Role/";
                            }
                            else {
                                Notification.Error("", data.errorMessage);
                            }
                        }
                    });
                }
            });
        }

    },
}