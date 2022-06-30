using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentAPI.Models;

[BsonIgnoreExtraElements]
public class Student
{
    public Guid StudentID { get; set; }

    [Required(ErrorMessage = "First Name is required!")]
    [RegularExpression("[a-zA-Z]+", ErrorMessage = "First Name cannot have numbers or symbols!")]
    [MaxLength(30, ErrorMessage = "First Name must not be longer than 30 characters!")]
    public String Firstname { get; set; }

    [Required(ErrorMessage = "Last Name is required!")]
    [RegularExpression("[a-zA-Z]+", ErrorMessage = "Last Name cannot have numbers or symbols!")]
    [MaxLength(30, ErrorMessage = "Last Name must not be longer than 30 characters!")]
    public String Lastname { get; set; }

    [Required(ErrorMessage = "Address is required!")]
    [MaxLength(50, ErrorMessage = "Address must not be longer than 50 characters!")]
    public String Address { get; set; }

    [Required(ErrorMessage = "Town name is required!")]
    [RegularExpression("[a-zA-Z]+", ErrorMessage = "Town name cannot have numbers or symbols!")]
    [MaxLength(50, ErrorMessage = "Town name must not be longer than 50 characters!")]
    public String Town { get; set; }

    [Required(ErrorMessage = "Country name is required!")]
    [RegularExpression("[a-zA-Z]+", ErrorMessage = "Country name cannot have numbers or symbols!")]
    [MaxLength(40, ErrorMessage = "Country name must not be longer than 40 characters!")]
    public String Country { get; set; }

    [Required(ErrorMessage = "Birth Date is required!")]
    public DateTime DateOfBirth { get; set; }

    [RegularExpression("m|f|M|F|", ErrorMessage = "Please enter sex in correct format (m/f).")]
    public string? Sex { get; set; }

    public List<Course> Courses { get; set; }

    public List<CourseMark> Marks { get; set; }

    public Student(string firstname,
                    string lastname,
                    string address,
                    string town,
                    string country,
                    DateTime dateOfBirth,
                    string? sex)
    {
        StudentID = Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Address = address;
        Town = town;
        Country = country;
        DateOfBirth = dateOfBirth;
        Sex = sex;
        Courses = new List<Course>();
        Marks = new List<CourseMark>(); 
    }


}
