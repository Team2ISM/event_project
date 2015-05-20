using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI;

namespace team2project.Models
{
    public class CitiesViewModel
    {
        [ScaffoldColumn(false)]
        public virtual string Id { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина должна быть от 2 до 50 символов")]
        public virtual string City { get; set; }
    }
}