using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class select_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE SelectAllProduct
                        AS
                        BEGIN
                            SELECT * FROM Products;
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SelectAllProduct";
            migrationBuilder.Sql(sp);
        }
    }
}
