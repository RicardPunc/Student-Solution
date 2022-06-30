using MongoDB.Driver;
using StudentAPI.Models;

namespace StudentAPI.IContext;

public interface IStudentContext
{
    IMongoCollection<Student> Students { get; }

    IMongoCollection<Course> Courses { get; }
}