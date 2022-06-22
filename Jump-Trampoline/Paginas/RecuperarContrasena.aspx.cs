using Jump_Trampoline.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jump_Trampoline.Paginas {
  public partial class RecuperarContrasena : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void btnEnviarCorreo_Click(object sender, EventArgs e) {
      using (var db = new BdModel()) {
        string correo = txtEmail.Text.Trim();
        var usuario = (from u in db.Usuario where u.Correo == correo select u).FirstOrDefault();
        if (usuario != null) Mensaje.Success(this, "Se ha neviado un correo a " + correo + " para crear su nueva contraseña.");
        else Mensaje.Success(this, "Correo " + correo + " no esta registrado en nuestro sistema.");
      }
    }
  }
}