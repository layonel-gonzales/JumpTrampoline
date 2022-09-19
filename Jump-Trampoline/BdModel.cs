using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Jump_Trampoline {
  public partial class BdModel : DbContext {
    public BdModel()
        : base("name=BdModel") {
    }

    public virtual DbSet<Clase> Clase { get; set; }
    public virtual DbSet<Deporte> Deporte { get; set; }
    public virtual DbSet<Horario> Horario { get; set; }
    public virtual DbSet<LoginUsuario> login { get; set; }
    public virtual DbSet<Sede> Sede { get; set; }
    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
    public virtual DbSet<Usuario> Usuario { get; set; }
    public virtual DbSet<VideosYoutube> VideosYoutube { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      modelBuilder.Entity<Deporte>()
          .Property(e => e.Nombre)
          .IsUnicode(false);

      modelBuilder.Entity<Deporte>()
          .Property(e => e.Descripcion)
          .IsUnicode(false);

      modelBuilder.Entity<Horario>()
          .Property(e => e.Dias)
          .IsUnicode(false);

      modelBuilder.Entity<LoginUsuario>()
          .Property(e => e.correo)
          .IsUnicode(false);

      modelBuilder.Entity<LoginUsuario>()
          .Property(e => e.usuario)
          .IsUnicode(false);

      modelBuilder.Entity<Sede>()
          .Property(e => e.Direccion)
          .IsUnicode(false);

      modelBuilder.Entity<Sede>()
          .Property(e => e.Latitud)
          .IsUnicode(false);

      modelBuilder.Entity<Sede>()
          .Property(e => e.Longitud)
          .IsUnicode(false);

      modelBuilder.Entity<Sede>()
          .Property(e => e.Comuna)
          .IsUnicode(false);

      modelBuilder.Entity<Sede>()
          .Property(e => e.Telefono)
          .IsUnicode(false);

      modelBuilder.Entity<TipoUsuario>()
          .Property(e => e.Nombre)
          .IsUnicode(false);

      modelBuilder.Entity<Usuario>()
          .Property(e => e.Rut)
          .IsUnicode(false);

      modelBuilder.Entity<Usuario>()
          .Property(e => e.Nombres)
          .IsUnicode(false);

      modelBuilder.Entity<Usuario>()
          .Property(e => e.Apellidos)
          .IsUnicode(false);

      modelBuilder.Entity<Usuario>()
          .Property(e => e.Sexo)
          .IsFixedLength()
          .IsUnicode(false);

      modelBuilder.Entity<Usuario>()
          .Property(e => e.Correo)
          .IsUnicode(false);

      modelBuilder.Entity<Usuario>()
          .Property(e => e.Contrasena)
          .IsUnicode(false);
    }
  }
}
