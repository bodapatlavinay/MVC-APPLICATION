using Microsoft.AspNetCore.Mvc;
using STUDB.Models;

namespace STUDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentApiController : ControllerBase
    {
        private readonly STUDENTDBContext _context;

        public StudentApiController(STUDENTDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student data is null.");
            }

            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null || id != updatedStudent.Id)
            {
                return BadRequest("Invalid student data.");
            }

            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Name = updatedStudent.Name;
            student.Email = updatedStudent.Email;
            student.Age = updatedStudent.Age;
            student.Address = updatedStudent.Address;

            _context.Students.Update(student);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
