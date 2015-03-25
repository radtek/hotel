var myurl;
var mydata;
var formname;
var postype = "POST";
var getype = "GET";
var jsontype = "json";
var htmltype = "html";
var contentype = "application/json; charset=utf-8";
//----------------------------  初始化  ---------------------------------
$(function () {
    $("#d_main").fadeIn(1000);
    initFaultGrid();
    getAreaData("1");
    bindFault();
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
function initFaultGrid() {
    dgObj.url = "../../DataService/WebService/Fault/FaultService.ashx";
    dgObj.queryParams.action = "QueryFault";
    dgObj.pagination = true;
    dgObj.pageSize = 20;
    dgObj.border = false;
    dgObj.toolbar = "#tb";
    dgObj.columns = [[
        { field: 'MachineName', title: '机器名称', align: 'center', width: 220 },
        { field: 'FaultName', title: '故障名称', align: 'center', width: 220 },
        { field: 'Reason', title: '问题原因', align: 'center', width: 220 },
        { field: 'SolvePerson', title: '解决人员', align: 'center', width: 95 },
        { field: 'SolveDtPara', title: '解决时间', align: 'center', width: 130 },
        { field: 'CreateDtPara', title: '发生时间', align: 'center', width: 130 }
    ]];
    dgObj.onLoadSuccess = function (data) {
        //data = eval('(' + data + ')');
        if (data.total) {
            if (data.loginout) {
                //$.messager.alert('提示', data.msg);
                window.location.href = "../../Login.aspx";
            }
        }
    }

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
* *绑定故障
**/
function bindFault() {
    myurl = "../../DataService/WebService/Fault/FaultService.ashx";
    mydata = { action: "BindFault" };
    ajaxData();
}

//----------------------------  页面事件  ---------------------------------
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
* *点击查询
**/
function query_click() {
    dgObj.url = "../../DataService/WebService/Fault/FaultService.ashx";
    dgObj.queryParams.action = "QueryFault";
    dgObj.queryParams.jdid = $("#selHotels").val();
    dgObj.queryParams.gzid = $("#selFault").val();
    dgObj.queryParams.kssj = $("#begintime").val();
    dgObj.queryParams.jssj = $("#endtime").val();
    $('#dg').datagrid(dgObj);
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
* *ajax后台返回故障数据绑定到select控件
**/
function ajax_BindFault(data) {
    var l = data.Rows.length;
    $("#selFault").empty();
    $("#selFault").append("<option value=''>—请选择—</option>");
    for (var i = 0; i < l; i++) {
        $("#selFault").append("<option value='" + data.Rows[i].Id + "'>" + data.Rows[i].Name + "</option>");
    }
    return true;
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

function onload(data) {
    if (data.total) {
        if (data.loginout) {
            //$.messager.alert('提示', data.msg);
            window.location.href = "../../Login.aspx";
        }
    }
}

/**
* *from表单提交方法
**/
function ajaxForm() {
    $("#" + formname).form('submit', {
        url: myurl,
        onSubmit: serviceOnSubmit,
        success: serviceSuccess
    });
}

/**
* *ajax提交时验证
**/
function serviceOnSubmit() {
    return $("#" + formname).form('validate');
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

