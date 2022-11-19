namespace Jump_Trampoline
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Spatial;

  [Table("TipoClase")]
  public partial class TipoClase
  {
    [Key]
    public int IdTipoClase { get; set; }

    [StringLength(50)]
    public string NombreTipoClase { get; set; }
  }
}