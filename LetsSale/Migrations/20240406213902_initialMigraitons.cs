using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetsSale.Migrations
{
    /// <inheritdoc />
    public partial class initialMigraitons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name:"Notifies",
                columns: table=> new
                {
                    NotifyID = table.Column<int>(type:"int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotifyType = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notify", x=>x.NotifyID);
                }
                );

            migrationBuilder.CreateTable(
                name:"User_Cars",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMainID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarPlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarYear = table.Column<int>(type: "int", nullable: false),
                    CarSaleDate = table.Column<DateTime>(type:"date", nullable:false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarID);
                });

            migrationBuilder.CreateTable(
                name: "Car_Pics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarPicsPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarID = table.Column<Guid>(type:"uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_Pics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CarCategories",
                columns: table => new
                {
                    CatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCategories", x => x.CatID);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMainID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarPrice = table.Column<int>(type: "int", nullable: false),
                    CarPlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarYear = table.Column<int>(type: "int", nullable: false),
                    CarColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarHP = table.Column<int>(type: "int", nullable: false),
                    CarTorque = table.Column<int>(type: "int", nullable: false),
                    CarMaxFuelTankCapacity = table.Column<int>(type: "int", nullable: false),
                    CarMaxSpeed = table.Column<int>(type: "int", nullable: false),
                    CarTransmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarCargoVolume = table.Column<int>(type: "int", nullable: false),
                    CarMainPic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarPicsID = table.Column<int>(type: "uniqueidentifier", nullable: false),
                    CarCategoryID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarsID);
                });

            migrationBuilder.CreateTable(
                name: "SaledCars",
                columns: table => new
                {
                    SaledCarsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SCMainID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SCarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCarBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCarPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCarPlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCarCategoryID = table.Column<int>(type: "int", nullable: false),
                    SCarMainPic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SCarSaleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.SaledCarsID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMainID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ELastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaledCarsID = table.Column<int>(type: "int", nullable: false),
                    ERankID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "ERanks",
                columns: table => new
                {
                    ERankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ERankName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERanks", x => x.ERankID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UMainID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCarsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car_Pics");

            migrationBuilder.DropTable(
                name: "CarCategories");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "ERanks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
