import { initDB } from "./initDB.js"
let students = [];
let courses = [];

//Add student button actions
$(".addstudent").hide();
$(".addstudentbtn").click(function() { $(".addstudent").show();});

$(".canceladdstudent").click(function() { $(".addstudent").hide();});
$(".confirmaddstudent").click(function() { AddStudent() });

//Add course button actions
$(".addcourse").hide();
$(".addcoursebtn").click(function() { $(".addcourse").show();});

$(".canceladdcourse").click(function() { $(".addcourse").hide();});
$(".confirmaddcourse").click(function() { AddCourse() });

await fetch("http://localhost:5000/v1/api/students/GetAllStudents")
.then(response => response.json())
.then(data => students = data);

if (students.length == 0) {
    initDB();
    setTimeout(() => {
        location.reload();
    }, 1000)
}


await fetch("http://localhost:5000/v1/api/courses/GetAllCourses")
.then(response => response.json())
.then(data => courses = data);

const table = document.createElement("table");
document.body.appendChild(table);

for (let i=0; i<=students.length; i++ ){

    let row = document.createElement("tr");

    for (let j=0; j<=courses.length; j++){

        let cell = document.createElement("td");

        if (j > 0){
            if (i==0) {
                cell.textContent = courses[j-1].name;
                cell.style.cursor = "pointer"
                cell.onclick = () => {
                    window.location.replace("/EditCourse/" + courses[j-1].courseID)
                }
            }
            else {
                let found = students[i-1].marks.find(x => x.courseID == courses[j-1].courseID);
                if (found) {
                    let index = students[i-1].marks.findIndex(x => x.courseID == courses[j-1].courseID)
                    cell.textContent = students[i-1].marks[index].mark;
                }
            }  
        }
        else {
            if (i!=0){
                cell.textContent = `${students[i-1].firstname} ${students[i-1].lastname}`
                cell.style.cursor = "pointer"
                cell.onclick = () => {
                    window.location.replace("/EditStudent/" + students[i-1].studentID)
                }
            }
        }

        row.appendChild(cell);
    }

    table.appendChild(row);
}

let AddStudent = function() {

    let student = {
        firstname: $("#firstname").val(),
        lastname: $("#lastname").val(),
        address: $("#address").val(),
        town: $("#city").val(),
        country: $("#state").val(),
        dateOfBirth: $("#dateofbirth").val()
    };



    fetch("http://localhost:5000/v1/api/students/AddNewStudent", {
        method: "POST",
        body: JSON.stringify(student),
        headers: { "Content-Type": "application/json" }
    }).then(response => (response.status == 400) ? response.json().then(data => {
        if (data.errors.Firstname) {
            $("#firstnamespan").text(data.errors.Firstname)
        }
        else {
            $("#firstnamespan").text("")
        }
        if (data.errors.Lastname) {
            $("#lastnamespan").text(data.errors.Lastname)
        }
        else {
            $("#lastnamespan").text("")
        }
        if (data.errors.Address) {
            $("#addressspan").text(data.errors.Address)
        }
        else {
            $("#addressspan").text("")
        }
        if (data.errors.Town) {
            $("#cityspan").text(data.errors.Town)
        }
        else {
            $("#cityspan").text("")
        }
        if (data.errors.Country) {
            $("#statespan").text(data.errors.Country)
        }
        else {
            $("#statespan").text("")
        }
    }) : StudentAdded());
}

let StudentAdded = function() {
    $(".addstudent").hide()
    alert("Student Added Successfully!")
    location.reload()
}

let AddCourse = function() {

    let course = {
        courseID: $("#code").val(),
        name: $("#name").val(),
        description: $("#description").val(),
    };



    fetch("http://localhost:5000/v1/api/courses/AddNewCourse", {
        method: "POST",
        body: JSON.stringify(course),
        headers: { "Content-Type": "application/json" }
    }).then(response => (response.status == 400) ? response.json().then(data => {
        if (data.errors.CourseID) {
            $("#codespan").text(data.errors.CourseID)
        }
        else {
            $("#codespan").text("")
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
    }) : CourseAdded());
}

let CourseAdded = function() {
    $(".addcourse").hide()
    alert("Course Added Successfully!")
    location.reload()
}