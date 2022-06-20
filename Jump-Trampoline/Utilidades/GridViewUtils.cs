using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Jump_Trampoline.Utilidades {
  public static class GridViewUtils {
    private static string GetPageID(this GridView grd) {
      return grd.Page.ToString() + "_" + grd.ID;
    }

    // Para PageIndexChanging
    public static void SetStaticPageIndex(this GridView grd, int newPageIndex) {
      HttpContext.Current.Session["PageIndex" + grd.GetPageID()] = newPageIndex;
    }

    // Para CargarTabla
    // Guardar una tabla completa es bastante tonto, pues ocupa mucha memoria... lo ideal sería poder guardar un IQueryable, o un CompiledQuery.
    public static void SetStaticDataTable(this GridView grd, DataTable newDataTable) {
      string currentTables = HttpContext.Current.Session["CurrentSavedTables"].SafeToString();
      if (!currentTables.Contains(grd.GetPageID())) HttpContext.Current.Session["CurrentSavedTables"] = currentTables + ";" + grd.GetPageID();

      HttpContext.Current.Session["DataTable" + grd.GetPageID()] = newDataTable;
    }

    // Para PageIndexChanging y OnSorting
    public static void ApplyChanges(this GridView grd, string sortExpression = "") {
      DataTable dt = HttpContext.Current.Session["DataTable" + grd.GetPageID()] as DataTable;
      if (dt != null) {
        grd.PageIndex = HttpContext.Current.Session["PageIndex" + grd.GetPageID()].SafeParse(0);
        if (!string.IsNullOrEmpty(sortExpression)) dt.DefaultView.Sort = sortExpression + " " + grd.GetSortDirection(sortExpression);

        grd.DataSource = dt;
        grd.DataBind();
      }
    }

    public static DataTable GetStaticDataTable(this GridView grd, bool reference = false) {
      return (HttpContext.Current.Session["DataTable" + grd.GetPageID()] as DataTable)?.Copy();
    }

    public static string GetSortDirection(this GridView grd, string column) {
      string sortDirection = "ASC";
      string sortExpression = HttpContext.Current.Session["SortExpression" + grd.GetPageID()] as string;

      if (sortExpression != null) {
        if (sortExpression == column) {
          string lastDirection = HttpContext.Current.Session["SortDirection" + grd.GetPageID()] as string;
          if ((lastDirection != null) && (lastDirection == "ASC")) {
            sortDirection = "DESC";
          }
        }
      }
      HttpContext.Current.Session["SortDirection" + grd.GetPageID()] = sortDirection;
      HttpContext.Current.Session["SortExpression" + grd.GetPageID()] = column;

      return sortDirection;
    }

    public static void FlushAllTables() {
      string[] tablesToDelete = HttpContext.Current.Session["CurrentSavedTables"].SafeToString().Trim(';').Split(';');
      foreach (string grdID in tablesToDelete) {
        HttpContext.Current.Session.Remove("DataTable" + grdID);
        HttpContext.Current.Session.Remove("PageIndex" + grdID);
        HttpContext.Current.Session.Remove("SortDirection" + grdID);
        HttpContext.Current.Session.Remove("SortExpression" + grdID);
      }
    }

    public static void OnSortingRowCreated(this GridView grd, GridViewRowEventArgs e, bool textNoWrap = true) {
      if (e.Row.RowType == DataControlRowType.Header) {
        foreach (TableCell tc in e.Row.Cells) {
          if (tc.HasControls()) {
            LinkButton lnk = (LinkButton)tc.Controls[0];
            string iconoCss = "";

            if (lnk != null && HttpContext.Current.Session["SortExpression" + grd.GetPageID()].SafeToString() == lnk.CommandArgument)
              iconoCss = HttpContext.Current.Session["SortDirection" + grd.GetPageID()].SafeToString() == "ASC" ? "mr-1 fas fa-arrow-up" : "mr-1 fas fa-arrow-down";
            else iconoCss = ""; //far fa-sort

            if (textNoWrap) lnk.CssClass = "text-nowrap";

            if (lnk.Text.Contains("{")) {
              lnk.AddTooltip(lnk.Text.Replace("{", "").Replace("}", ""));
              lnk.Text = "<i class='" + iconoCss + "'></i>" + lnk.Text.Substring(0, lnk.Text.IndexOf('{')) + "…";

            } else {
              lnk.Text = "<i class='" + iconoCss + "'></i>" + lnk.Text;
            }

          }
        }
      }
    }

    public static void OnRowSpanDataBound(this GridView grd, params int[] affectedColumns) {
      for (int rowIndex = grd.Rows.Count - 2; rowIndex >= 0; rowIndex--) {

        GridViewRow row = grd.Rows[rowIndex];
        GridViewRow previousRow = grd.Rows[rowIndex + 1];

        for (int i = 0; i < row.Cells.Count; i++) {
          if ((affectedColumns.Length == 0 || affectedColumns.Contains(i)) && row.Cells[i].Text == previousRow.Cells[i].Text) {

            if (previousRow.Cells[i].RowSpan < 2) {
              row.Cells[i].RowSpan = 2;
            } else {
              row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan + 1;
            }
            previousRow.Cells[i].Visible = false;
          }
        }
      }
    }

    public static void RemoveAllRowClasses(this GridView grd) {
      foreach (GridViewRow row in grd.Rows) {
        if (row != null) row.CssClass = "";
      }
    }

    public static DataTable ToDataTable<T>(this IEnumerable<T> varlist) {
      DataTable dtReturn = new DataTable();

      // column names 
      PropertyInfo[] oProps = null;

      if (varlist == null) return dtReturn;

      foreach (T rec in varlist) {
        // Use reflection to get property names, to create table, Only first time, others will follow 
        if (oProps == null) {
          oProps = ((Type)rec.GetType()).GetProperties();
          foreach (PropertyInfo pi in oProps) {
            Type colType = pi.PropertyType;

            if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>))) {
              colType = colType.GetGenericArguments()[0];
            }

            DataColumn dc = new DataColumn(pi.Name, colType);
            dtReturn.Columns.Add(dc);
          }
        }

        DataRow dr = dtReturn.NewRow();

        foreach (PropertyInfo pi in oProps) {
          dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
          (rec, null);
        }

        dtReturn.Rows.Add(dr);
      }
      return dtReturn;
    }

    // Variante para transformar dynamics (ExpandoObjects).
    public static DataTable ToDataTable(this IEnumerable<dynamic> items) {
      var data = items.ToArray();
      if (data.Count() == 0) return null;

      var dt = new DataTable();
      foreach (var pair in ((IDictionary<string, object>)data[0])) {
        dt.Columns.Add(pair.Key/*, pair.Value.GetType()*/); // TODO: REPARAR. No funciona si el elemento retornado es null.
      }
      foreach (var d in data) {
        dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
      }
      return dt;
    }
  }
}