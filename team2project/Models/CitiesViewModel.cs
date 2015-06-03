using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI;
using team2project.Helpers;

namespace team2project.Models
{
    public class CitiesViewModel
    {
        [ScaffoldColumn(false)]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненым")]
        public virtual string Name { get; set; }

    }
}