using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InventoryManager.Data.Migrations
{
    public partial class UserClothesRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Clothes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_OwnerId",
                table: "Clothes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_AspNetUsers_OwnerId",
                table: "Clothes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_AspNetUsers_OwnerId",
                table: "Clothes");

            migrationBuilder.DropIndex(
                name: "IX_Clothes_OwnerId",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Clothes");
        }
    }
}
