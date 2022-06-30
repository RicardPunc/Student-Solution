using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models;

public class CourseMark
{
    [Required(ErrorMessage = "Course ID is required!")]
    [MaxLength(20, ErrorMessage = "Course ID must not be longer than 20 characters!")]
    public String CourseID { get; set; }

    [Required(ErrorMessage = "Course Mark is required!")]
    [Range(6, 10, ErrorMessage = "Mark must be between 6 and 10!")]
    public int Mark { get; set; }

    public CourseMark(string courseID, int mark)
    {
        CourseID = courseID;
        Mark = mark;
    }
}
