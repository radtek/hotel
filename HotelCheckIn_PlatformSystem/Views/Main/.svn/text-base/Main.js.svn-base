var myurl;
var mydata;
var postype = "POST";
var getype = "GET";
var jsontype = "json";
var htmltype = "html";
var contentype = "application/json; charset=utf-8";
//----------------------------  初始化  ---------------------------------
$(function () {
    //设置每10分钟更新一次(todo:这个自己配置)
    setInterval(function () {
        initMainGrid();
    }, 600000);
    initMainGrid();
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
function initMainGrid() {
    dgObj.url = '../../DataService/WebService/Main/MainService.ashx';
    dgObj.queryParams.action = "InitMainGrid";
    dgObj.pagination = true;
    dgObj.pageSize = 20;
    dgObj.border = false;
    dgObj.columns = [[
        { field: 'JqId', title: '机器ID', align: 'left', width: 80 },
        { field: 'Name', title: '机器名称', align: 'left', width: 200 },
        { field: 'IP', title: '机器IP', align: 'center', width: 150 },
        { field: 'IsOnline', title: '当前在线状态', align: 'center', width: 130 },
        { field: 'HeartbeatDtPara', title: '在线时间', align: 'center', width: 200},//在线时间也就是心跳时间
        { field: 'Note', title: '备注', align: 'center', width: 255 }
    ]];
    $('#dg').datagrid(dgObj);
}
