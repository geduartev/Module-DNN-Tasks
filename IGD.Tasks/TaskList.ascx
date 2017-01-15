<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskList.ascx.cs" Inherits="IGD.Tasks.TaskList" %>

<asp:DataGrid ID="DataGridTasks" runat="server"
    AutoGenerateColumns="False"
    GridLines="None"
    OnItemCommand="DeleteTask">
    <HeaderStyle CssClass="taskListHeader"/>
    <ItemStyle CssClass="taskListRow"/>
    <Columns>
        <asp:BoundColumn
            DataField="Name"
            HeaderStyle-Width="100px"
            HeaderText="Name"></asp:BoundColumn>
        <asp:BoundColumn
            DataField="Description"
            HeaderStyle-Width="250px"
            HeaderText="Description"></asp:BoundColumn>
        <asp:BoundColumn
            DataField="IsComplete"
            HeaderStyle-Width="125px"
            HeaderText="Is Complete?"></asp:BoundColumn>
        <asp:TemplateColumn>
            <HeaderStyle Width="75px"></HeaderStyle>
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="HyperLinkEdit"
                    NavigateUrl='<%# ModuleContext.EditUrl("TaskId", Eval("TaskId").ToString(), "Edit") %>'
                    Text="Edit"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderStyle Width="75px"></HeaderStyle>
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="LinkButtonDelete"
                    CommandArgument='<%# Eval("TaskId") %>'
                    CommandName="Delete"
                    Text="Delete"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>

<ul class="dnnActions dnnClear">
    <li>
        <asp:HyperLink runat="server" ID="hyperLinkAdd" 
            CssClass="dnnPrimaryAction" 
            Text="Create New Task">
        </asp:HyperLink>
    </li>
</ul>