using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI;
namespace team2project.Models
{
    public class EventViewModel
    {
        public EventViewModel() 
        {
            Id = Guid.NewGuid().ToString();
            FromDate = DateTime.Now.AddDays(1);
            ToDate = DateTime.Now.AddDays(1);
            Active = true;
        }

        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина должна быть от 2 до 50 символов")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [DataType(DataType.MultilineText)]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public bool Checked { get; set; }
    }
}