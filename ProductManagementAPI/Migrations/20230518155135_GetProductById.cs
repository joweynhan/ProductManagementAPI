using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class GetProductById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE GetProductById
                    @ProductId INT
                    AS
                    BEGIN
                        SELECT *
                        FROM Products
                        WHERE ProductId = @ProductId
                    END
                    ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE GetProductById";
            migrationBuilder.Sql(sp);
        }
    }
}
