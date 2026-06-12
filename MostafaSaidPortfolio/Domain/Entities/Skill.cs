namespace MostafaSaidPortfolio.Domain.Entities
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = "Other";
        public int Proficiency { get; set; } = 80;
        public string IconName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static readonly string[] ValidCategories =
            { "Frontend", "Backend", "DevOps", "Design", "Tools", "Other" };
    }
}
