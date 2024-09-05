using Microsoft.EntityFrameworkCore;

namespace DotNetLogReg.Models
{
    public class MydbContext : DbContext
    {
        public MydbContext(DbContextOptions<MydbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        //lic DbSet<User> Voting { get; set; }
        
    }
}
