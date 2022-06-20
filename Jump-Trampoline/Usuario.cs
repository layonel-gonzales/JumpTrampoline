namespace Jump_Trampoline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [StringLength(13)]
        public string Rut { get; set; }

        [StringLength(100)]
        public string Nombres { get; set; }

        [StringLength(100)]
        public string Apellidos { get; set; }

        public int? Edad { get; set; }

        [StringLength(1)]
        public string Sexo { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }

        public int? TipoUsuario { get; set; }

        public string Contrasena { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
