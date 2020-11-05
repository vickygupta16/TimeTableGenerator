namespace TTG3.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TTG_DB_Entities : DbContext
    {
        public TTG_DB_Entities()
            : base("name=TTG_DB_Entities")
        {
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .Property(e => e.Room)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Subject_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Professor_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Visiting_Faculty)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Professor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .Property(e => e.Subject_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Subject_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Subject_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);
        }
    }
}
