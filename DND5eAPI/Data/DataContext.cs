using DND5eAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DND5eAPI.Data
{
    public class DataContext : DbContext
    { // this represents a session with the database and can be used
      // to query and save instances of your entities.

        public DataContext(DbContextOptions<DbContext> options) : base(options)         
        {

        }

        // https://www.connectionstrings.com/sql-server/

        public DbSet<Spell> Spells { get; set; }

    }
}
