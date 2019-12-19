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
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abreviation { get; set; }
    }
}
