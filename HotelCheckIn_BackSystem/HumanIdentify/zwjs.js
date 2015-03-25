//ajax对象
var zwobj = {
    url: "",
    data: {},
    post: "POST",
    get: "GET",
    json: "json",
    html: "html",
    contentype: "application/json; charset=utf-8",
    flag: "",
    formname: ""
};

var formobj = {
    target: '#output2',
    beforeSubmit: showRequest,
    success: showResponse,
    data:{},
    url: '',
    type: 'post',
    dataType: 'json',
    clearForm: true,
    resetForm: true,
    timeout: 3000
};



//数组方法(删除数组元素)
Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) return i;
    }
    return -1;
};
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};

/*
* 获取url参数值 s
*/
function getQueryString(name) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {};
    for (var i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[name.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

/**
* 日期时间格式化方法，
* 可以格式化年、月、日、时、分、秒、周
**/
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

//----------------------------  ajax  ---------------------------------

/**
* *ajax增删改查方法
**/
function ajaxData() {
    $.ajax({
        url: zwobj.url,
        type: zwobj.post,
        async: false,
        data: zwobj.data,
        dataType: zwobj.json,
        success: serviceSuccess,
        error: serviceError
    });
}

/**
* *ajax成功时返回resultObject是json数据
**/
function serviceSuccess(resultObject) {
    if (resultObject == null) {
        return true;
    }
    if (resultObject.IsSuccess) {
        eval(resultObject.JsExecuteMethod + "(resultObject)");
    } else {
        alert(resultObject.Msg);
        location.reload();
    }
    return true;
}

/**
* *ajax失败时返回
**/
function serviceError(result) {
    alert("ajax提交数据出现错误！");
    return false;
}

//------------------------------------ ajax form js -----------------------------------------------

function showRequest(formData, jqForm, options) {
    var queryString = $.param(formData);
    return true;
}

function showResponse(resultObject, statusText, xhr, $form) {
    if (resultObject == null) {
        return true;
    }
    if (resultObject.IsSuccess) {
        eval(resultObject.JsExecuteMethod + "(resultObject)");
    } else {
        alert(resultObject.Msg);
        return false;
    }
    return true;
} 