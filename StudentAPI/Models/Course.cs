using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentAPI.Models;

[BsonIgnoreExtraElements]
public class Course
{

    [Required(ErrorMessage = "Course ID is required!")]
    [MaxLength(20, ErrorMessage = "Course ID must not be longer than 20 characters!")]
    public String CourseID { get; set; }

    [Required(ErrorMessage = "Course name is required!")]
    [MaxLength(40, ErrorMessage = "Course name must not be longer than 40 characters!")]
    public String Name { get; set; }

    [Required(ErrorMessage = "Description is required!")]
    [MinLength(20, ErrorMessage = "Description must have at least 20 characters!")]
    [MaxLength(300, ErrorMessage = "Description must not be longer than 300 characters!")]
    public String Description { get; set; }

    public Course(string courseId, string name, string description)
    {
        CourseID = courseId;
        Name = name;
        Description = description;
    }
}
