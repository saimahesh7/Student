using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Student.Data
{
    public class StudentAuthDbContext : IdentityDbContext
    {
        public StudentAuthDbContext(DbContextOptions<StudentAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var writerRoleId = "01630216-faf2-4d16-86ec-4f29493817ab";
            var readerRoleId = "6829c18f-976b-407f-bb8a-27ec32dae29c";
            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="reader".ToUpper(),
                },
                new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
