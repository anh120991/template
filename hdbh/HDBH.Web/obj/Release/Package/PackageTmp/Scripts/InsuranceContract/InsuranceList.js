var insuranceList = {
    Init: function () {
        $('body').on('click', '#btnAdd', this.LoadForm);
        $('body').on('change', '#cbExcept', this.changeExcept);
    },
    LoadForm: function () {
        var modal = $(this).data("target");
        var viewName = $(this).data("template");
        var title = $(this).data("title");
        $.ajax({
            type: "GET",
            url: "/Common/loadFormByView",
            data: { viewName: viewName }, // serializes the form's elements.
            success: function (data) {
                async function f() {
                    $(modal).find('h5.modal-title').text(title);
                    $(modal).find('.modal-body').html(data);
                }
                f().then(function k() {
                    $('body').on('change', '#slContractType', insuranceList.chageContractType);
                    $('body').on('click', '#btnCountinue', insuranceList.OpenCreate);
                });

            }
        });
    },
    chageContractType: function () {
        var _val = $(this).find('option:selected').val();
        $.ajax({
            type: "POST",
            url: "/InsuranceContract/Manager/getListExceptType",
            data: { insuranceContractType: _val }, // serializes the form's elements.
            dataType: "json",
            success: function (data) {
                if ($('#exceptType').attr('disabled') != 'disabled' && $('#exceptType').attr('disabled') != true ) {
                    $('#exceptType').html(data);
                }
            }
        });
    },
    OpenCreate: function () {
        var pTypeCode = $('#slContractType option:selected').val();
        var pTypeName = $('#slContractType option:selected').text();
        var pExceptCode = $('#exceptType option:selected').val();
        if (pTypeCode != '') {
            var url = "/InsuranceContract/Manager/Create?pFormCode=new&pTypeCode=" + pTypeCode + "&pTypeName=" + pTypeName + "&pExceptCode=" + pExceptCode;
            location.href = url;
        }
        else {
            Notification.Error("", "Chưa chọn loại hợp đồng");
        }
    },
    changeExcept: function () {
        var isCheck = $(this).is(':checked');
        if (isCheck) {
            $('#exceptType').removeAttr('disabled');
            $('#slContractType').change();
        }
        else {
            $('#exceptType').val('');
            $('#exceptType').attr('disabled', true);
        }
    }
}