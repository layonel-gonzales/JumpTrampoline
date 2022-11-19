using Jump_Trampoline.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jump_Trampoline.Paginas
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    protected void btnArchivarNotificacion_Click(object sender, EventArgs e) {

    }

    protected void btnEliminarNotificacion_Click(object sender, EventArgs e) {

    }

    protected void btnCerrarSesion_Click(object sender, EventArgs e) {
      Sesion.CerrarSesion(false);
    }
  }
}