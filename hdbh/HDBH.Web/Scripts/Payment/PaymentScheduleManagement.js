$(window).on('load', function () {
    paymentschedule.init();
    paymentschedule.initTable();
    var gridPaymentSchedule;

})

var paymentschedule = {
    pageId: $('#rootList'),
    formId: $('#rootList #frmSearch'),
    gridId: $('#schedulePayment_tblist'),

    init: function () {
        paymentschedule.formId.on('click', '#btnSearch', paymentschedule.Search);
    },

    initTable: function () {
        gridPaymentSchedule = paymentschedule.gridId.DataTable({
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
                paymentschedule.fnDrawCallback();
            },
            "ajax": {
                'url': '/payment/manager/searchRepaymentSchedule',
                'contentType': 'application/json',
                'type': 'POST',
                'data': function (d) {
                    var _paymentStatus = [];
                    _paymentStatus = $('#paymentStatus').val();
                    // Truyền param
                    var param = {
                        fromDate: $('#fromDate').val(),
                        toDate: $('#toDate').val(),
                        insuranceContractNo: $('#insuranceContractNo').val(),
                        _paymentStatus: _paymentStatus != null ? _paymentStatus : null,
                        customerCif: $('#customerCif').val(),
                    }
                    return JSON.stringify($.extend(d, param));
                },
                "dataSrc": function (json) {

                    //Make your callback here.
                    $('#schedulePayment_tblist').append('<input name="totalRows" type="hidden" value="' + json.recordsTotal + '" >');
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
                        return '<input name="selectId" type="radio" onlick="SelectRadio(e)" data-insuranceid="' + row.insuranceId + '" data-repaymentid ="' + row.repaymentId + '" />'
                    },
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        return '<a href="/payment/manager/ApproveInsuranceContract?insuranceContractNo=' + row.insuranceContractNo + '&&mode=VIEW">' + row.insuranceContractNo + '</a>'
                    }
                },
                { "data": "scheduleId", "class": "text-center" },
                { "data": "_schedulePaymentDate", "class": "text-center" },
                { "data": "scheduleTotalAmount", "class": "text-center" },
                { "data": "customerInfo", "class": "text-center" },
                { "data": "paymentStatus", "class": "text-center" },
            ]
        });
    },

    Search: function () {
        gridPaymentSchedule.ajax.reload();
    },

    fnDrawCallback: function () {
        loadformatnumber();
        gridPaymentSchedule.columns.adjust();
    }
};