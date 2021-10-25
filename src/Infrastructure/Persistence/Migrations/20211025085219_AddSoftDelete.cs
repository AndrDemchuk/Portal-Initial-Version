using Microsoft.EntityFrameworkCore.Migrations;

namespace BvAcademyPortal.Infrastructure.Persistence.Migrations
{
    public partial class AddSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesUsers_Courses_CourseId",
                table: "CoursesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesUsers_Users_UserId",
                table: "CoursesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialNetworskUsers_SocialNetworks_SocialNetworkId",
                table: "SocialNetworskUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialNetworskUsers_Users_UserId",
                table: "SocialNetworskUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetworskUsers",
                table: "SocialNetworskUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetworks",
                table: "SocialNetworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesUsers",
                table: "CoursesUsers");

            migrationBuilder.RenameTable(
                name: "SocialNetworskUsers",
                newName: "SocialNetworkUsers");

            migrationBuilder.RenameTable(
                name: "SocialNetworks",
                newName: "SocialNetwork");

            migrationBuilder.RenameTable(
                name: "CoursesUsers",
                newName: "CourseUsers");

            migrationBuilder.RenameIndex(
                name: "IX_SocialNetworskUsers_UserId",
                table: "SocialNetworkUsers",
                newName: "IX_SocialNetworkUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialNetworskUsers_SocialNetworkId",
                table: "SocialNetworkUsers",
                newName: "IX_SocialNetworkUsers_SocialNetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesUsers_UserId",
                table: "CourseUsers",
                newName: "IX_CourseUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesUsers_CourseId",
                table: "CourseUsers",
                newName: "IX_CourseUsers_CourseId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Topics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SkillUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SkillTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Skills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SocialNetworkUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SocialNetwork",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetworkUsers",
                table: "SocialNetworkUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetwork",
                table: "SocialNetwork",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Users_UserId",
                table: "CourseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialNetworkUsers_SocialNetwork_SocialNetworkId",
                table: "SocialNetworkUsers",
                column: "SocialNetworkId",
                principalTable: "SocialNetwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialNetworkUsers_Users_UserId",
                table: "SocialNetworkUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Users_UserId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialNetworkUsers_SocialNetwork_SocialNetworkId",
                table: "SocialNetworkUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialNetworkUsers_Users_UserId",
                table: "SocialNetworkUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetworkUsers",
                table: "SocialNetworkUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetwork",
                table: "SocialNetwork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SkillUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SkillTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SocialNetworkUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SocialNetwork");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseUsers");

            migrationBuilder.RenameTable(
                name: "SocialNetworkUsers",
                newName: "SocialNetworskUsers");

            migrationBuilder.RenameTable(
                name: "SocialNetwork",
                newName: "SocialNetworks");

            migrationBuilder.RenameTable(
                name: "CourseUsers",
                newName: "CoursesUsers");

            migrationBuilder.RenameIndex(
                name: "IX_SocialNetworkUsers_UserId",
                table: "SocialNetworskUsers",
                newName: "IX_SocialNetworskUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialNetworkUsers_SocialNetworkId",
                table: "SocialNetworskUsers",
                newName: "IX_SocialNetworskUsers_SocialNetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUsers_UserId",
                table: "CoursesUsers",
                newName: "IX_CoursesUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUsers_CourseId",
                table: "CoursesUsers",
                newName: "IX_CoursesUsers_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetworskUsers",
                table: "SocialNetworskUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetworks",
                table: "SocialNetworks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesUsers",
                table: "CoursesUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesUsers_Courses_CourseId",
                table: "CoursesUsers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesUsers_Users_UserId",
                table: "CoursesUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialNetworskUsers_SocialNetworks_SocialNetworkId",
                table: "SocialNetworskUsers",
                column: "SocialNetworkId",
                principalTable: "SocialNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialNetworskUsers_Users_UserId",
                table: "SocialNetworskUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
