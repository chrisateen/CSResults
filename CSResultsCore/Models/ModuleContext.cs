using Microsoft.EntityFrameworkCore;


namespace CSResultsCore.Models
{
    public class ModuleContext:DbContext
    {
        public ModuleContext(DbContextOptions<ModuleContext> options) : base(options)
        {

        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>()
                .HasKey(r => new { r.moduleName,r.year });

            modelBuilder.Entity<Result>()
                .HasOne<Module>(r => r.Module)
            .WithMany(m => m.Results)
            .HasForeignKey(m => m.moduleID);
        }
    }
}
