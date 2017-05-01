using Exercise.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Services.HelperMethods
{
    class CalorieCalculator
    {
        public static void GetCalories(Workout entity)
        {

            if (entity.Type == "Bicycling" && entity.Intensity == "Low")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 6.0 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Bicycling" && entity.Intensity == "High")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 10 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Dancing" && entity.Intensity == "Low")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 5.0 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Dancing" && entity.Intensity == "High")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 7.0 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Running" && entity.Intensity == "Low")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 10.0 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Running" && entity.Intensity == "High")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 13.0 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Swimming" && entity.Intensity == "Low")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 6 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Swimming" && entity.Intensity == "High")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 10.0 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Walking" && entity.Intensity == "Low")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 3.5 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
            else if (entity.Type == "Walking" && entity.Intensity == "High")
            {
                entity.CaloriesBurned = entity.Duration * .0175 * 5.0 * (entity.OwnerWeight / 2.2);
                entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
            }
        }

    }
}

