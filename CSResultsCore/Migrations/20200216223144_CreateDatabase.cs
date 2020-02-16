using Microsoft.EntityFrameworkCore.Migrations;

namespace CSResultsCore.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    moduleID = table.Column<string>(nullable: false),
                    moduleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.moduleID);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    moduleName = table.Column<string>(nullable: false),
                    year = table.Column<string>(nullable: false),
                    mean = table.Column<double>(nullable: true),
                    median = table.Column<double>(nullable: true),
                    below30 = table.Column<double>(nullable: true),
                    below40 = table.Column<double>(nullable: true),
                    below50 = table.Column<double>(nullable: true),
                    below60 = table.Column<double>(nullable: true),
                    below70 = table.Column<double>(nullable: true),
                    below80 = table.Column<double>(nullable: true),
                    above80 = table.Column<double>(nullable: true),
                    moduleID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => new { x.moduleName, x.year });
                    table.ForeignKey(
                        name: "FK_Results_Modules_moduleID",
                        column: x => x.moduleID,
                        principalTable: "Modules",
                        principalColumn: "moduleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_moduleID",
                table: "Results",
                column: "moduleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
