using Microsoft.EntityFrameworkCore.Migrations;

namespace E_CommerceApp.EF.Migrations
{
    public partial class SeedDataAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'54fd30e0-0f25-4c69-8146-27d174e6ca82', N'Admin', N'ADMIN', N'admin@example.com', N'ADMIN@EXAMPLE.COM', 0, N'AQAAAAEAACcQAAAAEFjXpdkNJFh6f+SPqdCvtlq12hXm/Mfoz2bh8Btp9tN2EnarQxR9UpVnfF/o0nSAKQ==', N'LV4WE26FOUDCOH2TWP5UVGFXKAVX27CH', N'f4f8d2ed-c2a0-4223-a67f-bf5c60ff67af', NULL, 0, 0, NULL, 1, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [dbo].[AspNetUsers] Where [Id] = '54fd30e0-0f25-4c69-8146-27d174e6ca82'");
        }
    }
}
