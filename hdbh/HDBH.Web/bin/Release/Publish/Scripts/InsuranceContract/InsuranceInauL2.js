$(window).on('load', function () {
    insuranceinauL2.init();
    insuranceinauL2.initTable();
    var table;
})

var insuranceinauL2 = {
    pageId: $('#rootList'),
    formId: $('#rootList #frmSearch'),
    gridId: $('#rootList #gridInsuranceInauL2 #tblist_Insurance'),
    init: function () {
        insuranceinauL2.formId.on('click', '#btnSearchInauL1', insuranceinauL2.Search);
        insuranceinauL2.pageId.on('click', '#btnApprove', insuranceinauL2.Approve);
    },
    initTable: function () {
        debugger
        table = $('#gridInsuranceInauL2 #tblist_Insurance').DataTable({
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
                insuranceinauL2.fnDrawCallback();
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
                    $('#gridInsuranceInauL2 #tblist_Insurance').append('<input name="totalRows" type="hidden" value="' + json.recordsTotal + '" >');
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

                {
                    "render": function (data, type, row) {
                        return '<input type="radio" onlick="SelectRadio(this)" class="cursor_pointer" data-insuranceid="' + row.insuranceId + '" data-insurancecontractno="' + row.insuranceContractNo + '" data-contracttypename="' + row.contractTypeName +
                            '" data-processstatuscode="' + row.processStatusCode + '" />'
                    },
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        return '<a href="/InsuranceContract/manager/ApproveInsuranceContract?insuranceId=' + row.insuranceId + '">' + row.insuranceContractNo + '</a>'
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

    Approve: function () {
        if ($('#tblist_Insurance_wrapper input:checked').length == 0) {
            Notification.Error("", "Vui lòng chọn giao dịch");
        }
        else {
            var datasll = $('#tblist_Insurance_wrapper input:checked');
            var param = {
                insuranceId: datasll.data('insuranceid'),
                mode: "APPROVE",
                pApproveLevel: 2,
                insuranceContractNo: datasll.data('insurancecontractno'),
            }
            var url_param = "?insuranceId=" + param.insuranceId + "&&mode=" + param.mode + "&&pApproveLevel=" + param.pApproveLevel + "&&insuranceContractNo=" + param.insuranceContractNo + "";
            location.href = "/InsuranceContract/manager/ApproveInsuranceContract" + url_param;
            
        }
    }
};