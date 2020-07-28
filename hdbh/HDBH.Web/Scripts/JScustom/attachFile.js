
var attachFile = {
    init: function () {
        this.innitUpload();
        $('body').on('click', 'a[name=btnRemoveFile]', this.removeFile);
    },
    innitUpload: function () {
        if ($('#uploader').length > 0) {
            document.getElementById('uploader').onsubmit = function () {
                var formdata = new FormData(); //FormData object
                var fileInput = document.getElementById('fileInput');
                //Iterating through each files selected in fileInput
                for (i = 0; i < fileInput.files.length; i++) {
                    //Appending each file to FormData object
                    formdata.append(fileInput.files[i].name, fileInput.files[i]);
                }
                //Creating an XMLHttpRequest and sending
                var xhr = new XMLHttpRequest();
                xhr.open('POST', '/Common/UploadFile');
                xhr.send(formdata);
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        //alert(xhr.responseText);
                        var rs = JSON.parse(xhr.responseText);
                        if (rs.length > 0) {
                            var template = $('#attactment-table').find('#trAttactmentTemplate');
                            $.each(rs, function (index, item) {
                                if (item.fileName != "Error") {
                                    var classico;
                                    switch (item.extention) {
                                        case ".xlsx": classico = "fa fa-file-excel-o"; break;
                                        case ".docx": classico = "fa fa-file-word-o"; break;
                                        case ".pdf": classico = "fa fa-file-pdf-o"; break;
                                        case ".txt": classico = "fa fa-file-text"; break;
                                        case ".jpg": classico = "fa fa-picture-o"; break;
                                        case ".png": classico = "fa fa-picture-o"; break;
                                        default: classico = "fa fa-file"; break;
                                    }
                                    template.find('i[name=attachIco]').removeClass().addClass(classico);
                                    template.find('span[name=fileItem]').text(item.fileName);
                                    template.find('a[name=btnDownloadFile]').attr('href', "/Common/DownloadFile?url=" + item.filePath + "&fileName=" + item.fileName);
                                    template.find('a[name=btnRemoveFile]').attr('filePath', item.filePath);
                                    if (fileInput.hasAttribute('multiple')) {
                                        $('#attactment-table tbody').append('<tr name="atm">' + $(template).html() + '</tr>');
                                    }
                                    else {
                                        $('#attactment-table tbody').find('tr[name=atm]').remove();
                                        $('#attactment-table tbody').append('<tr name="atm">' + $(template).html() + '</tr>');
                                    }
                                    $('#uploader').find('label[for=fileInput]').html('<i class="fa fa-hand-o-right" aria-hidden="true"></i> <span>Bấm chọn file…</span>');
                                    var list = document.getElementById('filelist');
                                    list.innerHTML = '';
                                }
                                else {
                                    Notification.Error("", item.filePath);
                                }
                            });
                            document.getElementById("fileInput").value = "";
                        }
                    }
                }
                return false;
            }
            document.getElementById('fileInput').addEventListener('change', function (e) {
                var list = document.getElementById('filelist');
                list.innerHTML = '';
                for (var i = 0; i < this.files.length; i++) {
                    list.innerHTML += (i + 1) + '. ' + this.files[i].name + '\n';
                }
                if (list.innerHTML == '') list.style.display = 'none';
                else list.style.display = 'block';
            });
        }
    },
    removeFile: function () {
        var _this = $(this);
        var filePath = _this.attr('filePath');
        Lobibox.confirm({
            msg: "Bạn có chắc xóa tập tin này không?",
            callback: function ($this, type, ev) {
                if (type == 'yes') {
                    $.ajax({
                        type: "POST",
                        url: "/Common/RemoveFile",
                        dataType: "json",
                        data: { filePath: filePath }, // serializes the form's elements.
                        success: function (data) {
                            if (data.Code == 1) {
                                Notification.Success("", data.Message);
                                _this.closest('tr').remove();
                            }
                            else {
                                Notification.Error("", data.Message);
                            }
                        }
                    });
                }
            }
        });
    },
    getFilelist: function () {
        var fileList = [];
        $('#attactment-table').find('tr[name=atm]').each(function () {
            var _trItem = $(this);
            var item = {
                fileAutoId: _trItem.find('span[name=fileAutoId]').attr('fileId'),
                fileId: _trItem.find('span[name=fileItem]').attr('fileId'),
                fileName: _trItem.find('span[name=fileItem]').text(),
                filePath: _trItem.find('a[name=btnRemoveFile]').attr('filepath'),
            };
            fileList.push(item);
        });
        return fileList;
    }
}
