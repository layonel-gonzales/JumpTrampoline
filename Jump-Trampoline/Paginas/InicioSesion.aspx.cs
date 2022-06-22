using Jump_Trampoline.UserControl;
using Jump_Trampoline.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Jump_Trampoline.Utilidades.Constantes;

namespace Jump_Trampoline.Paginas{
    public partial class InicioSesion : System.Web.UI.Page{
        protected void Page_Load(object sender, EventArgs e){
          Page.DataBind();
          if (!string.IsNullOrWhiteSpace(Request.QueryString["cs"])) Mensaje.Generar(this, "Su sesión ha expirado, por favor vuelva a iniciar sesión.");
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e) {
          string email = txtEmail.Text;
          string password = Encrypt.GetSHA256(txtPassword.Text.Trim());

          if (Login(email, password)) {
            Response.Redirect("Home.aspx");
          } else {
            Mensaje.Danger(this, "Correo y/o contraseña incorrecto(s)");
            return;
          }
        }

        private bool Login(string email, string password) {
          try {
            using (var db = new BdModel()) {
              var usuario = (from u in db.Usuario where u.Correo == email && u.Contrasena == password select u).FirstOrDefault();

              if (usuario != null) {
                LoginUsuario login = new LoginUsuario();
                login.correo = usuario.Correo;
                login.usuario = usuario.Nombres + " " + usuario.Apellidos;
                login.fechaAcceso = DateTime.Now;
                login.tipoPerfil = usuario.TipoUsuario;
                db.login.Add(login);
                db.SaveChanges();

                Sesion.RutUsuario = usuario.Rut;
                Sesion.IdUsuario = usuario.IdUsuario;
                Sesion.NombrePerfil = usuario.Nombres + " " + usuario.Apellidos;
                Sesion.TipoPerfil = usuario.TipoUsuario.ToString();
                Sesion.Correo = usuario.Correo;
                return true;
              }
              return false;
            }
          } catch (Exception ex) {
            Mensaje.Danger(this, "Ocurrió un problema la iniciar sesión :", ex);
            return false; 
          }
        }
    }
}