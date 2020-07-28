$(window).on('load', function () {
    insuranceAction.init();
})

var insuranceAction = {
    pageId: $('#detailForm'),
    init: function () {
        insuranceAction.pageId.on('click', '#btnApprove', insuranceAction.Approve);
        insuranceAction.pageId.on('click', '#btnReject', insuranceAction.Approve);
        insuranceAction.pageId.on('click', '#btnReqEdit', insuranceAction.Approve);
    },
    //Approve
    Approve: function () {
        var pApproveLevel = $('#btnApprove').attr('pApproveLevel');
        var mode = $('#mode').val();
        var approveStatus = "";//param
        var insuranceId = $('#insuranceId').val();//param
        var approveContent = $("textarea[name=approveContent]").val();//param
        var exceptionTypeCode = $('#exceptionTypeCode').val();

        if (pApproveLevel == 1) {
            if (exceptionTypeCode == "UNCONTRACT_CP" || exceptionTypeCode == "COLLATERAL_OTHER")
                approveStatus = "CHK";
            else
                approveStatus = "A";
        }
        else if (pApproveLevel == 2) {
            approveStatus = "A";
        }
        var param = {
            approveStatus: approveStatus,
            insuranceId: insuranceId,
            approveContent: approveContent,
            pApproveLevel: pApproveLevel
        };

        if (mode == "APPROVE") {
            $.ajax({
                type: "POST",
                url: '/InsuranceContract/manager/Approve',
                data: param, // serializes the form's elements.
                dataType: "json",
                success: function (data) {
                    if (data.errorCode == 0) {
                        Notification.Success("", data.errorMessage);
                        setTimeout(function () {
                            if (pApproveLevel == 1) {
                                location.href = "/InsuranceContract/manager/InsuranceInauManagementL1";
                            } else {
                                location.href = "/InsuranceContract/manager/InsuranceInauManagementL2";
                            }
                        }, 2000);
                        insuranceAction.Exit();
                    }
                    else {
                        Notification.Error("", data.errorMessage);
                    }
                }
            });
        }
    },

    Exit: function () {
        var pApproveLevel = $('#btnSkip').attr('pApproveLevel');
        if (pApproveLevel == 1)
            insuranceinauL1.Search();
        else if (pApproveLevel == 2)
            insuranceinauL1.Search();
    }
}