using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudentAPI.IRepository;
using StudentAPI.Models;

namespace StudentAPI.Controllers;

[ApiController]
[Route("v1/api")]
public class StudentsController : ControllerBase
{

    private readonly IStudentRepository _repository;

    public StudentsController(IStudentRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    [Route("students/[action]")]
    public async Task<IActionResult> AddNewStudent([FromBody] Student student) 
    {
        if (ModelState.IsValid)
        {
            await _repository.CreateStudent(student);
            return Ok("Student Added Successfully!");
        }
        else 
        {
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(allErrors.ToList());
        }
    }

    [HttpGet]
    [Route("students/[action]")]
    public async Task<JsonResult> GetAllStudents() 
    {
        return new JsonResult(await _repository.GetStudents());
    }

    [HttpGet]
    [Route("students/[action]/{id}")]
    public async Task<JsonResult> GetStudent(Guid id) 
    {
        return new JsonResult(await _repository.GetStudent(id));
    }

    [HttpPut]
    [Route("students/[action]")]
    public async Task<IActionResult> UpdateStudent([FromBody] Student student)
    {
        if (ModelState.IsValid)
        {
            await _repository.UpdateStudent(student);
            return Ok("Student Added Successfully!");
        }
        else 
        {
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(allErrors.ToList());
        }
    }

    [HttpDelete]
    [Route("students/[action]/{id}")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        await _repository.DeleteStudent(id);
        return Ok("Student Deleted Successfully!");
    }

    [HttpPut]
    [Route("students/{studentID}/[action]")]
    public async Task<IActionResult> EnrollToCourse([FromRoute] Guid studentID, [FromBody] Course course)
    {
        Student student = await _repository.GetStudent(studentID);
        student.Courses.Add(course);
        await _repository.UpdateStudent(student);
        return Ok("Student enrolled to course!");
    }

    [HttpPut]
    [Route("students/{studentID}/[action]")]
    public async Task<IActionResult> AddCourseMark([FromRoute] Guid studentID, [FromBody] CourseMark mark)
    {
        Student student = await _repository.GetStudent(studentID);
        student.Marks.Add(mark);
        await _repository.UpdateStudent(student);
        return Ok("Student marks updated!");
    }


    [HttpPost]
    [Route("courses/[action]")]
    public async Task<IActionResult> AddNewCourse([FromBody] Course course) 
    {
        if (ModelState.IsValid)
        {
            await _repository.CreateCourse(course);
            return Ok("Course Added Successfully!");
        }
        else 
        {
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(allErrors.ToList());
        }
    }

    [HttpGet]
    [Route("courses/[action]")]
    public async Task<JsonResult> GetAllCourses() 
    {
        return new JsonResult(await _repository.GetCourses());
    }

    [HttpGet]
    [Route("courses/[action]/{courseID}")]
    public async Task<JsonResult> GetCourse(string courseID) 
    {
        return new JsonResult(await _repository.GetCourse(courseID));
    }

    [HttpPut]
    [Route("courses/[action]")]
    public async Task<IActionResult> UpdateCourse([FromBody] Course course)
    {
        await _repository.UpdateCourse(course);
        return Ok("Course Updated Successfully!");
    }

    [HttpDelete]
    [Route("courses/[action]/{courseID}")]
    public async Task<IActionResult> DeleteCourse(string courseID)
    {
        await _repository.DeleteCourse(courseID);
        return Ok("Course Deleted Successfully!");
    }

}
