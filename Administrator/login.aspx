<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="Administrator_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOGIN SYSTEM</title>
    <link href="Styles/admin_login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="wrapper">
	        <div id="header">
			        <div id="adv"><img src="<%=GetURL()+ "Images/logo.jpg"%>" width="100" alt="adv! Logo"></div>
	        </div>
        </div>
        <div id="ctr" align="center">
		    <div class="login">
		        <div class="login-form"><img src="images/login.gif" alt="Login">
			        <div class="form-block">
                        <asp:Login ID="Login1" runat="server">
                            <LoginButtonStyle CssClass="button" />
                        </asp:Login>
                    </div>
		        </div>
		        <div class="login-text">
			        <div class="ctr"><img src="images/security.png" width="64" height="64" alt="security"></div>
			        <p>Welcome !</p>
			        <p>Use a valid username and password to gain access to the administration console.</p>
		        </div>
    	        <div class="clr">
                </div>
                <div>
                    <asp:Label ID="LblErr" runat="server" Text=""></asp:Label>
                </div>
	        </div>
        </div>
        <div id="break"></div>
        <noscript>
        !Warning! Javascript must be enabled for proper operation of the Administrator
        </noscript>
        <div class="footer" align="center">
	        <div align="center">
		        <div align="center">Copyright © 2015 cuahangvattu.com Service Retail Corporation</div>
                <div align="center">Design By:&nbsp;<a href="http://www.cuahangvattu.com/" target="_blank"><b>cuahangvattu.com</b></a></div>
	        </div>
        </div>
    </div>
    </form>
</body>
</html>
