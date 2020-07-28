$(window).on('load', function () {
    paymentInau.init();
    paymentInau.initTable();
    var gridPaymentInau;
})

var paymentInau = {
    pageId: $('#rootList'),
    formId: $('#rootList #frmSearch'),
    gridId: $('#gridPaymentManagementInau #Repayment_tblist'),

    init: function () {
        paymentInau.formId.on('click', '#btnSearch', paymentInau.Search);
    },

    initTable: function () {
        //debugger
        gridPaymentInau = $('#gridPaymentManagementInau #Repayment_tblist').DataTable({
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
                paymentInau.fnDrawCallback();
            },
            "ajax": {
                'url': '/payment/manager/searchRepaymentInau',
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
                    $('#gridPaymentManagementInau #Repayment_tblist').append('<input name="totalRows" type="hidden" value="' + json.recordsTotal + '" >');
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
                        return '<input name="selectId" type="radio" onchange="selectRadio(this)" data-insuranceid="' + row.insuranceId + '" data-repaymentid ="' + row.repaymentId + '" data-processstatuscode ="' + row.processStatusCode + '" />'
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
        gridPaymentInau.ajax.reload();
    },

    fnDrawCallback: function () {
        loadformatnumber();
        gridPaymentInau.columns.adjust();
    },

    //SelectRadio: function () {
    //    var repaymentData = $('#Repayment_tblist_wrapper input:checked');
    //    var processCode = repaymentData.data('processstatuscode');
    //    if (processCode != "INAU")
    //        $('#btnApprove').attr("disabled", true);
    //}

}

function selectRadio(e) {
    var $data = gridPaymentInau.row($(e).parents('tr')).data();

    if ($data.processStatusCode != 'INAU')
        $('#btnApprove').attr("disabled", true);
    else
        $('#btnApprove').removeAttr("disabled");
}