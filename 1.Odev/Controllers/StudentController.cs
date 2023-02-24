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
            - İlk hafta geliştirdiğiniz api kullanılacaktır.
            - Rest standartlarına uygun olmalıdır.
            - solid prensiplerine uyulmalıdır.
            - Fake servisler geliştirilerek Dependency injection kullanılmalıdır.
            - Apinizde kullanılmak üzere extension geliştirin.
            - Projede swagger implementasyonu gerçekleştirilmelidir.
            - Global loglama yapan bir middleware(sadece actiona girildi gibi çok basit düzeyde)
            Bonus
            - Fake bir kullanıcı giriş sistemi yapın ve custom bir attribute ile bunu kontrol edin.
            - Global exception middleware i oluşturun
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
            if (student == null)
            {
                return NotFound();
            }

            student.Name = studentDto.Name != default ? studentDto.Name : student.Name;
            student.BirthYear = studentDto.BirthYear != default ? studentDto.BirthYear : student.BirthYear;
            student.Surname = studentDto.Surname != default ? studentDto.Surname : student.Surname;

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

            student.Name = name != default ? name : student.Name;

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
