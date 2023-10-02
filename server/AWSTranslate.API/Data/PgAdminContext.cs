using AWSTranslate.API.Model.Postgres;
using Microsoft.EntityFrameworkCore;

namespace AWSTranslate.API.Data
{
    public class PgAdminContext : DbContext
    {
        public PgAdminContext(DbContextOptions<PgAdminContext> options): base(options)
        {
                
        }

        public DbSet<EnglishTranslated> EnglishTranslatedWords { get; set; }
        public DbSet<HindiTranslated> HindiTranslatedWords { get; set; }
        public DbSet<MarathiTranslated> MarathiTranslatedWords { get; set; }
        public DbSet<TamilTranslated> TamilTranslatedWords { get; set; }
        public DbSet<TeluguTranslated> TeluguTranslatedWords { get; set; }
    }
}
