namespace oraclenhom3.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class contexts : DbContext
    {
        public contexts()
            : base("name=contexts")
        {
        }

        public virtual DbSet<CHITIETTRAM> CHITIETTRAMS { get; set; }
        public virtual DbSet<NUOC> NUOCS { get; set; }
        public virtual DbSet<TRAM> TRAMS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NUOC>()
                .HasMany(e => e.TRAMS)
                .WithRequired(e => e.NUOC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRAM>()
                .HasMany(e => e.CHITIETTRAMS)
                .WithRequired(e => e.TRAM)
                .WillCascadeOnDelete(false);
        }
    }
}
