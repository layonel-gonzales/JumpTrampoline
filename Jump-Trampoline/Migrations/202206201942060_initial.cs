namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clase",
                c => new
                    {
                        IdClase = c.Int(nullable: false, identity: true),
                        Instructor = c.Int(),
                        CantidadAlumnos = c.Int(),
                        Fecha = c.DateTime(),
                        HoraInicio = c.DateTime(),
                        HoraTermino = c.DateTime(),
                        Fk_IdLista = c.Int(),
                        Fk_IdSede = c.Int(),
                    })
                .PrimaryKey(t => t.IdClase);
            
            CreateTable(
                "dbo.Deporte",
                c => new
                    {
                        IdDeporte = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 100, unicode: false),
                        Descripcion = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.IdDeporte);
            
            CreateTable(
                "dbo.Horario",
                c => new
                    {
                        IdHorario = c.Int(nullable: false, identity: true),
                        Dias = c.String(maxLength: 50, unicode: false),
                        HoraInicio = c.DateTime(),
                        HoraTermino = c.DateTime(),
                        FK_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.IdHorario);
            
            CreateTable(
                "dbo.login",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        correo = c.String(maxLength: 100, unicode: false),
                        usuario = c.String(maxLength: 100, unicode: false),
                        fechaAcceso = c.DateTime(),
                        tipoPerfil = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sede",
                c => new
                    {
                        IdSede = c.Int(nullable: false, identity: true),
                        Direccion = c.String(unicode: false),
                        Latitud = c.String(maxLength: 50, unicode: false),
                        Longitud = c.String(maxLength: 50, unicode: false),
                        Comuna = c.String(maxLength: 50, unicode: false),
                        Telefono = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.IdSede);
            
            CreateTable(
                "dbo.TipoUsuario",
                c => new
                    {
                        IdTipoUsuario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.IdTipoUsuario);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false, identity: true),
                        Rut = c.String(maxLength: 13, unicode: false),
                        Nombres = c.String(maxLength: 100, unicode: false),
                        Apellidos = c.String(maxLength: 100, unicode: false),
                        Edad = c.Int(),
                        Sexo = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        Correo = c.String(maxLength: 100, unicode: false),
                        TipoUsuario = c.Int(),
                        Contrasena = c.String(unicode: false),
                        FechaCreacion = c.DateTime(),
                        FechaModificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuario");
            DropTable("dbo.TipoUsuario");
            DropTable("dbo.Sede");
            DropTable("dbo.login");
            DropTable("dbo.Horario");
            DropTable("dbo.Deporte");
            DropTable("dbo.Clase");
        }
    }
}
