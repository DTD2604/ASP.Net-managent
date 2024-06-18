using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManager.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    slug = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    leader = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    date_beginning = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    logo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__departme__3213E83F11342593", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true, defaultValueSql: "(NULL)"),
                    slug = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__3213E83F29CEC7F8", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "terms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__terms__3213E83FA989511E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    extra_code = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    last_name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    full_name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    avatar = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true, defaultValueSql: "(NULL)"),
                    information = table.Column<string>(type: "text", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83FE75CADF3", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    department_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__courses__3213E83F85DC9310", x => x.id);
                    table.ForeignKey(
                        name: "fk_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_id = table.Column<int>(type: "int", nullable: false),
                    term_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    student_numbers = table.Column<int>(type: "int", nullable: false),
                    teacher = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    captain = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__groups__3213E83F1BF06725", x => x.id);
                    table.ForeignKey(
                        name: "fk_department_id_groups",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_term_id",
                        column: x => x.term_id,
                        principalTable: "terms",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    ip_client = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    last_login = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    last_logout = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__accounts__3213E83F568C5A59", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "group_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<int>(type: "int", nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    teacher_id = table.Column<int>(type: "int", nullable: false),
                    absent = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    present = table.Column<byte>(type: "tinyint", nullable: false),
                    learning_date = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__group_st__3213E83F39D94D02", x => x.id);
                    table.ForeignKey(
                        name: "fk_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_student_id",
                        column: x => x.student_id,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_role_id",
                table: "accounts",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_user_id",
                table: "accounts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_courses_department_id",
                table: "courses",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_student_course_id",
                table: "group_student",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_student_group_id",
                table: "group_student",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_student_student_id",
                table: "group_student",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_student_teacher_id",
                table: "group_student",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_groups_department_id",
                table: "groups",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_groups_term_id",
                table: "groups",
                column: "term_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "group_student");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "terms");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "schedule");

            migrationBuilder.DropTable(
                name: "scheduleUser");
        }
    }
}
