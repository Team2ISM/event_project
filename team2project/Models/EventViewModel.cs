using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
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
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Lenght must be between 3 and 50")]
        public string Title { get; set; }

        [Required (ErrorMessage = "This field is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Lenght must be between 3 and 50")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ToDate { get; set; }

        [Required (ErrorMessage = "This field is required")]
        public string Location { get; set; }

        [Required (ErrorMessage = "This field is required")]
        [DataType(DataType.MultilineText)]
        public string LongDescription { get; set; }

    }
}