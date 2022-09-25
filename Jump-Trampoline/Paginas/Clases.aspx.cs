using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jump_Trampoline.Paginas {
    public partial class Clases : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                TraerClases();
            }

        }

        private void TraerClases() {
            using (var db = new BdModel()) {
                var listaClases = (from c in db.Clase select c).ToList();
                StringBuilder sb = new StringBuilder();

                if (listaClases.Count > 0) {
                    foreach (var clase in listaClases) {
                        sb.Append("<div class='card m-3 d-inline-block' style='width: 18rem;'>");
                        sb.Append("  <div class='card-header bg-info'><strong>" + clase.NombreClase + "</strong></div>");
                        sb.Append("  <div class='card-body'>");
                        sb.Append("    <h6 class='card-text'><strong>Dias :</strong><span>" + clase.Dias + "</span></h6>");
                        sb.Append("    <h6 class='card-text'><strong>Horario :</strong><span>" + clase.HoraInicio + " - " + clase.HoraTermino + "</span></h6>");
                        sb.Append("    <h6 class='card-text'><strong>Precio :</strong><span>$" + clase.Precio + "</span></h6>");
                        sb.Append("    <h6 class='card-text'><strong>Ubicación</strong></h6>");
                        sb.Append("    <div>" + clase.CodigoUbicacion + "</div>");
                        sb.Append("    <div class='text-center'><input type='button' id='btn-" + clase.IdClase.ToString() + "' class='btn btn-primary' value='Asistir' style='width:100%;' /></div>");
                        sb.Append("  </div>");
                        sb.Append("  <div class='card-footer text-muted'>Inscritos " + clase.CupoOcupado + "/" + clase.CantidadAlumnos + "</div>");
                        sb.Append("</div>");
                        contenedorClases.InnerHtml = sb.ToString();
                    }
                }
               
            }
        }
    }
}