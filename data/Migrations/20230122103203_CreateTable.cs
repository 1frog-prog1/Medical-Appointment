using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    patientid = table.Column<int>(name: "patient_id", type: "integer", nullable: false),
                    doctorid = table.Column<int>(name: "doctor_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fio = table.Column<string>(type: "text", nullable: false),
                    specialisationid = table.Column<int>(name: "specialisation_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SheldueDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    doctorid = table.Column<int>(name: "doctor_id", type: "integer", nullable: false),
                    daystart = table.Column<DateTime>(name: "day_start", type: "timestamp with time zone", nullable: false),
                    dayend = table.Column<DateTime>(name: "day_end", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheldueDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialisationsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialisationsDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    fio = table.Column<string>(type: "text", nullable: false),
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDb", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDb_login",
                table: "UserDb",
                column: "login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDb");

            migrationBuilder.DropTable(
                name: "DoctorDb");

            migrationBuilder.DropTable(
                name: "SheldueDb");

            migrationBuilder.DropTable(
                name: "SpecialisationsDb");

            migrationBuilder.DropTable(
                name: "UserDb");
        }
    }
}
