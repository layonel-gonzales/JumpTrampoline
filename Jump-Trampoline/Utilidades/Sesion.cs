using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Jump_Trampoline.Utilidades.Constantes;
using System.Web.Security;

namespace Jump_Trampoline.Utilidades {
  public class Sesion {
    // Variables de sesión del usuario.
    public static string RutUsuario {
      get { return Variable<string>("RutUsuario"); }
      set { HttpContext.Current.Session["RutUsuario"] = value; }
    }
    public static int IdUsuario {
      get { return Variable<int>("IdUsuario"); }
      set { HttpContext.Current.Session["IdUsuario"] = value; }
    }
    public static string NombrePerfil {
      get { return Variable<string>("TipoPerfil"); }
      set { HttpContext.Current.Session["TipoPerfil"] = value; }
    }
    public static string TipoPerfil {
      get { return Variable<string>("TipoPerfil"); }
      set { HttpContext.Current.Session["TipoPerfil"] = value; }
    }
    public static string Correo {
      get { return Variable<string>("Correo"); }
      set { HttpContext.Current.Session["Correo"] = value; }
    }

    public static T Variable<T>(string nombre) {
      try {
        T variable = (T)HttpContext.Current.Session[nombre];
        return variable;

      } catch (Exception) {
        CerrarSesion();
        return default(T);
      }
    }

    public static void CerrarSesion(bool mostrarMensaje = true) {
      Sesion.IdUsuario = 0;
      HttpContext.Current.Session.Clear();
      HttpContext.Current.Session.Abandon();
      HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
      HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-10);
      HttpContext.Current.Response.Redirect("InicioSesion.aspx" + (mostrarMensaje ? "?cs=true" : ""));

    }

        public static bool ValidarSesion() {
            var Session = HttpContext.Current.Session;
            var Request = HttpContext.Current.Request;
            var Response = HttpContext.Current.Response;

            if (Session.IsNewSession) {
                string cookieHeader = Request.Headers["Cookie"];
                if ((null != cookieHeader) && (cookieHeader.IndexOf("ASP.NET_SessionId") >= 0)) {
                    CerrarSesion();
                    return false;
                }
            }

            if (Session.Contents.Count == 0) {
                CerrarSesion();
                return false;
            }

            if (Session["IdUsuario"] == null || Session["IdUsuario"].ToString() == "") {
                CerrarSesion();
                return false;
            }

            return false;
        }
  }
}