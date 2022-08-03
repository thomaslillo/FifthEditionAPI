using DND5eAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DND5eAPI
{
    public class AppDataContext : DbContext
    { // this represents a session with the database and can be used
      // to query and save instances of your entities.


        // overriding the method to read the connection string of the database from appsettings.json
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the app settings
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            var connectionString = configuration.GetConnectionString("AppDBString");
            optionsBuilder.UseSqlServer(connectionString);

        }

        // this constructor passes an instance of the required DbContextOptions class
        // to the DbContext object - the <> specifies that the generic parameters should apply
        // to the DbContext class - the options obj is then passed to the base class
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }

        // the DbSet of Spell - the Spells object will be used to query and save
        // instances of the Spell class - the LINQ queries against this will be translated
        // into actual database queries
        public DbSet<Spell> Spells { get; set; }


        //https://stackoverflow.com/questions/69472240/asp-net-6-identity-sqlite-services-adddbcontext-how

    }
}