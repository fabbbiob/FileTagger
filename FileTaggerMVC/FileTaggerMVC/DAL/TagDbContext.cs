using FileTaggerMVC.Models;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace FileTaggerMVC.DAL
{
    public class TagDbContext : DbContext
    {
        public DbSet<TagType> TagTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<TagMap> TagMaps { get; set; }

        public TagDbContext() : base("ConnectionStringName") { } //TODO

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<TagDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}