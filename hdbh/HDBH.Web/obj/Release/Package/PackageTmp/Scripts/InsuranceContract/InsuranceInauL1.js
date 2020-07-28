$(window).on('load', function () {
    insuranceinauL1.init();
    insuranceinauL1.initTable();
    var table;
    //insuranceinauL1.LoadStatus();
})

var insuranceinauL1 = {
    pageId: $('#rootList'),
    formId: $('#rootList #frmSearch'),
    gridId: $('#rootList #gridInsuranceInauL1 #tblist_Insurance'),
    init: function () {
        insuranceinauL1.formId.on('click', '#btnSearchInauL1', insuranceinauL1.Search);
        insuranceinauL1.pageId.on('click', '#btnApprove', insuranceinauL1.OpenApproveUpdate)
    },
    initTable: function () {
        table = $('#gridInsuranceInauL1 #tblist_Insurance').DataTable({
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
                insuranceinauL1.fnDrawCallback();
            },
            "ajax": {
                'url': '/InsuranceContract/manager/searchInsuranceContractInau',
                'contentType': 'application/json',
                'type': 'POST',
                'data': function (d) {
                    // Truyền param
                    var param = {
                        fromDate: $('#fromDate').val(),
                        toDate: $('#toDate').val(),
                        insuranceContractNo: $('#insuranceContractNo').val(),
                        contractFormCode: $('#contractFormCode').val(),
                        userUpdated: $('#userUpdated').val(),
                        _processStatusList: $('#processStatusList').val(),
                        customerCif: $('#customerCif').val(),
                        branchCode: $('#branchCode').val(),
                    }
                    return JSON.stringify($.extend(d, param));
                },
                "dataSrc": function (json) {

                    //Make your callback here.
                    $('#gridInsuranceInauL1 #tblist_Insurance').append('<input name="totalRows" type="hidden" value="' + json.recordsTotal + '" >');
                    return json.data;
                }
            },
            "fnDrawCallback": function (settings, json) {
                $('#gridInsuranceInauL1').on('click', 'input[name=dataSelecting]', insuranceinauL1.selectRow);
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
                        return '<input type="radio" onlick="SelectRadio(this)" class="cursor_pointer" name="dataSelecting" data-insuranceid="' + row.insuranceId + '" data-contracttypecode="' + row.contractTypeCode + '" data-contracttypename="' + row.contractTypeName +
                            '" data-processstatuscode="' + row.processStatusCode + '" data-insurancecontractno="' + row.insuranceContractNo +'" />'
                    },
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        return '<a href="/InsuranceContract/manager/ApproveInsuranceContract?insuranceId=' + row.insuranceId + '&&?insuranceContractNo=' + row.insuranceContractNo + '&&?mode=VIEW">' + row.insuranceContractNo + '</a>'
                    },
                    "class": "text-center"
                },
                { "data": "customerInfo", "class": "text-center" },
                { "data": "contractFormName", "class": "text-center" },

                { "data": "insuranceValue", "class": "text-center" },
                { "data": "_effectiveDate", "class": "text-center" },
                { "data": "_dueDate", "class": "text-center" },
                { "data": "processStatusName", "class": "text-center" },
                { "data": "lastUserUpdated", "class": "text-center" },
                { "data": "_lastDatetimeUpdated", "class": "text-center" },
            ]
        });
    },

    Search: function () {
        table.ajax.reload();
    },

    fnDrawCallback: function () {
        loadformatnumber();
        table.columns.adjust();
    },

    OpenApproveUpdate: function () {
        if ($('#tblist_Insurance_wrapper input:checked').length == 0) {
            Notification.Error("", "Vui lòng chọn giao dịch");
        }
        else {
            var datasll = $('#tblist_Insurance_wrapper input:checked');
            var param = {
                insuranceId: datasll.data('insuranceid'),
                mode: "APPROVE",
                pApproveLevel: 1,
                insuranceContractNo: datasll.data('insurancecontractno'),
            }
            var url_param = "?insuranceId=" + param.insuranceId + "&&mode=" + param.mode + "&&pApproveLevel=" + param.pApproveLevel + "&&insuranceContractNo=" + param.insuranceContractNo + "";
            location.href = "/InsuranceContract/manager/ApproveInsuranceContract" + url_param;
        }
    },
    selectRow: function () {
        $this = $(this);
        if ($('#gridInsuranceInauL1').find('input[name=dataSelecting]:checked').length > 0) {
            $('#btnApprove').removeAttr('disabled');
        }
        else {
            $('#btnApprove').attr('disabled', true);
        }
    },
};