<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication1._default" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="p-4">
        <div class="card">
            <div class="card-header">
                <div class="text-left">
                    <a class="btn btn-primary btn-sm" href="PatientForm.aspx">Add Patient</a>
                </div>
            </div>
            <div class="card-body">
                <asp:ListView runat="server" ID="PatientList" OnItemDataBound="PatientList_ItemDataBound" OnItemCommand="PatientList_ItemCommand">
                    <LayoutTemplate>
                        <table id="data-table" class="table table-sm table-bordered table-striped table-hover no-margins">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Phone</th>
                                    <th>Email</th>
                                    <th>Gender</th>
                                    <th>Notes</th>
                                    <th style="width: 180px;">Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="vertical-align: middle;"><%# Eval("LastName")%>, <%# Eval("FirstName")%></td>
                            <td style="vertical-align: middle;"><%# Eval("Phone")%></td>
                            <td style="vertical-align: middle;"><%# Eval("Email")%></td>
                            <td style="vertical-align: middle;"><%# Eval("Gender")%></td>
                            <td style="vertical-align: middle;"><%# Eval("Notes")%></td>
                            <td class="text-center">
                                <a class="btn btn-link" href="<%# $"PatientForm.aspx?id={Eval("Id")}" %>">Edit</a>
                                <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-link" Text="Delete" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>


</asp:Content>
