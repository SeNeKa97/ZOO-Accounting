namespace ZOO_Accounting.Model.Concrete
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    using ZOO_Accounting.Model.Entities;

    public partial class EFDbContext : DbContext
    {
        public EFDbContext(): base("name=EFDbContext")
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalKind> AnimalKinds { get; set; }
        public virtual DbSet<Aviary> Aviaries { get; set; }
        public virtual DbSet<AviaryType> AviaryTypes { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventoryType> InventoryTypes { get; set; }
        public virtual DbSet<Ration> Rations { get; set; }
        public virtual DbSet<LogRecord> Log { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .Property(e => e.RegistryCode)
                .IsFixedLength();

            modelBuilder.Entity<Animal>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AnimalKind>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<AnimalKind>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Aviary>()
                .Property(e => e.Code)
                .IsFixedLength();

            modelBuilder.Entity<Aviary>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.RegistryCode)
                .IsFixedLength();

            modelBuilder.Entity<Inventory>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<InventoryType>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<InventoryType>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<LogRecord>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder.Entity<Animal>()
                .HasOptional<Aviary>(e => e.Aviary)
                .WithMany(e => e.Animals)
                .HasForeignKey(e => e.AviaryId)
                .WillCascadeOnDelete(true);
             
            modelBuilder.Entity<Inventory>()
                .HasOptional<Aviary>(e => e.Aviary)
                .WithMany(e => e.Inventories)
                .HasForeignKey(e => e.AviaryId)
                .WillCascadeOnDelete(true);
        }
    }

   
}