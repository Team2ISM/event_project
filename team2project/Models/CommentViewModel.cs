using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace team2project.Models
{
    public class CommentViewModel
    {
        public CommentViewModel(string eventId =  null)
        {
            this.Id = Guid.NewGuid().ToString();
            EventId = eventId;
        }

        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [ScaffoldColumn(false)]
        public string EventId { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(200)]
        [DataType(DataType.Text)]
        public string AuthorName { get; set; }

        public virtual string AuthorId { get; set; }

        [ScaffoldColumn(false)]
        public virtual DateTime PostingTime { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Длина должна быть от 2 до 1000 символов")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

    }
}