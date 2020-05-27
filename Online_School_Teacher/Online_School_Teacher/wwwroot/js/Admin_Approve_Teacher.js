var ApproveTable;

$(document).ready(function () {
    loadApproveDataTable();
});

function loadApproveDataTable() {
    ApproveTable = $('#Approve').DataTable({
        "ajax": {
            "url": "/api/getTeachers",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "email", "width": "25%" },
            { "data": "name", "width": "25%" },
            {
                "data": "email",
                "render": function (data) {
                    return `<div class="text-center">
                        <button id="Renew" value="${data}" class='btn btn-success text-white' style='cursor:pointer; width:90px;'>
                            Renew
                        </button>
                        &nbsp;
                        <button id="Block"  value="${data}" class='btn btn-danger text-white' style='cursor:pointer; width:90px;' onclick=Block(${data})>
                            Block
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

$('body').on('click', '#Renew', function () {


    swal({
        title: "Are you sure?",
        text: "Want to Renew This Teacher",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willRenew) => {
        if (willRenew) {

            var value = {
                email: $("#Renew").val(),
            };

            $.ajax({
                url: '/api/Teacher/Renew/' + value.email,
                type: 'GET',
                dataType: 'json',
                /// data: value,
                success: function (data, textStatus, xhr) {
                    swal({
                        icon: 'success',
                        title: 'Renewed',
                        text: 'The Teacher Has Renewed Successfully',
                    });
                    NotApproveTable.ajax.reload();
                    ApproveTable.ajax.reload();
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


$('body').on('click', '#Block', function () {


    swal({
        title: "Are you sure?",
        text: "Want to Block This Teacher",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willBlock) => {
        if (willBlock) {

            var value = {
                email: $("#Block").val(),
            };

            $.ajax({
                url: '/api/Teacher/Block/' + value.email,
                type: 'GET',
                dataType: 'json',
                /// data: value,
                success: function (data, textStatus, xhr) {
                    swal({
                        icon: 'success',
                        title: 'Blocked',
                        text: 'The Teacher Has Blocked Successfully',
                    });
                   /// NotApproveTable.ajax.reload();
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