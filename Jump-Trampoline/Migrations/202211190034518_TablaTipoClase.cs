namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablaTipoClase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoClase",
                c => new
                    {
                        IdTipoClase = c.Int(nullable: false, identity: true),
                        NombreTipoClase = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdTipoClase);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipoClase");
        }
    }
}
