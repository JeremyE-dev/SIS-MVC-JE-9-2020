using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class State
    {
        [Required(ErrorMessage = "State is required")]
        public string StateAbbreviation { get; set; }

       
        public string StateName { get; set; }
    }
}