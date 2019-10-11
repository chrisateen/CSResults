namespace CSResults.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        moduleID = c.String(nullable: false, maxLength: 128),
                        moduleName = c.String(),
                    })
                .PrimaryKey(t => t.moduleID);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        modName = c.String(nullable: false, maxLength: 128),
                        year = c.String(nullable: false, maxLength: 128),
                        mean = c.Double(),
                        median = c.Double(),
                        below30 = c.Double(),
                        below40 = c.Double(),
                        below50 = c.Double(),
                        below60 = c.Double(),
                        below70 = c.Double(),
                        below80 = c.Double(),
                        above80 = c.Double(),
                        modID = c.String(),
                        Module_moduleID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.modName, t.year })
                .ForeignKey("dbo.Modules", t => t.Module_moduleID)
                .Index(t => t.Module_moduleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "Module_moduleID", "dbo.Modules");
            DropIndex("dbo.Results", new[] { "Module_moduleID" });
            DropTable("dbo.Results");
            DropTable("dbo.Modules");
        }
    }
}
