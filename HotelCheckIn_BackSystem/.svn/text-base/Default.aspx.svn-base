<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelCheckIn_BackSystem.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>酒店入住自助终端后台系统</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="Scripts/jquery-easyui-1.3.1/themes/default/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="Scripts/jquery-easyui-1.3.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/jquery-easyui-1.3.1/demo/demo.css" rel="stylesheet" type="text/css" />
    <!--验证session是否过期-->
    <script type="text/javascript" src="DataService/WebService/Common/Common.ashx?action=isoverdue"></script>
    <script src="Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="Scripts/jquery-easyui-1.3.1/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-easyui-1.3.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="Scripts/jquery-easyui-1.3.1/easyui-validate.js" type="text/javascript"></script>
    <script src="Scripts/jquery-easyui-1.3.1/jquery.form.js" type="text/javascript"></script>

    <!--zw加的start-->
    <link href="HumanIdentify/skin/pink.flag/jplayer.pink.flag.css" rel="stylesheet" type="text/css" />
    <script src="HumanIdentify/js/jquery.jplayer.min.js" type="text/javascript"></script>
    <script src="HumanIdentify/zwjs.js" type="text/javascript"></script>
    <script src="HumanIdentify/Timer.js" type="text/javascript"></script>
    <!--end-->
    <!-- Bootstrap core CSS -->
    <link href="Scripts/bootstrap-3.0.2-dist/dist/css/bootstrap.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="Scripts/bootstrap-3.0.2-dist/dist/css/navbar.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/bootstrap-3.0.2-dist/dist/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <!-- Just for debugging purposes. Don't actually copy this line! -->
    <!--[if lt IE 9]><script src="../../docs-assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    
    <script type="text/javascript">
        $(function () {
            var mfHeight = window.screen.availHeight - 200;
            var bro = $.browser;
            var h = 695;
            if (bro.mozilla || (bro.msie && bro.version == "8.0")) {
                mfHeight = mfHeight - 60;
                h = 665;
            }
            mfHeight = mfHeight > h ? mfHeight : h;
            $("#contentFrame").css("height", mfHeight);
            $("body").css("width", window.screen.availWidth - 23);
            $("#form1").css("padding-left", (window.screen.availWidth - 1024 - 5) / 2);
        });
        function sf(url, obj) {
            url = url.substr(0, url.lastIndexOf('?') + 1) + escape(url.substr(url.lastIndexOf('?') + 1, url.length));
            $("#contentFrame").attr("src", url);
            //$(obj).parent().parent().fadeOut(1000);
            return false;
        }

        function logout() {
            location.href = "Login.aspx";
        }

        function messager(title, msg, icon) {
            if (!icon) {
                icon = "info";
            }
            $.messager.alert(title, msg, icon);
            //$(".messager-window").bgIframe();
        }

        function changePwd() {
            $("#contentFrame").attr("src", "views/ChangePassword/ChangePassword.aspx");
        }

        
    </script>
    <style type="text/css">
       .tbTip {
           border: none;
           background: none;
       }
       a.l-btn span.l-btn-left {
           height: inherit;
       }
    </style>
</head>
<body style="width: 1024px; margin: 0 auto;">
    <form id="form1" runat="server">
    <table id="tb1" cellspacing="0" border="0" cellpadding="0" style="width: 1024px;">
        <tr style="background-image: url('images/main_08_jd.gif');">
            <td nowrap="nowrap" style="height: 80px; overflow: hidden; vertical-align: top;">
                <div style="font-size: 12px; float: right; padding-right: 3px; padding-left: 3px;
                    margin-top: 3px; padding-top: 3px; height: 25px; background-color: RGBA(217,2234,249,0.5);">
                    <a href="#" class="aa" onclick="sf('views/Main/MainPage.htm',null)">首页</a><span style="color: #000;
                        margin: 2px;">|</span> <a href="#" onclick="changePwd()" class="aa">修改密码</a><span
                            style="color: #000; margin: 2px;">|</span> <a href="#" onclick="logout()" class="aa">
                                注销</a>&nbsp;&nbsp;&nbsp;&nbsp; <a id="start-Identification" href="#" style="margin-right: 10px;
                                    color: #000;" onclick="start_Identification()">启用人工识别</a>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <!-- Static navbar -->
                <div class="navbar navbar-default" role="navigation">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                                class="icon-bar"></span><span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="#" onclick="sf('views/main/mainpage.htm',null)">首页</a>
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <%--<li><a href="#" onclick="sf('/Views/CheckinOrder/CheckinOrder.htm',null)" >酒店入住查询</a></li>
                            <li><a href="#">机器管理</a></li>
                            <li><a href="#">心跳管理</a></li>
                            <li><a href="#">故障管理</a></li>
                            <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">系统管理
                                <b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li class="dropdown-submenu"><a href="#">部门管理</a></li>
                                    <li class="dropdown-submenu"><a href="#">人员管理</a></li>
                                    <li class="dropdown-submenu"><a href="#">权限管理</a></li>
                                </ul>
                            </li>--%>
                            <%=GetMenuList() %>
                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li><a><asp:TextBox ReadOnly="True" ID="tbTip" CssClass="tbTip" runat="server"></asp:TextBox></a></li>
                        </ul>
                    </div>
                    <!--/.nav-collapse -->
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;" valign="top">
                <iframe id="contentFrame" width="100%" src="views/main/mainpage.htm" frameborder="0"
                    runat="server" scrolling="no" height="100%"></iframe>
            </td>
        </tr>
    </table>
     <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="Scripts/bootstrap-3.0.2-dist/dist/js/bootstrap.min.js"></script>
    </form>
    <!--zw加的start-->
    <div id="jquery_jplayer_1" class="jp-jplayer">
            </div>
            <div id="jp_container_1" class="jp-audio">
                <div class="jp-type-single">
                    <div class="jp-gui jp-interface">
                        <ul class="jp-controls">
                            <li><a href="javascript:;" class="jp-play" tabindex="1">play</a></li>
                            <li><a href="javascript:;" class="jp-pause" tabindex="1">pause</a></li>
                            <li><a href="javascript:;" class="jp-stop" tabindex="1">stop</a></li>
                            <li><a href="javascript:;" class="jp-mute" tabindex="1" title="mute">mute</a></li>
                            <li><a href="javascript:;" class="jp-unmute" tabindex="1" title="unmute">unmute</a></li>
                            <li><a href="javascript:;" class="jp-volume-max" tabindex="1" title="max volume">max
                                volume</a></li>
                        </ul>
                        <div class="jp-progress">
                            <div class="jp-seek-bar">
                                <div class="jp-play-bar">
                                </div>
                            </div>
                        </div>
                        <div class="jp-volume-bar">
                            <div class="jp-volume-bar-value">
                            </div>
                        </div>
                        <div class="jp-current-time">
                        </div>
                        <div class="jp-duration">
                        </div>
                        <ul class="jp-toggles">
                            <li><a href="javascript:;" class="jp-repeat" tabindex="1" title="repeat">repeat</a></li>
                            <li><a href="javascript:;" class="jp-repeat-off" tabindex="1" title="repeat off">repeat
                                off</a></li>
                        </ul>
                    </div>
                    <div class="jp-title">
                        <ul>
                            <li>Cro Magnon Man</li>
                        </ul>
                    </div>
                    <div class="jp-no-solution">
                        <span>Update Required</span> To play the media you will need to either update your
                        browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                            target="_blank">Flash plugin</a>.
                    </div>
                </div>
            </div>
    <div id="dlg-detect" class="easyui-dialog" title="显示" style="width: 856px; height: 660px;overflow: hidden"
        data-options="
				iconCls: 'icon-save',
				buttons: '#dlg-detect-buttons',
                modal:true,
                closed: true,
                closable:false
			">
        <table class="table-detect">
            <tbody>
                <tr>
                    <td style="padding-left: 0px;">
                        身份证号:
                    </td>
                    <td valign="middle" style="padding-left: 10px;">
                        <input id="txt_sfzh" type="text" style="width: 250px;" />
                    </td>
                    <td>
                        姓名:
                    </td>
                    <td>
                        <input id="txt_name" type="text" />
                    </td>
                    <td>
                        性别:
                    </td>
                    <td>
                        <input id="txt_sex" type="text" />
                    </td>
                </tr>
                <tr style="height: 257px;">
                    <td colspan="6">
                        <img id="txt_sfztx" width="250" height="250" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img id="ps1" width="250" height="250" />
                        <img id="ps2" width="250" height="250" />
                        <img id="ps3" width="250" height="250" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="dlg-detect-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="
				iconCls: 'icon-ok'" onclick="btn_passed()">通过</a> 
        <a href="javascript:void(0)" class="easyui-linkbutton"data-options="
				iconCls: 'icon-cancel'" onclick="btn_notpassed1()">与本人不符</a>
                <a href="javascript:void(0)" class="easyui-linkbutton"
                    data-options="
				iconCls: 'icon-cancel'" onclick="btn_notpassed2()">不在范围内</a>
    </div>
    <input id="hidd_id" type="hidden" />
</body>
</html>
