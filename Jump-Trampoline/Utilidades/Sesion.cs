using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
      HttpContext.Current.Session.Clear();
      HttpContext.Current.Session.Abandon();
      HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
      HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-10);
      HttpContext.Current.Response.Redirect("InicioSesion.aspx" + (mostrarMensaje ? "?cs=true" : ""));
    }

  }
}