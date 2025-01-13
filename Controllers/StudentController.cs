using Microsoft.AspNetCore.Mvc;
using STUDB.Models;

namespace StudentCRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly STUDENTDBContext _context;

        public StudentController(STUDENTDBContext context)
        {
            _context = context;
        }

        // Index Action to Display All Students
        public IActionResult Index()
        {
            try
            {
                var students = _context.Students?.ToList() ?? new List<Student>();
                return View("~/Views/Home/Index.cshtml", students); // Reference the Home/Views path
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving students: {ex.Message}");
                return View("~/Views/Home/Index.cshtml", new List<Student>()); // Handle errors gracefully
            }
        }

        // Render the Create View
        public IActionResult Create()
        {
            return View("~/Views/Home/Create.cshtml"); // Explicitly specify the Create view path
        }

        // Handle Form Submission for Create
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Students.Add(student);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating student: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while saving the student.");
                }
            }
            return View("~/Views/Home/Create.cshtml", student);
        }

        // Render the Edit View
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View("~/Views/Home/Edit.cshtml", student); // Reference the Home/Views path
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/Home/Edit.cshtml", student); // Reference the Home/Views path
        }

        // Handle Deleting a Student
        public IActionResult Delete(int id)
        {
            try
            {
                var student = _context.Students.Find(id);
                if (student != null)
                {
                    _context.Students.Remove(student);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting student: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
