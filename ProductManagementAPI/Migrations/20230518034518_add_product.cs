using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementAPI.Migrations
{
    public partial class add_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE AddProduct
                            @UserId INT,
                            @Name VARCHAR(50),
                            @Price FLOAT
                        AS
                        BEGIN
                            INSERT INTO Products (UserId, Name, Price)
                            VALUES (@UserId, @Name,
                            @Price);
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