using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class select_productid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE SelectProductId
                            @Id INT
                        AS
                        BEGIN
                            SELECT * FROM Products WHERE Id = @Id;
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SelectProductId";
            migrationBuilder.Sql(sp);
        }
    }
}
