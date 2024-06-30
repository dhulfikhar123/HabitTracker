using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HabitTracker.Models
{
    public class Habit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Weekly Goal")]
        [Required]
        [Range(1, 7)]
        public int WeeklyGoal { get; set; }
        [DisplayName("Current Count")]
        public int CurrentCount { get; set; }
        public bool IsGoalMet { get; set; }
        public DateTime NextReset { get; set; }
    }
}
