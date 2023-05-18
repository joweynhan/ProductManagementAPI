using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class GetAllProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE GetAllProducts
            AS
            BEGIN
                SET NOCOUNT ON;
                SELECT *
                FROM Products
            END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE GetAllProducts";
            migrationBuilder.Sql(sp);
        }
    }
}
