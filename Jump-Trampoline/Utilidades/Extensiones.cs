using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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

  }
}