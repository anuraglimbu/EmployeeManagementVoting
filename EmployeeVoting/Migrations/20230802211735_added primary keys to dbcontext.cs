using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeVoting.Migrations
{
    /// <inheritdoc />
    public partial class addedprimarykeystodbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ev_Addresses",
                columns: table => new
                {
                    address_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    address_name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    employee_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ev_Addresses", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "ev_Contacts",
                columns: table => new
                {
                    contact_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    employee_contact = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    employee_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ev_Contacts", x => x.contact_id);
                });

            migrationBuilder.CreateTable(
                name: "ev_Departments",
                columns: table => new
                {
                    department_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    department_name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ev_Departments", x => x.department_id);
                });

            migrationBuilder.CreateTable(
                name: "ev_EmployeeHistories",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    employee_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    department_name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    role_name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    joined_role_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ev_EmployeeHistories", x => x.history_id);
                });

            migrationBuilder.CreateTable(
                name: "ev_Employees",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    employee_name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    employee_dob = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    employee_email = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    role_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ev_Employees", x => x.employee_id);
                });

            migrationBuilder.CreateTable(
                name: "ev_Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    department_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    role_name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ev_Roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "ev_Votes",
                columns: table => new
                {
                    vote_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    vote_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    voter_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    candidate_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ev_Votes", x => x.vote_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ev_Addresses");

            migrationBuilder.DropTable(
                name: "ev_Contacts");

            migrationBuilder.DropTable(
                name: "ev_Departments");

            migrationBuilder.DropTable(
                name: "ev_EmployeeHistories");

            migrationBuilder.DropTable(
                name: "ev_Employees");

            migrationBuilder.DropTable(
                name: "ev_Roles");

            migrationBuilder.DropTable(
                name: "ev_Votes");
        }
    }
}
