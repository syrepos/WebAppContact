using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactWebApp.Models
{
    public class State
    {
        [Key]
       
        public int Id { get; set; }

        [Display(Name ="State")]
        [Required(ErrorMessage ="Name of state is required")]
        [StringLength(ContactWebConstants.MAX_STATE_NAME_LENGTH)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The state abbreviation is required")]
        [StringLength(ContactWebConstants.MAX_STATE_ABBR_LENGTH)]
        public string Abbreviation { get; set; }
    }
}
