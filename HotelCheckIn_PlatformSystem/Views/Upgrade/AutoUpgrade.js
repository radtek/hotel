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
    initUpgradeGrid(); //初始化datagrid
    initDlg();
    initUploadObj();
});

/**
* *设置datagrid对象
**/
var dgObj = {
    url: '../../DataService/WebService/UpgradeFile/UpgradeFileService.ashx',
    queryParams: { action: 'InitUpgradeFile', id: '' },
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
    upload_url: "../../DataService/WebService/UpgradeFile/UpgradeFileService.ashx",
    //upload_url: "../../Views/Material/upload.aspx",
    post_params: {
        action: "SaveUpgradeFile", id: ""
    },
    // File Upload Settings
    file_size_limit: "10240",
    file_types: "*.jpg;*.htm;*.html;*.aspx;*.exe;*.ashx;*.asmx;*.xml;*.webconfig;",
    file_types_description: "*.jpg;*.htm;*.html;*.aspx;*.exe;*.ashx;*.asmx;*.xml;*.webconfig;",
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
    debug: false
};

//----------------------------  页面方法  ---------------------------------
/**
* *初始化上传文件控件
**/
function initUploadObj() {
    var swfu = new SWFUpload(swfObj);
}

/**
* *初始化datagrid
**/
function initUpgradeGrid() {
    dgObj.pagination = true;
    dgObj.pageSize = 20;
    dgObj.columns = [[
        { field: 'Id', hidden: true },
       { field: 'FileName', title: '文件名', align: 'left', width: 300 },
        { field: 'Size', title: '文件大小(kb)', align: 'center', width: 200 },
        { field: 'Type', title: '文件类型', align: 'center', width: 200 },
        { field: 'CreateDtPara', title: '创建日期', align: 'center', width: 200 },
         { field: 'Extension', title: '操作', align: 'center', width: 115,
             formatter: function (value, row, index) {
                 var file = row.FileName + row.Extension;
                 var str = "<a href='#' onclick='delUpgradeFiles_click(\"" + row.Id + "\",\"" + file + "\")' class='easyui-linkbutton' plain='true' title='' iconcls='icon-cancel'></a>";
                return str;
             }
         }
    ]],
    dgObj.onLoadSuccess = function () {
        $($('#dg_upgrade').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton();
    };
    $('#dg_upgrade').datagrid(dgObj);
}


/**
* *初始化对话框
**/
function initDlg() {
    $('#dlg_upgrade').dialog({
        title: '请稍等...',
        width: 475,
        height: 93,
        modal: true,
        closed: true,
        closable:false,
        draggable:false
    });
}



//----------------------------  页面事件  ---------------------------------


/**
* *删除文件
**/
function delUpgradeFiles_click(id, file) {
    myurl = "../../DataService/WebService/UpgradeFile/UpgradeFileService.ashx";
    mydata = { action: "DelUpgradeFile", id: id, file: file };
    ajaxData();
}

//----------------------------  后台返回js方法(反射)  ---------------------------------

/**
* *删除素材
**/
function ajax_DelUpgradeFile(data) {
    $.messager.show({
        title: '提示',
        msg: data.Msg,
        timeout: 3000,
        showType: 'slide'
    });
    $("#dg_upgrade").datagrid('reload');
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