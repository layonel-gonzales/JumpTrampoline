using Jump_Trampoline.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jump_Trampoline.Paginas {
  public partial class Home : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                MostrarVideos();
            }

    }

    private void MostrarVideos() {
            using (var db = new BdModel()) {
                var listaVideos = (from v in db.VideosYoutube select v).ToList();

                foreach (var item in listaVideos) {
                    item.Url.Replace("width=\"560\" height=\"315\"", "width=\"360\" height=\"215\"");
                }
            }
    }
  }
}