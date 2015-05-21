using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace team2project.Models
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            if (this.Id == null)
            {
                this.Id = Guid.NewGuid().ToString();
            }

            if (AuthorId == null)
            {
                AuthorId = "anon";
            }
        }

        public CommentViewModel(string eventId)
        {
            this.Id = Guid.NewGuid().ToString();
            EventId = eventId;
            AuthorId = "anon";
        }

        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [ScaffoldColumn(false)]
        public string EventId { get; set; }

        [StringLength(50)]
        [DataType(DataType.Text)]
        public string AuthorName { get; set; }

        public virtual string AuthorId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Lenght must be between 2 and 50")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

    }
}