using Jump_Trampoline.Plantillas;
using Jump_Trampoline.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jump_Trampoline.Utilidades;

namespace Jump_Trampoline.Paginas {
    public partial class AjustesVideos : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                TraerVideos();
            }
        }

        protected void Page_Init(object sender, EventArgs e) {
            grdVideo.PagerTemplate = new PagerTemplate(grdVideo, "primary");
        }

        

        private void TraerVideos(string nombre="") {
            using (var db = new BdModel()) {
                var listaVideos = (from v in db.VideosYoutube
                                   where v.NombreVideo.Contains(nombre)
                                   select new {
                                       Id = v.IdVideo,
                                       Nombre = v.NombreVideo,
                                       Url = v.Url,
                                       IndEstado = v.IndEstado
                                   }).ToList();

                if (listaVideos.Count > 0) {
                    grdVideo.DataSource = listaVideos;
                    grdVideo.DataBind();
                }
                
            }
        }

        protected void btnBuscarVideo_Click(object sender, EventArgs e) {

        }

        protected void grdVideo_RowCommand(object sender, GridViewCommandEventArgs e) {
            using (var db = new BdModel()) {
                int idVideo = Convert.ToInt32(e.CommandArgument);
                VideosYoutube video = db.VideosYoutube.Find(idVideo);

                if (e.CommandName == "Editar") {
                    //hfIdMaterial.Value = idMaterial.ToString();
                    txtNombre.Text = video.NombreVideo;
                    txtUrl.Text=video.Url;

                    //CargarTablaMateriales(txtBuscarMaterial.Text, idMaterial);
                    ScriptManager.RegisterStartupScript(grdVideo, GetType(), Guid.NewGuid().ToString(), "$('#modalEditarVideo').modal('show');", true);
                }
            }
        }

        protected void grdVideo_RowCreated(object sender, GridViewRowEventArgs e) {
            grdVideo.OnSortingRowCreated(e);
        }

        protected void grdVideo_Sorting(object sender, GridViewSortEventArgs e) {
            grdVideo.ApplyChanges(e.SortExpression);
        }

        protected void grdVideo_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            grdVideo.SetStaticPageIndex(e.NewPageIndex);
            grdVideo.ApplyChanges();
        }

        protected void btnAgregar_Click(object sender, EventArgs e) {
            try {
                using (var db = new BdModel()) {
                    string nombre = txtNombre.Text;
                    string url = txtUrl.Text;

                    VideosYoutube videosYoutube = new VideosYoutube();
                    videosYoutube.NombreVideo = nombre;
                    videosYoutube.Url = url;
                    videosYoutube.IndEstado = "V";
                    videosYoutube.FechaCreacion = DateTime.Now;
                    db.VideosYoutube.Add(videosYoutube);
                    db.SaveChanges();
                    txtNombre.Text = "";
                    txtUrl.Text = "";
                    TraerVideos();
                    Mensaje.Success(this, "Video guardado correctamente.");
                }
            } catch (Exception ex) {

                Mensaje.Danger(this, "Ocurrio un problema al guardar los datos. " + ex.ToString());
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e) {
            
            
        }
    }
}