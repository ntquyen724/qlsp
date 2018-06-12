﻿var UpfilePage = {

    init: function () {

        UpfilePage.onEvent();
        UpfilePage.upEvent();
    },

    onEvent: function () {

        jQuery(document).on("click", ".attachFile", function () {
            var data = jQuery(this).getData();
            jQuery(data.rel).attr("data-target", data.target);
            jQuery(data.rel).attr("data-id", data.id);
            jQuery(data.rel).attr("data-file-name", data.fileName);
            jQuery(data.rel).attr("data-file-path", data.filePath);
            jQuery(data.rel).attr("data-file-title", data.fileTitle);
            jQuery(data.rel).attr("data-file-url", data.fileUrl);

            jQuery(data.rel).val("").trigger("click");
        });
        jQuery(document).on("click", ".delMember", function () {
            jQuery(this).closest(".member").slideUp("slow", function () {
                var parent = jQuery(this).parent();
                if (parent.find(".member").length == 1) {
                    parent.addClass("hidden");
                }
                jQuery(this).remove();
            });
        });

    },
    upEvent: function (container) {
        if (Utils.isEmpty(container))
            container = jQuery(document);

        container.find(".inputUpFile").each(function () {
            var obj = jQuery(this);
            if (!obj.hasClass("setUpFiled")) {
                obj.hasClass("setUpFiled");
                obj.ajaxUploader({
                    name: "FileDocument",
                    postUrl: Cdata.Storage.domain + "/uploader/upfile",
                    //elTarget: obj.attr("data-target"),
                    onBegin: function (e, t) {
                        return true;
                    },
                    onClientLoadStart: function (e, file, t) {
                    },
                    onClientProgress: function (e, file, t) {
                        jQuery(obj.attr("data-target")).addClass("loading");
                    },
                    onServerProgress: function (e, file, t) {
                        jQuery(obj.attr("data-target")).addClass("loading");
                    },
                    onClientAbort: function (e, file, t) {
                        jQuery(obj.attr("data-target")).removeClass("loading");
                    },
                    onClientError: function (e, file, t) {
                        jQuery(obj.attr("data-target")).removeClass("loading");
                    },
                    onServerAbort: function (e, file, t) {
                        jQuery(obj.attr("data-target")).removeClass("loading");
                    },
                    onServerError: function (e, file, t) {
                        jQuery(obj.attr("data-target")).removeClass("loading");
                    },
                    onSuccess: function (e, file, t, data) {
                        var dataObj = obj.getData();
                        var targetObj = jQuery(dataObj.target);
                        targetObj.closest(".hidden").removeClass("hidden");
                        var url = dataObj.fileUrl;
                        if (!Utils.isEmpty(dataObj.fileUrl)) {
                            jQuery.ajax({
                                type: "POST",
                                async: false,
                                url: url,
                                data: ({
                                    Path: data.FilePath,
                                    Name: data.FileName,
                                    ID: dataObj.id,
                                }),
                                dataType: 'Json',
                                beforeSend: function () {

                                },
                                complete: function () {

                                },
                                error: function () {

                                },
                                success: function (response) {
                                    var currentUrl = window.location.href;
                                    currentUrl = updateQueryStringParameter(currentUrl, "IDDistric",  $("#IDDistric").val());
                                    currentUrl = updateQueryStringParameter(currentUrl, "IDProvice",  $("#IDProvice").val());
                                    window.location.href = currentUrl;
                                }
                            });
                            return;
                        }
                        else if (targetObj.hasClass("ui-group-item")) {
                            var item = jQuery(
                                "<div class='form-group has-feedback member'>" +
                                    "<div class='col-lg-12'>" +
                                        "<input name='" + dataObj.filePath + "' type='hidden' value='" + data.FilePath + "'/>" +
                                        "<input name='" + dataObj.fileName + "' type='text' class='form-control' data-bv-notempty-message='" + dataObj.bvNotemptyMessage + "' data-bv-notempty='true' data-bv-field='" + dataObj.fileName + "' value='" + data.FileName + "' />" +
                                        "<i class='glyphicon glyphicon-remove delMember'></i>" +
                                    "</div>" +
                                "</div>"
                            );
                            targetObj.removeClass("loading").append(item);
                            item.bootstrapValidator();
                        }
                        else {
                            targetObj.removeClass("loading").append(
                                "<span class='fileitem member'>" +
                                    "<img class='img-thumbnail' src='" + Cdata.SrcPath(data.FilePath) + "' title='" + data.FileName + "' />" +
                                    "<input name='" + dataObj.fileName + "' class='fileNames' type='text' value='" + data.FileName + "' />" +
                                    "<input name='" + dataObj.filePath + "' class='filePaths' type='hidden' value='" + data.FilePath + "'/>" +
                                    "<button type='button' class='btn btn-xs btn-link close delMember'>x</button>" +
                                "</span>"
                            );
                        }
                    }
                });
            }
        });

        container.find(".inputUpFiles").each(function () {
            var obj = jQuery(this);
            if (!obj.hasClass("setUpFiled")) {
                obj.hasClass("setUpFiled");
                obj.ajaxUploader({
                    name: "FileDocument",
                    postUrl: Cdata.Storage.domain + "/uploader/upfile",
                    //elTarget: obj.attr("data-target"),
                    onBegin: function (e, elTarget) {
                        return true;
                    },
                    onClientLoadStart: function (e, file, t) {

                    },
                    onClientProgress: function (e, file) {
                        UpfilePage.onProgress(e, file);
                    },
                    onServerProgress: function (e, file, t) {
                        UpfilePage.onProgress(e, file);
                    },
                    onClientAbort: function (e, file) {
                        UpfilePage.onAbort(e, file);
                    },
                    onClientError: function (e, file) {
                        UpfilePage.onAbort(e, file);
                    },
                    onServerAbort: function (e, file, t) {
                        UpfilePage.onAbort(e, file);
                    },
                    onServerError: function (e, file, t) {
                        UpfilePage.onAbort(e, file);
                    },
                    onSuccess: function (e, file, t, data) {

                    }
                });
            }
        });
        container.find("#InputAvatar").each(function () {
            var obj = jQuery(this);
            if (!obj.hasClass("setUpFiled")) {
                obj.ajaxUploader({
                    name: "FileDocument",
                    dragBox: "#AvatarDrager",
                    postUrl: Cdata.Storage.domain + "/uploader/upfile",
                    onBegin: function () {
                        jQuery("#AvatarDrager").addClass("loading");
                        return true;
                    },
                    onChange: function () {
                        return true;
                    },
                    onDrop: function () {
                        return true;
                    },
                    onClientLoadStart: function () {
                    },
                    onClientProgress: function () {
                    },
                    onClientAbort: function () {
                        jQuery("#AvatarDrager").removeClass("loading");
                    },
                    onClientError: function () {
                        jQuery("#AvatarDrager").removeClass("loading");
                    },
                    onSuccess: function (e, file, dragbox, data) {
                        var html = "";
                        var thumbPath = "/thumb/120xauto/" + data.FilePath;
                        var avatarUrl = Cdata.Storage.domain + thumbPath + "?" + (new Date).getTime();
                        html += "<a class='imgavt'>";
                        html += "<img id='ImageAvatar' src='" + avatarUrl + "' />";
                        html += "</a>";
                        html += "<input type='hidden' name='Avatar' value='" + thumbPath + "' />";

                        jQuery("#AvatarDrager").removeClass("loading");
                        jQuery("#AvatarDrager").html(html);
                        jQuery("#ImageAvatar").load(function () {
                            AvatarPage.initCrop();
                        });
                    }
                });
            }
        });
    },
    upEventRow: function (row) {
        row.find(".datetime").datetimepicker({
            format: "d-m-Y H:i",
            lang: "vi",
            scrollInput: false
        });
    },
    onProgress: function (e, file) {
        var pc = Math.floor(100 * (e.loaded / e.total));
        var rowId = UpfilePage.getRowId(file.id, true);
        jQuery(rowId).find(".progress-bar").css("width", pc + "%");
        jQuery(rowId).find(".progress-label").text(pc + "%");
    },
    onAbort: function (e, file) {
        jQuery(UpfilePage.getRowId(file.id, true))
            .find(".upStatus")
            .html("<a href='#' class='loadfail' title='Tải tài liệu lên không thành công' ></a>");
    },
    getRowId: function (fileId, isSelector) {
        return (isSelector ? "#" : "") + "DocUploadR" + fileId;
    }

};
function updateQueryStringParameter(uri, key, value) {
  var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
  var separator = uri.indexOf('?') !== -1 ? "&" : "?";
  if (uri.match(re)) {
    return uri.replace(re, '$1' + key + "=" + value + '$2');
  }
  else {
    return uri + separator + key + "=" + value;
  }
}
jQuery(document).ready(function () {
    UpfilePage.init();
})