using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ligueylou.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutFavoriId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Favoris",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favoris");
        }
    }
}
