var cpProductTable;
var productListInau = {
    Init: function () {
        $('body').on('click', '#btnSearch', this.Search);
        $('body').on('click', '#btnOpenApprove', this.OpenApprove);
        productListInau.InitTable();
    },
    InitTable: function () {
        cpProductTable = $('#cpProductTable').DataTable({
            "bLengthChange": false,
            "searching": false,
            "proccessing": true,
            "scrollX": true,
            "ordering": false,
            "serverSide": true,
            "ajax": {
                url: '/CPProduct/manager/SearchInau',
                "contentType": "application/json",
                "type": "POST",
                "data": function (d) {

                    var param = {
                        isInau: $('input[name=isInau]:checked').val(),
                        fromDate: $('#fromDate').val(),
                        toDate: $('#toDate').val(),
                        cif: $('#cif').val(),
                        shortName: $('#shortName').val(),
                        cpGroupCode: $('#cpGroupCode option:selected').val(),
                        userUpdated: $('#userUpdated').val(),
                        pageSize: 0,
                    };
                    return JSON.stringify($.extend(d, param));
                },
                "dataSrc": function (json) {
                    //Make your callback here.
                    $('#GridNews').append('<input name="totalRows" type="hidden" value="' + json.recordsTotal + '" >');
                    return json.data;
                }
            },
            "fnDrawCallback": function (settings, json) {
                $('#cpProductTable').on('click', 'input[name=rProduct]', productListInau.selectRow);
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
                // data
                {
                    "render": function (data, type, row) {
                        return '<input class="cursor_pointer" type="radio" name="rProduct" data-counterPartyId=' + row.counterPartyId + ' data-cifCounterParty="' + row.cifCounterParty + '" data-counterPartyGroup="' + row.counterPartyGroup + '" data-isInau=' + row.isInau + ' />'
                    }, "orderable": false, "className": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        if (row.cifCounterParty == null) {
                            return "";
                        }
                        return '<a href = "/CPProduct/manager/approve?counterPartyId=' + row.counterPartyId + '&cifCounterParty =' + row.cifCounterParty + '&counterPartyGroup=' + row.counterPartyGroup + '&isInau=' + row.isInau+'"  target="_blank">' + row.cifCounterParty + '</a >'

                    },
                    "class": "text-left",
                },
                {
                    "render": function (data, type, row) {
                        if (row.cifCounterParty == null) {
                            return '<a href = "/CPProduct/manager/approve?counterPartyId=' + row.counterPartyId + '&cifCounterParty =' + row.cifCounterParty + '&counterPartyGroup=' + row.counterPartyGroup + '&isInau=' + row.isInau +'"  target="_blank">' + row.counterPartyName + '</a >';
                        }
                        return row.counterPartyName;

                    },
                    "class": "text-left",
                },
                { "data": "paymentAccount", "class": "text-center" },
                { "data": "productStatus", "class": "text-center" },
                { "data": "lastUserUpdated", "class": "text-center" },
                {
                    "render": function (data, type, row) {
                        return convertDate(new Date(parseInt(row.lastDatetimeUpdated.substr(6))));

                    },
                    "class": "text-left",
                },

            ],
        });
    },
    Search: function () {
        cpProductTable.ajax.reload();
    },
    OpenApprove: function () {
        var $row = $('#cpProductTable').find('input[name=rProduct]:checked').first();
        var counterPartyGroup = $row.attr('data-counterPartyGroup');
        var counterPartyId = $row.attr('data-counterPartyId');
        var cifCounterParty = $row.attr('data-cifCounterParty');
        var isInau = $row.attr('data-isInau');
        if (counterPartyGroup != '') {
            var url = "/CPProduct/Manager/approve?counterPartyGroup=" + counterPartyGroup + "&counterPartyId=" + counterPartyId + "&cifCounterParty=" + cifCounterParty + "&isInau=" + isInau;
            location.href = url;
        }
        else {
            Notification.Error("", "Chưa chọn sản phẩm");
        }
    },
    selectRow: function () {
        $this = $(this);
        if ($('#cpProductTable').find('input[name=rProduct]:checked').length > 0) {
            $('#btnOpenApprove').removeAttr('disabled');
           
        }
        else {
            $('#btnOpenApprove').attr('disabled', true);
        }
    },
}