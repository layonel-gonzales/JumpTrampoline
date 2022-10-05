namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambioTablaUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Direccion", c => c.String());
            AddColumn("dbo.Usuario", "Comuna", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Comuna");
            DropColumn("dbo.Usuario", "Direccion");
        }
    }
}
