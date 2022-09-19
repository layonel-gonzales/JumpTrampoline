namespace Jump_Trampoline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaTablaparaMostrarVideos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideosYoutube",
                c => new
                    {
                        IdVideo = c.Int(nullable: false, identity: true),
                        NombreVideo = c.String(maxLength: 300),
                        Url = c.String(maxLength: 800),
                        FechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdVideo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VideosYoutube");
        }
    }
}
