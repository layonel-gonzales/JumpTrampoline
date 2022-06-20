namespace Jump_Trampoline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clase")]
    public partial class Clase
    {
        [Key]
        public int IdClase { get; set; }

        public int? Instructor { get; set; }

        public int? CantidadAlumnos { get; set; }

        public DateTime? Fecha { get; set; }

        public DateTime? HoraInicio { get; set; }

        public DateTime? HoraTermino { get; set; }

        public int? Fk_IdLista { get; set; }

        public int? Fk_IdSede { get; set; }
    }
}
