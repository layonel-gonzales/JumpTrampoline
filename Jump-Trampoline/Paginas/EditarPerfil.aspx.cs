using Jump_Trampoline.Utilidades;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing.Imaging;
using Jump_Trampoline.UserControl;

namespace Jump_Trampoline.Paginas {
    public partial class EditarPerfil : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(Sesion.IdUsuario == 0) Response.Redirect("InicioSesion.aspx");
            CargarDatos();
        }

        private void CargarDatos() {
            try {
                using (var db= new BdModel()) {
                    Usuario usuario = db.Usuario.Find(Sesion.IdUsuario);
                    txtNombre.Text = usuario.Nombres;
                    txtApellidos.Text = usuario.Apellidos;
                    txtDireccion.Text = usuario.Direccion;
                    txtEdad.Text = usuario.Edad.ToString();
                    ddlSexo.SelectedValue = usuario.Sexo;
                    ddlTipoUsuario.SelectedValue = usuario.TipoUsuario.ToString();
                    txtCorreo.Text = usuario.Correo;                    
                }
            } catch (Exception) {

                throw;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e) {

        }

        protected void btnCrearQR_Click(object sender, EventArgs e) {
            ScriptManager.RegisterStartupScript(this.btnCrearQR, this.GetType(), Guid.NewGuid().ToString(), "$('#modalQR').modal('show'); ", true);
            using (var db = new BdModel()) {
                //string token = Encrypt.GetSHA256(RandomUtils.GenerarConstrasenaAleatoria(6));
                //TuRendicionDBMaster.CrearNuevoTokenMovil(token, Sesion.IdUsuarioMaster);
                string token = "asad";
                Usuario usuario = db.Usuario.Find(Sesion.IdUsuario);
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(usuario.Correo + ":" + token, QRCodeGenerator.ECCLevel.Q);
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                string img = Server.MapPath("~/imagenes/trampolin.png");
                var imageBase64 = qrCode.GetGraphic(150, System.Drawing.Color.Black, System.Drawing.Color.White, (Bitmap)Bitmap.FromFile(img));
                byte[] bytes = Convert.FromBase64String(imageBase64);
                imagenQR.ImageUrl = "data:image.png;base64, " + Convert.ToBase64String(bytes);
            }
        }

        protected void btnCambiarContrasena_Click(object sender, EventArgs e) {
            try {
                using (var db = new BdModel()) {
                    Usuario usuario = db.Usuario.Find(Sesion.IdUsuario);
                    string pass = Encrypt.GetSHA256(txtContrasenaActual.Text);
                    if (usuario.Contrasena != pass) { Mensaje.Danger(this, "La contraseña actual no es la correcta."); return; }
                    usuario.Contrasena = Encrypt.GetSHA256(txtContrasenaNueva.Text);
                    db.SaveChanges();

                }
            } catch (Exception) {

                throw;
            }
        }
    }
}