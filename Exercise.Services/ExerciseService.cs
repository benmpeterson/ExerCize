using Exercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Data;


namespace Exercise.Services
{
    public class ExerciseService
    {
        private readonly Guid _userId;

        public ExerciseService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateExercise(ExerciseCreate model)
        {
            var entity =
                new Workout()
                {
                    OwnerId = _userId,
                    Type = model.Type,
                    Intensity = model.Intensity,
                    Duration = model.Duration,
                    CaloriesBurned = 0,
                    CreatedUtc = DateTimeOffset.UtcNow,
                };

            if (entity.Type == "Bicycling" && entity.Intensity == "Low")
            {

                entity.CaloriesBurned = entity.Duration * .0175 * 6.0;  
                
            }

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Workouts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ExerciseListItem> GetWorkouts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Workouts
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e => new ExerciseListItem
                            {
                                ExcerciseId = e.ExcerciseId,
                                Type = e.Type,
                                Intensity = e.Intensity,
                                Duration = e.Duration,
                                CaloriesBurned = e.CaloriesBurned,
                                CreatedUTC = e.CreatedUtc,
                            }
                            );
                return query.ToArray();
            }
        }

        public ExerciseDetail GetWorkoutById(int exerciseId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Workouts
                        .Single(e => e.ExcerciseId == exerciseId && e.OwnerId == _userId);
                return
                    new ExerciseDetail
                    {
                        ExerciseId = entity.ExcerciseId,
                        Type = entity.Type,
                        Intensity = entity.Intensity,
                        Duration = entity.Duration,
                        CaloritesBurned = entity.CaloriesBurned,
                        Created = entity.CreatedUtc,                       
                    };
            }
        }
    }
}