using System;
using System.Collections.Generic;
using System.Linq;
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

            }
        }
    }
}