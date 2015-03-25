<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HotelCheckIn_PlatformSystem.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>酒店平台系统</title>
    <script src="Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var h = $(window).height() > 600 ? $(window).height() : 600;
            $("#tm").css("margin-top", (h - 288) / 2).css("margin-bottom", (h - 288) / 2);
            $("body").height(h);
            $(window).resize(function () {
                h = $(window).height() > 600 ? $(window).height() : 600;
                $("#tm").css("margin-top", (h - 288) / 2).css("margin-bottom", (h - 288) / 2);
                $("body").height(h);
            });
            $("#txt_user").focus();
        });

        if (window != window.top) {
            window.top.location.href = window.location.href;
        }

        function cancel() {
            $("#txt_user").val("");
            $("#txt_password").val("");
            return false;
        }

        function login() {
            var name = $("#txt_user").val();
            var pwd = $("#txt_password").val();
            if (name.length == 0) {
                alert("用户名不能为空。");
                $("#txt_user").focus();
                return false;
            }
            if (pwd.length == 0) {
                alert("密码不能为空。");
                $("#txt_password").focus();
                return false;
            }
            $.ajax({
                url: "DataService/WebService/ChangePassword/PasswordService.ashx",
                type: "POST",
                data: { action: "login", name: name, pwd: pwd, r: Math.random() },
                success: function (data) {
                    if (data == 1) {
                        location.href = "Default.aspx";
                    } else {
                        alert("用户名或密码错误！");
                        $("#txt_password").val("").focus();
                    }
                },
                error: function (x, y, z) {
                    alert(x);
                }
            });
            return false;
        }
    </script>
    <style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	overflow:hidden;
}
.STYLE3 {color: #528311; font-size: 12px; }
.STYLE4 {
	color: #42870a;
	font-size: 12px;
}
-->
</style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td bgcolor="#e5f6cf">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="608" background="images/login_03.gif">
                <table width="862" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="266" background="images/login_04_pt.gif">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="95">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="424" height="95" background="images/login_06.gif">
                                        &nbsp;
                                    </td>
                                    <td width="183" background="images/login_07.gif">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="21%" height="30">
                                                    <div align="center">
                                                        <span class="STYLE3">用户:</span></div>
                                                </td>
                                                <td width="79%" height="30">
                                                    <asp:TextBox ID="txt_user" runat="server" Width="155px" Style="background-color: transparent;
                                                        line-height: 18px; border: #777 1px solid; height: 18px; width: 130px;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30">
                                                    <div align="center">
                                                        <span class="STYLE3">密码:</span></div>
                                                </td>
                                                <td height="30">
                                                    <asp:TextBox ID="txt_password" runat="server" TextMode="password" Width="155px" Style="background-color: transparent;
                                                        line-height: 18px; border: #777 1px solid; height: 18px; width: 130px;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30">
                                                    &nbsp;
                                                </td>
                                                <td height="30" align="center">
                                                    <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="Images/btn_ok.png" 
                                                        OnClientClick="return login()" Height="25px" Width="64px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="255" background="images/login_08.gif">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="247" valign="top" background="images/login_09.gif">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="22%" height="30">
                                        &nbsp;
                                    </td>
                                    <td width="56%">
                                        &nbsp;
                                    </td>
                                    <td width="22%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td height="30">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="44%" height="20">
                                                    &nbsp;
                                                </td>
                                                <td width="56%" class="STYLE4">
                                                    版本 2013V1.0
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td bgcolor="#a2d962">
                &nbsp;
            </td>
        </tr>
    </table>
    <map name="Map">
        <area shape="rect" coords="3,3,36,19" href="#">
        <area shape="rect" coords="40,3,78,18" href="#">
    </map>
    </form>
</body>
</html>
