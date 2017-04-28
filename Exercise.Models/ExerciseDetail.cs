using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Models
{
    public class ExerciseDetail
    {
        public int ExerciseId { get; set; }
        public string Type { get; set; }
        public string Intensity { get; set; }
        public int Duration { get; set; }

        public int CaloritesBurned { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
