$(function () {
    //$("#dlg").parent().bgiframe();
    //    $.extend($.fn.validatebox.defaults.rules, {
    //        nomore: {
    //            validator: function (value, param) {
    //                return parseInt($("#" + param[0]).val()) >= parseInt(value); 
    //            },
    //            message: '导出验证码数量不能大于可用验证码数量！'
    //        }
    //    });

    $("#d_main,#dlg").fadeIn(1000);
    $("#endtime").val((new Date()).Format("yyyy-MM-dd"));
    $("#begintime").val(new Date().getFullYear() + "-01-01");

    var tgs = $("#tgs").val();
    var xmmc = $("#xmmc").val();
    var sfyz = $("#sfyz").val();
    var begintime = $("#begintime").val();
    var endtime = $("#endtime").val();
    $("#t_checkno").datagrid({
        url: "/DataService/WebService/InternetGroup/CheckNoService.ashx",
        queryParams: { action: "querycheckno", tgs: tgs, xmmc: xmmc, sfyz: sfyz, begintime: begintime, endtime: endtime },
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
                    { field: "CheckId", title: "验证码", width: 100, align: 'center' },
                    { field: "CheckID_Front", title: "前缀码", width: 80, align: 'center' },
                    { field: "InternetGroupId", title: "团购商id", width: 10, align: 'center', hidden: true },
                    { field: "InternetGroup", title: "团购商", width: 80, align: 'center' },
                    { field: "CheckIdBeginTime", title: "不可使用开始时间", width: 120, align: 'center',
                        formatter: function (value) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd");
                            if (dt == "1-01-01")
                                return "-";
                            else
                                return dt;
                        }
                    },
                    { field: "CheckIdEndTime", title: "不可使用结束时间", width: 120, align: 'center',
                        formatter: function (value) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd");
                            if (dt == "1-01-01")
                                return "-";
                            else
                                return dt;
                        }
                    },
                    { field: "CreateDateTime", title: "生成日期", width: 150, align: 'center',
                        formatter: function (value) {
                            return eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                        }
                    },
                    { field: "MachineCheck", title: "终端验证", width: 100, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == "1") {
                                re = "<div style='color:red;'>未验证</div>";
                            } else if (value == "2") {
                                re = "<div style='color:green;'>已验证</div>";
                            }
                            return re;
                        }
                    },
                    { field: "MachineCheckDateTime", title: "终端验证日期", width: 100, align: 'center', hidden: true },
                    { field: "MachineCheckPeople", title: "终端验证机器id", width: 100, align: 'center', hidden: true },
                    { field: "InternetCheck", title: "团购验证", width: 100, align: 'center',
                        formatter: function (value) {
                            var re = "";
                            if (value == "1") {
                                re = "<div style='color:red;'>未验证</div>";
                            } else if (value == "2") {
                                re = "<div style='color:green;'>已验证</div>";
                            }
                            return re;
                        }
                    },
                    { field: "InternetCheckDateTime", title: "团购验证日期", width: 100, align: 'center', hidden: true },
                    { field: "InternetCheckPeople", title: "团购验证人", width: 100, align: 'center', hidden: true },
                    { field: "InSumDate", title: "入住天数（天）", width: 100, align: 'center' }
                ]],
        onLoadSuccess: function (data) {
            $($('#t_checkno').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton();
            //data = eval('(' + data + ')');
            if (data.total) {
                if (data.loginout) {
                    //$.messager.alert('提示', data.msg);
                    window.location.href = "../../Login.aspx";
                }
            }
        }
    });
});

//查询
function search() {
    var tgs = $("#tgs").val();
    var xmmc = $("#xmmc").val();
    var sfyz = $("#sfyz").val();
    var begintime = $("#begintime").val();
    var endtime = $("#endtime").val();
    $("#t_checkno").datagrid('load', {
        action: "querycheckno",
        tgs: tgs, xmmc: xmmc, sfyz: sfyz, begintime: begintime, endtime: endtime
    });
}

//单个验证码查询
function searchCheckno() {
    var checkno = $("#checkno").val();
    $("#t_checkno").datagrid('load', {
        action: "querycheckno",
        checkno: checkno
    });
}

//打开导出条件框
function openexpAll() {
    $("#dlgexpall").children().children().css("display", "block");
    $("#dlgexpall").dialog("open").dialog("setTitle", "验证码导出信息编辑");
    $("#tj_tgs,#tj_xmmc,#tj_kyyzmsl,#tj_dcyzmsl").val("");
}

//导出
function expall() {
    var tgs = $("#tj_tgs").val();
    if (tgs == "") {
        $.messager.alert('提示', '请选择团购商！');
        return;
    }
    var xmmc = $("#tj_xmmc").val();
    if (xmmc == "") {
        $.messager.alert('提示', '请选择项目！');
        return;
    }
    var kyyzmsl = $("#tj_kyyzmsl").val();
    var dcyzmsl = $("#tj_dcyzmsl").val();
    if ($("#dcform").form('validate')) {
        if (parseInt(dcyzmsl) > parseInt(kyyzmsl)) {
            $.messager.alert('提示', '导出验证码数量不能大于可用验证码数量！');
            return;
        }
        window.open("/DataService/WebService/InternetGroup/CheckNoService.ashx?action=expAll&tgs=" + tgs
        + "&xmmc=" + xmmc + "&dcyzmsl=" + dcyzmsl);
        $('#dlgexpall').dialog('close');
        $.messager.alert('提示', '成功导出了' + dcyzmsl + '条验证码！');  
    }
}

//团购验证
function checkGroup() {
    var selected = $('#t_checkno').datagrid('getSelected');
    if (!selected) {
        $.messager.alert('提示', '请选中一行数据！');
        return;
    }
//    if (selected.MachineCheck == "1") {
//        $.messager.alert('提示', '终端未验证，团购不能验证！');
//        return;
//    }
    if (selected.InternetCheck == "2") {
        $.messager.alert('提示', '团购已验证！');
        return;
    }
    $.messager.confirm('提示', '<div style="font-size:16px;">请确认验证码：' + selected.CheckId + "</div>",
                function (r) {
                    if (r) {
                        $.ajax({
                            url: "/DataService/WebService/InternetGroup/CheckNoService.ashx?action=checkgroup",
                            type: "post",
                            data: { "checkid": selected.CheckId},
                            datatype: "json",
                            success: function (result) {
                                result = eval('(' + result + ')');
                                if (result.success) {
                                    $("#t_checkno").datagrid("reload");
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
    $("#dlg").dialog("open").dialog("setTitle", "验证码基本信息编辑");
    $("#t_tgs,#t_xmmc,#t_begintime,#t_endtime").val("");
    $("#t_rzts,#t_scsl").val(1);
    url = "/DataService/WebService/InternetGroup/CheckNoService.ashx?action=addcheckno";
}

//保存方法
var url;
function save() {
    var tgs = $("#t_tgs").val();
    if (tgs == "") {
        $.messager.alert('提示', '请选择团购商！');
        return;
    }
    var xmmc = $("#t_xmmc").val();
    if (xmmc == "") {
        $.messager.alert('提示', '请选择项目！');
        return;
    }
    var rzts = $("#t_rzts").val();
    if (rzts >10) {
        $.messager.alert('提示', '入住天数最多为10天！');
        return;
    }
    var scsl = $("#t_scsl").val();
    if (scsl > 1000) {
        $.messager.alert('提示', '生成验证码数量最多为1000个！');
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
                $("#t_checkno").datagrid("reload");
                $.messager.alert('提示', result.msg);
            } else {
                $.messager.alert("提示", result.msg);
            }
        }
    });
}
//团购商改变联动项目方法
function tgschange(id) {
    if (id == "tgs") {
        var tgs = $("#tgs").val();
        if (tgs == "") {
            $("#xmmc").html("<option value='' >—请选择—</option>");
        } else {
            getProject(tgs, id);
        }
    } else if (id == "t_tgs") {
        var tTgs = $("#t_tgs").val();
        if (tTgs == "") {
            $("#t_xmmc").html("<option value='' >—请选择—</option>");
        } else {
            getProject(tTgs, id);
        }
    } else if (id == "tj_tgs") {
        var tjTgs = $("#tj_tgs").val();
        if (tjTgs == "") {
            $("#tj_xmmc").html("<option value='' >—请选择—</option>");
        } else {
            getProject(tjTgs, id);
        }
    }
}

//加载项目
function getProject(tgs,id) {
    $.ajax({
        url: "/DataService/WebService/InternetGroup/InternetGroup.ashx",
        type: "post",
        async: false,
        data: { action: "queryproject", tgs: tgs },
        dataType: "html",
        success: function (html) {
            var val = "";
            eval("val=" + html);
            var length = val.rows.length;
            if (id == "tgs") {
                $("#xmmc").html("<option value='' >—请选择—</option>");
                for (var i = 0; i < length; i++) {
                    $("#xmmc").append("<option value=" + val.rows[i].ProjectFrontNum + ">" + val.rows[i].ProjectName + "</option>");
                }
            } else if (id == "t_tgs") {
                $("#t_xmmc").html("<option value='' >—请选择—</option>");
                for (var j = 0; j < length; j++) {
                    $("#t_xmmc").append("<option value=" + val.rows[j].ProjectFrontNum + ">" + val.rows[j].ProjectName + "</option>");
                }
            } else if (id == "tj_tgs") {
                $("#tj_xmmc").html("<option value='' >—请选择—</option>");
                for (var k = 0; k < length; k++) {
                    $("#tj_xmmc").append("<option value=" + val.rows[k].ProjectFrontNum + ">" + val.rows[k].ProjectName + "</option>");
                }
                
            }
        }
    });
}

function xmmcchange() {
    var tgs = $("#tj_tgs").val();
    var xmmc = $("#tj_xmmc").val();
    GetYzmsl(tgs, xmmc);
}

//获取可用验证码数量
function GetYzmsl(tgs,xmmc) {
    $.ajax({
        url: "/DataService/WebService/InternetGroup/CheckNoService.ashx",
        type: "post",
        async: false,
        data: { action: "getyzmsl", tgs: tgs, xmmc: xmmc },
        dataType: "html",
        success: function (html) {
            var val = "";
            eval("val=" + html);
            $("#tj_kyyzmsl").val(val.rows[0].YesCheckIdNo);
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

