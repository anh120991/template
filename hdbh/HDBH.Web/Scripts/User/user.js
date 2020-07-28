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
                    var data = getFromData($form);
                    data.userName = $form.find('input[name=userName]').val();
                    data.fullName = $form.find('input[name=fullName]').val();
                    data.phoneNumber = $form.find('input[name=phoneNumber]').val();
                    data.email = $form.find('input[name=email]').val();
                    data.officerCode = $form.find('input[name=officerCode]').val();
                    data.userBranchCode = $form.find('select[name=userBranchCode] option:selected').val();
                    var lsRole = [];
                    $('input[name=chkOption]').each(function () {
                        if ($(this).is(':checked')) {
                            lsRole.push($(this).val());
                        }
                    });
                    data.userRoleList = lsRole;
                    var url = $form.attr('action');
                    $.ajax({
                        type: "POST",
                        url: url,
                        dataType: "json",
                        data: data, // serializes the form's elements.
                        success: function (data) {
                            if (data.errorCode == 0) {
                                Notification.Success("", data.errorMessage);
                                location.href = "/sys/User/";
                            }
                            else {
                                Notification.Error("", data.errorMessage);
                            }
                        }
                    });
                }
            });
        }

    }
}