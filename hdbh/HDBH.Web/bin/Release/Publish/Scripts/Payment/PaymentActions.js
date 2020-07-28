$(window).on('load', function () {
    paymentActions.init();
    paymentActions.loadPaymentList();
})

var paymentActions = {

    pageId: $('#rootList'),
    buttonId: $('#rootList #btnPayment'),
    pageApproveId: $('#formApprove'),
    pageUpdateId: $('#formUpdate'),

    init: function () {
        paymentActions.buttonId.on('click', '#btnAdd', paymentActions.OpenAddNew);
        paymentActions.buttonId.on('click', '#btnEdit', paymentActions.OpenUpdate);
        paymentActions.buttonId.on('click', '#btnRemove', paymentActions.OpenRemove);
        paymentActions.buttonId.on('click', '#btnApprove', paymentActions.OpenApprove);
        $('#rootList').on('click', '#btnCreateReceipt', paymentActions.OpenCreate);
        $('#formInsert').on('click', '#btnSearchContract', paymentActions.searchInfoUnpaid);
        $('#formInsert').on('change', '#scheduleId', paymentActions.loadPaymentList);

        $('#rootList').on('click', '#btnInsert', paymentActions.Insert);
        $('#formApprove').on('click', '#btnApprove', paymentActions.Approve);
        $('#formApprove').on('click', '#btnEditRequest', paymentActions.Approve);

        $('#formUpdate').on('click', '#btnUpdate', paymentActions.Update);
        $('#formUpdate').on('click', '#btnExit', paymentActions.Close);

    },

    OpenApprove: function () {
        var repaymentData = $('#Repayment_tblist_wrapper input:checked');
        if (repaymentData.length == 0) {
            Notification.Error("", "Vui lòng chọn thông tin thanh toán");
        } else if (repaymentData.data('processstatuscode') != "INAU") {
            Notification.Error("", "Không đủ điều kiện duyệt");
        }
        else {
            var param = {
                repaymentId: repaymentData.data('repaymentid'),
                mode: "APPROVE",
            }
            var url_param = "?repaymentId=" + param.repaymentId + "&&mode=" + param.mode + "";
            location.href = "/payment/manager/ApproveRepayment" + url_param;
        }
    },
    //Hàm approve
    Approve: function () {
        var mode = $('#mode').val();
        var param = {
            repaymentId: $('#repaymentId').val(),
            approveStatus: "A",
            approveContent: $('textarea[name=contentApproved]').val(),
        };
        if (mode == "APPROVE") {
            $.ajax({
                type: "POST",
                url: '/payment/manager/Approve',
                data: param,
                dataType: "json",
                success: function (data) {
                    if (data.errorCode == 0) {
                        Notification.Success("", data.errorMessage);
                        paymentActions.PaymentInauReload();
                    }
                    else if (data == null) {
                        Notification.Error("", data.errorMessage);
                    }
                }
            });
        }
    },

    OpenUpdate: function () {
        //alert('On Update');
        var repaymentData = $("#Repayment_tblist_wrapper input[name=selectId]:checked");
        if ($('#Repayment_tblist_wrapper input:checked').length == 0) {
            Notification.Error("", "Vui lòng chọn thông tin thanh toán");
        }
        else {
            var param = {
                repaymentId: $('#Repayment_tblist_wrapper input:checked').data('repaymentid'),
                mode: "UPDATE",
            }
            var url_param = "?repaymentId=" + param.repaymentId + "&&mode=" + param.mode + "";
            location.href = "/payment/manager/UpdateRepayment" + url_param;
        }
    },

    Update: function () {
        var _sumscheduleTotalAmount = parseFloat($('#sumScheduleTotalAmount').val());
        var param = {
            scheduleId: $('#scheduleId').val(),
            insuranceId: $('#insuranceId').val(),
            repaymentId: $('#repaymentId').val(),
            ftttNo: $('#ftttNo').val(),
            isExternalRepayment: $("input[id=isExternalRepayment]").is(":checked") ? 1 : 0,
            paymentDate: $('#paymentDate').val(),
            totalAmount: parseFloat($('#totalAmount').val()),
            fileList: attachFile.getFilelist(),
        };
        $.ajax({
            url: '/payment/Manager/Update',
            type: 'post',
            datatype: 'json',
            data: param,
            success: function (res) {
                if (res.errorCode == 0) {
                    Notification.Success("", "Chỉnh sửa phiếu thành công!");
                    insurance.Search();
                }
                else
                    Notification.Error("", res.errorMessage);
            }
        });
    },

    //Add dựa trên hợp đồng đã có
    OpenCreate: function () {
        var _insuranceContractNo = "";
        if ($('#tblist_Insurance_wrapper  input:checked').length == 0)
            Notification.Error("", "Vui lòng chọn hợp đồng");
        else if ($('#tblist_Insurance_wrapper  input:checked').data('processstatuscode') != "A")
            Notification.Error("", "Không đủ điều kiện để tạo phiếu thu");
        else {
            var insuranceContractNo = $('#tblist_Insurance_wrapper  input:checked').data('insurancecontractno');
            location.href = "/payment/manager/CreateRepayment?insuranceContractNo=" + insuranceContractNo + "";
        }
    },

    // Add new hoàn toàn
    OpenAddNew: function () {
        location.href = "/payment/manager/CreateRepayment";
    },


    Insert: function () {
        var _sumscheduleTotalAmount = $('#Repayment_tblist tr').data('sumscheduletotalamount');
        var param = {
            scheduleId: $('#scheduleId').val(),
            insuranceId: $('#insuranceId').val(),
            ftttNo: $('#ftttNo').val(),
            isExternalRepayment: $("input[id=isExternalRepayment]").is(":checked") ? 1 : 0,
            paymentDate: $('#paymentDate').val(),
            totalAmount: parseFloat($('#totalAmount').val()),
            fileList: attachFile.getFilelist(),
        };
        if (param.totalAmount < _sumscheduleTotalAmount) {
            Notification.Error("", "Chưa đủ số tiền cần thanh toán. Không cho phép lưu thông tin");
        }
        else {
            $.ajax({
                url: '/payment/Manager/Insert',
                type: 'post',
                datatype: 'json',
                data: param,
                success: function (res) {
                    if (res.errorCode == 0 && res.errorMessage != null) {
                        Notification.Success("", "Thêm phiếu thành công!");
                        insurance.Search();
                    }
                    else
                        Notification.Error("", res.errorMessage);
                }
            });
        }
    },

    OpenRemove: function () {
    },

    PaymentInauReload: function () {
        location.href = "/payment/manager/RepaymentInau";
    },

    searchInfoUnpaid: function () {

        var param = {
            scheduleId: $('#scheduleId').val(),
            insuranceContractNo: $('#insuranceContractNo').val(),
        };
        $('#Repayment_tblist tbody').empty();
        location.href = "/payment/manager/CreateRepayment?insuranceContractNo=" + param.insuranceContractNo + "";
    },

    loadPaymentList: function () {
        var data = [];
        var currentSelect = parseInt($("select[id=scheduleId] :selected").val());
        var lstPaymentStr = $("select[id=scheduleId] :selected").attr('paymentList');
        var schedulePaymentDate = $("select[id=scheduleId] :selected").attr('schedulepaymentdate');
        var sumschedulePaymentAmount = 0;
        var sumscheduleVatAmount = 0;
        var sumscheduleTotalAmount = 0;

        $('#schedulePaymentDate').val(schedulePaymentDate);

        if (lstPaymentStr != "null") {
            var lstlstPaymentObj = JSON.parse(lstPaymentStr);
            for (var item = 0; item < lstlstPaymentObj.length; item++) {
                if (currentSelect == lstlstPaymentObj[item].scheduleId) {
                    data.push(lstlstPaymentObj[item]);
                    sumschedulePaymentAmount += lstlstPaymentObj[item].schedulePaymentAmount;
                    sumscheduleVatAmount += lstlstPaymentObj[item].scheduleVatAmount;
                    sumscheduleTotalAmount += lstlstPaymentObj[item].scheduleTotalAmount;
                }
            }

            $.ajax({
                url: '/payment/Manager/_LoadDataPaymentSchedule',
                type: 'post',
                datatype: 'json',
                data: { lstPaymentObj: data },
                success: function (res) {
                    $('#createTbody').html(res);
                }
            });
        }
        setTimeout(function () {
            $('#schedulePaymentAmount').val(sumschedulePaymentAmount);
            $('#sumscheduleVatAmount').val(sumscheduleVatAmount);
            $('#sumScheduleTotalAmount').val(sumscheduleTotalAmount);
            loadformatnumber();
        }, 1000);

    },

    Close: function () {
        location.href = "/payment/manager";
    },


    validfttno: function () {
        debugger
        var sttVal = $('#ftttNo').val();
        var isError = false;
        var messError = '';
        if (sttVal.length == 0) {
            isError = true;
            messError += 'Vui lòng nhập </br>';
        }
        if (checkDiacritics(sttVal)) {
            isError = true;
            messError += 'Vui lòng không nhập unicode, tiếng việt có dấu </br>';
        }
        if (checkSpecialChar(sttVal)) {
            isError = true;
            messError += 'Vui lòng không nhập ký tự đặc biệt </br>';
        }
        if (checkSpace(sttVal)) {
            isError = true;
            messError += 'Vui lòng không nhập khoảng trắng </br>';
        }
        if (isError) {
            Notification.Error("", messError);
            $('label[id=ftttNo]').attr('title', messError);
            $('input[id=ftttNo]').addClass('border-red');
        }
        else {
            $('input[id=ftttNo]').removeClass('border-red');
        }
        return isError;
    }
}