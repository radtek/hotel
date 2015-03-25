$(function () {
    getRate();

});

function getRate() {
    $.ajax({
        url: "/DataService/WebService/InternetGroup/SettleService.ashx",
        type: "post",
        data: { action: "getrate" },
        async: false,
        dataType: "html",
        success: function (html) {
            if (html.length >= 60) {
                window.location.href = "../../Login.aspx";
            } else {
                var rate = html.split("#");
                $("#roomrate").val(rate[0]);
            
            }
            
        }
    });

}

function editfunc() {
    var rate =$("#roomrate").val();
    var re =/^[+]?[1-9]+\d*$/;
    if (!re.test(rate / 100)) {
        return $("#fm").form("validate");
    }
    $.ajax({
        url: "/DataService/WebService/InternetGroup/SettleService.ashx",
        type: "post",
        data: { action: "editrate", "rate": $("#roomrate").val() },
        datatype: "json",
        success: function (result) {
            result = eval('(' + result + ')');
            if (result.success) {
                getRate();
                $.messager.alert('提示', result.msg);
            } else {
                $.messager.alert("提示", result.msg);
            }
        }
    });

}