using Microsoft.EntityFrameworkCore;
using ReviewApp.Models;

namespace ReviewApp.Context
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options) { }

        public DbSet<Category?> Categories { get; set; }
        public DbSet<Country?> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon?> Pokemons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }


            // many to many Pokemon-Category
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pc => new{ pc.CategoryId, pc.PokemonId });
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(c  => c.PokemonId);
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(c => c.Category)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(c => c.CategoryId);

            // many to many Pokemon-Owner
            modelBuilder.Entity<PokemonOwner>()
                .HasKey(pc => new { pc.OwnerId, pc.PokemonId });
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(o => o.Owner)
                .WithMany(po => po.PokemonOwner)
                .HasForeignKey(o => o.OwnerId);
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(po => po.PokemonOwner)
                .HasForeignKey(p => p.PokemonId);
        }
    }
}
