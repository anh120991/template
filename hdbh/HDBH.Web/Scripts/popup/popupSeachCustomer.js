var _isInitTable = 0;
var customerTable;
var funcAfterLoad ;
var popupSearchCus = {
    Init: function (functionAfterload) {
        funcAfterLoad = functionAfterload;
        $('body').on('click', '#btnCallPopupSearchCustomer', this.LoadForm);
    },
    InitTable: function () {
        customerTable = $('#customerPopupTable').DataTable({
            "bLengthChange": false,
            "searching": false,
            "proccessing": true,
            "scrollX": true,
            "ordering": false,
            "serverSide": true,
            "width": "100%",
            "ajax": {
                url: '/common/searchPopupCustomer',
                "contentType": "application/json",
                "type": "POST",
                "data": function (d) {

                    var param = {
                        cif: $('#pCif').val(),
                        name: $('#pName').val(),
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
                        return '<button class="btn btn-primary" data-value=\'' + JSON.stringify(row) + '\' name="selectRow"><i class="fa fa-arrow-circle-down" aria-hidden="true"></i></button>'
                    }, "orderable": false, "className": "text-center"
                },

                { "data": "cif", "class": "text-center" },
                { "data": "customerName", "class": "text-center" },
                { "data": "refT24", "class": "text-center" },
                { "data": "customerShortName", "class": "text-center" },
                { "data": "lastUserUpdated", "class": "text-center" },
                { "data": "lastDatetimeUpdated", "class": "text-center" },

            ],
        });
    },
    Search: function () {
        customerTable.ajax.reload();
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
                f().then(popupSearchCus.EventAfterLoad());

            }
        });
    },
    EventAfterLoad: function () {
        async function f() {
            popupSearchCus.InitTable();
            $('#customerPopupTable').hide();
            showLoading();
        }
        f().then(
            setTimeout(function () {
                $('#customerPopupTable').DataTable().columns.adjust();
                $('#customerPopupTable').show();
                hideLoading();
            }, 1000)
        );
        $('#popupArea').on('click', '#btnPopupSearch', this.Search);
        $('#popupArea').on('click', 'button[name=selectRow]', funcAfterLoad);
    }
}