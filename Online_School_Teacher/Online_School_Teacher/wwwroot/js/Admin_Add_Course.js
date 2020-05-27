$(document).ready(function () {
    $("#Save").click(function () {

      

                var value = {
                    title: $("#Title").val(),
                    description: $("#Description").val(),
                   
                };

                $.ajax({
                    url: '/api/Course/New_Course',
                    type: 'POST',
                    dataType: 'json',
                    data: value,
                    success: function (data, textStatus, xhr) {
                        swal({
                            icon: 'success',
                            title: 'Successfully',
                            text: 'Added New Course',
                        });
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal({
                            icon: 'error',
                            title: 'Wrong Form Fill Up',
                            text: 'Please Fill Up the Form Correctly',
                        });


                    }
                });
    });
});