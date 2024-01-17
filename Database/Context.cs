using Kinmatch.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Kinmatch.Database
{
    public class Context : DbContext
    {
        public DbSet<Profile> profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string server = Config.DB.SERVER;
            string database = Config.DB.DATABASE;
            string uid = Config.DB.UID;
            string password = Config.DB.PASSWORD;
            string port = Config.DB.PORT;
            string connectionString = "SERVER=" + server + "; DATABASE=" +
            database + "; UID=" + uid + "; PASSWORD=" + password + "; PORT=" + port + ";";
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 33))
            );
        }
    }
}
