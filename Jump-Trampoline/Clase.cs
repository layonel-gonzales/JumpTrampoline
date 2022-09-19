namespace Jump_Trampoline
{
    using Org.BouncyCastle.Asn1.Cmp;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.AccessControl;
    using System.Security.Cryptography.X509Certificates;

    [Table("Clase")]
    public partial class Clase
    {
        [Key]
        public int IdClase { get; set; }

        public string NombreClase { get; set; }

        public int? CantidadAlumnos { get; set; }

        public DateTime? HoraInicio { get; set; }

        public DateTime? HoraTermino { get; set; }

        public int? Precio { get; set; }

        public int? Fk_IdSede { get; set; }

        public int? Fk_IdLista { get; set; }
        
    }
}
