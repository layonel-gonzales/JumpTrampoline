using Jump_Trampoline.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jump_Trampoline.UserControl {
  public partial class Mensaje : System.Web.UI.UserControl {
    public bool MostrarAlInicio = true;
    public bool Scrollable = false;
    public int TiempoAbierto = 5000;
    public string Mensajee = "";
    public string Icon = "";

    public static Mensaje Generar(Page page, string mensaje, int tiempoAbierto = 5000) {
      ScriptManager scriptManager = ScriptManager.GetCurrent(page);
      Mensaje msj;

      //https://stackoverrun.com/es/q/1167574
      //https://stackoverflow.com/questions/8184331/how-to-check-whether-updatepanel-is-posting-back
      //if(scriptManager.IsInAsyncPostBack) {
      //} else {
      //  snackbar = (Snackbar)page.LoadControl("~/Controls/Snackbar.ascx");
      //}

      msj = (Mensaje)page.LoadControl("../UserControl/Mensaje.ascx");
      msj.Mensajee = mensaje;
      msj.TiempoAbierto = tiempoAbierto;

      try {
        page.Controls.Add(msj);

      } catch (Exception ex) {
        Errores.CapturarError(ex);
      }

      return msj;
    }

    public static Mensaje Danger(Page page, string mensaje, Exception ex) {
      if (HttpContext.Current.Request.IsLocal) return Danger(page, mensaje + ":\n" + ex?.ToString(), true, true);
      return Danger(page, mensaje);
    }
    public static Mensaje Danger(Page page, string mensaje, bool duracionLarga = false, bool scrollable = false) {
      Mensaje msj = Generar(page, mensaje, duracionLarga ? -1 : 5000);
      msj.Icon = "<i class='fas fa-lg fa-exclamation-circle text-danger mr-3'></i>";
      msj.Scrollable = scrollable;
      return msj;
    }

    public static Mensaje Success(Page page, string mensaje) {
      Mensaje msj = Generar(page, mensaje);
      msj.Icon = "<i class='fas fa-lg fa-check text-success mr-3'></i>";
      return msj;
    }

  }
}