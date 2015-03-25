$(function () {
    
    $("#d_main,#dlg").fadeIn(1000);
    
    //获取服务器时间
    $.ajax({
        url: "/DataService/WebService/Checkinorder/CheckinOrder.ashx",
        type: "post",
        data: { action: "gettime" },
        async: false,
        dataType: "html",
        success: function (html) {
            var dateArr = html.split("#");
            $("#t_datebegin").val(dateArr[0]);
            $("#t_dateend").val(dateArr[1]);
        }
    });
    
    $.ajax({
        url: "/DataService/WebService/Checkinorder/CheckinOrder.ashx",
        type: "post",
        data: { action: "getmachine" },
        async: false,
        dataType: "html",
        success: function (html) {
            var val = null;
            eval("val=" + html);
            var length = val.rows.length;
            for (var i = 0; i < length; i++) {
                $("#s_machine").append("<option value=" + val.rows[i].id + " >" + val.rows[i].name + "</option>");
            }
        }
    });
    
    $("#t_checkinorder").datagrid({
        url: "/DataService/WebService/Checkinorder/CheckinOrder.ashx",
        queryParams: {
            action: "getdata",
            macid: $("#s_machine").val(),
            datebegin: $("#t_datebegin").val(),
            dateend: $("#t_dateend").val(),
            notdownload: $("#ck_notdownload").attr("checked") ? 0 : 1
        },
        async: false,
        fit: true,
        singleSelect: true,
        columns: [[
                    { field: "OrderId", title: "订单ID", width: 80, align: "left" },
					{ field: "RoomNum", title: "房间号", width: 80, align: "left" },
					{ field: "RoomType", title: "房间类型", width: 80, align: "left" },
					{ field: "Building", title: "楼栋", width: 50, align: "left" },
					{ field: "RoomCode", title: "房价代码", width: 70, align: "left" },
					{ field: "RoomRate", title: "房价", width: 60, align: "left" },
                    { field: "CheckinType", title: "入住类型", width: 80, align: "left" },
					{ field: "ViPcardNum", title: "会员卡号", width: 130, align: "left" },
					{ field: "ViPcardType", title: "会员卡类型", width: 80, align: "left" },
                    { field: "PeopleNum", title: "入住人数", width: 80, align: "left" },
					{ field: "CheckinTime", title: "入住日期", width: 110, align: "left",
					    formatter: function (value, src) {
					        return eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm");
					    }
					},
					{ field: "Hours", title: "钟点数", width: 50, align: "left" },
					{ field: "AdvancePayment", title: "预付金额", width: 80, align: "left" },
					{ field: "AdvanceType", title: "预付方式", width: 80, align: "left" },
					{ field: "CheckinImage", title: "入住签名", width: 80, align: "left",
					    formatter: function (value, src) {
					        return "<a href='javascript:void(0)' onclick='showCheckinImg(\"" + src.Orderid + "\")'>查看</a>";
					    }
					},
                    { field: "MacName", title: "终端名称", width: 80, align: "left" },
					{ field: "OrderTime", title: "处理时间", width: 100, align: "left",
					    formatter: function (value, src) {
					        return eval("new " + value.split('/')[1]).Format("yyyy-MM-dd HH:mm");
					    }
					}
                ]],
        onLoadSuccess: function (data) {
            var total = data.total;
            var pager = $('#t_checkinorder').datagrid('getPager');
            if (total > pager.pagination.defaults.pageList[0]) {
                pager.show();
            }
        },
        view: detailview,
        detailFormatter: function (index, row) {
            return "<div style='padding:2px;'><table id='ddv-" + index + "'></table></div>";
        },
        onExpandRow: function (index, row) {
            $("#ddv-" + index).datagrid({
                url: "/DataService/WebService/Checkinorder/CheckinOrder.ashx?action=getcustomer&orderid=" + row.OrderId,
                fitColums: true,
                singleSelect: true,
                rownumbers: true,
                loadMsg: "加载中...",
                height: "auto",
                columns: [[
                            { field: "Name", title: "姓名", width: 70 },
                            { field: "Sex", title: "性别", width: 50 },
                            { field: "IdentityCardNum", title: "身份证号", width: 200 },
                            { field: "CheckIDcard", title: "身份证照/摄像头照对比", width: 200,
                                formatter: function (value, src) {
                                    var ret = "";
                                    if (value == 0) {
                                        ret = "<a href='javascript:void(0);' disabled='disabled'>没有验证</a>";
                                    } else {
                                        ret = "<a href='javascript:void(0);' onclick='showImg(\"" + src.ID + "\")'>查看</a>";
                                    }
                                    return ret;
                                }
                            },
                            { field: "ExportCount", title: "是否已下载", width: 100,
                                formatter: function (value, src) {
                                    if (value == 0) {
                                        return "否";
                                    } else {
                                        return "是";
                                    }
                                }
                            }
                        ]],
                onLoadSuccess: function () {
                    setTimeout(function () {
                        $("#t_checkinorder").datagrid("fixDetailRowHeight", index);
                    });
                },
                onResize: function () {
                    $("#t_checkinorder").datagrid("fixDetailRowHeight", index);
                }
            });
        }
    });
});

//查询
function query() {
    $("#t_checkinorder").datagrid('load', {
        action: "getdata",
        macid: $("#s_machine").val(),
        datebegin: $("#t_datebegin").val(),
        dateend: $("#t_dateend").val(),
        notdownload: $("#ck_notdownload").attr("checked") ? 0 : 1
    });
}

//仅导出身份证照片
function expIDCardOnly() {
    var macid = $("#s_machine").val(), dateBegin = $("#t_datebegin").val(), dateEnd = $("#t_dateend").val();
    var notdownload = $("#ck_notdownload").attr("checked") ? 0 : 1;
    window.open("/DataService/WebService/Checkinorder/CheckinOrder.ashx?action=expIDCardOnly&macid=" + macid
                    + "&datebegin=" + dateBegin + "&dateend=" + dateEnd + "&notdownload=" + notdownload);
}

//导出所有
function expAll() {
    var macid = $("#s_machine").val(), dateBegin = $("#t_datebegin").val(), dateEnd = $("#t_dateend").val();
    var notdownload = $("#ck_notdownload").attr("checked") ? 0 : 1;
    window.open("/DataService/WebService/Checkinorder/CheckinOrder.ashx?action=expAll&macid=" + macid
                    + "&datebegin=" + dateBegin + "&dateend=" + dateEnd + "&notdownload=" + notdownload);
}

//显示身份证和摄像头照片的对比
function showImg(id) {
    $("#img_id").parent().parent().css("display", "block");
    $("#img_id").attr("src", "/DataService/WebService/Checkinorder/CheckinOrder.ashx?action=getCustomerIDimg&id=" + id);
    $("#img_camera").attr("src", "/DataService/WebService/Checkinorder/CheckinOrder.ashx?action=getCustomerCaneraimg&id=" + id);
    $("#dlg").dialog("open").dialog("setTitle", "入住身份证/摄像头照片对比");
}

//显示入住签名
function showCheckinImg(orderid) {
    $("#img_checkinimg").parent().parent().css("display", "block");
    $("#img_checkinimg").attr("src", "/DataService/WebService/Checkinorder/CheckinOrder.ashx?action=getcheckinimage&orderid=" + orderid);
    $("#dlg_checkin").dialog("open").dialog("setTitle", "入住签名图片");
}


//日期时间格式化方法，可以格式化年、月、日、时、分、秒、周
Date.prototype.Format = function (formatStr) {
    var Week = ['日', '一', '二', '三', '四', '五', '六'];
    return formatStr.replace(/yyyy|YYYY/, this.getFullYear())
 	             .replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : '0' + (this.getYear() % 100))
 	             .replace(/MM/, (this.getMonth() + 1) > 9 ? (this.getMonth() + 1).toString() : '0' + (this.getMonth() + 1)).replace(/M/g, (this.getMonth() + 1))
 	             .replace(/w|W/g, Week[this.getDay()])
 	             .replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate()).replace(/d|D/g, this.getDate())
 	             .replace(/HH|hh/g, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours()).replace(/H|h/g, this.getHours())
 	             .replace(/mm/g, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes()).replace(/m/g, this.getMinutes())
 	             .replace(/ss/g, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds()).replace(/S|s/g, this.getSeconds());
};