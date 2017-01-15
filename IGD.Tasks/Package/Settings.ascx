<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="IGD.Tasks.Settings" %>

<div id="dnnTaskSettings" class="dnnForm dnnClear">
    <fieldset>
        <div class="dnnFormItem">
            <div class="dnnLabel">
                <asp:Label runat="server" ID="labelIncludeComplete" Text="Include Complete"></asp:Label>
            </div>
            <asp:CheckBox runat="server" ID="checkBoxIncludeComplete"/>
        </div>
    </fieldset>
</div>