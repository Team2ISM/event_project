using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using Foolproof;
using Events.Business.Models;
namespace team2project.Models
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            Id = Guid.NewGuid().ToString();
            Active = true;
        }

        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [ScaffoldColumn(false)]
        public string AuthorId { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(6000,MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public string TextDescription { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [GreaterThan("FromDate",ErrorMessage="Даты введены неправильно")]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        public string Location { get; set; }   

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        public bool Checked { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfCreation { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}