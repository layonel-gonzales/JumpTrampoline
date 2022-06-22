using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace Jump_Trampoline.Utilidades {
  public class Errores {
    public static void CapturarError(Exception ex, string urlPagina = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null) {
      if (ex != null) {
        // envio de correo avisando de un error en el sistema
        //if (HttpContext.Current.Request.IsLocal) {
        //  Console.WriteLine(ex.ToString());
        //} else {
        //  bool modoAutomatico = urlPagina == "";
        //  if (modoAutomatico) urlPagina = ((Page)HttpContext.Current.CurrentHandler).AppRelativeVirtualPath;

        //  Usuario usuario;
        //  try {
        //    using (var db = new BdModel()) {
        //      usuario = db.Find<Usuario>(Sesion.IdUsuario);
        //    }

        //  } catch (Exception) {
        //    usuario = new Usuario {
        //      Nombres = "Desconocido",
        //      Apellidos = "(probablemente sin sesión iniciada)"
        //    };
        //  }

        //  CorreoTuRendicion correo = new CorreoTuRendicion("noreply@fincloud.cl");
        //  correo.Asunto = "Error en FinCloud - " + ex.GetType().Name;
        //  correo.Contenido = "El usuario <b>" + usuario.NombreCompleto + (Sesion.Caps ? " [CAPS]" : "")
        //    + "</b> se ha topado con una excepción en el sistema en la página <b>" + urlPagina
        //    + (modoAutomatico ? ", línea " + lineNumber + " (" + caller + ")" : "")
        //    + "</b>. A continuación se detalla el error encontrado: <br><br>"
        //    + "<p>" + ex.ToString() + "</p>";
        //  correo.ConEstilo = false;
        //  correo.Enviar(true);
        //}
      }
    }
  }
}