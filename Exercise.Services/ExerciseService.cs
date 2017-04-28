using Exercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Data;
using Exercise.Models;

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
                    CreatedUtc = DateTimeOffset.UtcNow,
                };
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
    }
}