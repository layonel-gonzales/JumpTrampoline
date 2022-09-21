using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using Jump_Trampoline.Utilidades;
using System.Data;

namespace Jump_Trampoline.Plantillas {
    public class PagerTemplate : ITemplate {
        public GridView AssociatedGridView { get; private set; }
        private string PagerStyle = "primary";
        private string OnClientClick = "";

        public PagerTemplate(GridView associatedGridView, string pagerStyle = "primary", string onClientClick = "") {
            AssociatedGridView = associatedGridView;
            PagerStyle = pagerStyle;
            OnClientClick = onClientClick;
        }

        public void InstantiateIn(Control container) {
            Panel panel = new Panel();
            panel.AddClass("pager " + PagerStyle);

            int pageCount = AssociatedGridView.PageCount;
            int currentPage = AssociatedGridView.PageIndex + 1;

            if (currentPage > 1) {
                LinkButton lnkPrev = new LinkButton();
                lnkPrev.Controls.Add(new HtmlGenericControl("i").AddClass("fas fa-chevron-left"));
                lnkPrev.CommandName = "Page";
                lnkPrev.CommandArgument = "" + (currentPage - 1);
                lnkPrev.AddClass("page-item arrow");
                lnkPrev.OnClientClick = OnClientClick;
                panel.Controls.Add(lnkPrev);
            }

            if (pageCount <= 9) {
                for (int i = 1; i <= AssociatedGridView.PageCount; i++) {
                    panel.Controls.Add(GeneratePageLink(i));
                }

            } else {
                if (currentPage <= 5) {
                    for (int i = 1; i <= 7; i++) {
                        panel.Controls.Add(GeneratePageLink(i));
                    }
                    panel.Controls.Add(GeneratePageSelector(8, pageCount - 1));
                    panel.Controls.Add(GeneratePageLink(pageCount));


                } else if (currentPage >= pageCount - 4) {
                    panel.Controls.Add(GeneratePageLink(1));
                    panel.Controls.Add(GeneratePageSelector(2, pageCount - 7));
                    for (int i = pageCount - 6; i <= pageCount; i++) {
                        panel.Controls.Add(GeneratePageLink(i));
                    }


                } else {
                    panel.Controls.Add(GeneratePageLink(1));
                    panel.Controls.Add(GeneratePageSelector(2, currentPage - 3));
                    for (int i = currentPage - 2; i <= currentPage + 2; i++) {
                        panel.Controls.Add(GeneratePageLink(i));
                    }
                    panel.Controls.Add(GeneratePageSelector(currentPage + 3, pageCount - 1));
                    panel.Controls.Add(GeneratePageLink(pageCount));
                }
            }

            if (currentPage < pageCount) {
                LinkButton lnkNext = new LinkButton();
                lnkNext.Controls.Add(new HtmlGenericControl("i").AddClass("fas fa-chevron-right"));
                lnkNext.CommandName = "Page";
                lnkNext.CommandArgument = "" + (currentPage + 1);
                lnkNext.AddClass("page-item" /* arrow */);
                lnkNext.OnClientClick = OnClientClick;
                panel.Controls.Add(lnkNext);
            }

            HtmlGenericControl spanResuts = new HtmlGenericControl("small");
            spanResuts.AddClass("text-muted mt-1");
            spanResuts.Attributes.Add("style", "display:block; width:100%; text-align:center;");
            DataTable dt = AssociatedGridView.GetStaticDataTable();
            int fullRowsCount = AssociatedGridView.GetStaticDataTable().Rows.Count;
            int firstRow = (AssociatedGridView.PageIndex * AssociatedGridView.PageSize) + 1;
            int lastRow = Math.Min(fullRowsCount, firstRow + AssociatedGridView.PageSize - 1);
            spanResuts.InnerText = firstRow + "-" + lastRow + " de " + fullRowsCount + " resultados";

            container.Controls.Add(panel);
            container.Controls.Add(spanResuts);
        }

        private Control GeneratePageLink(int index) {
            if (AssociatedGridView.PageIndex + 1 != index) {
                LinkButton lnkPage = new LinkButton();
                lnkPage.Text = "" + index;
                lnkPage.CommandName = "Page";
                lnkPage.CommandArgument = "" + index;
                lnkPage.AddClass("page-item");
                lnkPage.OnClientClick = OnClientClick;
                return lnkPage;

            } else {
                Label lblPage = new Label();
                lblPage.Text = "" + index;
                lblPage.AddClass("page-item current");
                return lblPage;
            }
        }

        private Control GeneratePageSelector(int firstPage, int lastPage) {
            //string id = AssociatedGridView.ID + "-" + firstPage + "-" + lastPage;
            HtmlGenericControl dropdown = new HtmlGenericControl("div");
            dropdown.AddClass("dropdown dropup page-item");

            HtmlGenericControl a = new HtmlGenericControl("a");
            a.Attributes.Add("data-toggle", "dropdown");
            a.InnerHtml = "<i class='far fa-ellipsis-h'></i>";

            HtmlGenericControl menu = new HtmlGenericControl("div");
            menu.AddClass("dropdown-menu scrollable-menu");

            for (int i = firstPage; i <= lastPage; i++) {
                LinkButton lnkPage = new LinkButton();
                lnkPage.AddClass("dropdown-item");
                lnkPage.Text = "" + i;
                lnkPage.CommandName = "Page";
                lnkPage.CommandArgument = "" + i;
                lnkPage.OnClientClick = OnClientClick;
                menu.Controls.Add(lnkPage);
            }

            dropdown.Controls.Add(a);
            dropdown.Controls.Add(menu);
            return dropdown;
        }

    }
}