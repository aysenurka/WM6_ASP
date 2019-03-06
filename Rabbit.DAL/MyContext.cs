using Rabbit.Models.Entities;
using System;
using System.Data.Entity;

namespace Rabbit.DAL
{
    public class MyContext : DbContext
    {
        public MyContext() : base("name=MyCon")
        {
            this.InstanceDate = DateTime.Now;
        }

        public DateTime InstanceDate { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<MailLog> MailLogs { get; set; }
    }
}
