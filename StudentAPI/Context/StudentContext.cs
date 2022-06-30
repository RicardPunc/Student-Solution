using MongoDB.Driver;
using StudentAPI.IContext;
using StudentAPI.Models;

namespace StudentAPI.Context;

public class StudentContext : IStudentContext
{

    public StudentContext()
    {
        var client = new MongoClient("mongodb://mongodb:27017");
        var database = client.GetDatabase("StudentsDB");

        Students = database.GetCollection<Student>("Students");
        Courses = database.GetCollection<Course>("Courses");
    }
    public IMongoCollection<Student> Students { get; }

    public IMongoCollection<Course> Courses { get; }
}