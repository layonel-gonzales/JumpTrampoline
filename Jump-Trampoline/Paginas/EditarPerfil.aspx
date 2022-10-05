<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Menu.Master" AutoEventWireup="true" CodeBehind="EditarPerfil.aspx.cs" Inherits="Jump_Trampoline.Paginas.EditarPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTitle" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="titulo" runat="server">Editar - Perfil</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="extra" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="c" runat="server">

    <script type="text/javascript">

      $(document).ready(function () {
          $(".botonConfirmar").attr('disabled', 'disabled');
          $("li").css("list-style", "none");
          $("li").addClass("font-weight-bold");

          // mostrar y ocultar contraseña actual
          $('.show_passwordActual').hover(function show() {
              //Cambiar el atributo a texto
              $('.ContrasenaActual').attr('type', 'text');
              $('.iconActual').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
          },
              function () {
                  //Cambiar el atributo a contraseña
                  $('.ContrasenaActual').attr('type', 'password');
                  $('.iconActual').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
              });

          //mostrar y ocultar nueva contraseña
          $('.show_password').hover(function show() {
              //Cambiar el atributo a texto
              $('.ContrasenaNueva').attr('type', 'text');
              $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
          },
              function () {
                  //Cambiar el atributo a contraseña
                  $('.ContrasenaNueva').attr('type', 'password');
                  $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
              });

          $(".ContrasenaNueva").on("keyup", function () {
              var regExp = [/^(?=.*[A-Z])/, /^(?=.*[.!@#$%&*])/, /^(?=.*[0-9])/, /^(?=.*[a-z])/, /^(?=.{8})/];
              var elementos = [$(".mayuscula"), $(".especial"), $(".numeros"), $(".minuscula"), $(".largo")];
              var pass = $(".ContrasenaNueva").val();
              var check = 0;

              for (var i = 0; i < regExp.length; i++) {
                  if (regExp[i].test(pass)) {
                      elementos[i].removeClass("fal fa-times-circle text-danger");
                      elementos[i].addClass("fal fa-check-circle text-primary");
                      check++;
                  } else {
                      elementos[i].removeClass("fal fa-check-circle text-primary");
                      elementos[i].addClass("fal fa-times-circle text-danger");
                  }
              }

              if (check >= 0 && check <= 2) {
                  $(".Mensaje").text("Inseguro").css("color", "red");
              } else if (check >= 3 && check <= 4) {
                  $(".Mensaje").text("Fuerte").css("color", "orange");
              } else if (check == 5) {
                  $(".Mensaje").text("Seguro").css("color", "green");
                  $(".botonConfirmar").removeAttr('disabled');
              }

              if (check < 5) {
                  $(".botonConfirmar").attr('disabled', 'disabled');
              }

              if (pass.length == 0) {
                  $(".Mensaje").text("");
              }
          });
      });

      $(document).on("click", ".openImageDialog", function () {
          var myQRCode = $(this).data('qrcode');
          myQRCode = "data:image/Png;base64," + myQRCode;
          $(".modal-body #myImage").attr("src",myQRCode);
      });

      function uploadImage() {
        $('#<%=btnFileUpload.ClientID%>').click();
      }       

    </script>
   
    <div class="row">
        <div class="col-lg-12 m-3">
            <div class="nav nav-pills" id="myTab" role="tablist" aria-orientation="horizontal">
                <a class="nav-link active" id="datos-tab" data-toggle="pill" href="#datos" role="tab" aria-controls="datos" aria-selected="true">Datos personales</a>
                <a class="nav-link" id="contrasena-tab" data-toggle="pill" href="#contrasena" role="tab" aria-controls="contrasena" aria-selected="false">Contraseña</a>
            </div>

            <div class="tab-content col-10" id="myTabContent">
                <hr/>
                <div class="tab-pane fade show active p-2" id="datos" role="tabpanel" aria-labelledby="v-pills-home-tab">
                    <div class="row">                       
                        <div class="col-md-3">                           
                            <div class="card bg-light p-3 mb-3">                               
                                <h6>Foto de perfil</h6>
                                <img runat="server" id="imgFotoPerfil" class="foto-perfil mb-2" src="../imagenes/usuarioDefault.png" />
                                <asp:Label runat="server" ID="lbSubirFoto" AssociatedControlID="fileSubirFoto" CssClass="btn btn-secondary"><i class="fas fa-camera mr-1"></i>Subir foto</asp:Label>
                                <asp:FileUpload ID="fileSubirFoto" PersistFile="true" runat="server" CausesValidation="false" onchange="uploadImage()" accept=".png,.jpg,.jpeg,.gif" Style="display: none;" ClientIDMode="Static" />
                                <asp:Button runat="server" ID="btnFileUpload" CssClass="d-none" />
                            </div>
                        </div>
                    
                        <div class="col-md-9">
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Nombres:</label>
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" maxlength="50" />
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" Display="Dynamic"
                                      ControlToValidate="txtNombre" ForeColor="Red" ValidationGroup="ModificarUsuario"
                                      InitialValue="" ErrorMessage="Debe indicar su nombre de usuario" CssClass="error-input" />
                                </div>

                                <div class="form-group col-md-6">
                                  <label>Apellidos:</label>
                                  <asp:TextBox runat="server" ID="txtApellidos" CssClass="form-control" maxlength="50" />
                                  <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" Display="Dynamic"
                                      ControlToValidate="txtApellidos" ForeColor="Red" ValidationGroup="ModificarUsuario"
                                      InitialValue="" ErrorMessage="Debe indicar su apellido de usuario" CssClass="error-input" />
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Edad:</label>
                                    <asp:TextBox runat="server" ID="txtEdad" CssClass="form-control" TextMode="Number" maxlength="3" />
                                    <asp:RequiredFieldValidator ID="rfvEdad" runat="server" Display="Dynamic"
                                      ControlToValidate="txtEdad" ForeColor="Red" ValidationGroup="ModificarUsuario"
                                      InitialValue="" ErrorMessage="Debe indicar la edad" CssClass="error-input" />
                                </div>

                                <div class="form-group col-md-6">
                                    <label>Sexo:</label>
                                    <asp:DropDownList ID="ddlSexo" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSexo" runat="server" Display="Dynamic"
                                      ControlToValidate="ddlSexo" ForeColor="Red" ValidationGroup="ModificarUsuario"
                                      InitialValue="0" ErrorMessage="Debe seleccionar un sexo" CssClass="error-input" />
                                </div>                           
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                  <label>Teléfono:</label>
                                  <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" onkeypress="return isNumber(event)" maxlength="12" />
                                </div>

                                <div class="form-group col-md-6">
                                  <label>Ciudad:</label>
                                  <asp:TextBox runat="server" ID="txtCiudad" CssClass="form-control" maxlength="50" />
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Tipo usuario:</label>
                                    <asp:DropDownList ID="ddlTipoUsuario" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="1">Administrador</asp:ListItem>
                                        <asp:ListItem Value="2">Instructor</asp:ListItem>
                                        <asp:ListItem Value="3">Alumno</asp:ListItem>                                                                                
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipoUsuario" runat="server" Display="Dynamic"
                                      ControlToValidate="ddlTipoUsuario" ForeColor="Red" ValidationGroup="ModificarUsuario"
                                      InitialValue="0" ErrorMessage="Debe seleccionar un tipo de usuario" CssClass="error-input" />
                                </div>

                                <div class="form-group col-md-6">
                                    <label>Correo:</label>
                                    <asp:TextBox runat="server" ID="txtCorreo" CssClass="form-control" TextMode="Email" maxlength="50" />
                                    <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" Display="Dynamic"
                                      ControlToValidate="txtCorreo" ForeColor="Red" ValidationGroup="ModificarUsuario"
                                      InitialValue="" ErrorMessage="Debe indicar un correo" CssClass="error-input" />
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-12">
                                    <label>Dirección:</label>
                                    <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" Display="Dynamic"
                                      ControlToValidate="txtDireccion" ForeColor="Red" ValidationGroup="ModificarUsuario"
                                      InitialValue="" ErrorMessage="Debe indicar una dirección" CssClass="error-input" />
                                </div>
                            </div>
                        
                            <div class="text-right">
                              <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" Text="Guardar cambios" ValidationGroup="ModificarUsuario" />
                            </div>
                        </div>                                                        
                    </div>
                </div>

                <div class="tab-pane fade p-2" id="contrasena" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="col-12">
                        <fieldset>
                        <h5>Cambiar contraseña:</h5>
                        <div>
                            <label>Contraseña actual:</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtContrasenaActual" CssClass="form-control mb-2 ContrasenaActual" TextMode="Password" maxlength="20" />                               
                                <div class="input-group-append" style="display:block;">
                                    <button class="btn btn-primary form-control show_passwordActual" type="button"><i class="fa fa-eye-slash iconActual"></i></button>
                                </div>           
                            </div>  
                            <asp:RequiredFieldValidator ID="rfvContrasenaActual" runat="server" Display="Dynamic"
                               ControlToValidate="txtContrasenaActual" ForeColor="Red" ValidationGroup="ModificarContrasena"
                               InitialValue="" ErrorMessage="Debe indicar su contraseña actual" CssClass="error-input" />
                        </div>

                        <div>
                            <label>Contraseña nueva:</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtContrasenaNueva" CssClass="form-control ContrasenaNueva" TextMode="Password" maxlength="20" />
                                <div class="input-group-append" style="display:block;">
                                  <button class="btn btn-primary form-control show_password" type="button"><i class="fa fa-eye-slash icon"></i></button>
                                </div>           
                            </div>
                            <asp:RequiredFieldValidator ID="rfvContrasenaNueva" runat="server" Display="Dynamic"
                               ControlToValidate="txtContrasenaNueva" ForeColor="Red" ValidationGroup="ModificarContrasena"
                                InitialValue="" ErrorMessage="Debe indicar una nueva contraseña" CssClass="error-input" />
                            <b class="Mensaje"></b>
                        </div>

                        <ul class="mt-3">
                            <li><i class="fas fa-times-circle text-danger mayuscula"></i> Una mayúscula.</li>
                            <li><i class="fas fa-times-circle text-danger especial"></i> Un carácter especial (!@#$.%&*).</li>
                            <li><i class="fas fa-times-circle text-danger numeros"></i> Numero(s).</li>
                            <li><i class="fas fa-times-circle text-danger minuscula"></i> Minúscula(s).</li>
                            <li><i class="fas fa-times-circle text-danger largo"></i> Mínimo 8 caracteres.</li>
                        </ul>

                        <div class="text-right">
                            <asp:Button runat="server" ID="btnCambiarContrasena" CssClass="btn btn-primary botonConfirmar"
                             OnClick="btnCambiarContrasena_Click" Text="Cambiar contraseña" ValidationGroup="ModificarContrasena" />
                        </div>
                        </fieldset>
                    </div>

                    <div class="col-12">
                      <hr />
                      <h5>Código QR para acceder a la aplicación móvil de JumpTrampoline:</h5>
                      <asp:Button runat="server" ID="btnCrearQR" CssClass="btn btn-primary" OnClick="btnCrearQR_Click" Text="Obtener código QR" />
                    </div>

                    <div class="espacio"></div>                   
                </div>
            </div>

            <div class="modal fade" tabindex="-1" role="dialog" id="modalQR">
                        <div class="modal-dialog modal-md" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                  <h5 class="modal-title">Código QR</h5>
                                  <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                </div>

                                <div class="modal-body">
                                  <p>Puede escanear este código QR desde la app móvil JumpTrampoline para iniciar sesión.</p>
                                  <asp:Image ID="imagenQR" runat="server" style="width: 100%; height: 100%;" />
                                </div>

                                <div class="modal-footer"><button type="button" class="btn" data-dismiss="modal">Cerrar</button></div>
                            </div>
                        </div>
                    </div>
        </div>
    </div>
</asp:Content>
