using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE AddProduct
                        @Name NVARCHAR(50),
                        @Price FLOAT
                    AS
                    BEGIN
                        INSERT INTO Products (Name, Price)
                        VALUES (@Name, @Price)
                    END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE AddProduct";
            migrationBuilder.Sql(sp);
        }
    }
}
