<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="FourInARowGameSite.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="visual.css" type="text/css" />
</head>
<body>
    <asp:Label runat="server" ID="WinText" />
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="VictoryScreen" CssClass="">
            <asp:Label runat="server" ID="VictoryText"/>
            <br />
            <asp:Button runat="server" ID="ReButton" OnClick="ReButton_Click" Text="Play Again"/>
        </asp:Panel>
        <div class="Center">
            <asp:Panel runat="server" CssClass="GameBoard" ID="GameBoard"/>
            <div class="Settings">
                <div>
                    <asp:Label runat="server" AssociatedControlID="XSIZE" Text="Rows Amount:"/>
                    <asp:TextBox runat="server" Text="8" TextMode="Number" ID="XSIZE" />
                </div>
                <br />
                <div>
                    <asp:Label runat="server" AssociatedControlID="YSIZE" Text="Columns Amount:"/>
                    <asp:TextBox runat="server" Text="8" TextMode="Number" ID="YSIZE" />
                </div>
                <br />
                <div>
                    <asp:Label runat="server" AssociatedControlID="WINLENGTH" Text="Length To Win:" />
                    <asp:TextBox runat="server" TextMode="Number" Text="4" ID="WINLENGTH" />
                </div>
                <br />
                <div>
                    <asp:Button runat="server" ID="CreateButton" Text="Create" OnClick="Create_Board_Click" UseSubmitBehavior="false" />
                </div>
                
            </div>

        </div>
    </form>
</body>
</html>
