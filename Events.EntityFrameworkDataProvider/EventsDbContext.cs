using Events.Business.Models;
using System.Data.Entity;

namespace Events.EntityFrameworkDataProvider
{
     public partial class EventsDbContext: DbContext
    {
         public EventsDbContext()
             : base("name=EntityFrameworkConnection")
        {
        }

         public virtual DbSet<EventModel> Event { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
