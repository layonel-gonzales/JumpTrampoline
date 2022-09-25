namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposTablaClase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clase", "Dias", c => c.String());
            AddColumn("dbo.Clase", "CodigoUbicacion", c => c.String());
            AddColumn("dbo.Clase", "TipoClase", c => c.String());
            AddColumn("dbo.Clase", "Comuna", c => c.Int());
            AddColumn("dbo.Clase", "CupoOcupado", c => c.String());
            AddColumn("dbo.Clase", "IndEstado", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clase", "IndEstado");
            DropColumn("dbo.Clase", "CupoOcupado");
            DropColumn("dbo.Clase", "Comuna");
            DropColumn("dbo.Clase", "TipoClase");
            DropColumn("dbo.Clase", "CodigoUbicacion");
            DropColumn("dbo.Clase", "Dias");
        }
    }
}
