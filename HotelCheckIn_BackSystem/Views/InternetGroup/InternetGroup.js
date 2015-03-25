$(function () {
    //$("#dlg").parent().bgiframe();
    $("#d_main,#dlg").fadeIn(1000);
    initDlg();
    getRoomQuality();
    $("#t_projectgroup").datagrid({
        url: "/DataService/WebService/InternetGroup/InternetGroup.ashx",
        queryParams: { action: "queryproject" },
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
                    { field: "HotelId", title: "酒店", width: 100, align: 'center',
                        formatter: function (value) {
                            if (value == "sjgj") {
                                return "速8酒店";
                            } 
                        }
                     },
                    { field: "ProjectFrontNum", title: "前缀码", width: 60, align: 'center' },
                    { field: "InternetGroupId", title: "团购商id", width: 10, align: 'center', hidden: true },
                    { field: "InternetGroup", title: "团购商", width: 70, align: 'center' },
                    { field: "ProjectName", title: "项目名称", width: 70, align: 'center' },
                    { field: "RoomTypeId", title: "房间类型id", width: 10, align: 'center', hidden: true },
                    { field: "RoomTypeName", title: "房间类型", width: 120, align: 'center' },
                    { field: "Rate", title: "房价(元)", width: 60, align: 'center',
                        formatter: function (value) {
                            if (value == 0 ) {
                                return "-";
                            } else {
                                return value;
                            }
                        }
                     },
                    { field: "RateCode", title: "房价代码", width: 70, align: 'center' ,
                        formatter: function (value) {
                            if (value == null || value == "") {
                                return "-";
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: "CreateDt", title: "创建时间", width: 120, align: "center",
                        formatter: function (value, src) {
                            return eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                        }
                    },
                    { field: "Creater", title: "创建人", width: 60, align: 'center' },
                    { field: "UpdateDt", title: "更新时间", width: 120, align: "center",
                        formatter: function (value, src) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                            if (dt == "1-01-01 08:00:00")
                                return "-";
                            else
                                return dt;
                        }
                    },
                    { field: "Updater", title: "更新人", width: 60, align: 'center',
                        formatter: function (value) {
                            if (value == null || value == "") {
                                return "-";
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: "Remarks", title: "备注", width: 90, align: 'center',
                        formatter: function (value) {
                            if (value == null || value == "") {
                                return "-";
                            } else {
                                return value;
                            }
                        }
                    }
//                    { field: 'action', title: '操作', width: 60, align: 'center',
//                        formatter: function () {
//                            var html = '<a href="javascript:editfunc()" class="easyui-linkbutton" plain="true" title="修改" iconcls="icon-edit"></a>' +
//                                       '<a href="javascript:delfunc()" class="easyui-linkbutton" plain="true" title="删除" iconcls="icon-cancel"></a>';
//                            return html;
//                        }
//                    }
                ]],
        onLoadSuccess: function () {
            $($('#t_projectgroup').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton();
        }
    });
});
//初始化dlg
function initDlg() {
    $('#dlg').dialog({
        title: '项目基本信息编辑',
        modal: true,
        closed: true,
        buttons: "#dlg-buttons"
    });
}

//添加方法
function addfunc() {
    $("#dlg").children().children().css("display", "block");
    $("#dlg").dialog("open");
    $("#qzm").attr("disabled", false);
    $("#tgs").attr("disabled", false);
    $("#tgs").val("");
    $("#hotel").val("");
    $("#qzm,#projecmc,#roomtype,#bz,#rate,#rateCode").val("");
    url = "/DataService/WebService/InternetGroup/InternetGroup.ashx?action=addproject";
}
//修改方法
function editfunc() {
    $("#dlg").children().children().css("display", "block");
    var selected = $('#t_projectgroup').datagrid('getSelected');
    if (!selected) {
        $.messager.alert('提示', '请选中一行数据！');
        return;
    }
    $("#dlg").dialog("open");
    $("#hotel").val(selected.HotelId);
    $("#tgs").val(selected.InternetGroupId);
    $("#tgs").attr("disabled", "disabled");
    $("#htgs").val(selected.InternetGroupId);
    $("#qzm").val(selected.ProjectFrontNum);
    $("#qzm").attr("disabled", "disabled");
    $("#hqzm").val(selected.ProjectFrontNum);
    $("#projecmc").val(selected.ProjectName);
    $("#roomtype").val(selected.RoomTypeId + "#" + selected.RoomTypeName);
    if (selected.Rate == 0) {
        $("#rate").val("");
    } else { $("#rate").val(selected.Rate);}
    
    $("#rateCode").val(selected.RateCode);
    $("#bz").val(selected.Remarks);
    url = "/DataService/WebService/InternetGroup/InternetGroup.ashx?action=editproject";

}
//保存方法
var url;
function save() {
    var hotel = $("#hotel").val();
    if (hotel == "") {
        $.messager.alert('提示', '请选择酒店！');
        return;
    }
    var tgs = $("#tgs").val();
    if (tgs == "") {
        $.messager.alert('提示', '请选择团购商！');
        return;
    }
    var roomtype = $("#roomtype").val();
    if (roomtype == "") {
        $.messager.alert('提示', '请选择房间类型！');
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
                $("#t_projectgroup").datagrid("reload");
                $.messager.alert('提示', result.msg);
            } else {
                $.messager.alert("提示", result.msg);
            }
        }
    });


}
//删除方法
function delfunc() {
    var selected = $('#t_projectgroup').datagrid('getSelected');
    if (!selected) {
        $.messager.alert('提示', '请选中一行数据！');
        return;
    }
    $.messager.confirm('提示', '确定删除?',
                function (r) {
                    if (r) {
                        $.ajax({
                            url: "/DataService/WebService/InternetGroup/InternetGroup.ashx?action=delproject",
                            type: "post",
                            data: { "qzm": selected.ProjectFrontNum, "tgsid": selected.InternetGroupId },
                            datatype: "json",
                            success: function (result) {
                                result = eval('(' + result + ')');
                                if (result.success) {
                                    $('#dlg').dialog('close');
                                    $("#t_projectgroup").datagrid("reload");
                                    $.messager.alert('提示', result.msg);
                                } else {
                                    $.messager.alert("提示", result.msg);
                                }
                            }
                        });
                    }
                });
}

//加载房间属性
function getRoomQuality() {
    $.ajax({
        url: "/DataService/WebService/InternetGroup/InternetGroup.ashx",
        type: "post",
        async: false,
        data: "action=queryroomquality&typeid="+1,
        dataType: "html",
        success: function (html) {
            var val = "";
            eval("val=" + html);
            var length = val.rows.length;
            $("#roomtype").html("<option value='' >—请选择—</option>");
            for (var i = 0; i < length; i++) {
                $("#roomtype").append("<option value=" + val.rows[i].TypeId + "#" + val.rows[i].Name + " >" + val.rows[i].Name + "</option>");
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

