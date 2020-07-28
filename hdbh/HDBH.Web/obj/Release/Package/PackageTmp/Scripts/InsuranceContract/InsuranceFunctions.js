$(window).on('load', function () {
    insurance.init();
    insurance.initTable();
    var table;
    insurance.LoadStatus();
})

var insurance = {
    pageId: $('#rootList'),
    formId: $('#rootList #frmSearch'),
    gridId: $('#rootList #gridInsurance #tblist_Insurance'),

    init: function () {
        insurance.formId.on('click', '#btnSearch', insurance.Search);
        insurance.formId.on('click', '#btnExport', insurance.Export);
        insurance.formId.on('change', '#isInau', insurance.LoadStatus);
        $('body').on('click', '#btnRenew', this.ReNewContract);
        $('body').on('click', '#btnEdit', this.EditContract);
        $('body').on('click', '#btnClose', this.closeContract);

    },

    initTable: function () {
        table = insurance.gridId.DataTable({
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 10,
            "bLengthChange": true,
            "lengthChange": true,
            "searching": true,
            "proccessing": true,
            "scrollX": true,
            "paging": true,
            "ordering": false,
            "serverSide": true,
            'fnDrawCallback': function () {
                insurance.fnDrawCallback();
            },
            "ajax": {
                'url': '/InsuranceContract/manager/SearchList',
                'contentType': 'application/json',
                'type': 'POST',
                'data': function (d) {
                    var _isInau = parseInt($('input[name=isInau]:checked').val());
                    var _processStatusList = [];
                    _processStatusList = $('#processStatusList').val();
                    // Truyền param
                    var param = {
                        fromDate: $('#fromDate').val(),
                        toDate: $('#toDate').val(),
                        isInau: _isInau,
                        insuranceContractNo: $('#insuranceContractNo').val(),
                        refInsuranceNo: $('#refInsuranceNo').val(),
                        contractTypeCode: $('#contractTypeCode').val(),
                        contractFormCode: $('#contractFormCode').val(),
                        userUpdated: $('#userUpdated').val(),
                        _processStatusList: _processStatusList != null ? _processStatusList : null,
                        customerCif: $('#customerCif').val(),
                        renewStatus: $('#renewStatus').val(),
                        branchCode: $('#branchCode').val(),
                    }
                    return JSON.stringify($.extend(d, param));
                },
                "dataSrc": function (json) {

                    //Make your callback here.
                    $('#gridInsurance #tblist_Insurance').append('<input name="totalRows" type="hidden" value="' + json.recordsTotal + '" >');
                    return json.data;
                }
            },
            "fnDrawCallback": function (settings, json) {
                $('body').on('click', 'input[name=selectRow]', insurance.SelectRow);
                loadformatnumber();
            },
            "language": {
                "search": "",
                "searchPlaceholder": "Tìm theo mã...",
                "lengthMenu": "Hiển thị _MENU_",
                "zeroRecords": "Không tìm thấy dữ liệu",
                "info": "Đang hiển thị _START_ đến _END_ của _TOTAL_ dữ liệu",
                "infoEmpty": "Không tìm thấy dữ liệu",
                "infoFiltered": "(lọc từ _MAX_ tất cả)",
                "paginate": {
                    "first": "Đầu",
                    "last": "Cuối",
                    "next": icon_next,
                    "previous": icon_review
                },
            },
            "dom": '<"top">rt<"bottom"<"col-sm-4"i><"col-sm-8"pl>>',
            "columns": [

                {
                    "render": function (data, type, row) {
                        return '<input type="radio" onlick="SelectRadio(e)" data-insuranceid="' + row.insuranceId + '" data-contracttypecode="' + row.contractTypeCode + '" data-contracttypename="' + row.contractTypeName +
                            '" data-processstatuscode="' + row.processStatusCode + '" name="selectRow" class="cursor_pointer" data-contractNo="' + row.insuranceContractNo + '" data-exceptionTypeCode="' + row.exceptionTypeCode + '" data-dueDate="' + row.dueDate + '" />'
                    },
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        return '<a href="/InsuranceContract/manager/View?insuranceId=' + row.insuranceId + '&insuranceContractNo=' + row.insuranceContractNo + '">' + row.insuranceContractNo + '</a>'
                    }
                },
                { "data": "customerInfo", "class": "text-center" },
                { "data": "insuranceValue", "class": "text-center numberinput2" },
                { "data": "_effectiveDate", "class": "text-center" },
                { "data": "_dueDate", "class": "text-center" },
                { "data": "processStatusName", "class": "text-center" },
                {
                    "render": function (data, type, row) {
                        if (row.isRenewSchedule == 1)
                            return '<input type="checkbox" checked" disabled/>';
                        else
                            return '<input type="checkbox" checked" disabled/>';
                    },
                    "class": "text-center"
                },
                { "data": "lastUserUpdated", "class": "text-center" },
                { "data": "_lastDatetimeUpdated", "class": "text-center" },
            ]
        });
    },

    Search: function () {
        table.ajax.reload();
    },

    Export: function () {
        var param = {
            fromDate: $('#fromDate').val(),
            toDate: $('#toDate').val(),
            isInau: _isInau,
            insuranceContractNo: $('#insuranceContractNo').val(),
            refInsuranceNo: $('#refInsuranceNo').val(),
            contractTypeCode: $('#contractTypeCode').val(),
            contractFormCode: $('#contractFormCode').val(),
            userUpdated: $('#userUpdated').val(),
            _processStatusList: $('#processStatusList').val(),
            customerCif: $('#customerCif').val(),
            renewStatus: $('#renewStatus').val(),
            branchCode: $('#branchCode').val(),
            pageNumber: 0,
            pageSize: 0,
        };
        $.ajax({
            url: '/InsuranceContract/manager/_ExportExcel',
            dataType: 'json',
            type: 'POST',
            data: param,
            success: function (res) {
                if (res != null) {
                    alert('OKKKKK');
                }
            }
        });
    },

    fnDrawCallback: function () {
        loadformatnumber();
        table.columns.adjust();
    },

    // Hàm load status khi thay đổi trạng thái Đã duyệt/ Đang xử lý
    LoadStatus: function () {
        var _isInau = $('input[id=actived]').is(':checked') ? 0 : 1;
        $('#processStatusList').html(null);
        if (_isInau != null) {
            $.ajax({
                url: '/InsuranceContract/manager/_getListStatus',
                datatype: 'json',
                type: 'post',
                data: { inau: _isInau },
                success: function (result) {
                    var itemOption = "<option value=''>-- Tất cả --</option>";
                    if (result) {
                        //result.forEach(function (index, item) {
                        $.each(result, function (key, index) {
                            //alert('Has data');
                            itemOption += '<option value=' + index.processStatusCode + '>' + index.processStatusName + '</option>'
                        })
                        $('#processStatusList').append(itemOption);
                    }
                    else
                        $('#processStatusList').append(itemOption);
                }
            });
        }
    },
    SelectRow: function () {
        $this = $(this);
        if ($('#gridInsurance').find('input[name=selectRow]:checked').length > 0) {
            $('#btnRenew').removeAttr('disabled');
            var processstatuscode = $this.attr('data-processstatuscode');
            if (processstatuscode == 'INAU' || processstatuscode == 'REQEDIT') {
                $('#btnEdit').removeAttr('disabled');
            }
            else {
                $('#btnEdit').attr('disabled', true);
            }
            if (processstatuscode == 'A') {
                var numberPattern = /\d+/g;
                var dueDate = $this.attr('data-dueDate').match(numberPattern);
                var currentDate = new Date().getTime();
                if (dueDate <= currentDate) {
                    $('#btnClose').removeAttr('disabled');
                }
                else {
                    $('#btnClose').attr('disabled', true);
                }
            }
            else {
                $('#btnClose').attr('disabled', true);
            }
        }
        else {
            $('#btnRenew').attr('disabled', true);
        }
    },
    ReNewContract: function () {
        $row = $('#gridInsurance').find('input[name=selectRow]:checked').first();
        var pContractNo = $row.attr('data-contractNo');
        var pTypeCode = $row.attr('data-exceptionTypeCode');
        var pTypeName = $row.attr('data-contracttypename');
        var pExceptCode = $row.attr('data-contracttypecode');
        if (pTypeCode != '') {
            var url = "/InsuranceContract/Manager/Create?pFormCode=RENEW&pTypeCode=" + pTypeCode + "&pTypeName=" + pTypeName + "&pExceptCode=" + pExceptCode + "&pContractNo=" + pContractNo;
            location.href = url;
        }
        else {
            Notification.Error("", "Chưa chọn loại hợp đồng");
        }
    },
    EditContract: function () {
        $row = $('#gridInsurance').find('input[name=selectRow]:checked').first();
        var insuranceid = $row.attr('data-insuranceid');
        var contractno = $row.attr('data-contractno');
        var url = ("/InsuranceContract/Manager/Update?insuranceId="+insuranceid+ "&insuranceContractNo=" + contractno).trim();
        location.href = url;
    },
    closeContract: function () {
        $row = $('#gridInsurance').find('input[name=selectRow]:checked').first();
        var insuranceId = $row.attr('data-insuranceid');
        var insuranceContractNo = $row.attr('data-contractno');
        Lobibox.confirm({
            msg: "Bạn có chắc muốn đóng hợp đồng " + insuranceContractNo + "?",
            callback: function ($this, type, ev) {
                if (type == 'yes') {
                    $.ajax({
                        type: "POST",
                        url: '/InsuranceContract/manager/updateCloseInsurContract/',
                        dataType: "json",
                        data: {
                            insuranceId: insuranceId,
                            insuranceContractNo: insuranceContractNo
                        },
                        success: function (data) {
                            if (data.errorCode == 0) {
                                Notification.Success("", data.errorMessage);
                            }
                            else {
                                Notification.Error("", data.errorMessage);
                            }
                        }
                    });
                }

            }
        });
    }
}