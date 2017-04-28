using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Models
{
    public class ExerciseCreate
    {
        [Required]        
        public string Type { get; set; }
        
        [Required]        
        public string Intensity { get; set; }

        [Required]        
        public int Duration { get; set; }
    }
}
