var myurl;
var mydata;
var postype = "POST";
var getype = "GET";
var jsontype = "json";
var htmltype = "html";
var contentype = "application/json; charset=utf-8";
var flag;
var selectarray = new Array(); //选择的数组(保存的是文件的id)
var downloadarray = new Array(); //是否要下载的数组(保存的是是否要下载文件，isflag=true)
Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) return i;
    }
    return -1;
};
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};
//----------------------------  初始化  ---------------------------------
$(function () {
    initMachineGrid();
    getAreaData("1");
    initDlg();
    initFormDlg();
    close_click();
});
/**
* *设置datagrid对象
**/
var dgObj = {
    url: '../../DataService/WebService/Machine/MachineService.ashx',
    queryParams: { action: 'InitMachineGrid', jdid: '' },
    fit: true,
    singleSelect: true,
    border: false,
    striped: true,
    toolbar: "#tb"
};


//----------------------------  页面方法  ---------------------------------

/**
* *初始化datagrid
**/
function initMachineGrid() {
    dgObj.url = '../../DataService/WebService/Machine/MachineService.ashx';
    dgObj.queryParams.action = "InitMachineGrid";
    dgObj.queryParams.jdid = $("#selHotels").val(); //酒店id
    dgObj.queryParams.qyid = $("#selAreas").val(); //区域id
    dgObj.pagination = true;
    dgObj.pageSize = 20;
    dgObj.singleSelect = false;
    dgObj.border = false;
    dgObj.toolbar = "#tb";
    dgObj.columns = [[
        { field: 'ck1', checkbox: true },
        { field: 'JqId', title: '机器ID', align: 'left', width: 80 },
        { field: 'Name', title: '机器名称', align: 'left', width: 100 },
        { field: 'IP', title: '机器IP', align: 'center', width: 96 },
        { field: 'HotelName', title: '所属酒店', align: 'center', width: 120 },
        { field: 'FaultId', title: '故障', align: 'center', width: 97 },
        { field: 'MaterialUrl', title: '素材路径', align: 'center', width: 280 },
    //{ field: 'HeartbeatDtPara', title: '心跳时间', align: 'center', width: 130 },
    //        { field: 'CreateDtPara', title: '创建时间', align: 'center', width: 130 },
            {field: 'UpdateDtPara', title: '更新日期', align: 'center', width: 130 },
        { field: 'Isdisabled', title: '是否可用', align: 'center', width: 82,
            formatter: function (value, row, index) {
                if (row.Isdisabled == 1) {
                    return "是";
                } else if (row.Isdisabled == 2) {
                    return "否";
                } else {   
                    return "";
                }
            }
        }
    ]];
    dgObj.onClickRow = function (index, data) {

    };
    $('#dg_machine').datagrid(dgObj);
}

/**
* *初始化素材文件datagrid
**/
function initMaterialGrid() {
    dgObj.url = "../../DataService/WebService/Material/MaterialService.ashx";
    dgObj.queryParams.action = "FindByMaterialGrid";
    dgObj.pagination = false;
    dgObj.border = true;
    dgObj.singleSelect = true;
    dgObj.toolbar = "";
    dgObj.columns = [[
        { field: 'Name', title: '素材名称', align: 'left', width: 100 },
        { field: 'Url', title: '主路径', align: 'left', width: 337 }
    ]];
    dgObj.onClickRow = function (index, data) {
        $("#txtUrl").val(data.Url); //绑定素材url
    };
    $('#dg_material').datagrid(dgObj);
}


/**
* *初始化升级文件datagrid
**/
function initUpgradeFileGrid() {
    var selectmachine = $("#dg_machine").datagrid("getChecked");
    var jqid = selectmachine[0].JqId;
    dgObj.url = "../../DataService/WebService/UpgradeFile/UpgradeFileService.ashx";
    dgObj.queryParams.action = "GetUpgradeFile";
    dgObj.queryParams.jqid = jqid;
    dgObj.pagination = false;
    dgObj.border = true;
    dgObj.singleSelect = true;
    dgObj.toolbar = "";
    dgObj.columns = [[
        { field: 'ckselect', title: '选择', formatter: function (val, data) {
            var str = "";
            var id = data.Id;
            if (jqid == data.MachineId) {
                str = "<input id='ck1" + id + "' checked=true type='checkbox' onchange='javascript:ckselect_onchange(\"" + id + "\");'>";
                selectarray.push(id);
            } else {
                str = "<input id='ck1" + id + "' type='checkbox' onchange='javascript:ckselect_onchange(\"" + id + "\");'>";
            }
            return str;
        }
        },
        { field: 'ckdownload', title: "是否要下载", align: 'center', formatter: function (val, data) {
            var str = "";
            var id = data.Id;
            if (jqid == data.MachineId && data.IsDownland == 0) {
                str = "<input id='ck2" + id + "' type='checkbox' onchange='javascript:ckdownload_onchange(\"" + id + "\");'>";
            }
            else if (jqid == data.MachineId && data.IsDownland == 1) {
                str = "<input id='ck2" + id + "' checked=true type='checkbox' onchange='javascript:ckdownload_onchange(\"" + id + "\");'>";
                downloadarray.push(id);
            }
            else {
                str = "<input id='ck2" + id + "' type='checkbox' onchange='javascript:ckdownload_onchange(\"" + id + "\");'>";
            }
            return str;
        }
        },
        { field: 'FileName', title: '文件名', align: 'left', width: 200 },
        { field: 'Type', title: '类型', align: 'center', width: 40 },
        { field: 'Url', title: '路径', align: 'left', width: 210 },
        { field: 'IsFlag', title: '下载状态', align: 'center', width: 60, formatter: function (val, data) {
            switch (data.IsFlag) {
                case 0:
                    return "未下载";
                case 1:
                    return "已下载";
                default:
                    return "未下载";
            }
        }
        }
    ]];
    $('#dg_upgrade').datagrid(dgObj);
}

/**
* *初始化对话框
**/
function initDlg() {
    $('#dlg').dialog({
        title: '选择素材文件',
        iconCls: 'icon-upload',
        width: 475,
        height: 460,
        modal: true,
        closable: false,
        closed: true,
        buttons: [{
            text: '确 定',
            iconCls: 'icon-ok',
            handler: function () {
                var txtUrl = $("#txtUrl").val();
                if (txtUrl.length <= 0) {
                    $.messager.show({
                        title: '提示',
                        msg: "请输入素材地址！",
                        timeout: 3000,
                        showType: 'slide'
                    });
                    return false;
                }
                close_click();
                myurl = "../../DataService/WebService/Machine/MachineService.ashx";
                var selectmachine = $("#dg_machine").datagrid("getChecked");
                var jqid = "";
                for (var i = 0; i < selectmachine.length; i++) {
                    jqid += selectmachine[i].JqId + "&";
                }
                jqid = jqid.substring(0, jqid.length - 1);
                mydata = {
                    action: "ModifyMachineByJqid",
                    jqid: jqid,
                    materialurl: txtUrl
                };
                ajaxData();
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
    $('#dlg_upgrade').dialog({
        title: '选择升级文件',
        iconCls: 'icon-upload',
        width: 665,
        height: 475,
        modal: true,
        closable: false,
        closed: true,
        buttons: [{
            text: '确 定',
            iconCls: 'icon-ok',
            handler: function () {
                var selectmachine = $("#dg_machine").datagrid("getChecked");
                myurl = "../../DataService/WebService/UpgradeMachine/UpgradeMachineService.ashx";
                mydata = {
                    action: 'SaveUpgradeMachine',
                    jqid: selectmachine[0].JqId,
                    ckseleck: selectarray.join('&'),
                    download: downloadarray.join('&')
                };
                ajaxData();
                $('#dlg_upgrade').dialog('close');
                $("#dg_machine").datagrid("reload", {//InitMachineGrid
                    action: "InitMachineGrid", jdid: $("#selHotels").val()
                });
                return true;
            }
        }, {
            text: '取 消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#dlg_upgrade').dialog('close');
                $("#dg_machine").datagrid("reload", {//InitMachineGrid
                    action: "InitMachineGrid", jdid: $("#selHotels").val()
                });
            }
        }]
    });
}

/**
* *初始化form对话框
**/
function initFormDlg() {
    $('#dlg_form').dialog({
        title: '打开窗体',
        iconCls: 'icon-form',
        width: 396,
        height: 430,
        closable: false,
        modal: true,
        closed: true,
        buttons: [{
            text: '确 定',
            iconCls: 'icon-ok',
            handler: function () {
                myurl = '../../DataService/WebService/Machine/MachineService.ashx?action=AddAndEditMachine&flag=' + flag;
                $("#documentForm").form('submit', {
                    url: myurl,
                    onSubmit: function () {
                        return $("#documentForm").form('validate');
                    },
                    success: function (data) {
                        var val = null;
                        eval("val=" + data);
                        $.messager.show({
                            title: '提示',
                            msg: val.Msg,
                            timeout: 3000,
                            showType: 'slide'
                        });
                        close_click();
                    }
                });

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

//----------------------------  页面事件  ---------------------------------
/**
* *选择checkbox控件onchange事件
**/
function ckselect_onchange(id) {
    if ($("#ck1" + id).attr("checked")) {
        selectarray.push(id);
    } else {
        $("#ck2" + id).attr("checked", false);
        downloadarray.remove(id);
        selectarray.remove(id);
    }
}

/**
* *是否要下载checkbox控件onchange事件
**/
function ckdownload_onchange(id) {
    if ($("#ck2" + id).attr("checked")) {
        $("#ck1" + id).attr("checked", true);
        selectarray.remove(id);
        selectarray.push(id);
        downloadarray.push(id);
    } else {
        downloadarray.remove(id);
    }
}

/**
* *查询
**/
function query_click() {
    initMachineGrid(); //根据选中的区域和酒店，初始化机器数据
}

/**
* *添加
**/
function add_click(type) {
    $("input[type=radio]").removeAttr("checked").get(0).checked = true;   
    flag = type;
    getAreaData("2");
    $('#dlg_form').dialog('open');
    $("#txt_note").val('');
    $("#txt_jqid").attr("readonly", false);
}
/**
* *修改
**/
function edit_click(type) {
    flag = type;
    var selectmachine = $("#dg_machine").datagrid("getChecked");
    if (selectmachine.length == 0) {
        $.messager.show({
            title: '提示',
            msg: "请先选择机器！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    } else if (selectmachine.length > 1) {
        $.messager.show({
            title: '提示',
            msg: "只能选择一条数据进行修改！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    myurl = "../../DataService/WebService/Machine/MachineService.ashx?action=GetMachine";
    mydata = { jqid: selectmachine[0].JqId };
    ajaxData();
    return true;
}

/**
* *删除
**/
function del_click() {
    var selectmachine = $("#dg_machine").datagrid("getChecked");
    if (selectmachine.length == 0) {
        $.messager.show({
            title: '提示',
            msg: "请先选择机器！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    var jqid = "";
    for (var i = 0; i < selectmachine.length; i++) {
        jqid += selectmachine[i].JqId + "&";
    }
    jqid = jqid.substring(0, jqid.length - 1);
    //删除数据
    $.messager.confirm('提示', '确定删除机器?删除将不可恢复！',
        function (r) {
            if (r) {
                myurl = '../../DataService/WebService/Machine/MachineService.ashx?action=DelMachine';
                mydata = { jqid: jqid };
                ajaxData();
                $("#dg_machine").datagrid("reload", {//InitMachineGrid
                    action: "InitMachineGrid", jdid: $("#selHotels").val()
                });
            }
        });
    return true;
}
/**
* *选择素材路径
**/
function selectMaterial_click() {
    var select = $("#dg_machine").datagrid("getChecked");
    if (select.length == 0) {
        $.messager.show({
            title: '提示',
            msg: "请先选择机器！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    $('#dlg').dialog('open');
    $("#txtUrl").focus();
    initMaterialGrid();
    return true;
}

/**
* *选择升级文件点击事件
**/
function selectUpgrade_click() {
    var selectmachine = $("#dg_machine").datagrid("getChecked");
    if (selectmachine.length == 0) {
        $.messager.show({
            title: '提示',
            msg: "请先选择机器！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    } else if (selectmachine.length > 1) {
        $.messager.show({
            title: '提示',
            msg: "只能选择一条数据！",
            timeout: 3000,
            showType: 'slide'
        });
        return false;
    }
    $('#dlg_upgrade').dialog('open');
    selectarray = [];
    downloadarray = [];
    initUpgradeFileGrid();
    return true;
}

/**
* *选择区域点击事件
**/
function selAreas_onchange(type) {
    var areaid;
    myurl = "../../DataService/WebService/Hotels/HotelsService.ashx";
    switch (type) {
        case "1":
            areaid = $("#selAreas").val(); //区域id
            break;
        case "2":
            areaid = $("#selAreas2").val(); //区域id
            break;
        default:
    }
    mydata = { action: "BindHotelsData", areaid: areaid, type: type };
    ajaxData();
}

/**
* *关闭告警处理界面
**/
function close_click() {
    $('#dlg').dialog('close');
    $('#dlg_form').dialog('close');
    $('#dlg_upgrade').dialog('close');

    $('#dlg').dialog({
        onClose: function () {
            $("#txtUrl").val('');
        }
    });
    $('#dlg_form').dialog({
        onClose: function () {
            $("#selHotels2").empty();
            $("#txt_jqid").val('');
            $("#txt_name").val('');
            $("#txt_pass").val('');
            $("#txt_ip").val('');
            $("#txt_note").val('');
        }
    });
    $("#dg_machine").datagrid("reload", {//InitMachineGrid
        action: "InitMachineGrid", jdid: $("#selHotels").val()
    });
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
* *ajax后台返回酒店数据绑定到select控件
**/
function ajax_BindHotelsData(data) {
   
    var l = data.Rows.length;
    switch (data.Other) {
        case "1":
            $("#selHotels").empty();
            for (var i = 0; i < l; i++) {
                $("#selHotels").append("<option value='" + data.Rows[i].Id + "'>" + data.Rows[i].Name + "</option>");
            }
            break;
        case "2":
            $("#selHotels2").empty();
            for (var i = 0; i < l; i++) {
                $("#selHotels2").append("<option value='" + data.Rows[i].Id + "'>" + data.Rows[i].Name + "</option>");
            }
            break;
        default:
    }
    return true;
}
/**
* *ajax后台返回更新机器表datagrid
**/
function ajax_ModifyMachineByJqid(data) {
    $.messager.show({
        title: '提示',
        msg: data.Msg,
        timeout: 3000,
        showType: 'slide'
    });

    $("#dg_machine").datagrid("reload", {//InitMachineGrid
        action: "InitMachineGrid", jdid: $("#selHotels").val()
    });
}

/**
* *ajax后台返回删除数据
**/
function ajax_DelMachine(data) {
    $.messager.show({
        title: '提示',
        msg: data.Msg,
        timeout: 3000,
        showType: 'slide'
    });
}

/**
* *ajax后台返回机器数据
**/
function ajax_GetMachine(data) {
    getAreaData("2");
    $('#dlg_form').dialog('open');
    $("#txt_jqid").attr("readonly", true);
    $("#selAreas2").val(data.Rows[0].Areaid);
    selAreas_onchange("2");
    //$("#selHotels2").val(data.Rows[0].HotelId);
    $("#txt_jqid").val(data.Rows[0].JqId);
    $("#txt_name").val(data.Rows[0].Name);
    $("#txt_pass").val(data.Rows[0].Password);
    $("#txt_ip").val(data.Rows[0].IP);
    $("#txt_note").val(data.Rows[0].Note);
    if (data.Rows[0].Isdisabled == 1) {
        $("input[type=radio]").removeAttr("checked").get(0).checked = true;
    } else if (data.Rows[0].Isdisabled == 2) {
        $("input[type=radio]").removeAttr("checked").get(1).checked = true;
    } else {
        $("input[type=radio]").removeAttr("checked");
    }
}

/**
* *说明:ajax后台返回|作用:保存升级机器表
**/
function ajax_SaveUpgradeMachine(data) {
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
            msg: data.Msg,
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