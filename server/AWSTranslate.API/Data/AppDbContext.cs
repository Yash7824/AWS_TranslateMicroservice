using AWSTranslate.API.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace AWSTranslate.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
                
        }

        public DbSet<EnglishTranslated> EnglishTranslatedWords { get; set; }
        public DbSet<HindiTranslated> HindiTranslatedWords { get; set; }
        public DbSet<MarathiTranslated> MarathiTranslatedWords { get; set; }
        public DbSet<TamilTranslated> TamilTranslatedWords { get; set; }
        public DbSet<TeluguTranslated> TeluguTranslatedWords { get; set; }
    }
}
