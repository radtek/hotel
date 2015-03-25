<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelCheckIn_PlatformSystem.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>酒店平台系统</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="Content/Style.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/jquery-easyui-1.3.1/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Scripts/jquery-easyui-1.3.1/themes/icon.css" />
    <!-- Bootstrap core CSS -->
    <link href="Scripts/bootstrap-3.0.2-dist/dist/css/bootstrap.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="Scripts/bootstrap-3.0.2-dist/dist/css/navbar.css" rel="stylesheet" />
    <link href="Scripts/bootstrap-3.0.2-dist/dist/css/bootstrap-theme.css" rel="stylesheet"
        type="text/css" />
    <!-- Just for debugging purposes. Don't actually copy this line! -->
    <!--[if lt IE 9]><script src="../../docs-assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <!--验证session是否过期-->
    <script type="text/javascript" src="DataService/WebService/Common/Common.ashx?action=isoverdue"></script>
    <script src="Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="Scripts/jquery-easyui-1.3.1/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-easyui-1.3.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var mfHeight = window.screen.availHeight - 215;
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
            text-align: right;
        }
    </style>
</head>
<body style="width: 1024px; margin: 0 auto; padding: 0;">
    <form id="form1" runat="server">
    <table id="tb1" cellspacing="0" border="0" cellpadding="0" style="width: 1024px;">
        <tr style="background-image: url('images/main_08_pt.gif');">
            <td nowrap="nowrap" style="height: 80px; overflow: hidden; vertical-align: top;">
                <div style="font-size: 12px; float: right; padding-right: 3px; padding-left: 3px; margin-top: 3px;
                    padding-top: 3px; height: 25px; background-color: RGBA(217,2234,249,0.5);">
                    <a href="#" class="aa" onclick="sf('views/main/mainpage.htm',null)">
                        <img src="Images/homepage.gif" style="margin: 0px 1px; border: 0; height: 14px; width: 14px;margin-top: -2px;" alt="首页" />首页</a>
                            <span style="color: #000; margin: 2px;">|</span>
                            <a href="#" onclick="changePwd()" class="aa">修改密码</a>
                            <span style="color: #000; margin: 2px;">|</span>
                            <a href="#" onclick="logout()" class="aa">注销</a>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <!-- Static navbar -->
                <div class="navbar navbar-default" role="navigation">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#" onclick="sf('views/main/mainpage.htm',null)">首页</a>
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <%--<li><a href="#">素材管理</a></li>
                            <li><a href="#">机器管理</a></li>
                            <li><a href="#">心跳管理</a></li>
                            <li><a href="#">酒店管理</a></li>
                            <li><a href="#">故障管理</a></li>
                            <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">系统管理
                                <b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li><a href="javascript:void(0);" onclick="sf('Views/Account/departmentManage.htm',null)">
                                        部门管理</a></li>
                                    <li><a href="javascript:void(0);" onclick="sf('Views/Account/employerManage.htm',null)">
                                        人员管理</a></li>
                                    <li><a href="javascript:void(0);" onclick="sf('Views/Role/roleManage.htm',null)">
                                        权限管理</a></li>
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
    <script src="Scripts/bootstrap-3.0.2-dist/dist/js/bootstrap.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
