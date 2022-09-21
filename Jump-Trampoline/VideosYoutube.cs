namespace Jump_Trampoline {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VideosYoutube")]
    public partial class VideosYoutube {
        [Key]
        public int IdVideo { get; set; }

        [StringLength(300)]
        public string NombreVideo { get; set; }

        [StringLength(800)]
        public string Url { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        [StringLength(1)]
        public string IndEstado { get; set; }
    }
}
