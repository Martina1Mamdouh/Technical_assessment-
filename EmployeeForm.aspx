<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeForm.aspx.cs" Inherits="technical_assessment.EmployeeForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="lstMonthly" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="lstHourly" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="lstFreeLancer" runat="server"></asp:DropDownList>
        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        <div>
            <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Employee Name" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="BirthDate" HeaderText="Birth Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="Graduation" HeaderText="Graduation" />
                    <asp:BoundField DataField="EmploymentType" HeaderText="Employment Type" />
                    <asp:BoundField DataField="Salary" HeaderText="Salary" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>

</html>
