using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryPatternWithUOW.EF.Migrations
{
    /// <inheritdoc />
    public partial class uniteUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Unites_UniteId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Solution_Assignment_AssignmentId",
                table: "Solution");

            migrationBuilder.DropForeignKey(
                name: "FK_Solution_Users_StudentId",
                table: "Solution");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Courses_CourseId",
                table: "StudentCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Users_StudentId",
                table: "StudentCourse");

            migrationBuilder.DropTable(
                name: "UserConnection");

            migrationBuilder.DropIndex(
                name: "IX_Unites_CourseId",
                table: "Unites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourse",
                table: "StudentCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Solution",
                table: "Solution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignment_UniteId",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "StudentCourse",
                newName: "StudentCourses");

            migrationBuilder.RenameTable(
                name: "Solution",
                newName: "Solutions");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assignments");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourse_StudentId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Solution_StudentId",
                table: "Solutions",
                newName: "IX_Solutions_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses",
                columns: new[] { "CourseId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Solutions",
                table: "Solutions",
                columns: new[] { "AssignmentId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "AssignmentId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$oaB0l7afGUkezbjnOOOvA.ZQgzfP0BZn5mAFUwYKHZWyvlgsktZUG");

            migrationBuilder.CreateIndex(
                name: "IX_Unites_CourseId",
                table: "Unites",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_UniteId",
                table: "Assignments",
                column: "UniteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Unites_UniteId",
                table: "Assignments",
                column: "UniteId",
                principalTable: "Unites",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Assignments_AssignmentId",
                table: "Solutions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Users_StudentId",
                table: "Solutions",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Users_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Unites_UniteId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Assignments_AssignmentId",
                table: "Solutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Users_StudentId",
                table: "Solutions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Users_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_Unites_CourseId",
                table: "Unites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Solutions",
                table: "Solutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_UniteId",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "StudentCourses",
                newName: "StudentCourse");

            migrationBuilder.RenameTable(
                name: "Solutions",
                newName: "Solution");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Assignment");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourse",
                newName: "IX_StudentCourse_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Solutions_StudentId",
                table: "Solution",
                newName: "IX_Solution_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourse",
                table: "StudentCourse",
                columns: new[] { "CourseId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Solution",
                table: "Solution",
                columns: new[] { "AssignmentId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                column: "AssignmentId");

            migrationBuilder.CreateTable(
                name: "UserConnection",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RequestedToVideo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnection", x => new { x.StudentId, x.ConnectionId });
                    table.ForeignKey(
                        name: "FK_UserConnection_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$RK/fA5T9ai6ZGcbSlDvB3uym9WflNKrI2A8rheUKNtBRqbTUmP8z6");

            migrationBuilder.CreateIndex(
                name: "IX_Unites_CourseId",
                table: "Unites",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_UniteId",
                table: "Assignment",
                column: "UniteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConnection_StudentId",
                table: "UserConnection",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Unites_UniteId",
                table: "Assignment",
                column: "UniteId",
                principalTable: "Unites",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solution_Assignment_AssignmentId",
                table: "Solution",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solution_Users_StudentId",
                table: "Solution",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Courses_CourseId",
                table: "StudentCourse",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Users_StudentId",
                table: "StudentCourse",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
