namespace Jump_Trampoline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Horario")]
    public partial class Horario
    {
        [Key]
        public int IdHorario { get; set; }

        [StringLength(50)]
        public string Dias { get; set; }

        public DateTime? HoraInicio { get; set; }

        public DateTime? HoraTermino { get; set; }

        public int? FK_IdUsuario { get; set; }
    }
}
