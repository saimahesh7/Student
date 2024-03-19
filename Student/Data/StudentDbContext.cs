using Microsoft.EntityFrameworkCore;
using Student.Models.Domain;

namespace Student.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Students> Students { get; set; }
    }
}
