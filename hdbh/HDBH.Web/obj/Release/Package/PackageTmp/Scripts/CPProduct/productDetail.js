/*
 * READ ME
 * Việc xử lý ẩn hiện các button xử lý thêm dòng, xóa dòng, warning dòng bằng class hidden
 */

var $ProductListTable = $('#tblProduct');
var $CommisionTable = $('#tblCommisionList');
var $form = $('#frm');
var product = {
    Init: function () {
        productList.Init();
        commisionList.Init();
        $('body').on('click', '#btnSubmitApprove', this.Insert);
        $('body').on('click', '#btnApprove', this.Approve);
        $('body').on('click', '#btnReqEdit', this.Approve);
    },
    Insert: function () {
        var isError = false;
        var productList = [];
        var commisionList = [];
        $ProductListTable.find('tr[name=rowData]').each(function () {
            if ($(this).find('label[name=warningRow]').hasClass('hidden')) {
                var item = {
                    productAutoId: $(this).find('input[name=productAutoId]').val(),
                    isUsed: $(this).find('input[name=isUsed]').val(),
                    productId: $(this).find('input[name=productId]').val(),
                    productCode: $(this).find('input[name=productCode]').val(),
                    productName: $(this).find('input[name=productName]').val(),
                };
                productList.push(item);
            }
            else {
                isError = true;
                return;
            }
        });
        $CommisionTable.find('tr[name=rowData]').each(function () {
            if ($(this).find('label[name=warningRow]').hasClass('hidden')) {
                var item = {
                    commisAutoId: $(this).find('input[name=commisAutoId]').val(),
                    isUsed: $(this).find('input[name=isUsed]').val(),
                    commisionId: $(this).find('input[name=commisionId]').val(),
                    productCode: $(this).find('select[name=productCode] option:selected').val(),
                    productName: $(this).find('select[name=productCode] option:selected').attr('data-name'),
                    effectedFromDate: $(this).find('input[name=effectedFromDate]').val(),
                    effectedToDate: $(this).find('input[name=effectedToDate]').val(),
                    totalCommisRate: parseFloat($(this).find('input[name=totalCommisRate]').val().replace('%', '')),
                    agencyCommisRate: parseFloat($(this).find('input[name=agencyCommisRate]').val().replace('%', '')),
                    supportCommisRate: parseFloat($(this).find('input[name=supportCommisRate]').val().replace('%', '')),
                    serviceCostRate: parseFloat($(this).find('input[name=serviceCostRate]').val().replace('%', '')),
                    commisRate: parseFloat($(this).find('input[name=commisRate]').val().replace('%', '')),
                    remainRate: parseFloat($(this).find('input[name=remainRate]').val().replace('%', ''))
                };
                commisionList.push(item);
            }
            else {
                isError = true;
                return;
            }
        });


        var data = {
            counterPartyId: $form.find('input[name=counterPartyId]').val(),
            counterPartyGroup: $form.find('input[name=counterPartyGroup]').val(),
            counterPartyStatus: $form.find('input[name=counterPartyGroup]').val().toUpperCase() == 'UNCONTRACT' ? 'A' : 'INAU', //Đối với loại đối tác chưa liên kết thì mặc định là A, còn đã liên kết là Inau
            cifCounterParty: $form.find('input[name=cifCounterParty]').val(),
            isInau: $form.find('input[name=isInau]').val(),
            productList: productList,
            commisionList: commisionList,
            fileList: attachFile.getFilelist()
        }
        if (isError) {
            Notification.Error("", "Vui lòng kiểm tra các dòng lỗi");
        }
        else {
            $.ajax({
                type: "POST",
                url: urlAction,
                dataType: "json",
                data: data, // serializes the form's elements.
                success: function (data) {
                    if (data.errorCode == 0) {
                        Notification.Success("", data.errorMessage);
                        setTimeout(function () {
                            location.href = "/CPProduct/manager";
                        }, 2000);
                        
                    }
                    else {
                        Notification.Error("", data.errorMessage);
                    }
                }
            });
        }
    },
    Approve: function () {
        var approveStatus = $(this).attr('approveStatus');
        var approveContent = $('body').find('textarea[name=approveContent]').val();
        data = {
            counterPartyId: $form.find('input[name=counterPartyId]').val(),
            counterPartyGroup: $form.find('input[name=counterPartyGroup]').val(),
            approveStatus: approveStatus,
            approveContent: approveContent
        };
        $.ajax({
            type: "POST",
            url: '/Cpproduct/Manager/ApproveProduct',
            dataType: "json",
            data: data, // serializes the form's elements.
            success: function (data) {
                if (data.errorCode == 0) {
                    Notification.Success("", data.errorMessage);
                    setTimeout(function () {
                        location.href = "/CPProduct/manager/Inau";
                    }, 2000);
                }
                else {
                    Notification.Error("", data.errorMessage);
                }
            }
        });
    }
}

var productList = {
    Init: function () {
        $ProductListTable.on('click', 'label[name=addRow]', this.AddRow);
        $ProductListTable.on('click', 'label[name=removeRow]', this.removeRow);
        $ProductListTable.on('change', 'input[name=productCode]', this.focusOutRow);
        $('body').on('change', 'input[id=importProduct]', this.importProduct);

    },
    CheckValidate: function ($thisTr) {
        var sttVal = $thisTr.find('input[name=productCode]').val();
        var isError = false;
        var messError = '';
        if (sttVal.length == 0) {
            isError = true;
            messError += 'Vui lòng nhập </br>';
        }
        if (checkDiacritics(sttVal)) {
            isError = true;
            messError += 'Vui lòng không nhập unicode, tiếng việt có dấu </br>';
        }
        if (checkSpecialChar(sttVal)) {
            isError = true;
            messError += 'Vui lòng không nhập ký tự đặc biệt </br>';
        }
        if (checkSpace(sttVal)) {
            isError = true;
            messError += 'Vui lòng không nhập khoảng trắng </br>';
        }
        if (isError) {
            Notification.Error("", messError);
            $thisTr.find('input[name=productCode]').focus();
            $thisTr.find('input[name=productCode]').addClass('border-red');
            $thisTr.find('label[name=warningRow]').removeClass('hidden');
            $thisTr.find('label[name=warningRow]').attr('title', messError);
            $thisTr.attr('isError', 1);
        }
        else {
            $thisTr.find('input[name=productCode]').removeClass('border-red');
            $thisTr.find('label[name=warningRow]').addClass('hidden');
            $thisTr.attr('isError', 0);
        }
        return isError;
    },
    AddRow: function () {
        var $thisTr = $(this).closest('tr');
        if (!productList.CheckValidate($thisTr)) {
            var strHtml = $ProductListTable.find('#trTemplate').html();
            $ProductListTable.find('tbody').append('<tr name="rowData">' + strHtml + '</tr>');
            $thisTr.find('label[name=addRow]').addClass('hidden');
            $thisTr.find('label[name=removeRow]').removeClass('hidden');
        } else {

        }
    },
    focusOutRow: function () {
        var $tr = $(this).closest('tr');
        productList.CheckValidate($tr);
        commisionList.setOption();
    },
    removeRow: function () {
        var currentIndex = $(this).closest('tr').index();
        //nếu là dòng cuối
        if ($ProductListTable.find('tr[name=rowData]').length == currentIndex && currentIndex > 1) {
            var $PreTr = $ProductListTable.find('tr[name=rowData]').eq(currentIndex - 2);
            $PreTr.find('label[name=addRow]').removeClass('hidden');
            $(this).closest('tr').remove();
        }
        //nếu chỉ có 1 dòng
        else if ($ProductListTable.find('tr[name=rowData]').length == 1) {
            var $FirstTr = $ProductListTable.find('tr[name=rowData]').eq(0);
            $FirstTr.find('input').val('');
            $FirstTr.find('label[name=addRow]').removeClass('hidden');
            $FirstTr.find('label[name=removeRow]').addClass('hidden');
        }
        else {
            $(this).closest('tr').remove();
        }
        //validate
        var isExists = 0;
        var lsProductOptions = [];
        $ProductListTable.find('tbody').find('tr[name=rowData]').each(function (index, tr) {
            if ($(this).find('label[name=warningRow]').hasClass('hidden')) {
                var _val = $(tr).find('input[name=productCode]').val();
                lsProductOptions.push(_val);
            }
        });
        $CommisionTable.find('tr[name=rowData]').each(function () {
            $currentTr = $(this);
            var currProductCode = $currentTr.find('select[name=productCode] option:selected').val();
            if (lsProductOptions.indexOf(currProductCode) > -1) {
                $currentTr.find('label[name=warningRow]').addClass('hidden');
            }
            else {
                $currentTr.find('label[name=warningRow]').removeClass('hidden');
                $currentTr.find('label[name=warningRow]').attr('title', 'Không tìm thấy mã sản phẩm');
            }
        });
        //end
    },
    importProduct: function () {
        var files = $("#importProduct").get(0).files;

        var formData = new FormData();
        formData.append('importProduct', files[0]);

        $.ajax({
            url: '/CPProduct/Manager/ImportProduct',
            data: formData,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (data) {
                async function f() {
                    var d = document.createElement('tbody');
                    d.innerHTML = data;
                    if ($(d).find('tr[name=rowData]').length < 2) {
                        Notification.Error("", "File sai định dạng hoặc không tìm thấy data!");
                    }
                    else {
                        var $lasttr = $ProductListTable.find('tr[name=rowData]').last();
                        if ($lasttr.find('input[name=productCode]').val().length == 0) {
                            if ($lasttr.find('input[name=productName]').val().length == 0) {
                                $lasttr.remove();
                            }
                            else {
                                $lasttr.find('label[name=addRow]').addClass('hidden');
                            }
                        }
                        else {
                            $lasttr.find('label[name=addRow]').addClass('hidden');
                        }
                        $(d).find('#trTemplate').remove();
                        $ProductListTable.find('tbody').append($(d).html());
                    }
                }

                f().then(function k() {
                    commisionList.setOption();
                    document.getElementById("importProduct").value = "";
                }
                ); // 1
            }
        });
    }
}

var commisionList = {
    Init: function () {
        $ProductListTable.on('change', 'input[name=productName]', this.setOption); //load option lại
        $ProductListTable.on('change', 'input[name=productCode]', this.setOption); //load option lại
        $CommisionTable.on('click', 'label[name=addRow]', this.AddRow); //thêm dòng mới
        $CommisionTable.on('click', 'label[name=removeRow]', this.removeRow); //xóa dòng hiện tịa
        $('body').on('change', 'input[id=importCommisionList]', this.importCommisionList); 
        $('body').on('click', 'span[id=exportCommisionList]', this.exportExcel);
        $CommisionTable.on('change', 'input[name=effectedFromDate]', this.validate);
        $CommisionTable.on('change', 'input[name=effectedToDate]', this.validate);
        $CommisionTable.on('change', 'select[name=productCode]', this.validate);
        $CommisionTable.on('change', '.changeRow', this.changeValue);
    },
    setOption: function () {
        var strOption = '';
        var ischange = false;
        $ProductListTable.find('tr[name=rowData]').each(function () {
            var isWarning = $(this).find('label[name=warningRow]').hasClass('hidden');
            if (isWarning) {
                var _val = $(this).find('input[name=productCode]').val();
                var _text = $(this).find('input[name=productName]').val();
                if (_val.length > 0 && _text.length > 0) {
                    strOption += '<option value="' + _val + '" data-Name="' + _text + '">' + _val + ' - ' + _text + '</option>';
                    ischange = true;
                }
            }
        });
        if (ischange) {
            $('#tblCommisionList').find('tbody').find('tr').each(function (index, tr) {
                var $select = $(tr).find('select[name=productCode]');
                var currenOption = $select.find('option:selected').val();
                $select.html(strOption);
                if (currenOption != undefined) {
                    $select.val(currenOption);
                }

            });
        }

    },
    CheckValidate: function ($thisTr) {
        var sttVal = $thisTr.find('select[name=productCode]').val();
        var isError = false;
        var messError = '';
        if (sttVal == undefined || sttVal.length == 0) {
            isError = true;
            messError += 'Vui lòng chọn </br>';
        }
        if (isError) {
            Notification.Error("", messError);
            $thisTr.find('select[name=productCode]').focus();
            $thisTr.find('select[name=productCode]').addClass('border-red');
            $thisTr.find('label[name=warningRow]').removeClass('hidden');
            $thisTr.find('label[name=warningRow]').attr('title', messError);
            $thisTr.attr('isError', 1);
        }
        else {
            $thisTr.find('select[name=productCode]').removeClass('border-red');
            $thisTr.find('label[name=warningRow]').addClass('hidden');
            $thisTr.attr('isError', 0);
        }
        return isError;
    },
    AddRow: function () {
        var $thisTr = $(this).closest('tr');
        if (!commisionList.CheckValidate($thisTr)) {
            var strHtml = $CommisionTable.find('#trTemplateProduct').html();
            $CommisionTable.find('tbody').append('<tr name="rowData">' + strHtml + '</tr>');
            $thisTr.find('label[name=addRow]').addClass('hidden');
            $thisTr.find('label[name=removeRow]').removeClass('hidden');
            loadSingleFormatDate($thisTr.next().find('.datepicker')); //reload format date
            $thisTr.find('select[name=productCode]').change(); //validate current node tr
            $thisTr.next().find('select[name=productCode]').change(); //validate next node tr
            loadformatnumber();
        }
    },
    removeRow: function () {
        var currentIndex = $(this).closest('tr').index();
        //nếu là dòng cuối
        if ($CommisionTable.find('tr[name=rowData]').length == currentIndex && currentIndex > 1) {
            var $PreTr = $CommisionTable.find('tr[name=rowData]').eq(currentIndex - 2);
            $PreTr.find('label[name=addRow]').removeClass('hidden');
            $(this).closest('tr').remove();
        }
        //nếu chỉ có 1 dòng
        else if ($CommisionTable.find('tr[name=rowData]').length == 1) {
            var $FirstTr = $CommisionTable.find('tr[name=rowData]').eq(0);
            $FirstTr.find('input').val('');
            $FirstTr.find('label[name=addRow]').removeClass('hidden');
            $FirstTr.find('label[name=removeRow]').addClass('hidden');
        }
        else {
            $(this).closest('tr').remove();
        }
        
    },
    importCommisionList: function () {
        var files = $("#importCommisionList").get(0).files;

        var formData = new FormData();
        formData.append('importCommisionList', files[0]);

        $.ajax({
            url: '/CPProduct/Manager/ImportCommisionList',
            data: formData,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.Code == 0) {
                    async function f() {
                        var d = document.createElement('tbody');
                        d.innerHTML = data.Message; // set html thanh element
                        if ($(d).find('tr[name=rowData]').length < 1) { //kiểm tra xem có rowdata nào không
                            Notification.Error("", "File sai định dạng hoặc không tìm thấy data!");
                        }
                        else {
                            $(d).find('#trTemplateProduct').remove();
                            $CommisionTable.find('tr[name=rowData]').last().find('label[name=addRow]').remove(); //xoa nut them dong ở dòng cuối cùng hiện tại
                            $CommisionTable.find('tbody').append($(d).html()); //append vào bên dưới
                        }
                    }
                    f().then(function k() {
                        loadformatnumber();
                        loadFormatDate();
                        commisionList.setOption();
                    }); // 1
                }
                else {
                    Notification.Error("", data.Message);
                }
                document.getElementById("importCommisionList").value = "";
            }
        });
    },
    exportExcel: function () {
        var url = "/CPProduct/Manager/ExportCommisionList";
        window.open(url, '_blank');
    },
    validate: function () {
        var $currentTr = $(this).closest('tr[name=rowData]');
        var currenProductCode = $currentTr.find('select[name=productCode] option:selected').val(); // sản phẩm hiên tại
        var currenFromDate = commisionList.convertDateToNumber($currentTr.find('input[name=effectedFromDate]').val()); // ngày hiệu lực từ
        var currentToDate = commisionList.convertDateToNumber($currentTr.find('input[name=effectedToDate]').val()); // ngày hiệu lực đến
        var newDate = new Date();
        var currenDAte = commisionList.convertDateToNumber(newDate.getDate() + "/" + (newDate.getMonth() + 1) + "/" + newDate.getFullYear());

        var currentIndex = $currentTr.index();
        var productName = $(this).attr('data-name');
        var minDate = [];
        var maxDate = [];
        // lấy ngày thấp nhất và ngày lớn nhất theo sản phẩm
        $CommisionTable.find('tr[name=rowData]').each(function () {
            if ($(this).find('select[name=productCode] option:selected').val() == currenProductCode && currentIndex != $(this).index()) {
                var fromDate = $(this).find('input[name=effectedFromDate]').val();
                var toDate = $(this).find('input[name=effectedToDate]').val();
                minDate.push(commisionList.convertDateToNumber(fromDate));
                maxDate.push(commisionList.convertDateToNumber(toDate));
            }
        });
        var min = Math.min.apply(null, minDate);// ngày lớn nhất
        var max = Math.max.apply(null, maxDate);// ngày nhỏ nhất

        var isError = false;
        var messageError = '';
        if ((currenFromDate >= min && currenFromDate <= max) || (currentToDate >= min && currentToDate <= max)) {
            isError = true;
            messageError += 'Lỗi khoảng ngày không được trùng';
        }

        var isExists = 0; 
        // kiểm tra sản phẩm có tồn tại hay không
        $ProductListTable.find('tbody').find('tr[name=rowData]').each(function (index, tr) {
            if ($(this).find('label[name=warningRow]').hasClass('hidden')) {
                var _val = $(tr).find('input[name=productCode]').val();
                isExists = currenProductCode == _val;
                isExists++;
            }
        });
        if (isExists < 1) {
            isError = true;
            messageError += 'Không tìm thấy mã sản phẩm';
        }
        if (currenFromDate < currenDAte) {
            isError = true;
            messageError += 'Ngày hiệu lực từ ngày phải lớn hơn ngày hiện tại';
        }
        if (currentToDate < currenDAte) {
            isError = true;
            messageError += 'Ngày hiệu lực đến ngày phải lớn hơn ngày hiện tại';
        }

        //xử lý điều kiện
        if (isError) {
            $currentTr.find('label[name=warningRow]').removeClass('hidden');
            $currentTr.find('label[name=warningRow]').attr('title', messageError);
        }
        else {
            $currentTr.find('input[name=productName]').val(productName);
            $currentTr.find('label[name=warningRow]').addClass('hidden');
        }
    },
    changeValue: function () {
        var $tr = $(this).closest('tr');
        var agencyCommisRate = parseFloat($tr.find('input[name=agencyCommisRate]').val().replace('%', ''));
        var supportCommisRate = parseFloat($tr.find('input[name=supportCommisRate]').val().replace('%', ''));
        var serviceCostRate = parseFloat($tr.find('input[name=serviceCostRate]').val().replace('%', ''));
        var commisRate = parseFloat($tr.find('input[name=commisRate]').val().replace('%', ''));
        var totalCommisRate = 0, remainRate = 0;
        totalCommisRate = agencyCommisRate + supportCommisRate + serviceCostRate;
        remainRate = totalCommisRate - commisRate;
        $tr.find('input[name=totalCommisRate]').val(totalCommisRate);
        $tr.find('input[name=remainRate]').val(remainRate);
        loadformatnumber();
    },
    convertDateToNumber: function (date) {
        var result = 0;
        if (date != '' && date.indexOf('/') > 0) {
            var partDate = date.split('/');
            var month = partDate[1].length == 1 ? "0" + partDate[1] : partDate[1];
            var date = partDate[0].length == 1 ? "0" + partDate[0] : partDate[0];
            result = parseInt(partDate[2] + month + date);
        }
        return result;
    }
}