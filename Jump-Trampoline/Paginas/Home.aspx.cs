using Jump_Trampoline.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Jump_Trampoline.Utilidades.Constantes;

namespace Jump_Trampoline.Paginas {
  public partial class Home : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
            Sesion.ValidarSesion();
            if (!IsPostBack) {
                MostrarVideos();
            }

    }

    private void MostrarVideos() {
            using (var db = new BdModel()) {
                var listaVideos = (from v in db.VideosYoutube select v).ToList();
                StringBuilder sb = new StringBuilder();
                foreach (var item in listaVideos) {
                    item.Url.Replace("560", "360");
                    sb.Append("<div class='m-2 d-inline-block'>");
                    sb.Append(item.Url);
                    sb.Append("</div>");
                }
                contVideo.InnerHtml= sb.ToString();


            }
        }
  }
}