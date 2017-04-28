using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Models
{
    class ExerciseEdit
    {
        public int ExerciseId { get; set; }
        public string Type { get; set; }
        public string Intensity { get; set; }
        public int Duration { get; set; }
    }
}
