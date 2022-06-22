<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarContrasena.aspx.cs" Inherits="Jump_Trampoline.Paginas.RecuperarContrasena" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <title>Jump-Trampoline</title>
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
  </asp:PlaceHolder>

  <!--Responsive-->
  <meta name="viewport" content="width=device-width, inital-scale=1.0" />
</head>

<body class="mt-5 row col-md-12 justify-content-center align-items-center">
  <form id="form2" runat="server">
    <div class="login-form col-auto">

      <div class="text-center mt-5 mb-3">
        <div class="login-logo-fav"></div>
      </div>

      <h3 class="mb-3" runat="server" id="lbTitulo">Recuperar contraseña</h3>
      <p runat="server" id="lbParrafo">Ingrese su correo electrónico asociado a la cuenta. Le enviaremos un correo para crear su nueva contraseña.</p>
      
      <div class="form-group" id="fgCorreo" runat="server">
        <label>Correo de la cuenta:</label>
        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" maxlength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
          ControlToValidate="txtEmail" CssClass="error-input" Display="Dynamic"
          ErrorMessage="Debe ingresar un correo" ValidationGroup="EnviarCorreo" ForeColor="Red" />
      </div>

      <div class="row px-3">
        <a runat="server" id="btnVolver" class="btn btn-secondary col h-50" href="InicioSesion.aspx">Volver</a>
        <div class="col-9 pr-0 pl-2" id="contenedorBtnEnviarCorreo" runat="server">
          <asp:Button runat="server" ID="btnEnviarCorreo" CssClass="btn btn-primary col-12" ValidationGroup="EnviarCorreo"
            OnClick="btnEnviarCorreo_Click" Text="Restablecer contraseña"/>
        </div>
      </div>  
    </div>
  </form>
</body>
</html>

