var interval; //定时器
var interval_val = 1000; //整体框架获取数据时间间隔

$(function () {
    initPlayer();
    $("#jp_container_1").css("display", "none");
});

function initPlayer() {
    $("#jquery_jplayer_1").jPlayer({
        ready: function (event) {
            $(this).jPlayer("setMedia", {
                mp3: "../HumanIdentify/myspace.mp3"
            });
        },
        swfPath: "../HumanIdentify/js/Jplayer.swf",
        supplied: "mp3",
        wmode: "window",
        loop: true,
        smoothPlayBar: true,
        keyEnabled: true
    });
}

function getData() {
    zwobj.url = '../DataService/WebService/DetectService.ashx?action=GetFirstData';
    ajaxData();
}
//----------------------------------------------------------------------

//调用主框架的方法
function start_Identification() {
    start_Identification();
}

function modifyA1() {
    $("#start-Identification").html("禁用人工识别");
}
function modifyA2() {
    $("#start-Identification").html("启用人工识别");
}
//------------------------------------------------------------------------
//启用人工识别和禁用人工识别之间切换
function start_Identification() {
    if (interval != undefined || interval != null) {
        window.clearInterval(interval);
        interval = undefined;
        modifyA2();
    } else {
        interval = self.setInterval("getData()", interval_val);
        modifyA1();
    }
}

//--------------------------------------------------------------------------

//通过2
function btn_passed() {
    zwobj.url = '../DataService/WebService/DetectService.ashx?action=Update';
    zwobj.data = { id: $("#hidd_id").val(), sftg: 2 };
    ajaxData();
}

//和本人不符3
function btn_notpassed1() {
    zwobj.url = '../DataService/WebService/DetectService.ashx?action=Update';
    zwobj.data = { id: $("#hidd_id").val(), sftg: 3 };
    ajaxData();
}

//不在范围内4
function btn_notpassed2() {
    zwobj.url = '../DataService/WebService/DetectService.ashx?action=Update';
    zwobj.data = { id: $("#hidd_id").val(), sftg: 4 };
    ajaxData();
}


//--------------------------------------------------------------------------

function ajax_GetFirstData(data) {
    var len = data.Data.length;
    if (len > 0) {
        $("#jquery_jplayer_1").jPlayer("play", 0);
        window.clearInterval(interval);
        $("#hidd_id").val(data.Data[0].Id);
        $("#txt_sfzh").val(data.Data[0].IdCard);
        $("#txt_name").val(data.Data[0].Name);
        $("#txt_sex").val(data.Data[0].Sex);
        $("#txt_sfztx").attr("src", "../DataService/WebService/DetectService.ashx?action=GetPicture&type=IdCardImg&id=" + data.Data[0].Id);
        $("#ps1").attr("src", "../DataService/WebService/DetectService.ashx?action=GetPicture&type=Camera1&id=" + data.Data[0].Id);
        $("#ps2").attr("src", "../DataService/WebService/DetectService.ashx?action=GetPicture&type=Camera2&id=" + data.Data[0].Id);
        $("#ps3").attr("src", "../DataService/WebService/DetectService.ashx?action=GetPicture&type=Camera3&id=" + data.Data[0].Id);
        $('#dlg-detect').dialog('open');
    }
}

function ajax_Update(data) {
    $('#dlg-detect').dialog('close');
    $("#jquery_jplayer_1").jPlayer("stop");
    interval = self.setInterval("getData()", interval_val);
}