using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE UpdateProduct
                        @ProductId INT,
                        @Name NVARCHAR(50),
                        @Price FLOAT
                    AS
                    BEGIN
                        UPDATE Products
                        SET Name = @Name,
                            Price = @Price
                        WHERE ProductId = @ProductId;
                    END
                    ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE UpdateProduct";
            migrationBuilder.Sql(sp);
        }
    }
}
