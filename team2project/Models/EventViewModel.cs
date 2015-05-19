using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI;
using System.Windows.Forms;
namespace team2project.Models
{
    public class EventViewModel
    {
        public EventViewModel() 
        {
            Id = Guid.NewGuid().ToString();
           // From = new DateTime();
            //To = DateTime.Now.AddDays(1);
        }

        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required (ErrorMessage="This field is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Lenght must be between 3 and 50")]
        public string Title { get; set; }

        [Required (ErrorMessage = "This field is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Lenght must be between 3 and 50")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }

        [Required (ErrorMessage = "This field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Lenght must be between 2 and 50")]
        public string Location { get; set; }

        [Required (ErrorMessage = "This field is required")]
        [DataType(DataType.MultilineText)]
        public string LongDescription { get; set; }

    }
}