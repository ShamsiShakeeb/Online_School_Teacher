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
                        <button id="Delete" value="${data}" class='btn btn-success text-white' style='cursor:pointer; width:90px;'>
                            Delete
                        </button>

                        <a href="/Admin/From_Core_Update_Course/${data}"  class='btn btn-success text-white' style='cursor:pointer; width:90px;'>
                            Update
                        </a>
                      
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}
$('body').on('click', '#Delete', function () {

    swal({
        title: "Are you sure?",
        text: "Want to Delete This Course",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {

            var value = {
                title: $("#Delete").val(),
            };

            $.ajax({
                url: '/api/Course/Delete_Course/' + value.title,
                type: 'GET',
                dataType: 'json',
                /// data: value,
                success: function (data, textStatus, xhr) {
                    swal({
                        icon: 'success',
                        title: 'Course Deleted',
                        text: '',
                    });
                    ////  NotApproveTable.ajax.reload();
                    CourseList.ajax.reload();
                    
                },
                error: function (xhr, textStatus, errorThrown) {

                    swal({
                        icon: 'error',
                        title: 'Ooopsss!',
                        text: 'Something Went Wrong',
                    });

                }
            });
        }
    });


});

///This Update Works From From_Core_Core_Update_Course

$('body').on('click', '#Update_The_Course', function () {

    swal({
        title: "Are you sure?",
        text: "Want to Update This Course",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willUpdate) => {
        if (willUpdate) {

            var value = {
                id: $("#id").val(),
                title: $("#title").val(),
                description: $("#description").val(),
            };

            $.ajax({
                url: '/api/Course/Update_Course/' + value.id,
                type: 'PUT',
                dataType: 'json',
                data: value,
                success: function (data, textStatus, xhr) {
                    swal({
                        icon: 'success',
                        title: 'Course Updated',
                        text: data.message,
                    });
                    ////  NotApproveTable.ajax.reload();
                    CourseList.ajax.reload();

                },
                error: function (xhr, textStatus, errorThrown) {

                    swal({
                        icon: 'error',
                        title: 'Ooopsss!',
                        text: 'Something Went Wrong',
                    });

                }
            });
        }
    });


});

