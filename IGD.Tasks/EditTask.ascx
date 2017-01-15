<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditTask.ascx.cs" Inherits="IGD.Tasks.EditTask" %>

<div id="dnnUsers" class="dnnForm dnnClear">
    <fieldset>
        <div class="dnnFormItem">
            <div class="dnnLabel">
                <asp:Label runat="server" ID="labelName" Text="Name: "></asp:Label>
            </div>
            <asp:TextBox runat="server" ID="textBoxName" MaxLength="200"></asp:TextBox>
        </div>
        <div class="dnnFormItem">
            <div class="dnnLabel">
                <asp:Label runat="server" ID="labelDescription" Text="Description: "></asp:Label>
            </div>
            <asp:TextBox runat="server" ID="textBoxDescription" MaxLength="200"></asp:TextBox>
        </div>
        <div class="dnnFormItem">
            <div class="dnnLabel">
                <asp:Label runat="server" ID="labelIsComplete" Text="Is Complete: "></asp:Label>
            </div>
            <asp:CheckBox runat="server" ID="checkBoxIsComplete"/>
        </div>
    </fieldset>
</div>

<ul class="dnnActions dnnClear">
    <li>
        <asp:LinkButton runat="server" ID="linkButtonSave"
            CssClass="dnnPrimaryAction"
            OnClick="SaveTask"
            Text="Save Task"></asp:LinkButton>
    </li>
    <li>
        <asp:LinkButton runat="server" ID="linkButtonCancel"
            CssClass="dnnSecondaryAction"
            OnClick="Cancel"
            Text="Cancel"></asp:LinkButton>
    </li>
</ul>