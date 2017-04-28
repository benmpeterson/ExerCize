using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Models
{
    public class ExerciseListItem
    {
        public int ExcerciseId { get; set; }
        public string Type { get; set; }
        public string Intensity { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }

        [Display(Name="Create")]
        public DateTimeOffset CreatedUTC { get; set; }

    }
}
