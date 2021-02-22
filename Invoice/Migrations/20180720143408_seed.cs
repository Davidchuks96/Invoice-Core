using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Invoice.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users (ConfirmPassword,CreateDate, Email,IsDelete,Password, UpdateDate) VALUES ('12345', '02-03-2018', 'admin@kodauthor.com', 'false', '12345', '02-03-2018')");
            migrationBuilder.Sql("INSERT INTO Customer (Name,Email,Phone, CreateDate, UpdateDate,IsDelete) VALUES ('Walk in Customer', 'customer@gmail.com', '0000000000', '02-03-2018','02-03-2018', 'false')");
            migrationBuilder.Sql("INSERT INTO StoreSetting (Logo,StoreName,Email, Phone, Currency, Address, CreateDate,UpdateDate,IsDelete, web) VALUES ('wwwroot/images/k_logo.png', 'Kodauthor','admin@kodauthor.com', '0000000000', '$', 'Dhaka, Bangladesh', '02-03-2018','02-03-2018', 'false', 'www.kodauthor.com')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
