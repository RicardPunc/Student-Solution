let course = null;
let id = location.href.split("/")[4]

await fetch("http://localhost:5000/v1/api/courses/GetCourse/" + id)
.then(response => response.json())
.then(data => course = data);

$("#courseID").val(course.courseID)
$("#name").val(course.name)
$("#description").val(course.description)

$(".updateeditcourse").click(function() {

    course.courseID= $("#courseID").val();
    course.name= $("#name").val();
    course.description= $("#description").val();

    fetch("http://localhost:5000/v1/api/courses/UpdateCourse", {
        method: "PUT",
        body: JSON.stringify(course),
        headers: { "Content-Type": "application/json" }
    }).then(response => (response.status == 400) ? response.json().then(data => {
        if (data.errors.CourseID) {
            $("#courseIDspan").text(data.errors.CourseID)
        }
        else {
            $("#courseIDspan").text("")
        }
        if (data.errors.Name) {
            $("#namespan").text(data.errors.Name)
        }
        else {
            $("#namespan").text("")
        }
        if (data.errors.Description) {
            $("#descriptionspan").text(data.errors.Description)
        }
        else {
            $("#descriptionspan").text("")
        }
    }) : CourseUpdated());
})

let CourseUpdated = function() {
    alert("Course Updated Successfully!");
    window.location.replace("/")
}

$(".deleteeditcourse").click(function() {
    fetch("http://localhost:5000/v1/api/courses/DeleteCourse/" + course.courseID, {
        method: "DELETE",
    }) 

    alert("Course Deleted Successfully!")
    window.location.replace("/")
})

$(".canceleditcourse").click(function() {
    window.location.replace("/")
})