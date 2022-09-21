namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaIndEstadoTablaVideo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideosYoutube", "IndEstado", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideosYoutube", "IndEstado");
        }
    }
}
