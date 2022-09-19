namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeRealizaCambiosTablaClase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clase", "NombreClase", c => c.String());
            AddColumn("dbo.Clase", "Comuna", c => c.Int());
            AddColumn("dbo.Clase", "Direccion", c => c.String());
            DropColumn("dbo.Clase", "Fecha");
            DropColumn("dbo.Clase", "Fk_IdSede");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clase", "Fk_IdSede", c => c.Int());
            AddColumn("dbo.Clase", "Fecha", c => c.DateTime());
            DropColumn("dbo.Clase", "Direccion");
            DropColumn("dbo.Clase", "Comuna");
            DropColumn("dbo.Clase", "NombreClase");
        }
    }
}
