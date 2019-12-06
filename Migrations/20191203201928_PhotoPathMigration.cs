using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApplication.Migrations
{
    public partial class PhotoPathMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "033JmfKN0G/Mc046JLo9w");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "83YGrg7AX062YONbq21s5g");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "fJruiCEoSUOeGDFMCURSmg");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Products",
                newName: "PhotoPath");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "PhotoPath", "Price" },
                values: new object[] { "fbfv3lUw5U64UaGYDJUCxA", "Good Laptop Bag", "Laptop Bag", "laptopbag.jpg", 1000.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "PhotoPath", "Price" },
                values: new object[] { "T1Psub5QBkSutdhU9wH1kA", "Good Water Bottle", "Water Bottle", "waterbottle.jpg", 1200.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "PhotoPath", "Price" },
                values: new object[] { "chPHfKCA/kaFcjRDFS9/qw", "Good Red Sox", "Red Sox Hat", "redsox.jpg", 1000.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "chPHfKCA/kaFcjRDFS9/qw");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "fbfv3lUw5U64UaGYDJUCxA");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "T1Psub5QBkSutdhU9wH1kA");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Products",
                newName: "Photo");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Photo", "Price" },
                values: new object[] { "fJruiCEoSUOeGDFMCURSmg", "Good Laptop Bag", "Laptop Bag", "laptopbag.jpg", 1000.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Photo", "Price" },
                values: new object[] { "033JmfKN0G/Mc046JLo9w", "Good Water Bottle", "Water Bottle", "waterbottle.jpg", 1200.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Photo", "Price" },
                values: new object[] { "83YGrg7AX062YONbq21s5g", "Good Red Sox", "Red Sox Hat", "redsox.jpg", 1000.0 });
        }
    }
}
