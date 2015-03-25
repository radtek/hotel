$(function () {
    //$("#dlg").parent().bgiframe();
    $("#d_main,#dlg,#dlgsettle").fadeIn(1000);
    $("#qendtime").val((new Date()).Format("yyyy-MM-dd"));
    $("#qbegintime").val(new Date().getFullYear() + "-01-01");
    var qbegintime = $("#qbegintime").val();
    var qendtime = $("#qendtime").val();
    $("#t_settle").datagrid({
        url: "/DataService/WebService/InternetGroup/SettleService.ashx",
        queryParams: { action: "querysettle", qbegintime: qbegintime, qendtime: qendtime },
        fit: true,
        pagination: true,
        nowrap: false,
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        striped: true,
        border: false,
        pageList: [10, 20, 30, 50],
        pageSize: 20,
        columns: [[
                    { field: "BeginTime", title: "结算开始时间", width: 120, align: 'center',
                        formatter: function (value) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                            if (dt == "1-01-01 08:00:00")
                                return "总计";
                            else
                                return dt;
                        }
                    },
                    { field: "EndTime", title: "结算结束时间", width: 120, align: 'center',
                        formatter: function (value) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                            if (dt == "1-01-01 08:00:00")
                                return "-";
                            else
                                return dt;
                        }
                    },
                    { field: "InMoney", title: "收入金额（元）", width: 120, align: 'center' },
                    { field: "OutMoney", title: "支出金额（元）", width: 120, align: 'center' },
                    { field: "SumMoney", title: "差额（元）", width: 100, align: 'center',
                        formatter: function (value, src) {
                            var re = "";
                            if (src.InMoney < src.OutMoney) {
                                re = "<div style='color:red;'>" + value + "</div>";
                            } else if (src.InMoney > src.OutMoney) {
                                re = "<div style='color:green;'>" + value + "</div>";
                            } else if (value == 0) {
                                re = 0;
                            }
                            return re;
                        }
                    },
                    { field: "SettleDateTime", title: "结算时间", width: 120, align: 'center',
                        formatter: function (value) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                            if (dt == "1-01-01 08:00:00")
                                return "-";
                            else
                                return dt;
                        }
                    },
                    { field: "OptId", title: "结算人id", width: 100, align: 'center', hidden: true },
                    { field: "OptName", title: "结算人", width: 100, align: 'center' }
                ]],
        onLoadSuccess: function (data) {
            $($('#t_settle').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton();
            //data = eval('(' + data + ')');
            if (data.total) {
                if (data.loginout) {
                    //$.messager.alert('提示', data.msg);
                    window.location.href = "../../Login.aspx";
                }
            }
            //            var length = data.rows.length;
            //            var zsr = 0;
            //            var zzc = 0;
            //            var zce = 0;
            //            if (length>0) {
            //                for (var i = 0; i < length; i++) {
            //                    zsr += data.rows[i].InMoney;
            //                    zzc += data.rows[i].OutMoney;
            //                    zce += data.rows[i].SumMoney;
            //                }
            //            }
            //            $('#t_settle').datagrid('appendRow', {
            //                BeginTime: '/Date(-62135596800000)/',
            //                EndTime: '/Date(-62135596800000)/',
            //                SettleDateTime: '/Date(-62135596800000)/',
            //                InMoney: zsr,
            //                OutMoney:zzc,
            //                SumMoney:zce,
            //                OptName: '-',
            //                
            //            });
        },
        onDblClickRow: function (index, row) { checkIo(row.BeginTime, row.EndTime); }
    });
});

//双击行事件
function checkIo(begintime, endtime) {
    begintime = eval("new " + begintime.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
    endtime = eval("new " + endtime.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
    $("#dlgsettle").children().children().css("display", "block");
    $("#dlgsettle").dialog("open").dialog("setTitle", "结算结果");
    queryio(begintime, endtime);
    settleIo(begintime, endtime);
}

//查询
function search() {
    var qbegintime = $("#qbegintime").val();
    var qendtime = $("#qendtime").val();
    $("#t_settle").datagrid('load', {
        action: "querysettle",
        qbegintime: qbegintime, qendtime: qendtime
    });
}



//添加结算
function addfunc() {
    $("#dlg").children().children().css("display", "block");
    $("#dlg").dialog("open").dialog("setTitle", "结算信息");
    $("#t_endtime").val((new Date()).Format("yyyy-MM-dd HH:mm:ss "));
    $.ajax({
        url: "/DataService/WebService/InternetGroup/SettleService.ashx",
        type: "post",
        async: false,
        data: { action: "querynewtime" },
        dataType: "html",
        success: function (html) {
            var val = "";
            eval("val=" + html);
            var re = val.rows[0].EndTime;
            var dt = eval("new " + re.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
            if (dt == "1-01-01 08:00:00") {
                $("#t_begintime").val(new Date().getFullYear() + "-01-01 00:00:00");
            } else {
                $("#t_begintime").val(dt);
            }
        }
    });
    
}

//预结算
function advanceSettle() {
    $("#dlgsettle").children().children().css("display", "block");
    $("#dlgsettle").dialog("open").dialog("setTitle", "结算结果");
    var begintime = $("#t_begintime").val();
    var endtime = $("#t_endtime").val();
    queryio(begintime, endtime);
    settleIo(begintime, endtime);
}

//查询收支流水账记录
function queryio(begintime, endtime) {
    $("#t_io").datagrid({
        url: "/DataService/WebService/InternetGroup/IoJournalService.ashx",
        queryParams: { action: "queryio", begintime: begintime, endtime: endtime,querysign:1},
        fit: true,
        pagination: true,
        nowrap: false,
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        striped: true,
        border: false,
        pageList: [10, 20, 30, 50],
        pageSize: 10,
        columns: [[
                    { field: "IoTime", title: "收支时间", width: 150, align: 'center',
                        formatter: function (value) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                            return dt;
                        }
                    },
                    { field: "IoId", title: "收支者id", width: 80, align: 'center', hidden: true },
                    { field: "IoName", title: "收支者名称", width: 80, align: 'center' },
                    { field: "IoMoney", title: "收支金额（元）", width: 100, align: 'center' },
                    { field: "IoTag", title: "收支标识", width: 80, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == 1) {
                                re = "入住收入";
                            } else if (value == 2) {
                                re = "退房支出";
                            }
                            return re;
                        }
                    },
                    { field: "IsUse", title: "作废标识", width: 80, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == 1) {
                                re = "正常";
                            } else if (value == 2) {
                                re = "作废";
                            }
                            return re;
                        }
                    },
                    { field: "IoFrom", title: "收支来源", width: 150, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == 1) {
                                re = "机器自动客户来源";
                            } else if (value == 2) {
                                re = "机器自动收款员来源";
                            } else if (value == 3) {
                                re = "收款员手工维护来源";
                            }
                            return re;
                        }
                    },
                    { field: "OrderId", title: "订单id", width: 100, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == "") {
                                re = "-";
                            } else {
                                re = value;
                            }
                            return re;
                        }
                    },
                    { field: "RoomNo", title: "房间号", width: 100, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == "") {
                                re = "-";
                            } else {
                                re = value;
                            }
                            return re;
                        }
                    }
                ]]
    });

}

//结算收支记录
function settleIo(begintime, endtime) {
    $.ajax({
        url: "/DataService/WebService/InternetGroup/SettleService.ashx",
        type: "post",
        async: false,
        data: { action: "settleio", begintime: begintime, endtime: endtime },
        dataType: "html",
        success: function (html) {
            var val = "";
            eval("val=" + html);
            var length = val.rows.length;
            var zsr = 0, zzc = 0, ce = 0;
            for (var i = 0; i < length; i++) {
                if (val.rows[i].IsUse == "1") {
                    if (val.rows[i].IoTag == "1") {
                        zsr = zsr + val.rows[i].IoMoney;
                    } else if (val.rows[i].IoTag == "2") {
                        zzc = zzc + val.rows[i].IoMoney;
                    }
                }
            }
            if (zsr > zzc) {
                ce = zsr - zzc;
            } else if (zsr < zzc) {
                ce = zzc - zsr;
            }
            $("#t_zsr").val(zsr);
            $("#t_zzc").val(zzc);
            $("#t_ce").val(ce);
        }
    });
    
}

//保存结算记录
var url;
function save() {
    var begintime = $("#t_begintime").val();
    var endtime = $("#t_endtime").val();
    $.ajax({
        url: "/DataService/WebService/InternetGroup/SettleService.ashx",
        type: "post",
        async: false,
        data: { action: "addsettle", begintime: begintime, endtime: endtime },
        dataType: "html",
        success: function (result) {
            result = eval('(' + result + ')');
            if (result.success) {
                $('#dlg').dialog('close');
                $("#t_settle").datagrid("reload");
                advanceSettle();
                $.messager.alert('提示', result.msg);
            } else {
                $.messager.alert("提示", result.msg);
            }
        }
    });
}


//日期时间格式化方法，可以格式化年、月、日、时、分、秒、周
Date.prototype.Format = function (formatStr) {
    var week = ['日', '一', '二', '三', '四', '五', '六'];
    return formatStr.replace(/yyyy|YYYY/, this.getFullYear())
 	             .replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : '0' + (this.getYear() % 100))
 	             .replace(/MM/, (this.getMonth() + 1) > 9 ? (this.getMonth() + 1).toString() : '0' + (this.getMonth() + 1)).replace(/M/g, (this.getMonth() + 1))
 	             .replace(/w|W/g, week[this.getDay()])
 	             .replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate()).replace(/d|D/g, this.getDate())
 	             .replace(/HH|hh/g, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours()).replace(/H|h/g, this.getHours())
 	             .replace(/mm/g, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes()).replace(/m/g, this.getMinutes())
 	             .replace(/ss/g, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds()).replace(/S|s/g, this.getSeconds());
};

