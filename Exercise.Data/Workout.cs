using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Data
{
    public class Workout
    {
        [Key]
        public int ExcerciseId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public double OwnerWeight { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Intensity { get; set; }

        [Required]
        public double Duration { get; set; }

        public double CaloriesBurned { get; set; }

        //Carries timezone info with it
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        //If the excercise is modified
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

