using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaOrderingApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PizzaFlavour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.PizzaId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HasCode = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemsId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "PizzaId", "Description", "IsVegetarian", "PizzaFlavour", "PizzaName", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "A classic pizza with simple and delicious flavors.", true, "Classic tomato, basil, mozzarella", "Margherita", 8.9900000000000002, 100.0 },
                    { 2, "A classic favorite with spicy pepperoni slices.", false, "Pepperoni, mozzarella", "Pepperoni", 10.99, 80.0 },
                    { 3, "A flavorful vegetarian option with a variety of vegetables.", true, "Assorted vegetables, mozzarella", "Vegetarian", 9.9900000000000002, 70.0 },
                    { 4, "A tropical delight with ham and pineapple toppings.", false, "Ham, pineapple, mozzarella", "Hawaiian", 11.99, 0.0 },
                    { 5, "A tangy BBQ flavor with grilled chicken and onions.", false, "BBQ sauce, chicken, onions, mozzarella", "BBQ Chicken", 12.99, 50.0 },
                    { 6, "A mushroom lover's dream with a variety of mushrooms.", true, "Assorted mushrooms, mozzarella", "Mushroom Lovers", 10.99, 75.0 },
                    { 7, "A meat lover's delight with a variety of meats.", false, "Pepperoni, sausage, ham, bacon, mozzarella", "Meat Feast", 13.99, 0.0 },
                    { 8, "A cheese lover's pizza with four different cheeses.", true, "Mozzarella, cheddar, parmesan, provolone", "Four Cheese", 11.99, 65.0 },
                    { 9, "An ultimate combination of toppings for a satisfying pizza.", false, "Pepperoni, sausage, bell peppers, onions, olives, mozzarella", "Supreme", 12.99, 45.0 },
                    { 10, "A flavorful pesto sauce with grilled chicken and tomatoes.", false, "Pesto sauce, chicken, tomatoes, mozzarella", "Pesto Chicken", 12.99, 60.0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "HasCode", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", new byte[0], "JohnDoe", new byte[0] },
                    { 2, "jane.smith@example.com", new byte[0], "JaneSmith", new byte[0] },
                    { 3, "alice.johnson@example.com", new byte[0], "AliceJohnson", new byte[0] }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, 19.98, 1 },
                    { 2, 15.99, 2 },
                    { 3, 25.489999999999998, 3 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemsId", "OrderId", "PizzaId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 8.9900000000000002, 2 },
                    { 2, 1, 2, 10.99, 1 },
                    { 3, 2, 3, 9.9900000000000002, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PizzaId",
                table: "OrderItems",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pizzas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
