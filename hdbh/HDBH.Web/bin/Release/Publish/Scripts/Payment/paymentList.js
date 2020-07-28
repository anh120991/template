$(window).on('load', function () {
    payment.init();
    payment.initTable();
    var gridPayment;
    payment.LoadStatus();
})

var payment = {
    pageId: $('#rootList'),
    formId: $('#rootList #frmSearch'),
    gridId: $('#rootList #frmSearch #gridPaymentManagement #Repayment_tblist'),

    init: function () {
        payment.pageId.on('click', '#btnSearch', payment.Search);
        payment.formId.on('change', '#isInau', payment.LoadStatus);
        payment.formId.on('click', '#btnExport', payment.ExportRepaymentManagement);
    },

    initTable: function () {
        //debugger
        gridPayment = $('#gridPaymentManagement #Repayment_tblist').DataTable({
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
                payment.fnDrawCallback();
            },
            "ajax": {
                'url': '/payment/manager/searchlist',
                'contentType': 'application/json',
                'type': 'POST',
                'data': function (d) {
                    var _isInau = parseInt($('#isInau').val());
                    var _externalRepayment = parseInt($('#externalRepayment').val());
                    var _processStatusList = [];
                    _processStatusList = $('#processStatusList').val();
                    // Truyền param
                    var param = {
                        fromDate: $('#fromDate').val(),
                        toDate: $('#toDate').val(),
                        isInau: _isInau,
                        insuranceContractNo: $('#insuranceContractNo').val(),
                        externalRepayment: _externalRepayment,
                        _processStatusList: _processStatusList != null ? _processStatusList : null,
                        ftttNo: $('#ftttNo').val(),
                        userUpdated: $('#userUpdated').val(),
                        branchCode: $('#branchCode').val(),
                    }
                    return JSON.stringify($.extend(d, param));
                },
                "dataSrc": function (json) {

                    //Make your callback here.
                    $('#gridPaymentManagement #Repayment_tblist').append('<input name="totalRows" type="hidden" value="' + json.recordsTotal + '" >');
                    return json.data;
                }
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

                //Columns display
                {
                    "render": function (data, type, row) {
                        return '<input name="selectId" type="radio" onchange="payment.SelectRadio()" data-insuranceid="' + row.insuranceId + '" data-repaymentid ="' + row.repaymentId + '" data-processstatuscode ="' + row.processStatusCode + '" />'
                    },
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        return '<a href="/payment/manager/ApproveRepayment?repaymentId=' + row.repaymentId + '&&mode=VIEW">' + row.ftttNo + '</a>'
                    }
                },
                { "data": "insuranceContractNo", "class": "text-center" },
                { "data": "scheduleId", "class": "text-center" },
                { "data": "totalAmount", "class": "text-center" },
                {
                    "render": function (data, type, row) {
                        if (row.isExternalRepayment == 1)
                            return '<input type="checkbox" checked" disabled/>';
                        else
                            return '<input type="checkbox" disabled/>';
                    },
                    "class": "text-center"
                },
                { "data": "_paymentDate", "class": "text-center" },
                { "data": "processStatusName", "class": "text-center" },
                { "data": "lastUserUpdated", "class": "text-center" },
                { "data": "_lastDatetimeUpdated", "class": "text-center" },
            ]
        });
    },

    Search: function () {
        gridPayment.ajax.reload();
    },

    fnDrawCallback: function () {
        loadformatnumber();
        gridPayment.columns.adjust();
    },

    // Hàm load status khi thay đổi trạng thái Đã duyệt/ Đang xử lý
    LoadStatus: function () {
        var _isInau = $('#isInau').is(':checked') ? 0 : 1;
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
                        result.forEach(function (index, item) {
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

    ExportRepaymentManagement: function () {
        var _isInau = parseInt($('#isInau').val());
        var _externalRepayment = parseInt($('#externalRepayment').val());
        var _processStatusList = [];
        _processStatusList = $('#processStatusList').val();
        // Truyền param
        var param = {
            fromDate: $('#fromDate').val(),
            toDate: $('#toDate').val(),
            isInau: _isInau,
            insuranceContractNo: $('#insuranceContractNo').val(),
            externalRepayment: _externalRepayment,
            _processStatusList: _processStatusList != null ? _processStatusList : null,
            ftttNo: $('#ftttNo').val(),
            userUpdated: $('#userUpdated').val(),
            branchCode: $('#branchCode').val(),
        };
        $.ajax({
            url: '/payment/manager/ExportRepaymentManagement',
            datatype: 'json',
            type: 'post',
            data: param,
            success: function (res) {
                alert(res);
            }
        });
    },

    SelectRadio: function () {
        var tdata = $('#Repayment_tblist_wrapper input:checked');
        if (tdata.data('processstatuscode') == 'INAU' || tdata.data('processstatuscode') == 'REQEDIT')
            $('#btnEdit').removeAttr("disabled");
        else
            $('#btnEdit').attr("disabled", true);
    }

}

