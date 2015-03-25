var myurl;
var mydata;
var postype = "POST";
var getype = "GET";
var jsontype = "json";
var htmltype = "html";
var contentype = "application/json; charset=utf-8";
var flag;
var formname;
//----------------------------  初始化  ---------------------------------
$(function () {
    initMaterialGrid(); //初始化datagrid
    initDlg();
    initFormDlg();
    closedDlg();
});

/**
* *设置datagrid对象
**/
var dgObj = {
    url: '../../DataService/WebService/Material/MaterialService.ashx',
    queryParams: { action: 'InitMaterialGrid', scid: '' },
    fit: true,
    singleSelect: true,
    border: false,
    striped: true,
    toolbar: "#tb"
};

/**
* *定义swfupload配置文件对象
**/
var swfObj = {
    // Backend Settings
    upload_url: "../../DataService/WebService/SwfUpload/SwfUploadService.ashx",
    //upload_url: "../../Views/Material/upload.aspx",
    post_params: {
        action: "SaveFile", scid: ""
    },
    // File Upload Settings
    file_size_limit: "10240",
    file_types: "*.jpg;*.htm;*.html;*.rmvb;*.avi;*.mp3;",
    file_types_description: "*.jpg;*.htm;*.html;*.rmvb;*.avi;*.mp3;",
    file_upload_limit: "0",    // Zero means unlimited
    // Event Handler Settings - these functions as defined in Handlers.js
    //  The handlers are not part of SWFUpload but are part of my website and control how
    //  my website reacts to the SWFUpload events.
    file_queue_error_handler: fileQueueError,
    file_dialog_complete_handler: fileDialogComplete,
    upload_progress_handler: uploadProgress,
    upload_error_handler: uploadError,
    upload_success_handler: uploadSuccess,
    upload_complete_handler: uploadComplete,
    // Button settings
    button_image_url: "../../Images/XPButtonNoText_160x22.png",
    button_placeholder_id: "spanButtonPlaceholder",
    button_width: 160,
    button_height: 22,
    button_text: '<span class="button">选择文件上传 <span class="buttonSmall">(最大 10Mb)</span></span>',
    button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt;} .buttonSmall { font-size: 10pt; }',
    button_text_top_padding: 1,
    button_text_left_padding: 5,
    // Flash Settings
    flash_url: "../../Scripts/swfupload/swfupload.swf", // Relative to this file
    custom_settings: {
        upload_target: "divFileProgressContainer"
    },
    // Debug Settings
    debug: true
};

//----------------------------  页面方法  ---------------------------------

/**
* *初始化datagrid
**/
function initMaterialGrid() {
    dgObj.pagination = true;
    dgObj.pageSize = 20;
    dgObj.columns = [[
        { field: 'Id', hidden: true },
        { field: 'Name', title: '素材名称', align: 'left', width: 100 },
        { field: 'Url', title: '主路径', align: 'left', width: 296 },
        { field: 'DateTimePara', title: '日期', align: 'center', width: 130 },
        { field: 'Operator', title: '操作人', align: 'center', width: 80 },
        { field: 'Note', title: '备注', align: 'center', width: 198 },
        { field: 'UpdateDtPara', title: '更新日期', align: 'center', width: 130 },
        { field: 'UpdatePerson', title: '更新人', align: 'center', width: 80 }
    ]];
    $('#dg_material').datagrid(dgObj);
}

/**
* *初始化datagrid
**/
function initFileGrid(scid) {
    dgObj.url = "../../DataService/WebService/Files/FilesService.ashx";
    dgObj.queryParams.action = "InitFilesGrid";
    dgObj.queryParams.scid = scid;
    dgObj.pagination = false;
    dgObj.toolbar = "";
    dgObj.height = "100";
    dgObj.border = true;
    dgObj.columns = [[
        { field: 'Id', hidden: true },
        { field: 'FileName', title: '文件名', align: 'left', width: 125 },
        { field: 'Size', title: '文件大小(kb)', align: 'center', width: 78 },
        { field: 'Type', title: '文件类型', align: 'center', width: 60 },
        { field: 'StrCreateDt', title: '创建日期', align: 'center', width: 130 },
         { field: 'Extension', title: '操作', align: 'center', width: 40,
             formatter: function (value, row, index) {
                 var file = row.FileName + row.Extension;
                 var str = "<a href='#' onclick='delFiles_click(\"" + row.Id + "\",\"" + file + "\",\"" + scid + "\")' class='easyui-linkbutton' plain='true' title='' iconcls='icon-cancel'></a>";
                 return str;
             }
         }
    ]],
    dgObj.onLoadSuccess = function () {
        $($('#dg_file').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton();
    };
    $('#dg_file').datagrid(dgObj);
}

/**
* *初始化对话框
**/
function initDlg() {
    $('#dlg').dialog({
        title: '上传文件',
        iconCls: 'icon-upload',
        width: 475,
        height: 413,
        modal: true,
        closable: false,
        closed: true,
        buttons: [{
            text: '确 定',
            iconCls: 'icon-ok',
            handler: function () {
                var selectmaterial = $("#dg_material").datagrid("getSelected");
                var scid = selectmaterial.Id;
                var selectfile = $("#dg_file").datagrid("getSelected");
                if (!selectfile) {
                    $.messager.show({
                        title: '提示',
                        msg: "请先选择一条数据，确定素材路径！",
                        timeout: 3000,
                        showType: 'slide'
                    });
                    return false;
                }
                $('#dlg').dialog('close');
                myurl = "../../DataService/WebService/Material/MaterialService.ashx";
                var file = selectfile.FileName + selectfile.Extension;
                mydata = { action: "SaveFilePath", scid: scid, file: file };
                ajaxData();
                return true;
            }
        }, {
            text: '取 消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#dlg').dialog('close');
            }
        }]
    });

    $('#dlg_material').dialog({
        title: '请稍等...',
        width: 375,
        height: 93,
        modal: true,
        closed: true,
        closable: false,
        draggable: false
    });
}


/**
* *初始化form对话框
**/
function initFormDlg() {
    $('#dlg_form').dialog({
        title: '打开窗体',
        iconCls: 'icon-form',
        width: 380,
        height: 187,
        modal: true,
        closed: true,
        closable: false,
        buttons: [{
            text: '确 定',
            iconCls: 'icon-ok',
            handler: function () {
                var scid = '';
                if (flag != "add") {
                    var selectmaterial = $("#dg_material").datagrid("getSelected");
                    scid = selectmaterial.Id;
                }
                formname = "documentForm";
                myurl = '../../DataService/WebService/Material/MaterialService.ashx?action=AddAndEditMaterial&flag=' + flag + "&id=" + scid;
                ajaxForm();
                return true;
            }
        }, {
            text: '取 消',
            iconCls: 'icon-cancel',
            handler: function () {
                closeform_click();
            }
        }]
    });
}
//----------------------------  页面事件  ---------------------------------
/**
* *关闭对话框事件
**/
function closedDlg() {
    $('#dlg').dialog({
        onClose: function () {
            location.reload();
        }
    });
}


/**
* *删除文件
**/
function delFiles_click(fileid, file, scid) {
    myurl = "../../DataService/WebService/Files/FilesService.ashx";
    mydata = { action: "DelFilesById", fileid: fileid, file: file, scid: scid };
    ajaxData();
}

/**
* *添加
**/
function add_click(type) {
    flag = type;
    $("#documentForm").form('clear');
    $('#dlg_form').dialog('open');
    //$("#txt_note").val('');
}
/**
* *修改
**/
function edit_click(type) {
    var selectmaterial = $("#dg_material").datagrid("getSelected");
    if (selectmaterial == null) {
        $.messager.show({
            title: '提示',
            msg: "请先选择素材！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    flag = type;
    $("#documentForm").form('clear');
    $('#dlg_form').dialog('open');
    myurl = "../../DataService/WebService/Material/MaterialService.ashx";
    mydata = { action: "GetMaterial", id: selectmaterial.Id };
    ajaxData();
    return true;
}

/**
* *删除
**/
function del_click() {
    var selectmaterial = $("#dg_material").datagrid("getSelected");
    if (!selectmaterial) {
        $.messager.show({
            title: '提示',
            msg: "请先选择素材！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    //删除数据
    $.messager.confirm('提示', '确定删除素材?删除将不可恢复！',
        function (r) {
            if (r) {
                myurl = "../../DataService/WebService/Material/MaterialService.ashx";
                mydata = { action: "DelMaterial", id: selectmaterial.Id };
                ajaxData();
                $("#dg_material").datagrid("reload");
            }
        });
    return true;
}
/**
* *上传文件
**/
function upload_click() {
    var select = $("#dg_material").datagrid("getSelected");
    if (!select) {
        $.messager.show({
            title: '提示',
            msg: "请先选择素材！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    swfObj.post_params.scid = select.Id;
    var swfu = new SWFUpload(swfObj);
    $('#dlg').dialog('open');
    initFileGrid(select.Id);
    return true;
}


/**
* *关闭告警处理界面
**/
function close_click() {
    $('#dlg').dialog('close');
}

/**
* *关闭form界面
**/
function closeform_click() {
    $('#dlg_form').dialog('close');
}

//----------------------------  后台返回js方法(反射)  ---------------------------------
/**
* *删除文件结果(ajax返回)
**/
function ajax_DelFilesById(data) {
    $.messager.show({
        title: '提示',
        msg: data.Msg,
        timeout: 3000,
        showType: 'slide'
    });
    $('#dg_file').datagrid("reload");
}

/**
* *保存文件路径(ajax返回)
**/
function ajax_SaveFilePath() {
    //    $.messager.show({
    //        title: '提示',
    //        msg: data.Msg,
    //        timeout: 3000,
    //        showType: 'slide'
    //    });
    //    $('#dg_material').datagrid("reload");
    //因为关闭对话框整个页面都刷新了，感觉这里用不到
}

/**
* *添加和编辑素材
**/
function ajax_AddAndEditMaterial(data) {
    $.messager.show({
        title: '提示',
        msg: data.Msg,
        timeout: 3000,
        showType: 'slide'
    });
    closeform_click();
    $("#dg_material").datagrid('reload');
}

/**
* *添加和编辑素材
**/
function ajax_GetMaterial(data) {
    $("#txt_name").val(data.Rows[0].Name);
    $("#txt_note").val(data.Rows[0].Note);
}

/**
* *删除素材
**/
function ajax_DelMaterial(data) {
    $.messager.show({
        title: '提示',
        msg: data.Msg,
        timeout: 3000,
        showType: 'slide'
    });
    closeform_click();
    $("#dg_material").datagrid('reload');
}
//----------------------------  ajax方法  ---------------------------------

/**
* *ajax增删改查方法
**/
function ajaxData() {
    $.ajax({
        url: myurl,
        type: postype,
        async: false,
        data: mydata,
        dataType: jsontype,
        success: serviceSuccess,
        error: serviceError
    });
}

/**
* *from表单提交方法
**/
function ajaxForm() {
    $("#" + formname).form('submit', {
        url: myurl,
        onSubmit: formOnSubmit,
        success: formSuccess
    });
}

/**
* *ajax提交时验证
**/
function formOnSubmit() {
    return $("#" + formname).form('validate');
}

/**
* *ajax成功时返回resultObject是json数据
**/
function formSuccess(resultObject) {
    if (resultObject == null) {
        return true;
    }
    var data = eval('(' + resultObject + ')');
    if (data.IsSuccess) {
        eval(data.JsExecuteMethod + "(data)");
    } else {
        $.messager.show({
            title: '提示',
            msg: data.Msg,
            timeout: 3000,
            showType: 'slide'
        });
    }
    return true;
}

/**
* *ajax成功时返回resultObject是json数据
**/
function serviceSuccess(resultObject) {
    if (resultObject == null) {
        return true;
    }
    if (resultObject.IsSuccess) {
        eval(resultObject.JsExecuteMethod + "(resultObject)");
    } else {
        $.messager.show({
            title: '提示',
            msg: resultObject.Msg,
            timeout: 3000,
            showType: 'slide'
        });
    }
    return true;
}

/**
* *ajax失败时返回
**/
function serviceError(result) {
    return false;
}