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

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина должна быть от 2 до 50 символов")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

    }
}