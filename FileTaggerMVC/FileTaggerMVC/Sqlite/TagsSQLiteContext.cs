using FileTaggerMVC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace FileTaggerMVC.Sqlite
{
    public class TagsSQLiteContext : DbContext
    {
        public TagsSQLiteContext(string connectionString) : base(new SQLiteConnection() { ConnectionString = connectionString }, true) { }

        public DbSet<TagType> TagTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<TagMap> TagMaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties()
                        .Where(p => p.Name == "Id")
                        .Configure(p => p.IsKey().HasColumnName("Id"));
        }
    }
}