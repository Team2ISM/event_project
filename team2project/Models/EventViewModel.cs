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
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required")]
        public DateTime? ToDate { get; set; }

        [Required (ErrorMessage = "This field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Lenght must be between 2 and 50")]
        public string Location { get; set; }

        [Required (ErrorMessage = "This field is required")]
        [DataType(DataType.MultilineText)]
        public string LongDescription { get; set; }

    }
}