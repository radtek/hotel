$(function () {

});

function btn_roomData() {
    $.ajax({
        url: '/DataService/WebService/PmsService/PmsService.ashx?action=AddRoom',
        type: "POST",
        async: true,
        dataType: "json",
        success: function (data) {
            if (data) {
                alert("获取成功");
            } else {
                alert("获取失败");
            }
        },
        error: function (data) {

        }
    });
}

function btn_roomQuality() {
    $.ajax({
        url: '/DataService/WebService/PmsService/PmsService.ashx?action=AddRoomQuality',
        type: "POST",
        async: true,
        dataType: "json",
        success: function (data) {
            if (data) {
                alert("获取成功");
            } else {
                alert("获取失败");
            }
        },
        error: function (data) {

        }
    });
}