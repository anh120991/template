var rootDiv = $('#detailForm');
var form = $('#form');
var format = /[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/;
approveForm = $('#frmApprove');


var counterParty = {
    Init: function () {
        rootDiv.on('change', '#counterPartyGroup', this.changeCounterPartyGroup);
        rootDiv.on('focusout', '#cifCounterParty', this.searchCif);
        this.validateCreateCounterParty();
    },
    validateCreateCounterParty: function () {
        if (form) {
            form.validate({
                rules: {
                    cifCounterParty: "required",
                    counterPartyName: "required",
                    shortName: "required",
                    signedContractDate: "required",
                    paymentAccount: "required"
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
                                setTimeout(function () {
                                    location.href = "/CounterParty/manager";
                                }, 2000)
                              
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
    changeCounterPartyGroup: function () {
        var _val = $(this).val();
        if (_val != '') {
            $.ajax({
                type: "GET",
                url: urlLoadForm,
                data: { counterPartyGroup: _val }, // serializes the form's elements.
                success: function (data) {
                    $('#counterPartyInfo').html(data);
                    loadFormatDate();
                }
            });
        }

    },
    selectRow: function () {
        var modal = $('#' + $(this).closest('.modal').attr('id'));
        var data = $(this).data('value');
        if (data != null) {
            counterParty.setData(data);
            modal.modal('hide');
        }
    },
    searchCif: function () {
        var cif = $(this).val();
        var param = {
            cif: cif,
            pageSize: 0
        };
        debugger;
        $.ajax({
            type: "POST",
            url: '/common/searchPopupCustomer',
            data: param, // serializes the form's elements.
            success: function (msg) {
                if (msg.data != null) {
                    counterParty.setData(msg.data[0]);
                }
            }
        });
    },
    setData: function (data) {
        if (data != null) {
            $('#cifCounterParty').val(data.cif);
            $('#counterPartyName').val(data.customerName);
            $('#shortName').val(data.customerShortName);
        }
    }
}

