using Microsoft.EntityFrameworkCore;
using SEP_Web.Models;

namespace SEP_Web.Database
{
    public class SEP_Context : DbContext
    {

        public SEP_Context(DbContextOptions<SEP_Context> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=sep_DB;uid=gianluca.vialli;password=Ann@1170615;SslMode=Preferred;ConvertZeroDateTime=True;pooling=no");
        }

        public DbSet<Users> Users { get; set; }
        
    }
}