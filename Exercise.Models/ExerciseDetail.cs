using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Models
{
    public class ExerciseDetail
    {
        [Key]
        public int ExerciseId { get; set; }
        public string Type { get; set; }
        public string Intensity { get; set; }
        public double Duration { get; set; }

        public double CaloritesBurned { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
