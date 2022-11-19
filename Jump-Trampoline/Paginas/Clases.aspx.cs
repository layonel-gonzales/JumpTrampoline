using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jump_Trampoline.Utilidades;

namespace Jump_Trampoline.Paginas {
    public partial class Clases : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                TraerClases();
                LlenarFiltros();
            }

        }

    private void LlenarFiltros()
    {
      using (var db = new BdModel())
      {
        var tipoClases = (from t in db.TipoClase select t).ToList();
        ddlTipoClase.DataSource = tipoClases;
        ddlTipoClase.DataTextField = "NombreTipoClase";
        ddlTipoClase.DataValueField = "IdTipoClase";
        ddlTipoClase.DataBind();

        ddlDias.DataSource = ConsultasStaticas.TraerDias().ToList();
        ddlDias.DataValueField = "Id";
        ddlDias.DataTextField = "NombreDia";
        ddlDias.DataBind();
        ddlDias.Items.Insert(0, new ListItem("Todos", "0"));
      }
    }

    private void TraerClases() {
            using (var db = new BdModel()) {
                var listaClases = (from c in db.Clase select c).ToList();
                StringBuilder sb = new StringBuilder();

                if (listaClases.Count > 0) {
                    foreach (var clase in listaClases) {
                        sb.Append("<div class='card d-inline-block' style='margin: 15px 15px 15px 0px; width: 18rem; box-shadow: 0px 10px 10px 0px;'>");
                        sb.Append("  <div class='card-header' style='background-color: #0d6efd; color: white;'><strong>" + clase.NombreClase + "</strong></div>");
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