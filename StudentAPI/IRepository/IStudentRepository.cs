using StudentAPI.Models;

namespace StudentAPI.IRepository;

public interface IStudentRepository
{
    Task<List<Student>> GetStudents();

    Task<Student> GetStudent(Guid id);

    Task CreateStudent(Student student);

    Task UpdateStudent(Student student);

    Task DeleteStudent(Guid id);

    Task<List<Course>> GetCourses();

    Task<Course> GetCourse(string courseId);

    Task CreateCourse(Course course);

    Task UpdateCourse(Course course);

    Task DeleteCourse(string courseID);
}