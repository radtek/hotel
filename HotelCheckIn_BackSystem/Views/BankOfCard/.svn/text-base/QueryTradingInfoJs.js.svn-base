var myurl;
var mydata;
var postype = "POST";
var getype = "GET";
var jsontype = "json";
var htmltype = "html";
var contentype = "application/json; charset=utf-8";
//----------------------------  初始化  ---------------------------------
$(function () {
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
    dgObj.url = '../../DataService/WebService/BankOfCard/BankOfCardService.ashx';
    dgObj.queryParams.action = "QueryTradingData";
    dgObj.pagination = true;
    dgObj.pageSize = 20;
    dgObj.border = false;
    dgObj.columns = [[
        { field: 'TradingType', title: '交易类型', align: 'center', width: 80,
            formatter: function (value, row, index) {
                switch (value) {
                    case "1": return "预授权";
                    case "2": return "预授权完成";
                    default: return "未知";
                }
            }
        },
        { field: 'RoomNumber', title: '房间号', align: 'center', width: 80 },
        { field: 'CheckOrderNumber', title: '入住订单号', align: 'center', width: 80 },
        { field: 'ReturnCode', title: '返回码', align: 'center', width: 80,
            formatter: function (value, row, index) {
                switch (value) {
                    case "00":
                        return "交易成功";
                    default:
                        return "交易失败";
                }
            }
        },
        { field: 'BankNumber', title: '银行行号', align: 'center', width: 60 },
        { field: 'CardNumber', title: '卡号', align: 'center', width: 120 },
        { field: 'LotNo', title: '批次号', align: 'center', width: 80 },
        { field: 'Money', title: '金额(元)', align: 'center', width: 80,
            formatter: function (value, row, index) {
                value = value.substring(0, value.length - 2);
                return parseFloat(value);
            }
        },
        { field: 'ContactNo', title: '商户号', align: 'center', width: 100 },
        { field: 'TerminalNo', title: '终端号', align: 'center', width: 80 },
        { field: 'TransactionReferenceNumber', title: '交易参考号', align: 'center', width: 80 },
        { field: 'TradingDate', title: '交易日期', align: 'center', width: 60 },
        { field: 'TradingTime', title: '交易时间', align: 'center', width: 60 },
        { field: 'AuthorizationNumber', title: '授权号', align: 'center', width: 60 }
    ]];
    $('#dg').datagrid(dgObj);
}
