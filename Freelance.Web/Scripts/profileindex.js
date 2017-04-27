$(function () {

    $('[data-toggle="tooltip"]').tooltip();

    //выравнивание блоков по высоте
    $('.container').each(function () {
        var highestBox = 0;
        $('.eq', this).each(function () {
            if ($(this).height() > highestBox) {
                highestBox = $(this).height();
            }
        });
        $('.eq', this).height(highestBox);
    });



    $('a[data-toggle="modal"]').on('click', function (e) {

        var deleteId = $(this).attr('data-profile-id');
        $('input[name="id"]').val(deleteId);
    });


});