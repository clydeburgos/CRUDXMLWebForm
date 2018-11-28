<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientForm.aspx.cs" Inherits="WebApplication1.PatientForm" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-6 offset-3">
                <div class="form-group">
                    <label>First Name</label>
                    <input runat="server" id="txtFirstName" type="text" class="form-control" placeholder="Enter First Name" required>
                </div>

                <div class="form-group">
                    <label>Last Name</label>
                    <input runat="server" id="txtLastName" type="text" class="form-control" placeholder="Enter Last Name" required>
                </div>

                <div class="form-group">
                    <label>Phone Number</label>
                    <input runat="server" id="txtPhone" type="text" class="form-control" placeholder="Enter Phone Number">
                </div>

                <div class="form-group">
                    <label>Email</label>
                    <input runat="server" id="txtEmail" type="email" class="form-control" placeholder="Enter Email" required>
                </div>

                <div class="form-group">
                    <label>Gender</label>
                    <select runat="server" id="slcGender" class="form-control">
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                    </select>
                </div>

                <div class="form-group">
                    <label>Notes</label>
                    <textarea runat="server" id="txtNotes" class="form-control" placeholder="Enter Notes" rows="4"></textarea>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-6 offset-3 text-right">
                <a class="btn btn-light" href="default.aspx">Cancel</a>
                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
