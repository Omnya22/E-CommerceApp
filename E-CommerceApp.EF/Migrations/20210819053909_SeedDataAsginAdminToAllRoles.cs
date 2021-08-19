using Microsoft.EntityFrameworkCore.Migrations;

namespace E_CommerceApp.EF.Migrations
{
    public partial class SeedDataAsginAdminToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into [dbo].[AspNetUserRoles] (UserId,RoleId) select '54fd30e0-0f25-4c69-8146-27d174e6ca82',Id From [dbo].[AspNetRoles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [dbo].[AspNetUserRoles] where UserId ='54fd30e0-0f25-4c69-8146-27d174e6ca82'");
        }
    }
}
