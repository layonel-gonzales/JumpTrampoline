namespace Jump_Trampoline{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    [Table("LoginUsuario")]
    public partial class LoginUsuario {
        public int Id { get; set; }

        [StringLength(100)]
        public string correo { get; set; }

        [StringLength(100)]
        public string usuario { get; set; }

        public DateTime? fechaAcceso { get; set; }

        public int? tipoPerfil { get; set; }
    }
}
