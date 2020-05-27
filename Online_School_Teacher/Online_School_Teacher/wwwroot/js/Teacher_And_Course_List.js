var CourseList;

$(document).ready(function () {
    loadCourseTable();
});

function loadCourseTable() {
    CourseList = $('#CourseTable').DataTable({
        "ajax": {
            "url": "/api/Course/CourseList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "title", "width": "10%" },
            { "data": "description", "width": "80%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Teachers/Answer_The_Qus/${data}" class='btn btn-success text-white' style='cursor:pointer; width:90px;'>
                            Answer
                        </a>

                        </div>`;
                }, "width": "70%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}