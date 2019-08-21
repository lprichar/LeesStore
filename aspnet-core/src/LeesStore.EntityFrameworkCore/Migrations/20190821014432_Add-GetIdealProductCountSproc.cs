using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeesStore.Migrations
{
    public partial class AddGetIdealProductCountSproc : Migration
    {
        private readonly SprocService _sprocService = new SprocService();

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var contents = _sprocService.GetFile("GetIdealProductQuantity.sql");
            migrationBuilder.Sql(contents);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE GetIdealProductQuantity");
        }
    }

    public class SprocService
    {
        public string GetFile(string file)
        {
            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("LeesStore.StoredProcedures." + file))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
