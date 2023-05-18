using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class delete_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE DeleteProduct
                            @Id INT
                        AS
                        BEGIN
                            DELETE FROM Products WHERE Id = @Id;
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE DeleteProduct";
            migrationBuilder.Sql(sp);
        }
    }
}
