var cpProductTable;
var productList = {
    Init: function () {
        $('body').on('click', '#btnAdd', this.LoadForm);
        $('body').on('click', '#btnSearch', this.Search);
        $('body').on('click', '#btnEdit', this.EditContract);
        $('body').on('click', '#btnRemove', this.removeProduct);

        productList.InitTable();
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
                url: '/CPProduct/manager/Search',
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
                        status: $('#status option:selected').val(),
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
                $('#cpProductTable').on('click', 'input[name=rProduct]', productList.selectRow);
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
                        return '<input class="cursor_pointer" type="radio" name="rProduct" data-counterPartyId="' + row.counterPartyId + '" data-cifCounterParty="' + row.cifCounterParty + '" data-counterPartyGroup="' + row.counterPartyGroup + '" data-isInau="' + row.isInau + '" data-productStatus ="'+row.productStatus+'" />'
                    }, "orderable": false, "className": "text-center"
                },
                { "data": "cifCounterParty", "class": "text-center" },
                {
                    "render": function (data, type, row) {
                        return '<a href = "/CPProduct/manager/view?counterPartyId=' + row.counterPartyId + '&cifCounterParty =' + row.cifCounterParty + '&counterPartyGroup=' + row.counterPartyGroup + '&isInau=' + row.isInau + '"  target="_blank">' + row.counterPartyName + '</a >';

                    },
                    "class": "text-left",
                },
                { "data": "paymentAccount", "class": "text-center" },
                { "data": "counterPartyGroupName", "class": "text-center" },
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
                    $('body').on('change', '#slCounterPartyGroup', productList.loadCounterPartyByGroupId);
                    $('body').on('click', '#btnCountinue', productList.OpenCreate);
                    $('#slCounterParty').select2();
                });

            }
        });
    },
    loadCounterPartyByGroupId: function () {
        var groupCode = $(this).find('option:selected').val();
        $.ajax({
            type: "GET",
            url: "/CPProduct/Manager/LoadCounterPartyByGroupId",
            data: { groupCode: groupCode }, // serializes the form's elements.
            success: function (data) {
                $('#slCounterParty').html(data);
            }
        });
    },
    OpenCreate: function () {
        var counterPartyGroup = $('#slCounterPartyGroup option:selected').val();
        var counterPartyId = $('#slCounterParty option:selected').val();
        var cifCounterParty = $('#slCounterParty option:selected').attr('data-cif');
        if (counterPartyGroup != '') {
            var url = "/CPProduct/Manager/Create?counterPartyGroup=" + counterPartyGroup + "&counterPartyId=" + counterPartyId + "&cifCounterParty=" + cifCounterParty;
            location.href = url;
        }
        else {
            Notification.Error("", "Chưa chọn nhóm đối tác");
        }
    },
    selectRow: function () {
        $this = $(this);
        if ($('#cpProductTable').find('input[name=rProduct]:checked').length > 0) {
            $('#btnEdit').removeAttr('disabled');
            if (($this.attr('data-productStatus') == 'INAU' || $this.attr('data-productStatus') == 'EDIT') && $this.attr('data-isInau') == 1) {
                $('#btnRemove').removeAttr('disabled');
            }
            else {
                $('#btnRemove').attr('disabled', true);
            }
        }
        else {
            $('#btnEdit').attr('disabled', true);
        }
    },
    EditContract: function () {
        $row = $('#cpProductTable').find('input[name=rProduct]:checked').first();
        var counterPartyId = $row.attr('data-counterPartyId');
        var cifCounterParty = $row.attr('data-cifCounterParty');
        var counterPartyGroup = $row.attr('data-counterPartyGroup');
        var isInau = $row.attr('data-isInau');
        var url = ('/CPProduct/manager/update?counterPartyId=' + counterPartyId + '&cifCounterParty =' + cifCounterParty + '&counterPartyGroup=' + counterPartyGroup + '&isInau=' + isInau).trim();
        location.href = url;
    },
    removeProduct: function () {
        $row = $('#cpProductTable').find('input[name=rProduct]:checked').first();
        var counterPartyId = $row.attr('data-counterPartyId');
        var counterPartyGroup = $row.attr('data-counterpartygroup');
        var status = $row.attr('data-productstatus');

        Lobibox.confirm({
            msg: "Bạn có chắc muốn xóa sản phẩm?",
            callback: function ($this, type, ev) {
                if (type == 'yes') {
                    $.ajax({
                        type: "POST",
                        url: '/CPProduct/manager/deleteCpProdCommis/',
                        dataType: "json",
                        data: {
                            counterPartyId: counterPartyId,
                            counterPartyGroup: counterPartyGroup,
                            status: status
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