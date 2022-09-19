<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Menu.Master" AutoEventWireup="true" CodeBehind="Clases.aspx.cs" Inherits="Jump_Trampoline.Paginas.Clases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTitle" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="titulo" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="extra" runat="server">Mantenedor - Clases</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="c" runat="server">
   <asp:UpdatePanel ID="Updatepanel" runat="server">
       <ContentTemplate>
           <div class="p-3 w-100 h-100">
               <div class="filtros">
                   <h4 id="filtrosToggle" aria-expanded="true">Filtrar Clases</h4>
                   <div id="filtros" class="collapse show">
                     <div class="form-row mt-2">
                       <div class="form-group col-md-12">
                         <label>Comuna :</label>
                         <asp:DropDownList runat="server" ID="ddlComuna" CssClass="form-control form-control-sm" />
                         <asp:RequiredFieldValidator ID="rfvComuna" runat="server"
                             ControlToValidate="ddlComuna" CssClass="error-input" Display="Dynamic"
                             ErrorMessage="Debe seleccionar una comuna" InitialValue="-1"
                             ValidationGroup="Filtro" ForeColor="Red" />
                       </div>                   
                     </div>
                   </div>
                </div>
                <div id="contenedorClases">
                    
                </div>
           </div>
       </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
