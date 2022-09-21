using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using Jump_Trampoline.UserControl;
using OfficeOpenXml;
using System.Data.Common;
using System.Data.Entity;
using System.Drawing;
using System.Dynamic;


namespace Jump_Trampoline.Utilidades {
  public static class Extensiones {

    public static string SafeToString(this object o, string defaultText = "") {
      if (o == null) return defaultText;
      else return o.ToString();
    }

    public static T SafeParse<T>(this object o, T defaultValue) {
      try {
        T value = (T)((IConvertible)o).ToType(typeof(T), null);
        return value;

      } catch (Exception) {
        return defaultValue;
      }
    }

    public static HtmlControl AddTooltip(this HtmlControl control, string title, string placement = "top") {
      control.Attributes["data-toggle"] = "tooltip";
      control.Attributes["data-placement"] = placement;
      control.Attributes["title"] = title;
      return control;
    }
    public static HtmlControl RemoveTooltip(this HtmlControl control) {
      control.Attributes.Remove("data-toggle");
      control.Attributes.Remove("data-placement");
      control.Attributes.Remove("title");
      return control;
    }
    public static WebControl AddTooltip(this WebControl control, string title, string placement = "top") {
      control.Attributes["data-toggle"] = "tooltip";
      control.Attributes["data-placement"] = placement;
      control.Attributes["title"] = title;
      return control;
    }
    public static WebControl RemoveTooltip(this WebControl control) {
      control.Attributes.Remove("data-toggle");
      control.Attributes.Remove("data-placement");
      control.Attributes.Remove("title");
      return control;
    }

    public static WebControl AddClass(this WebControl control, string cssClass) {
        if (!Regex.IsMatch(control.Attributes["class"].DefaultIfEmpty(""), @"\b" + cssClass + "\b"))
            control.CssClass = control.CssClass += " " + cssClass;
        return control;
    }

    public static HtmlControl AddClass(this HtmlControl control, string cssClass) {
        if (!Regex.IsMatch(control.Attributes["class"].DefaultIfEmpty(""), @"\b" + cssClass + "\b"))
            control.Attributes["class"] = control.Attributes["class"] += " " + cssClass;
        return control;
    }

    public static Control FindControlRecursive(this Control root, string id) {
      if (root.ID == id) return root;
      foreach (Control c in root.Controls) {
          Control t = FindControlRecursive(c, id);
          if (t != null) return t;
      }
      return null;
    }

    public static string DefaultIfEmpty(this string texto, string porDefecto) {
        if (texto == null || texto.Trim() == "") return porDefecto;
        else return texto;
    }

  }
}