var BlockTeacherTable;

$(document).ready(function () {
    loadBlockTeacherTable();
});

function loadBlockTeacherTable() {
    BlockTeacherTable = $('#BlockTable').DataTable({
        "ajax": {
            "url": "/api/BlockTeacher",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "email", "width": "20%" },
            { "data": "name", "width": "20%" },
            {
                "data": "email",
                "render": function (data) {
                    return `<div class="text-center">
                        <button id="Unblock" value="${data}" class='btn btn-success text-white' style='cursor:pointer; width:90px;'>
                            Unblock
                        </button>
                      
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

$('body').on('click', '#Unblock', function () {

    swal({
        title: "Are you sure?",
        text: "Want to Unblock This Teacher",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willUnblock) => {
        if (willUnblock) {

            var value = {
                email: $("#Unblock").val(),
            };

            $.ajax({
                url: '/api/Teacher/Renew/' + value.email,
                type: 'GET',
                dataType: 'json',
                /// data: value,
                success: function (data, textStatus, xhr) {
                    swal({
                        icon: 'success',
                        title: 'Unblocked',
                        text: 'The Teacher Has Unblocked Successfully',
                    });
                  ////  NotApproveTable.ajax.reload();
                    ApproveTable.ajax.reload();
                    BlockTeacherTable.ajax.reload();
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