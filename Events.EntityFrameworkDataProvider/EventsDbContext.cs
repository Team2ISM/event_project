using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Data.Entity;

namespace Events.EntityFrameworkDataProvider
{
     public partial class EventsDbContext<TModel>: DbContext
         where TModel : class
    {
         public EventsDbContext()
             : base("name=EntityFrameworkConnection")
        {
        }

         public virtual DbSet<TModel> Event { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
