<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="Jump_Trampoline.Paginas.InicioSesion" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="utf-8" />
  <title>Jump-Trampoline</title>
  <!--Logo Jump_Trampoline-->
  <link rel="icon" href="#" type="image/png" />

  <asp:PlaceHolder runat="server">
    <style>
      html{
        --primary-normal: #f16f62;
        --primary-normal-500: rgba(241, 111, 98, 0.5);
        --primary-normal-075: rgba(241, 111, 98, 0.075);
        --primary-normal-rich: #ef5a4b;
        --primary-normal-richer: #ef5a4b;
        --primary-normal-richest: #ed4433;
        --primary-light: #fad3cf;
        --primary-lighter: #fce2df;
        --primary-light-rich: #f9c5c0;
        --primary-dark: #783731;
        --primary-darker: #48211d;
      }

      .badges{
	      display: flex;
	      align-items: center;
        justify-content: center;
      }
      .play-badge img{
	      width: 163px;
      }
      .app-badge img{
        margin-left: 12px;
	      width: 128px;
	      height: auto;
      }
    </style>
    <script src="https://kit.fontawesome.com/b0df700715.js" crossorigin="anonymous"></script>
    <link href="../css/fontawesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/themes.css?v='v1.0'" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-themed.css?v='v1.0'" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-tour.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link href="../css/estilo.css?v='v1.0'" rel="stylesheet" type="text/css" />
    <link href="../css/all.min.css" rel="stylesheet" type="text/css" />
    <!--JQuery-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../js/chosen.jquery.js?v='v1.0'" type="text/javascript"></script>
    <!--Popper.js-->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <!--Bootstrap JS-->
    <script src="../js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-tour.js" type="text/javascript"></script>
    
    <script src="../js/utils.js" type="text/javascript"></script>
    <script src="../js/js.cookie.min.js" type="text/javascript"></script>
    <!--reCaptcha-->
    <script src="https://www.google.com/recaptcha/api.js"></script>

    <script>
      window.dataLayer=window.dataLayer||[];
      function gtag() { dataLayer.push(arguments); }
      gtag('js',new Date());
      gtag('config', 'UA-136609660-1');   

      function onSubmit(token) {
        $('#<%=btnIniciarSesion.ClientID%>').click();
      }

        
    </script>
  </asp:PlaceHolder>

  <!--Responsive-->
  <meta name="viewport" content="width=device-width, inital-scale=1.0" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="page-login">
      <a href="#"><img src="#" class="logo" /></a>
      <div class="login-box text-center">
        <div class="tab-content">
          <div class="tab-pane fade show active" id="iniciarSesion">
            <img src="#" class="logo" />
            <h2>¡Bienvenido!</h2>
            <p class="mt-2">Inicia sesión para comenzar</p>

            <div class="form-group col-12 text-left">
              <label>E-mail:</label>
              <div class="input-group">
                <div class="input-group-prepend">
                  <span class="input-group-text"><i class="fa fa-user fa-fw"></i></span>
                </div>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
              </div>
              <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                ControlToValidate="txtEmail" CssClass="error-input" Display="Dynamic"
                ErrorMessage="Debe ingresar un correo" ValidationGroup="IngresarUsuario" ForeColor="Red" />
            </div>

            <div class="form-group col-12 text-left no-gutters">
              <label>Contraseña:</label>
              <div class="input-group">
                <div class="input-group-prepend">
                  <span class="input-group-text"><i class="fa fa-lock fa-fw"></i></span>
                </div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
              </div>
              <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                ControlToValidate="txtPassword" CssClass="error-input" Display="Dynamic"
                ErrorMessage="Debe ingresar una contraseña" ValidationGroup="IngresarUsuario" ForeColor="Red" />
              <small class="col-12 text-right d-block mt-2"><a href="ReestablecerContrasena.aspx">¿Olvidó su contraseña?</a></small>
            </div>
            <div class="form-group col-12">
              <button class="btn btn-primary text-uppercase button g-recaptcha" data-sitekey="6LfKaAMbAAAAAB-4V3vr281zT6ISGC1dx3QS5G9M" data-callback='onSubmit' data-action='submit'>Iniciar sesión</button>
              <asp:Button ID="btnIniciarSesion" runat="server" ValidationGroup="IngresarUsuario" CssClass="d-none"
                 Text="Iniciar sesión" OnClick="btnIniciarSesion_Click" />
            </div>

            <hr class="my-5" />
            <p class="col-12">
              ¿Aún no tienes la app?
            </p>
            <div class="badges">
              <a class="app-badge" href="#"><img src="#" alt="Download on the App Store" /></a>
		      <a class="play-badge" href='#'><img alt='Disponible en Google Play' src='#'/></a>
            </div>

          </div>
        </div>
      </div>

      <div class="app-version">
        <a href="#" data-toggle="modal" data-target=""><i class="fa fa-info-circle mr-2"></i>v1.0</a>
      </div>

      <!-- Modal -->
      <div class="modal fade" id="modalVersion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">Jump Trampoline | Actualizaciones</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <b class="d-block">v1.0</b>
              <span class="d-block text-muted">20 de Junio, 2020</span>
              <span class="my-3 badge badge-success">Nuevo</span>
              <ul>
                <li>Se actualizó la interfaz de inicio de sesión</li>
                <li>Se mejoró la usabilidad en la pantalla de registro</li>
                <li>Mejoras en la velocidad de carga de las páginas en general</li>
              </ul>
            </div>
          </div>
        </div>
      </div>

    </div>

  </form>
</body>
</html>

