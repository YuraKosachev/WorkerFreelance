$(function () {

    

  



    $('a[data-toggle="modal"]').on('click', function (e) {

        var deleteId = $(this).attr('data-profile-id');
        $('input[name="id"]').val(deleteId);
    });


});