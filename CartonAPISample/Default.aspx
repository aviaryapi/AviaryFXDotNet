<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AviryFX Example</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">	
			
			<div id="leftPanel">
				<div class="pancontainer" data-orient="center" data-canzoom="yes">
					<img id="imageArea" src="images/ostrich.jpg" runat="server" />
				</div>
			</div>
			
			
			
			<div id="rightPanel">
				<div id="uploadForm" runat="server">
				    <asp:FileUpload ID="fileUpload" runat="server" />
				    <asp:Button ID="btnUpload" runat="server" Text="Upload" />
				</div>
	

				<div id="effectOptions" runat="server" visible="false">
				    <asp:DropDownList ID="ddlFilters" runat="server" AutoPostBack="true" />
					<br />
					<b>AviaryFX File Name:</b>
					<br />
					<asp:Label ID="lblAviaryFXFileName" runat="server" />
					<br />
					<b>AviaryFX Thumbs URL:</b>
					<br />
                    <asp:Label ID="lblAviaryFXThumbsURL" runat="server" />					
				</div>
				
                <div style="width:500px" id="thumbs">
                    <asp:Repeater ID="rptThumbs" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkThumbClick" runat="server">
                                <div id='id' class='thumb' style="background-image:url('<% =lblAviaryFXThumbsURL.Text %>');background-position: <%# Convert.ToInt32(Eval("col").ToString()) * thumbWidth * -1 %>px <%# Convert.ToInt32(Eval("Row").ToString()) * thumbHeight * -1 %>px;height:<%=thumbHeight%>px; width:<%=thumbWidth%>px">
                                    <asp:Label ID="lblViewStateKey" runat="server" />
                                    <asp:TextBox ID="txtRenderParameters" runat="server" Visible="false" />
                                </div>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
			</div>
			
			<div class="clearfix"></div>
			
		</div>
    </form>
</body>
</html>
