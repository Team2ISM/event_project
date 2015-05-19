using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team2project.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        [Compare("Password")]
        public string RepeatPassword { get; set; }

        [Required]
        public string Location { get; set; }

        public UserViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}