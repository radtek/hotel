<%@ Page Language="C#" MasterPageFile="~/NormalPage.Master" AutoEventWireup="true"
    CodeBehind="TopFrame.aspx.cs" Inherits="HotelCheckIn_BackSystem.TopFrame" Title="无标题页" %>

<asp:Content ID="contenthead" ContentPlaceHolderID="ContextStyle" runat="server">
    <script src="scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function edit() {
            parent.editPassword();
        }

        //调用主框架的方法
        function start_Identification() {
            parent.start_Identification();
        }

        function modifyA1() {
            $("#start-Identification").html("禁用人工识别");
        }
        function modifyA2() {
            $("#start-Identification").html("启用人工识别");
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(images/Page_Title.bmp);
        width: 100%; height: 100%; border: 0; border-spacing: 0; overflow: hidden;">
        <tr style="width: 100%; height: 90%;">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="right">
            <td>
                欢迎您：<%=GetUserName()%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="#"
                    onclick="edit()" style="color: #000">
                    <img src="images/key_yellow.png" alt="修改密码" style="vertical-align: middle; width: 12px;
                        height: 12px;" />&nbsp;修改密码</a> <a href="login.aspx?newlogin=1" style="color: #000">
                            <img src="images/lock_unlock.png" alt="注销" style="vertical-align: middle; width: 12px;
                                height: 12px;" />&nbsp;注销</a> &nbsp;&nbsp;&nbsp;&nbsp; <a id="start-Identification"
                                    href="#" style="margin-right: 10px; color: #000;" onclick="start_Identification()">
                                    启用人工识别</a>
            </td>
        </tr>
    </table>
</asp:Content>
