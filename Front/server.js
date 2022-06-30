const path = require('path');
const app = require("./app")
const port = 3000;

app.get("/", (req, res) => {
    res.status(200);
})

app.get("/EditStudent/:studentID", (req, res) => {
    res.sendFile(__dirname + "/static/editStudent.html")
})

app.get("/EditCourse/:studentID", (req, res) => {
    res.sendFile(__dirname + "/static/editCourse.html")
})


app.listen(port, () => console.log(`The server is listening on port ${port}`))