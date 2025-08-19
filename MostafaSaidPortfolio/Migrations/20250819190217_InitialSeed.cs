using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MostafaSaidPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "ImageUrl", "Slug", "Summary" },
                values: new object[] { "Mostafa Said", null, null, "" });

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "ImageUrl", "Slug", "Summary" },
                values: new object[] { "Mostafa Said", null, null, "" });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "ImageUrl", "IsPublished", "Slug", "Summary", "Title" },
                values: new object[] { 5, "Mostafa Said", 1, "Step-by-step guide to building a full-featured blog platform with authentication and CRUD...", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, "", "Creating a Blog Platform with ASP.NET Core" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Productivity" },
                    { 4, "API Integration" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedAt", "DemoUrl", "Description", "GithubUrl", "ImageUrl", "TagId", "Title" },
                values: new object[,]
                {
                    { 3, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/blog", "A full-featured blog platform with authentication and CRUD operations.", "https://github.com/mostafasaid/blog-platform", "/images/projects/blog.png", null, "Blog Platform" },
                    { 4, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/tasks", "A productivity app for managing tasks with deadlines and priorities.", "https://github.com/mostafasaid/task-manager", "/images/projects/tasks.png", null, "Task Manager App" },
                    { 5, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/weather", "A real-time weather dashboard using public APIs and responsive UI.", "https://github.com/mostafasaid/weather-dashboard", "/images/projects/weather.png", null, "Weather Dashboard" }
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "ImageUrl", "IsPublished", "Slug", "Summary", "Title" },
                values: new object[,]
                {
                    { 3, "Mostafa Said", 3, "In this post, I walk through building a productivity app with ASP.NET Core...", new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, "", "Building a Task Manager App" },
                    { 4, "Mostafa Said", 4, "Learn how to fetch real-time weather data from external APIs and display it in your app...", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, "", "Integrating Weather APIs in .NET" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Author",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "BlogPosts");
        }
    }
}
