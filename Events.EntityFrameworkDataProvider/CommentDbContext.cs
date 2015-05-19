using Events.Business.Models;
using System.Data.Entity;

namespace Events.EntityFrameworkDataProvider
{
     public partial class CommentDbContext: DbContext
    {
         public CommentDbContext()
             : base("name=EntityFrameworkConnection")
        {

        }

         public virtual DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
