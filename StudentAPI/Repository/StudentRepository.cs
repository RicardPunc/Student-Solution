using MongoDB.Bson;
using MongoDB.Driver;
using StudentAPI.IContext;
using StudentAPI.IRepository;
using StudentAPI.Models;

namespace StudentAPI.Repository;

public class StudentRepository : IStudentRepository
{

    private readonly IStudentContext _context;

    public StudentRepository(IStudentContext context)
    {
        _context = context;
    }

    public async Task CreateCourse(Course course)
    {
        FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(c => c.CourseID, course.CourseID);

        Course oldCourse = await _context.Courses.Find(filter).FirstOrDefaultAsync();

        if (oldCourse == null) {
            await _context.Courses.InsertOneAsync(course);
        }
    }

    public async Task CreateStudent(Student student)
    {
        FilterDefinition<Student> filter = Builders<Student>.Filter.Eq(s => s.StudentID, student.StudentID);

        Student oldStudent = await _context.Students.Find(filter).FirstOrDefaultAsync();

        if (oldStudent == null) {
            await _context.Students.InsertOneAsync(student);
        }
    }

    public async Task DeleteCourse(string courseID)
    {
        FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(c => c.CourseID, courseID);

        Course oldCourse = await _context.Courses.FindOneAndDeleteAsync(filter);

        List<Student> students = await _context.Students.Find(new BsonDocument()).ToListAsync();

        foreach (Student student in students)
        {
            List<CourseMark> marks = student.Marks.Where(x => x.CourseID == courseID).ToList();
            
            foreach (CourseMark mark in marks)
            {
                student.Marks.Remove(mark);
            }

            await UpdateStudent(student);
        }

        
    }

    public async Task DeleteStudent(Guid id)
    {
        FilterDefinition<Student> filter = Builders<Student>.Filter.Eq(s => s.StudentID, id);

        await _context.Students.FindOneAndDeleteAsync(filter);
    }

    public async Task<Course> GetCourse(string courseId)
    {
        FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(c => c.CourseID, courseId);

        return await _context.Courses.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Course>> GetCourses()
    {
        return await _context.Courses.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Student> GetStudent(Guid id)
    {
         FilterDefinition<Student> filter = Builders<Student>.Filter.Eq(s => s.StudentID, id);

        return await _context.Students.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Student>> GetStudents()
    {
        return await _context.Students.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateCourse(Course course)
    {
        FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(c => c.CourseID, course.CourseID);

        Course oldCourse = await _context.Courses.FindOneAndReplaceAsync(filter, course);
    }

    public async Task UpdateStudent(Student student)
    {
        FilterDefinition<Student> filter = Builders<Student>.Filter.Eq(s => s.StudentID, student.StudentID);

        await _context.Students.FindOneAndReplaceAsync(filter, student);
    }
}