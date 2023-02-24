using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.Odev.Base;
using _1.Odev.Dtos;
using _1.Odev.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.Odev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstController : ControllerBase
    {
        /*
        + GET,POST,PUT,DELETE,PATCH methodları kullanılmalıdır.
        + Http status code standartlarına uyulmalıdır. Error Handler ile 500, 400, 404, 200, 201 hatalarının standart format ile verilmesi
        + Modellerde zorunlu alanların kontrolü yapılmalıdır.
        + Routing kullanılmalıdır.
        + Model binding işlemleri hem body den hemde query den yapılacak şekilde örneklendirilmelidir. Bonus:
        + Standart crud işlemlerine ek olarak, listeleme ve sıralama işlevleride eklenmelidir. Örn: /api/first/list?name=abc
        */
        private static List<Student> _students = new List<Student>()
        {
            new Student(1, "Halil1", "Özler", new DateTime(1999, 4,28)),
            new Student(2, "Halil2", "Özler2", new DateTime(2000, 4, 28)),
            new Student(3, "Halil3", "Özler3", new DateTime(2005, 4, 28)),
            new Student(4, "Halil4", "Özler4", new DateTime(2003, 4, 28))
        };
        private static int count = 4;

        public FirstController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new CommonResponse<List<Student>>(_students));
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(StudentDto studentDto)
        {
            count++;
            Student student = new Student(count, studentDto.Name, studentDto.Surname, studentDto.BirthYear);
            _students.Add(student);
            return Created("student", new CommonResponse<Student>(student));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] StudentDto studentDto, int id)
        {
            var student = _students.Find(x => x.Id == id);
            if(student == null)
            {
                return NotFound();
            }
            Student newStudent = new Student(student.Id, studentDto.Name, studentDto.Surname, studentDto.BirthYear);
            _students.Remove(student);
            _students.Add(newStudent);
            return Ok(new CommonResponse<Student>(student));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] string name, int id)
        {
            //Patch is like a put but can be used in a different change.
            var student = _students.Find(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            Student newStudent = new Student(student.Id, name, student.Surname, student.BirthYear);
            _students.Remove(student);
            _students.Add(newStudent);
            return Ok(new CommonResponse<Student>(student));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleted(int id)
        {
            var student = _students.Find(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            _students.Remove(student);
            return Ok();
        }

        [HttpGet("/list")]
        public async Task<IActionResult> Crud([FromQuery] string name)
        {
            if(name.Length < 2)
            {
                return BadRequest("name must be greater than 2 digits");
            }
            var student = _students.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(new CommonResponse<Student>(student));
        }
    }
}
