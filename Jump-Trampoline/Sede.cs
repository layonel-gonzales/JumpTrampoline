namespace Jump_Trampoline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sede")]
    public partial class Sede
    {
        [Key]
        public int IdSede { get; set; }

        public string Direccion { get; set; }

        [StringLength(50)]
        public string Latitud { get; set; }

        [StringLength(50)]
        public string Longitud { get; set; }

        [StringLength(50)]
        public string Comuna { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }
    }
}
