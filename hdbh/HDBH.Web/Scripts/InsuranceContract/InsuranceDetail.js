
$exceptTypeCode = $('select[name=exceptTypeCode]');
$tblProduct = $('#tblProduct');
$collateralList = $('.collateralList');
$tbProductSchedule = $('#tbProductSchedule');
$schedule = $('#schedule');
$mainTab = $('#mainTab');
$tblCollateralList = $('#tblCollateralList');
$tbScheduleList = $('#tbScheduleList');


var insuranceDetail = {
    Init: function () {
        $('body').on('change', '#cbExcept', this.changeExcept);
        $('body').on('change', 'select[name=exceptTypeCode]', this.changeExceptTypeCode);
        $('body').on('click', '#btnSubmitApprove', this.submitData);
        $('body').on('change', 'select[name=contractFormCode]', this.changeContractFormCode);
        //this.loadExeptType(TypeCode);
        productList.Init();
        collateralList.Init();
        schedule.Init();
    },
    changeContractFormCode: function () {
        var selected = $(this).find('option:selected').val();
        if (selected == 'NEW') {
            $mainTab.find('input[name=refInsuranceNo]').attr('disabled', true);
        }
        else {
            $mainTab.find('input[name=refInsuranceNo]').removeAttr('disabled');
        }
    },
    selectRow: function () {
        var modal = $('#' + $(this).closest('.modal').attr('id'));
        var data = $(this).data('value');
        if (data != null) {
            insuranceDetail.setData(data);
            modal.modal('hide');
        }
    },
    setData: function (data) {
        if (data != null) {
            $('#customerCif').val(data.cif);
            $('input[name=customerName]').val(data.customerName);
            $('input[name=shortName]').val(data.customerShortName);
        }
    },
    loadExeptType: function (contractTypeCode) {
        $.ajax({
            type: "POST",
            url: "/InsuranceContract/Manager/getListExceptType",
            data: { insuranceContractType: contractTypeCode }, // serializes the form's elements.
            dataType: "json",
            success: function (data) {
                if ($exceptTypeCode.attr('disabled') != 'disabled' && $exceptTypeCode.attr('disabled') != true) {
                    $exceptTypeCode.html(data);
                }
            }
        });
    },
    changeExcept: function () {
        var isCheck = $(this).is(':checked');
        var typeCode = $(this).attr('typecode');
        if (isCheck) {
            $exceptTypeCode.removeAttr('disabled');
            insuranceDetail.loadExeptType(TypeCode);//load gia tri by typecode
        }
        else {
            $exceptTypeCode.val('');
            $exceptTypeCode.attr('disabled', true);
        }
    },
    changeExceptTypeCode: function () {
        var select = $(this).find('option:selected').val();
        var typeCode = $(this).attr('typecode');
        Lobibox.confirm({
            msg: "Bạn có chắc muốn thay đổi không?",
            callback: function ($this, type, ev) {
                if (type == 'yes') {
                    var strHtml = $tblProduct.find('tr[id=trTemplate]').html();
                    //set product list empty
                    $tblProduct.find('tr[name=rowData]').remove();
                    $tblProduct.append('<tr name="rowData">' + strHtml + '</tr>');
                        //end
                    $tbScheduleList.find('tbody').html('');
                    //end
                    if (select == 'FREE_AUDIENCE') {
                        $tblProduct.closest('fieldset').addClass('hidden');
                        $('#schedule-tab').addClass('hidden');
                    }
                    else {
                        $tblProduct.closest('fieldset').removeClass('hidden');
                        $('#schedule-tab').removeClass('hidden');
                        insuranceDetail.loadCounterPartyByExceptCode(select);
                    }

                    if (select == 'WALK_IN_GUEST') {
                        $('#divNoncifCustomerName').removeClass('hidden');
                    }
                    else {
                        $('#divNoncifCustomerName').addClass('hidden');
                    }
                }
            }
        });
    },
    submitData: function () {
        var totalFeeAmount = 0;
        var exceptTypeCode = $mainTab.find('select[name=exceptTypeCode] option:selected').val()
        var collateralList = [];
        var productList = [];
        var scheduleList = [];
        var paymentDetailList = [];

        if (exceptTypeCode != 'FREE_AUDIENCE') {
            $tblProduct.find('tr[name=rowData]').each(function () {
                totalFeeAmount += parseFloat($(this).find('input[name=feeAfterVat]').val());
            });
            $tblProduct.find('tr[name=rowData]').each(function () {
                var $tr = $(this);
                if ($tr.find('select[name=counterPartyId] option:selected').val() != '') {
                    var item = {
                        productId: $tr.find('select[name=productId] option:selected').val(),
                        commisionId: $tr.find('input[name=commisionId]').val(),
                        counterPartyId: $tr.find('select[name=counterPartyId] option:selected').val(),
                        subCompanyName: $tr.find('input[name=subCompanyName]').val(),
                        insurancePercent: parseFloat($tr.find('input[name=insurancePercent]').val()),
                        feeAmount: parseFloat($tr.find('input[name=feeAmount]').val()),
                        feeVatAmount: parseFloat($tr.find('input[name=feeVatAmount]').val()),
                        commisRate: parseFloat($tr.find('input[name=commisRate]').val()),
                        insProdId: $tr.find('input[name=insProdId]').val()
                    };
                    productList.push(item);
                }
            });

            $tbScheduleList.find('tr[name=trSchedule]').each(function () {
                var $tr = $(this);
                var item = {
                    scheduleId: $tr.find('input[name=scheduleId]').val(),
                    schedulePaymentDate: $tr.find('input[name=scheduleDate]').val()
                };
                scheduleList.push(item);
            });
            $tbScheduleList.find('tr[name=rowData]').each(function () {
                $trProduct = $(this);
                var item = {
                    scheduleId: $trProduct.find('input[name=scheduleId]').val(),
                    productId: $trProduct.find('input[name=productId]').val(),
                    schedulePaymentAmount: parseFloat($trProduct.find('input[name=schedulePaymentAmount]').val()),
                    scheduleVatAmount: parseFloat($trProduct.find('input[name=scheduleVatAmount]').val()),
                    scheduleTotalAmount: parseFloat($trProduct.find('input[name=scheduleTotalAmount]').val())
                };
                paymentDetailList.push(item);
            });
        }
        $tblCollateralList.find('tbody tr').each(function () {
            var $tr = $(this);
            var item = {
                collateralCode: $tr.find('input[name=collateralCode]').val(),
                collateralTypeCode: $tr.find('input[name=collateralTypeCode]').val(),
                collateralValue: parseFloat($tr.find('input[name=collateralValue]').val()),
                executionValue: parseFloat($tr.find('input[name=executionValue]').val()),
                manBranchCode: $tr.find('input[name=manBranchCode]').val(),
                insCollId: $tr.find('input[name=insCollId]').val()
            };
            collateralList.push(item);

        });
        var fileList = attachFile.getFilelist()

        var data = {
            insuranceId: $mainTab.find('input[name=insuranceId]').val(),
            insuranceContractNo: $mainTab.find('input[name=insuranceContractNo]').val(),
            contractTypeCode: $mainTab.find('select[name=contractTypeCode] option:selected').val(),
            contractFormCode: $mainTab.find('select[name=contractFormCode] option:selected').val(),
            insuranceCertification: $mainTab.find('input[name=insuranceCertification]').val(),
            refInsuranceNo: $mainTab.find('input[name=refInsuranceNo]').val(),
            exceptionTypeCode: exceptTypeCode,
            customerCif: $mainTab.find('input[name=customerCif]').val(),
            customerName: $mainTab.find('input[name=customerName]').val(),
            //noncifCustomerName: $mainTab.find('input[name=noncifCustomerName]').val(), //underfined
            //contactNumber: $mainTab.find('input[name=contactNumber]').val(), //
            consultantOfficerCode: $mainTab.find('input[name=consultantOfficerCode]').val(),
            consultantOfficerName: $mainTab.find('input[name=consultantOfficerName]').val(),
            consultantBranchCode: $mainTab.find('select[name=consultantBranchCode] option:selected').val(),
            isContractOcb: $mainTab.find('select[name=exceptTypeCode] option:selected').val() == 'NOT_FROM_OCB' ? 0 : 1,
            insuranceValue: $mainTab.find('input[name=insuranceValue]').val(),
            totalFeeAmount: totalFeeAmount,//
            signedDate: $mainTab.find('input[name=signedDate]').val(),//
            effectiveDate: $mainTab.find('input[name=effectiveDate]').val(), //
            dueDate: $mainTab.find('input[name=dueDate]').val(),
            contractDescription: $mainTab.find('input[name=contractDescription]').val(), //
            noncifCustomerName: $mainTab.find('input[name=noncifCustomerName]').val(), 
            collateralList: collateralList,
            productList: productList,
            scheduleList: scheduleList,
            paymentDetailList: paymentDetailList,
            fileList: fileList
        }
        var isError = false;
        var strMessage = "";
        if (data.customerCif == '') {
            isError = true;
            strMessage += '-  nhập mã khách hàng!';
        }
        if (data.customerName == '') {
            isError = true;
            strMessage += '-  nhập tên khách hàng! </ br>';
        }
        if (data.exceptionTypeCode == 'WALK_IN_GUEST' && data.noncifCustomerName == '') {
            isError = true;
            strMessage += '-  nhập khách hàng vãng lai!';
        }
        
        if (data.signedDate == '') {
            isError = true;
            strMessage += '-  nhập Ngày ký HĐ! </ br>';
        }
        if (data.effectiveDate == '') {
            isError = true;
            strMessage += '-  nhập Ngày Ngày hiệu lực HĐ! </ br>';
        }
        if (data.dueDate == '') {
            isError = true;
            strMessage += '-  nhập Ngày hết hạn HĐ ! </ br>';
        }
        if (data.consultantOfficerCode == '') {
            isError = true;
            strMessage += '-  nhập Nhân viên tư vấn ! </ br>';
        } 
        if (data.contractTypeCode == 'COLLATERAL' && collateralList.length < 1) {
            isError = true;
            strMessage += '-  Vui lòng nhập tài sản đảm bảo ! </ br>';
        }

        if (isError) {
            Notification.Error("", strMessage);
        }
        else {
            $.ajax({
                type: "POST",
                url: actionUrl,
                data: data, // serializes the form's elements.
                dataType: "json",
                success: function (data) {
                    if (data.errorCode == 0) {
                        Notification.Success("", data.errorMessage);
                        setTimeout(function () {
                            location.href = "/InsuranceContract/manager";
                        }, 2000);
                    }
                    else {
                        Notification.Error("", data.errorMessage);
                    }
                }
            });
        }
       
    },
    loadCounterPartyByExceptCode: function (exceptCode) {
        cpGroupCode = exceptCode != undefined && exceptCode == 'UNCONTRACT_CP' ?  "UNCONTRACT" : "CONTRACTED";
        $.ajax({
            type: "POST",
            url: '/CounterParty/Manager/getListCounterPartyComboBox',
            data: {
                cpGroupCode: cpGroupCode
            }, // serializes the form's elements.
            dataType: "json",
            success: function (data) {
                $tblProduct.find('select[name=counterPartyId]').html(data);
            }
        });
    }
}

var productList = {
    Init: function () {
        this.functionAfterLoad();
        $tblProduct.on('click', 'label[name=addRow]', this.AddRow);
        $tblProduct.on('click', 'label[name=removeRow]', this.removeRow);
        $tblProduct.on('change', 'input.changeValue', this.changeVAT);

    },
    changeCounterPartyId: function () {
        $tr = $(this).closest('tr');
        var selected = $(this).find('option:selected').val();
        var paymentAccount = $(this).find('option:selected').attr('paymentAccount');
        $tr.find('input[name=paymentAccount]').val(paymentAccount);
        $.ajax({
            type: "POST",
            url: "/InsuranceContract/Manager/getListCpProdForContract",
            data: { counterPartyId: parseInt(selected) }, // serializes the form's elements.
            dataType: "json",
            success: function (data) {
                $tr.find('select[name=productId]').html(data);
                $tr.find('select[name=productId]').change();

            }
        });
    },
    changeProduct: function () {
        $tr = $(this).closest('tr');
        $select = $(this);
        if ($select.find('option:selected').attr('group') != undefined) {
            var object = JSON.parse(JSON.parse($select.find('option:selected').attr('group')).Name);
            $tr.find('input[name=productName]').val(object.productName);
            if (object.commisionList.length > 0) {
                $tr.find('input[name=commisionId]').val(object.commisionList[0].commisionId);
                $tr.find('input[name=commisRate]').val(object.commisionList[0].commisRate);
            }

        }
    },
    AddRow: function () {
        var $thisTr = $(this).closest('tr');
        var strHtml = $tblProduct.find('#trTemplate').html();
        $tblProduct.find('tbody').append('<tr name="rowData">' + strHtml + '</tr>');
        $thisTr.find('label[name=addRow]').addClass('hidden');
        $thisTr.find('label[name=removeRow]').removeClass('hidden');
        productList.functionAfterLoad();
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
    },
    functionAfterLoad: function () {
        $tblProduct.on('change', 'select[name=counterPartyId]', this.changeCounterPartyId);
        $tblProduct.on('change', 'select[name=productId]', this.changeProduct);
    },
    changeVAT: function () {
        $tr = $(this).closest('tr');
        var feeAmount = $tr.find('input[name=feeAmount]').val().replace(/\,/g, '');
        var feeVatAmount = $tr.find('input[name=feeVatAmount]').val().replace(/\,/g, '');
        var feeAfterVat = parseFloat(feeAmount) + parseFloat(feeVatAmount);
        $tr.find('input[name=feeAfterVat]').val(feeAfterVat);
    }
}

var collateralList = {
    Init: function () {
        $collateralList.on('click', '#btnSearchCollateral', this.Search);
        $collateralList.on('click', 'label[name=removeRow]', this.renmoveRow);
    },
    Search: function () {
        var collateralCode = $collateralList.find('#txtCollateral').val();
        if (collateralCode != '') {
            $.ajax({
                type: "POST",
                url: "/InsuranceContract/Manager/getDetailCollateral",
                data: { collateralCode: collateralCode }, // serializes the form's elements.
                dataType: "json",
                success: function (data) {
                    if (data.Code == 0) {
                        $collateralList.find('#tblCollateralList').find('tbody').html(data);
                        loadformatnumber();
                    }
                    else {
                        Notification.Error("", "Không tìm thấy mã tài sản");
                    }
                }
            });
        }
        else {
            Notification.Error("", "Nhập mã tài sản đảm bảo");
        }
    },
    renmoveRow: function () {
        $(this).closest('tr').remove();
    }
}

var schedule = {
    Init: function () {
        $schedule.on('click', '#btnCalscheduleId', this.calScheduleList);
        $schedule.on('click', '#btnReloadProductList', this.GetProduct);
    },
    GetProduct: function () {
        $tbProductSchedule.find('tr[name=rowData]').remove();
        $strHtml = $tbProductSchedule.find('#template_ProductSchedule');
        var tempHtml = $strHtml.html();
        $tbProductSchedule.find('#template_ProductSchedule').html(tempHtml);
        $tblProduct.find('tr[name=rowData]').each(function () {
            if ($(this).find('select[name=counterPartyId] option:selected').val() != '') {
                $tr = $(this).closest('tr');
                $strHtml.find('td[name=productCode]').html($tr.find('select[name=productId] option:selected').text());
                $strHtml.find('td[name=productName]').html($tr.find('input[name=productName]').val());
                $strHtml.find('input[name=productId]').val($tr.find('select[name=productId] option:selected').val());
                $tbProductSchedule.append('<tr name="rowData">' + $strHtml.html() + '</tr>');
            }
        });
        loadformatnumber();
    },
    calScheduleList: function () {
        var scheduleQuanlity = $schedule.find('input[name=txtScheduleQuanlity]').val();
        var scheduleTerm = $schedule.find('input[name=txtScheduleTerm]').val();
        var tempDate = $schedule.find('input[name=dtpStartDate]').val();
        var startDate;
        if (tempDate != '') {
            var lsTemp = tempDate.split('/');
            if (lsTemp.length > 2) {
                startDate = new Date(lsTemp[2], lsTemp[1] - 1, lsTemp[0])
            }

        }
        var strProduct = '';
        var listProduct = [];
        $schedule.find('#tbProductSchedule').find('tr[name=rowData]').each(function () {
            $tr = $(this);
            var item = {
                productId: $tr.find('input[name=productId]').val(),
                productCode: $tr.find('td[name=productCode]').text(),
                productName: $tr.find('td[name=productName]').text(),
                tbfeeAmount: $tr.find('td[name=tbfeeAmount]').find('input').val(),
                tbVATPercent: $tr.find('td[name=tbVATPercent]').find('input').val()
            }
            listProduct.push(item)
        })
        $.ajax({
            type: "GET",
            url: "/InsuranceContract/Manager/calScheduleList",
            data: {
                scheduleQuanlity: scheduleQuanlity,
                scheduleTerm: scheduleTerm,
                dtpStartDate: tempDate,
                strProduct: JSON.stringify(listProduct)
            }, // serializes the form's elements.
            dataType: "json",
            success: function (data) {
                $schedule.find('#tbScheduleList').find('tbody').html(data);
                loadformatnumber();
            }
        });
    },
    changeValue: function () {

    }
}