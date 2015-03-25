$(function () {
    //$("#dlg").parent().bgiframe();
    $("#d_main,#dlg").fadeIn(1000);
    $("#endtime").val((new Date()).Format("yyyy-MM-dd"));
    $("#begintime").val(new Date().getFullYear() + "-01-01");
    var begintime = $("#begintime").val();
    var endtime = $("#endtime").val();
    $("#t_iojournal").datagrid({
        url: "/DataService/WebService/InternetGroup/IoJournalService.ashx",
        queryParams: { action: "queryio", begintime: begintime, endtime: endtime, querysign: 0 },
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
                    { field: "IoTime", title: "收支时间", width: 150, align: 'center',
                        formatter: function (value) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                            return dt;
                        }
                    },
                    { field: "IoId", title: "收支者id", width: 100, align: 'center', hidden: true },
                    { field: "IoName", title: "收支者名称", width: 100, align: 'center' },
                    { field: "IoMoney", title: "收支金额（元）", width: 80, align: 'center' },
                    { field: "IoTag", title: "收支标识", width: 80, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == 1) {
                                re = "<div style='color:green;'>收入</div>";
                            } else if (value == 2) {
                                re = "<div style='color:red;'>支出</div>";
                            }
                            return re;
                        }
                    },
                    { field: "IsUse", title: "作废标识", width: 80, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == 1) {
                                re = "<div style='color:green;'>正常</div>";
                            } else if (value == 2) {
                                re = "<div style='color:red;'>作废</div>";
                            }
                            return re;
                        }
                    },
                    { field: "IoFrom", title: "收支来源", width: 130, align: 'center',
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
                    { field: "OrderId", title: "订单id", width: 120, align: 'center',
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
                    { field: "RoomNo", title: "房间号", width: 80, align: 'center',
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
                    { field: "InOrOutCard", title: "是否发卡", width: 100, align: 'center',
                        formatter: function (value, rows) {
                            var re = "-";
                            if (rows.IoFrom == 1 && rows.IoTag == 1) {
                                if (value == 1) {
                                    re = "<div style='color:red;'>未发卡</div>";
                                } else if (value == 2) {
                                    re = "<div style='color:green;'>已发卡</div>";
                                }
                            }
                            //                            else if (rows.IoFrom == 1 && rows.IoTag == 2) {
                            //                                if (value == 1) {
                            //                                    re = "<div style='color:red;'>未出钞</div>";
                            //                                } else if (value == 2) {
                            //                                    re = "<div style='color:green;'>已出钞</div>";
                            //                                }
                            //                            } 
                            return re;
                        }
                    }
                ]],
        onLoadSuccess: function (data) {
            $($('#t_iojournal').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton();
            //data = eval('(' + data + ')');
            if (data.total) {
                if (data.loginout) {
                    //$.messager.alert('提示', data.msg);
                    window.location.href = "../../Login.aspx";
                }
            }
        },
        rowStyler: function (index, row) {
            if (row.IsUse == 2) {
                return 'color:red;';
            }
        }
    });
});

//查询
function search() {
    var begintime = $("#begintime").val();
    var endtime = $("#endtime").val();
    $("#t_iojournal").datagrid('load', {
        action: "queryio",
        begintime: begintime,
        endtime: endtime, 
        querysign:"0"
    });
}


//作废操作
function delfunc() {
    var selected = $('#t_iojournal').datagrid('getSelected');
    if (!selected) {
        $.messager.alert('提示', '请选中一行数据！');
        return;
    }
    if (selected.IoFrom!=3) {
        $.messager.alert('提示', '请选择收款员手工维护的数据！');
        return;
    }
    var dt = eval("new " + selected.IoTime.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
    $.messager.confirm('提示', '确认作废？',
                function (r) {
                    if (r) {
                        $.ajax({
                            url: "/DataService/WebService/InternetGroup/IoJournalService.ashx?action=delio",
                            type: "post",
                            data: { "iotime": dt, "ioid": selected.IoId, "ioname": selected.IoName },
                            datatype: "json",
                            success: function (result) {
                                result = eval('(' + result + ')');
                                if (result.success) {
                                    $("#t_iojournal").datagrid("reload");
                                    $.messager.alert('提示', result.msg);
                                } else {
                                    $.messager.alert("提示", result.msg);
                                }
                            }
                        });
                    }
                });
}

//添加方法
function addfunc() {
    $("#dlg").children().children().css("display", "block");
    $("#dlg").dialog("open").dialog("setTitle", "收支流水账编辑");
    $("#t_szje,#t_szbz,#t_szly").val("");
    $("#t_rzts,#t_scsl").val(1);
    url = "/DataService/WebService/InternetGroup/IoJournalService.ashx?action=addio";
}

//保存方法
var url;
function save() {
    var szbz = $("#t_szbz").val();
    if (szbz == "") {
        $.messager.alert('提示', '请选择收支标识！');
        return;
    }
    var szly = $("#t_szly").val();
    if (szly == "") {
        $.messager.alert('提示', '请选择收支来源！');
        return;
    }
    $('#fm').form('submit', {
        url: url,
        onSubmit: function () {
            return $(this).form('validate');
        },
        success: function (result) {
            result = eval('(' + result + ')');
            if (result.success) {
                $('#dlg').dialog('close');
                $("#t_iojournal").datagrid("reload");
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

