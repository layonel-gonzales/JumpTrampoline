namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambioTipoClaseTablaClase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clase", "TipoClase", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clase", "TipoClase", c => c.String());
        }
    }
}
