var tbUserList = $('#tbUserList');
var userList = {
    Init: function () {
        $('body').on('click', '#btnSearch', this.Search);
        $('body').on('change', 'input[id=btnImportUser]', this.importUserList);
        this.InitTable();
    },
    InitTable: function () {
        cpProductTable = tbUserList.DataTable({
            "bLengthChange": false,
            "searching": false,
            "proccessing": true,
            "scrollX": true,
            "ordering": false,
            "serverSide": true,
            "ajax": {
                url: '/sys/user/Search',
                "contentType": "application/json",
                "type": "POST",
                "data": function (d) {

                    var param = {
                        userName: $('#userName').val(),
                        fullName: $('#fullName').val(),
                        branchCode: $('select[name=branchCode] option:selected').val(),
                        isActive: $('select[name=isActive] option:selected').val(),
                        isDelete: 0,
                        pageSize: 0,
                    };
                    return JSON.stringify($.extend(d, param));
                },
                "dataSrc": function (json) {
                    //Make your callback here.
                    $('#gridUser').append('<input name="totalRows" type="hidden" value="' + json.recordsTotal + '" >');
                    return json.data;
                }
            },
            "fnDrawCallback": function (settings, json) {
                //$('#tbUserList').on('click', 'input[name=rProduct]', productList.selectRow);
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
                        return '<a href="/Sys/User/View?userName=' + row.userName + '">' + row.userName + '</a>'
                    }
                },
                { "data": "fullName", "class": "text-center" },
                { "data": "officerCode", "class": "text-center" },
                { "data": "userBranchCode", "class": "text-center" },
                { "data": "isActive", "class": "text-center" },
                { "data": "isDelete", "class": "text-center" },
                { "data": "lastUserUpdated", "class": "text-center" },
                {
                    "render": function (data, type, row) {
                        return convertDate(new Date(parseInt(row.lastDatetimeUpdated.substr(6))));

                    },
                    "class": "text-left",
                },
                { "data": "isDelete", "class": "text-center" },
            ],
        });
    },
    Search: function () {
        cpProductTable.ajax.reload();
    },
    importUserList: function () {
        var files = $("#btnImportUser").get(0).files;

        var formData = new FormData();
        formData.append('btnImportUser', files[0]);

        $.ajax({
            url: '/Sys/User/ImportUserList',
            data: formData,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.Code == 0) {
                    Notification.Success("", data.errorMessage);
                }
                else {
                    Notification.Error("", data.errorMessage);
                }
            }
        });
    }
}