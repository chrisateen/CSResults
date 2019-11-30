namespace CSResults.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Results", "Module_moduleID", "dbo.Modules");
            DropIndex("dbo.Results", new[] { "Module_moduleID" });
            DropColumn("dbo.Results", "modID");
            RenameColumn(table: "dbo.Results", name: "Module_moduleID", newName: "modID");
            AlterColumn("dbo.Results", "modID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Results", "modID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Results", "modID");
            AddForeignKey("dbo.Results", "modID", "dbo.Modules", "moduleID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "modID", "dbo.Modules");
            DropIndex("dbo.Results", new[] { "modID" });
            AlterColumn("dbo.Results", "modID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Results", "modID", c => c.String());
            RenameColumn(table: "dbo.Results", name: "modID", newName: "Module_moduleID");
            AddColumn("dbo.Results", "modID", c => c.String());
            CreateIndex("dbo.Results", "Module_moduleID");
            AddForeignKey("dbo.Results", "Module_moduleID", "dbo.Modules", "moduleID");
        }
    }
}
