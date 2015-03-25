$(function () {
    $("#d_main").fadeIn(1000);
    $("#endtime").val((new Date()).Format("yyyy-MM-dd"));
    $("#begintime").val(new Date().getFullYear() + "-01-01");
    var begintime = $("#begintime").val();
    var endtime = $("#endtime").val();
    $("#t_roomlock").datagrid({
        url: "/DataService/WebService/RoomLock/RoomLockService.ashx",
        queryParams: { action: "queryroomlock",begintime: begintime, endtime: endtime },
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
                    { field: "HotelId", title: "酒店", width: 100, align: 'center' },
                    { field: "MacId", title: "机器", width: 100, align: 'center' },
                    { field: "RoomNum", title: "房间号", width: 100, align: 'center' },
                    { field: "CheckId", title: "验证码", width: 100, align: 'center' },
                    { field: "LockTime", title: "锁定时间", width: 120, align: 'center',
                        formatter: function (value) {
                            var dt = eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm:ss");
                            if (dt == "1-01-01 08:00:00")
                                return "";
                            else
                                return dt;
                        }
                    }
                ]],
        onLoadSuccess: function (data) {
            $($('#t_roomlock').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton();
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
    var begintime = $("#begintime").val();
    var endtime = $("#endtime").val();
    $("#t_roomlock").datagrid('load', {
        action: "queryroomlock",
        begintime: begintime, endtime: endtime
    });
}

//解锁方法
function unlock() {
    var selected = $('#t_roomlock').datagrid('getSelected');
    if (!selected) {
        $.messager.alert('提示','请选中一行数据！');
        return;
    }
    $.ajax({
        url: "/DataService/WebService/RoomLock/RoomLockService.ashx",
        type: "post",
        async: false,
        data: { action: "unlock", checkid: selected.CheckId },
        dataType: "html",
        success: function (result) {
            result = eval('(' + result + ')');
            if (result.success) {
                $("#t_roomlock").datagrid("reload");
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

