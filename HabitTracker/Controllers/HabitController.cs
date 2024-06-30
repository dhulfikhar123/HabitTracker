using HabitTracker.Data.Context;
using HabitTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Controllers
{
    public class HabitController : Controller
    {
        private readonly HabitDbContext _context;
        public HabitController(HabitDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Habit> habitsList = _context.Habits;
            ResetCount(habitsList);
            return View(habitsList);
        }
    

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Habit habit)
        {
            if (ModelState.IsValid)
            {
                habit.NextReset = DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek);
                _context.Habits.Add(habit);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(habit);
        }

        public IActionResult Track(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _context.Habits.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            obj.CurrentCount++;

            if (obj.CurrentCount >= obj.WeeklyGoal)
            {
                obj.IsGoalMet = true;
            }

            _context.Habits.Update(obj);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _context.Habits.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Habit habit)
        {
            if (ModelState.IsValid)
            {
                _context.Habits.Update(habit);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(habit);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _context.Habits.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Habits.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Habits.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ResetCount(IEnumerable<Habit> habitsList)
        {
            var date = DateTime.Today.Date;
            foreach (Habit habit in habitsList)
            {
                if (DateTime.Compare(date, habit.NextReset) >= 0)
                {
                    habit.NextReset = DateTime.Now.AddDays(7 - (int)date.DayOfWeek);
                    habit.CurrentCount = 0;
                    _context.Habits.Update(habit);
                }
            }
            _context.SaveChanges();

        }
    }
}
