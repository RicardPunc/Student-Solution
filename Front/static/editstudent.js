let student = null;
let id = location.href.split("/")[4]

await fetch("http://localhost:5000/v1/api/students/GetStudent/" + id)
.then(response => response.json())
.then(data => student = data);

var day = "0" + student.dateOfBirth.split("-")[2];
var month = "0" + student.dateOfBirth.split("-")[1];
var year = "0" + student.dateOfBirth.split("-")[0];

var date = year+"-"+(month)+"-"+(day) ;

$("#firstname").val(student.firstname)
$("#lastname").val(student.lastname)
$("#address").val(student.address)
$("#city").val(student.town)
$("#state").val(student.country)
$("#dateofbirth").val(student.dateOfBirth.split("T")[0])

$(".updateeditstudent").click(function() {

    student.firstname= $("#firstname").val();
    student.lastname= $("#lastname").val();
    student.address= $("#address").val();
    student.town= $("#city").val();
    student.country= $("#state").val();
    student.dateOfBirth= $("#dateofbirth").val();

    fetch("http://localhost:5000/v1/api/students/UpdateStudent", {
        method: "PUT",
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
    }) : StudentUpdated());
})

let StudentUpdated = function() {
    alert("Student Updated Successfully!");
    window.location.replace("/")
}

$(".deleteeditstudent").click(function() {
    fetch("http://localhost:5000/v1/api/students/DeleteStudent/" + student.studentID, {
        method: "DELETE",
    }) 

    alert("Student Deleted Successfully!")
    window.location.replace("/")
})

$(".canceleditstudent").click(function() {
    window.location.replace("/")
})