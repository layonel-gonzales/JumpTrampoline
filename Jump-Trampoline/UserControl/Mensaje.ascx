<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mensaje.ascx.cs" Inherits="Jump_Trampoline.UserControl.Mensaje" %>

<script>
  function cerrarMensaje(){
    $('.snackbar-wrap').removeClass('show');
    setTimeout(function(){ $('.snackbar-wrap').addClass('d-none'); }, 300);
  }

  function mostrarMensaje(){
    $('.snackbar-wrap').addClass('show');
    <% if (TiempoAbierto > 0) {%>
      setTimeout(function(){
        cerrarMensaje();
      }, <%=TiempoAbierto%>);
    <% } %>
  }

  <% if(MostrarAlInicio) { %>
    $(function(){
      mostrarMensaje();
    });
  <% } %>
</script>

<div class="snackbar-wrap <%=Scrollable ? "scrollable " : "" %>dark-mode-bordered">
	<div class="snackbar-icon"><%=Icon%></div>
	<div class="snackbar-content"><%=Mensajee%></div>
	<div class="snackbar-action d-none"> <!-- TODO: Implementar acciones a futuro. -->
		<a href="#">Reintentar</a>
	</div>
	<div class="snackbar-action snackbar-close <%=TiempoAbierto <= 0 || TiempoAbierto >= 5000 ? "" : "d-none"%>">
    <a onclick="cerrarMensaje();"><i class="fas fa-times" onclick=""></i></a>
	</div>
</div>