<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Menu.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="AjustesVideos.aspx.cs" Inherits="Jump_Trampoline.Paginas.AjustesVideos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="titulo" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="extra" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="c" runat="server">

  <script type="text/javascript">

      $(document).ready(function () {
          $('#btnLimpiarBuscar').click(function () {
              $('#<%=txtBuscarVideo.ClientID%>').val("")
              eval($('#<%=btnBuscarVideo.ClientID%>').attr('href'))
              $('#<%=btnBuscarVideo.ClientID%>').click()
              $('#btnLimpiarBuscar').hide()
          });          
      });

      function mostrarOcultar() {
        if($('#<%=txtBuscarVideo.ClientID%>').val().length>0) {
          $('#btnLimpiarBuscar').show()
          $('#btnLimpiarBuscar span').addClass('estilo-limpiar-texto')
        } else {
          $('#btnLimpiarBuscar').hide()
        }
      }

      function cambiarBordeR() {
        $('#btnLimpiarBuscar span').css('border-color','#f46c64');
      }

      function cambiarBordeO() {
        $('#btnLimpiarBuscar span').css('border-color','var(--form-control-border)');
      }

      function validarFormulario(group) {
          console.log("entra");
            $(".botonIngresarVideo").addClass('d-none');
            $(".botonEditarVideo").addClass('d-none');
            if (DoPageClientValidate(group)) {
                if (DoPageClientValidate("Advertencias" + group)) {
                    console.log("true");
                    $(".boton" + group).click();
                } else {
                    console.log("false");
                    $('#modalAdvertencias').modal('show');
                    $(".boton" + group).removeClass('d-none');
                }
            }
      }

      function eliminarAlmacen(idUsuario, nombre) {
           
      }

      function openModal() {
          console.log('entro');
          $("#modalEditarVideo").modal('show');        
      }

      function validarRepeticionCodigo(oSrc, args) {
          if (arrayCodigos) {
              for (var i = 0; i < arrayCodigos.length; i++) {
                  if (arrayCodigos[i] == args.Value) {
                      args.IsValid = false;
                      $('#advCodigo').removeClass('d-none').html(' - El código de video <b>' + args.Value + '</b> ya existe');
                      return;
                  }
              }
              args.IsValid = true;
              $('#advCodigo').addClass('d-none');
          }
      }

      function eliminarVideo(id, nombre) {
          $('#modalEstaSeguro').modal('show');
          $('#<%=hfIdEliminarVideo.ClientID%>').val(id);
          $('#<%=lbNombreVideo.ClientID%>').text(nombre);
      }
  </script>

  <div class="row p-3">
   <div class="col-md-4">
     <h6 class="text-600">Ingresar nuevo Video:</h6>
     <div class="form-row">
       <div class="form-group col-md-6">
         <label>Nombre :</label>
         <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control txtNombre" MaxLength="50"></asp:TextBox>
         <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
           ControlToValidate="txtNombre" CssClass="error-input" Display="Dynamic"
           ErrorMessage="Debe ingresar el nombre del video"
           ValidationGroup="IngresarVideo"
           ForeColor="Red">
         </asp:RequiredFieldValidator>
       </div>
  
       <div class="form-group col-md-12">
         <label>Url :</label>
         <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" Rows="6" style="resize:none;" TextMode="MultiLine"></asp:TextBox>
         <asp:RequiredFieldValidator ID="rfvUrl" runat="server"
           ControlToValidate="txtUrl" CssClass="error-input" Display="Dynamic"
           ErrorMessage="Debe ingresar la URL del video"
           ValidationGroup="IngresarVideo"
           ForeColor="Red">
         </asp:RequiredFieldValidator>
       </div>

       
    </div>
  
    <div class="form-group col-md-12">
       <button type="button" class="btn btn-primary float-right" onclick="validarFormulario('IngresarVideo')">Agregar Video</button>
    </div>
    
    <div class="espacio"></div>
  </div>

  <div class="col-md-8">
    <div class="row no-gutters mb-2">
      <div class="input-group col">
        <asp:TextBox runat="server" ID="txtBuscarVideo" CssClass="form-control"
          placeholder="Buscar por nombre de Video" onfocus="cambiarBordeR()" onfocusout="cambiarBordeO()" 
          onkeyup="mostrarOcultar()" Style="border-right: none; box-shadow: none;">
        </asp:TextBox>
        <a id="btnLimpiarBuscar" class="input-group-append ">
          <span class="input-group-text"><i class="fas fa-times" data-toggle="tooltip" title="Borrar"></i></span>
        </a>
        <asp:LinkButton runat="server" ID="btnBuscarVideo" CssClass="input-group-append"
          OnClick="btnBuscarVideo_Click">
            <span class="input-group-text"><i class="fas fa-search"></i></span>
        </asp:LinkButton>
     </div>
   </div>
  
     <asp:GridView ID="grdVideo" CssClass="table table-hover"
       runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass=""
       OnRowCommand="grdVideo_RowCommand" GridLines="None" OnRowCreated="grdVideo_RowCreated"
       AllowPaging="true" PageSize="10" AllowSorting="true" OnSorting="grdVideo_Sorting"
       PagerStyle-CssClass="page-item" PagerSettings-Mode="NumericFirstLast"
       OnPageIndexChanging="grdVideo_PageIndexChanging">
       <Columns>
         <asp:BoundField HeaderText="Nombre" DataField="Nombre" SortExpression="Nombre" />
         <asp:BoundField HeaderText="Url" DataField="Url" SortExpression="Url" />
         <asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
           <ItemTemplate>
             <div style="width: 80px;" class='<%#Eval("IndEstado").ToString() == "V" ? "" : "d-none" %>'>
               <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("Id")%>' data-toggle="tooltip" title="Editar"><i class="far fa-edit action-icon"></i></asp:LinkButton>
               <i style="padding-left: 11px; padding-right: 11px;" class="fas fa-times action-icon ml-2 cursor-pointer" 
                  onclick="eliminarVideo(<%#Eval("Id")%>,'<%#Eval("Nombre").ToString()%>')" data-toggle="tooltip" title="Eliminar">
               </i>
             </div>
             </div>
           </ItemTemplate>
           <ItemStyle CssClass="text-center" />
         </asp:TemplateField>
       </Columns>
       <EmptyDataTemplate>
         <asp:Label runat="server" Text="No se encontraron Videos" />
       </EmptyDataTemplate>
     </asp:GridView>
   </div>

  <!-- MODAL EDITAR DEPOSITO -->
  <div class="modal fade" id="modalEditarVideo" >
    <div class="modal-dialog modal-md">
      <div class="modal-content">
        <div class="modal-header bg-primary text-white">
          <h5 class="modal-title">Editar video</h5>
          <button type="button" class="close" data-dismiss="modal">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>

        <div class="modal-body">
          <div class="form-row">
            <div class="form-group col-md-6">
              <label>Nombre :</label>
              <asp:TextBox ID="txtEditarNombre" runat="server" CssClass="form-control" ></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvEditarNombre" runat="server"
                ControlToValidate="txtEditarNombre" CssClass="error-input" Display="Dynamic"
                ErrorMessage="Debe ingresar el nombre del video"
                ValidationGroup="EditarVideo"
                ForeColor="Red">
              </asp:RequiredFieldValidator>
            </div>
  
            <div class="form-group col-md-12">
              <label>Url :</label>
              <asp:TextBox ID="txtEditarUrl" runat="server" CssClass="form-control" Rows="4" style="resize:none;" TextMode="MultiLine"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvEditarUrl" runat="server"
                ControlToValidate="txtEditarUrl" CssClass="error-input" Display="Dynamic"
                ErrorMessage="Debe ingresar la URL del video"
                ValidationGroup="EditarVideo"
                ForeColor="Red">
              </asp:RequiredFieldValidator>
            </div>      
          </div>
        </div>

        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
          <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" ValidationGroup="EditarVideo"
            OnClick="btnGuardar_Click" Text="Guardar cambios" />
          <asp:HiddenField runat="server" ID="hfIdEditarVideo" Value="-1" />
        </div>
      </div>
    </div>
  </div>

  <!-- MODAL ¿ESTÁ SEGURO? -->
  <div class="modal fade" tabindex="-1" role="dialog" id="modalEstaSeguro">
    <div class="modal-dialog modal-sm" role="document">
      <div class="modal-content">
        <div class="modal-header bg-danger text-white">
          <h5 class="modal-title">¿Está seguro...</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <p>
            ... de eliminar el video <b runat="server" id="lbNombreVideo">Video</b>?<br />
            no podra volverlo a visualizar.
          </p>
          <asp:HiddenField runat="server" ID="hfIdEliminarVideo" Value="-1" />
        </div>
        <div class="modal-footer">
          <asp:Button runat="server" CssClass="btn btn-danger" Text="Sí, estoy seguro"
            ID="btnEliminarVideo" OnClick="btnEliminarVideo_Click" />
          <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
        </div>
      </div>
    </div>
  </div>

  <!-- MODAL ADVERTENCIAS -->
  <div class="modal fade" tabindex="-1" role="dialog" id="modalAdvertencias">
    <div class="modal-dialog modal-sm" role="document">
      <div class="modal-content">
        <div class="modal-header bg-warning">
          <h5 class="modal-title">Advertencia</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <p>Se encontraron las siguientes advertencias:</p>

          <p>¿Está seguro de ingresar estos valores?</p>
        </div>
        <div class="modal-footer">
          <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-warning botonIngresarVideo"
            Text="Sí, estoy seguro" ValidationGroup="IngresarVideo" OnClick="btnAgregar_Click" />
          <asp:Button ID="btnGuardarCambios" runat="server" ValidationGroup="EditarVideo"
            CssClass="btn btn-warning botonEditarVideo" OnClick="btnGuardarCambios_Click" Text="Sí, estoy seguro" />
          <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
        </div>
      </div>
    </div>
  </div>
  
  
  </div>
</asp:Content>
