using Microsoft.EntityFrameworkCore.Migrations;

namespace LeesStore.Migrations
{
    public partial class UpdateAbpUsersSetDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE AbpUsers SET Discriminator = 'User'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
