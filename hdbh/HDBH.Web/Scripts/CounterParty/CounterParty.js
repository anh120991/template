var counterPartyListTable;

var CounterPartyList = {
    Init: function () {
       
        $('body').on('click', '#btnSearch', this.Search);
        $('body').on('click', '#btnExport', this.exportExcel);
        $('body').on('click', '#btnEdit', this.EditContract);
        CounterPartyList.InitTable();
    },
    InitTable: function () {
        counterPartyListTable = $('#counterPartyTable').DataTable({
            "bLengthChange": false,
            "searching": false,
            "proccessing": true,
            "scrollX": true,
            "ordering": false,
            "serverSide": true,
            "ajax": {
                url: '/counterparty/manager/Search',
                "contentType": "application/json",
                "type": "POST",
                "data": function (d) {

                    var param = {
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
                $('#counterPartyTable').on('click', 'input[name=rCounterParty]', CounterPartyList.selectRow);
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
                        return '<input class="cursor_pointer" type="radio" name="rCounterParty" data-counterPartyId="' + row.counterPartyId + '" data-cifCounterParty="' + row.cifCounterParty+'" />'
                    }, "orderable": false, "className": "text-center"
                },
                { "data": "cifCounterParty", "class": "text-center" },

                {
                    "render": function (data, type, row) {
                        return '<a href = "/CounterParty/manager/viewdetail?counterPartyId=' + row.counterPartyId + '&cifCounterParty =' + row.cifCounterParty+'"  target="_blank">' + row.counterPartyName + '</a >'

                    },
                    "class": "text-left",
                },

                { "data": "paymentAccount", "class": "text-center" },
                { "data": "counterPartyGroupName", "class": "text-center" },
                {
                    "render": function (data, type, row) {
                        return row.counterPartyStatus == 'A' ? "Kích hoạt" : 'Đã đóng';

                    },
                    "class": "text-center",
                },
                { "data": "lastUserUpdated", "class": "text-center" },
                {
                    "render": function (data, type, row) {
                        return convertDate(new Date(parseInt(row.lastDatetimeUpdated.substr(6))));

                    },
                    "class": "text-left",
                },
                {
                    "render": function (data, type, row) {
                        var strHtml = '<a  href="/CounterParty/manager/update?counterPartyId=' + row.counterPartyId + '&cifCounterParty =' + row.cifCounterParty + '"  target="_blank" ><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a >';
                        strHtml += '<a  href="/CounterParty/manager/viewdetail?counterPartyId=' + row.counterPartyId + '&cifCounterParty =' + row.cifCounterParty + '"  target="_blank" ><i class="fa fa-eye" aria-hidden="true"></i></a >';
                        return strHtml;

                    },
                    "class": "text-center",
                },

            ],
        });
    },
    Search: function () {
        counterPartyListTable.ajax.reload();
    },
    exportExcel: function () {
        var url = "/CounterParty/Manager/ExportCounterParty";
        var param = {
            fromDate: $('#fromDate').val(),
            toDate: $('#toDate').val(),
            cif: $('#cif').val(),
            shortName: $('#shortName').val(),
            cpGroupCode: $('#cpGroupCode option:selected').val(),
            userUpdated: $('#userUpdated').val(),
            status: $('#status option:selected').val(),            
            pageSize: 0,
        };

        url += "?fromDate=" + param.fromDate;
        url += "?toDate=" + param.toDate;
        url += "&cif=" + param.cif;
        url += "&shortName=" + param.shortName;
        url += "&cpGroupCode=" + param.cpGroupCode;
        url += "&userUpdated=" + param.userUpdated;
        url += "&status=" + param.status;
        location.href = url;
    },
    selectRow: function () {
        $this = $(this);
        if ($('#counterPartyTable').find('input[name=rCounterParty]:checked').length > 0) {
            $('#btnEdit').removeAttr('disabled');
        }
        else {
            $('#btnEdit').attr('disabled', true);
        }
    },
    EditContract: function () {
        $row = $('#counterPartyTable').find('input[name=rCounterParty]:checked').first();
        var counterPartyId = $row.attr('data-counterPartyId');
        var cifCounterParty = $row.attr('data-cifCounterParty');
        var urlcifCounterParty = cifCounterParty != "null" ? "&cifCounterParty=" + cifCounterParty : ''
        var url = ("/CounterParty/manager/update?counterPartyId=" + counterPartyId + urlcifCounterParty).trim();
        location.href = url;
    },
}

