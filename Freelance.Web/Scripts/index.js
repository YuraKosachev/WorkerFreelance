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
});