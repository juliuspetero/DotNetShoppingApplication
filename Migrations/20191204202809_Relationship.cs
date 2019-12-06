using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApplication.Migrations
{
    public partial class Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<string>(nullable: true),
                    modifiedOn = table.Column<string>(nullable: true),
                    SubscriptionId = table.Column<string>(nullable: true),
                    AccountType = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true),
                    AccountStatus = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    AlertLevel = table.Column<string>(nullable: true),
                    AlertChannel = table.Column<string>(nullable: true),
                    AlertEmailAddress = table.Column<string>(nullable: true),
                    AlertPhoneNumber = table.Column<string>(nullable: true),
                    CallBackUri = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    AccountPackage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TotalAmount = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    PlaceOn = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProviders",
                columns: table => new
                {
                    PaymentId = table.Column<string>(nullable: false),
                    PaymentItemId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PaymentRegex = table.Column<string>(nullable: true),
                    PaymentRegexStart = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LongDescription = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviders", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RequestId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    BatchId = table.Column<string>(nullable: true),
                    Amount = table.Column<string>(nullable: true),
                    PaymentProvider = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    ProductId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "OrderId", "PhotoPath", "Price" },
                values: new object[] { "y5THO7OJpEmF5kvd81c3SA", "Good Laptop Bag", "Laptop Bag", null, "laptopbag.jpg", 1000.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "OrderId", "PhotoPath", "Price" },
                values: new object[] { "RO0qtBX2k212u4OAIMEGg", "Good Water Bottle", "Water Bottle", null, "waterbottle.jpg", 1200.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "OrderId", "PhotoPath", "Price" },
                values: new object[] { "LVbg62KXv0uav7AMnDJFSA", "Good Red Sox", "Red Sox Hat", null, "redsox.jpg", 1000.0 });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "PaymentProviders");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "LVbg62KXv0uav7AMnDJFSA");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "RO0qtBX2k212u4OAIMEGg");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "y5THO7OJpEmF5kvd81c3SA");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

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
    }
}
