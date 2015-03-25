var myurl;
var mydata;
var formname;
var postype = "POST";
var getype = "GET";
var jsontype = "json";
var htmltype = "html";
var contentype = "application/json; charset=utf-8";
var flag = "";
var formname = "";
//----------------------------  初始化  ---------------------------------
$(function () {
    initHotelGrid();
    initFormDlg();
    getAreaData("1");
});
/**
* *设置datagrid对象
**/
var dgObj = {
    url: '',
    queryParams: {},
    fit: true,
    singleSelect: true,
    border: false,
    striped: true,
    toolbar: ""
};


//----------------------------  页面方法  ---------------------------------

/**
* *初始化datagrid
**/
function initHotelGrid() {
    dgObj.url = "../../DataService/WebService/Hotel/HotelService.ashx";
    dgObj.queryParams.action = "InitHotelGrid";
    dgObj.queryParams.qyid = $("#selAreas").val();
    dgObj.pagination = true;
    dgObj.pageSize = 20;
    dgObj.border = false;
    dgObj.toolbar = "#tb";
    dgObj.columns = [[
        { field: 'AreaName', title: '所属区域', align: 'center', width: 220 },
        { field: 'Name', title: '名称', align: 'center', width: 220 },
        { field: 'Address', title: '地址', align: 'center', width: 220 },
        { field: 'Tel', title: '电话', align: 'center', width: 130 },
        { field: 'Contact', title: '联系人', align: 'center', width: 95 },
        { field: 'Note', title: '备注', align: 'center', width: 130 }
    ]];
    $('#dg').datagrid(dgObj);
}


/**
* *绑定区域
**/
function getAreaData(type) {
    myurl = "../../DataService/WebService/Machine/MachineService.ashx";
    mydata = { action: "GetAreaData", type: type };
    ajaxData();
}

/**
* *初始化form对话框
**/
function initFormDlg() {
    $('#dlg_form').dialog({
        title: '打开窗体',
        iconCls: 'icon-form',
        width: 396,
        height: 383,
        closable: false,
        modal: true,
        closed: true,
        buttons: [{
            text: '确 定',
            iconCls: 'icon-ok',
            handler: function () {
                var dghotel = $("#dg").datagrid("getSelected");
                if (dghotel != null) {
                    myurl = '../../DataService/WebService/Hotel/HotelService.ashx?action=AddAndEditHotel&flag=' + flag + "&hotelid=" + dghotel.Id;
                } else {
                    myurl = '../../DataService/WebService/Hotel/HotelService.ashx?action=AddAndEditHotel&flag=' + flag;
                }
                formname = "documentForm";
                if (!formOnSubmit()) {
                    return false;
                }
                ajaxForm();
                close_click();
                return true;
            }
        }, {
            text: '取 消',
            iconCls: 'icon-cancel',
            handler: function () {
                close_click();
            }
        }]
    });
}

/**
* *绑定区域
**/
function getAreaData(type) {
    myurl = "../../DataService/WebService/Machine/MachineService.ashx";
    mydata = { action: "GetAreaData", type: type };
    ajaxData();
}

/**
* *绑定form
**/
function bindForm(hotelid) {
    myurl = "../../DataService/WebService/Hotel/HotelService.ashx";
    mydata = { action: "GetHotels", hotelid: hotelid };
    ajaxData();
}

//----------------------------  页面事件  ---------------------------------
/**
* *说明：查询酒店
**/
function query_click() {
    initHotelGrid();
}
/**
* *添加酒店
**/
function add_click(type) {
    flag = type;
    $('#documentForm').form('clear');
    $('#dlg_form').dialog('open');
    getAreaData("2");
    $("#txt_note").val('');
}

/**
* *编辑酒店
**/
function edit_click(type) {
    flag = type;
    var dghotel = $("#dg").datagrid("getSelected");
    if (dghotel == null) {
        $.messager.show({
            title: '提示',
            msg: "请选择一条数据！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    getAreaData("2");
    bindForm(dghotel.Id);
    $('#dlg_form').dialog('open');
    return true;
}

/**
* *删除酒店
**/
function del_click() {
    var dghotel = $("#dg").datagrid("getSelected");
    if (dghotel == null) {
        $.messager.show({
            title: '提示',
            msg: "请选择一条数据！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    //删除数据
    $.messager.confirm('提示', '确定删除酒店?删除将不可恢复！',
        function (r) {
            if (r) {
                myurl = '../../DataService/WebService/Hotel/HotelService.ashx?action=DelHotel';
                mydata = { hotelid: dghotel.Id };
                ajaxData();
                $("#dg").datagrid("reload", {
                    action: "InitHotelGrid", qyid: $("#selAreas").val()
                });
            }
        });
    return true;
}


function close_click() {
    $('#dlg_form').dialog('close');
}

//----------------------------  后台返回js方法(反射)  ---------------------------------
/**
* *ajax后台返回区域数据绑定到select控件
**/
function ajax_GetAreaData(data) {
    var l = data.Rows.length;
    switch (data.Other) {
        case "1":
            $("#selAreas").empty();
            $("#selAreas").append("<option value=''>—请选择—</option>");
            for (var i = 0; i < l; i++) {
                $("#selAreas").append("<option value='" + data.Rows[i].Id + "'>" + data.Rows[i].Name + "</option>");
            }
            break;
        case "2":
            $("#selAreas2").empty();
            $("#selAreas2").append("<option value=''>—请选择—</option>");
            for (var i = 0; i < l; i++) {
                $("#selAreas2").append("<option value='" + data.Rows[i].Id + "'>" + data.Rows[i].Name + "</option>");
            }
            break;
        default:
    }

    return true;
}

/**
* * 说明：ajax后台返回 
* * 作用：添加和编辑酒店数据
**/
function ajax_AddAndEditHotel(data) {
    $.messager.show({
        title: '提示',
        msg: data.Msg,
        timeout: 3000,
        showType: 'slide'
    });
    $("#dg").datagrid("reload", {
        action: "InitHotelGrid", qyid: $("#selAreas").val()
    });
}

/**
* * 说明：ajax后台返回 
* * 作用：绑定酒店数据
**/
function ajax_GetHotels(data) {
    $("#selAreas2").val(data.Rows[0].AreaId);
    $("#txt_name").val(data.Rows[0].Name);
    $("#txt_address").val(data.Rows[0].Address);
    $("#txt_tel").val(data.Rows[0].Tel);
    $("#txt_contact").val(data.Rows[0].Contact);
    $("#txt_note").val(data.Rows[0].Note);
}


/**
* * 说明：ajax后台返回 
* * 作用：删除酒店数据
**/
function ajax_DelHotel(data) {
    $.messager.show({
        title: '提示',
        msg: data.Msg,
        timeout: 3000,
        showType: 'slide'
    });
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
