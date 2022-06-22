namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambioNombreLogin : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.login", newName: "LoginUsuario");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.LoginUsuario", newName: "login");
        }
    }
}
