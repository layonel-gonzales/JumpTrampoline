<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Menu.Master" AutoEventWireup="true" CodeBehind="Clases.aspx.cs" Inherits="Jump_Trampoline.Paginas.Clases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTitle" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="titulo" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="extra" runat="server">Mantenedor - Clases</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="c" runat="server">

  <script>
      $('#ajustes').collapse();
      $(function () {
          $('#<%=ddlDias.ClientID%>').chosen({ no_results_text: "No se encontró el dia: ", width: "100%", placeholder_text_multiple: "Seleccionar dias" });

        $('#<%=ddlDias.ClientID%>').on('change', function (evt, params) {
            if ('selected' in params) {
                if (params.selected == '-1') $(this).find('option').prop('selected', true);

            } else if ('deselected' in params) {
                if (params.deselected == '-1') $(this).find('option').prop('selected', false);
                else $(this).find('option[value="-1"]').prop('selected', false);
            }

            $(this).trigger('chosen:updated');
        });
    });
  </script>
   <asp:UpdatePanel ID="Updatepanel" runat="server">
       <ContentTemplate>
           <div class="w-100 h-100">
               <div class="filtros mt-2">
                   <h4 data-toggle="collapse" data-target="#filtros">Búsqueda por filtros <i class="fas fa-sort"></i></h4>
                   <div id="filtros" class="collapse">
                     <div class="form-row mt-2">
                       <div class="form-group col-md-2">
                         <label>Comuna :</label>
                         <asp:DropDownList runat="server" ID="ddlComuna" CssClass="form-control form-control-sm" >
                             <asp:ListItem Value="0">Todos</asp:ListItem>
                         </asp:DropDownList>
                       </div>
                       
                       <div class="form-group col-md-2">
                         <label>Tipo Clase :</label>
                         <asp:DropDownList runat="server" ID="ddlTipoClase" CssClass="form-control form-control-sm" >
                           <asp:ListItem Value="0">Todos</asp:ListItem>
                         </asp:DropDownList>
                       </div>

                       <div class="form-group col-md-8">
                         <label>Dias :</label>
                         <asp:ListBox runat="server" ID="ddlDias" SelectionMode="Multiple" />
                       </div>
                     </div>
                   </div>
                </div>

                <div id="contenedorClases" runat="server"></div>
           </div>
       </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
