using System;
using MostafaSaidPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace MostafaSaidPortfolio.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            // Seed Projects
            builder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Title = "Portfolio Website",
                    Description = "A personal portfolio showcasing projects, blog posts, and contact info.",
                    LiveUrl = "https://example.com/portfolio",
                    GitHubUrl = "https://github.com/mostafasaid/portfolio",
                    ImageUrl = "/images/projects/portfolio.png",
                    CreatedAt = new DateTime(2023, 01, 01)
                },
                new Project
                {
                    Id = 2,
                    Title = "E-commerce App",
                    Description = "An online shop built with ASP.NET Core and EF Core.",
                    LiveUrl = "https://example.com/shop",
                    GitHubUrl = "https://github.com/mostafasaid/ecommerce",
                    ImageUrl = "/images/projects/ecommerce.png",
                    CreatedAt = new DateTime(2023, 02, 15)
                },
                new Project
                {
                    Id = 3,
                    Title = "Blog Platform",
                    Description = "A full-featured blog platform with authentication and CRUD operations.",
                    LiveUrl = "https://example.com/blog",
                    GitHubUrl = "https://github.com/mostafasaid/blog-platform",
                    ImageUrl = "/images/projects/blog.png",
                    CreatedAt = new DateTime(2023, 04, 05)
                },
                new Project
                {
                    Id = 4,
                    Title = "Task Manager App",
                    Description = "A productivity app for managing tasks with deadlines and priorities.",
                    LiveUrl = "https://example.com/tasks",
                    GitHubUrl = "https://github.com/mostafasaid/task-manager",
                    ImageUrl = "/images/projects/tasks.png",
                    CreatedAt = new DateTime(2023, 05, 20)
                },
                new Project
                {
                    Id = 5,
                    Title = "Weather Dashboard",
                    Description = "A real-time weather dashboard using public APIs and responsive UI.",
                    LiveUrl = "https://example.com/weather",
                    GitHubUrl = "https://github.com/mostafasaid/weather-dashboard",
                    ImageUrl = "/images/projects/weather.png",
                    CreatedAt = new DateTime(2023, 06, 10)
                }
            );

            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Development" },
                new Category { Id = 2, Name = "Design" },
                new Category { Id = 3, Name = "Productivity" },
                new Category { Id = 4, Name = "API Integration" }
            );

            // Seed BlogPosts
            builder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "Getting Started with ASP.NET Core",
                    Content = "ASP.NET Core is a cross-platform framework for building web apps...",
                    CreatedAt = new DateTime(2023, 03, 01),
                    FeaturedImageUrl = "/images/blog1.jpg", // must provide a value
                    IsPublished = true,
                    CategoryId = 1
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "UI/UX Design Principles",
                    Content = "Design is not just what it looks like and feels like. Design is how it works...",
                    CreatedAt = new DateTime(2023, 03, 10),
                    FeaturedImageUrl = "/images/blog1.jpg", // must provide a value
                    IsPublished = true,
                    CategoryId = 2
                },
                new BlogPost
                {
                    Id = 3,
                    Title = "Building a Task Manager App",
                    Content = "In this post, I walk through building a productivity app with ASP.NET Core...",
                    CreatedAt = new DateTime(2023, 04, 12),
                    FeaturedImageUrl = "/images/blog1.jpg", // must provide a value
                    IsPublished = true,
                    CategoryId = 3
                },
                new BlogPost
                {
                    Id = 4,
                    Title = "Integrating Weather APIs in .NET",
                    Content = "Learn how to fetch real-time weather data from external APIs and display it in your app...",
                    CreatedAt = new DateTime(2023, 05, 05),
                    FeaturedImageUrl = "/images/blog1.jpg", // must provide a value
                    IsPublished = true,
                    CategoryId = 4
                },
                new BlogPost
                {
                    Id = 5,
                    Title = "Creating a Blog Platform with ASP.NET Core",
                    Content = "Step-by-step guide to building a full-featured blog platform with authentication and CRUD...",
                    CreatedAt = new DateTime(2023, 06, 01),
                    FeaturedImageUrl = "/images/blog1.jpg", // must provide a value
                    IsPublished = true,
                    CategoryId = 1
                }
            );
        }
    }
}
