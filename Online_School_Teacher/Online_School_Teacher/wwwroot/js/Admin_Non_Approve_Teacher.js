var NotApproveTable;

$(document).ready(function () {
    loadNotApprobeDataTable();
});

function loadNotApprobeDataTable() {
    NotApproveTable = $('#NotApprove').DataTable({
        "ajax": {
            "url": "/api/nonApproveTeachers",
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
                        <button id="Approves" value="${data}" class='btn btn-success text-white' style='cursor:pointer; width:90px;'>
                            Approve
                        </button>
                        &nbsp;
                        <button id="Delete" value="${data}" class='btn btn-danger text-white' style='cursor:pointer; width:90px;'>
                            Delete
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

$('body').on('click', '#Delete', function () {
   

        swal({
            title: "Are you sure?",
            text: "Want to Delete",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((willDelete) => {
            if (willDelete) {

                var value = {
                    email: $("#Delete").val(),
                };
               //// document.writeln(value.email);
                $.ajax({
                    url: '/api/Teacher/Delete/' + value.email,
                    type: 'DELETE',
                    dataType: 'json',
                   /// data: value,
                    success: function (data, textStatus, xhr) {
                        swal({
                            icon: 'success',
                            title: 'Deleted',
                            text: 'The Teacher Has Deleted Successfully',
                        });
                        NotApproveTable.ajax.reload();
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


$('body').on('click', '#Approves', function () {


    swal({
        title: "Are you sure?",
        text: "Want to Approve This Teacher",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willApprove) => {
        if (willApprove) {

            var value = {
                email: $("#Approves").val(),
            };

           ///   document.writeln(value.email);

            $.ajax({
                url: '/api/Teacher/Renew/' + value.email,
                type: 'PUT',
                dataType: 'json',
                /// data: value,
                success: function (data, textStatus, xhr) {
                    swal({
                        icon: 'success',
                        title: 'Approved',
                        text: 'The Teacher Has Approved Successfully',
                    });
                    NotApproveTable.ajax.reload();
                    ApproveTable.ajax.reload();
                },
                error: function (xhr, textStatus, errorThrown) {

                    if (xhr.status == 500) {
                        alert('Something Went Wrong 500')
                    }
                    else if (xhr.status == 415) {
                        alert('Something Went Wrong 415');
                    }
                    else if (xhr.status == 400) {
                        alert('Something Went Wrong 400');
                    }

                    else {

                        swal({
                            icon: 'error',
                            title: 'Ooopsss!',
                            text: "Something Went Wrong " + xhr.status,
                        });
                    }
                }
            });
        }
    });


});