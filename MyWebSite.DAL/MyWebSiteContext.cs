using System.Data.Entity;
using MyWebSite.DAL.MyEntities;

namespace MyWebSite.DAL
{
    public class MyWebSiteContext : DbContext
    {
        public MyWebSiteContext()
        {

        }

        public DbSet<User> User { get; set; }
    }
}
