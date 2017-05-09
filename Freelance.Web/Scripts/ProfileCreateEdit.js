$(function () {

    var timeTo = $(".timeTo").datetimepicker({
        locale: 'ru',
        format: 'LT',
    });
    var timeFrom = $(".timeFrom").datetimepicker({
        locale: 'ru',
        format: 'LT',
    });
    timeTo.on('dp.change', function (selected) {
        
        timeFrom.data("DateTimePicker").maxDate(selected.date);
    });
    timeFrom.on('dp.change', function (selected) {
        
        timeTo.data("DateTimePicker").minDate(selected.date);
    });

    $('button[data-source="file"]').on("click", function (e) {
        $("#upload").trigger('click');
    });

    $("#upload").on("change", function (data) {

        if ($(this)[0].files.length == 0) {
            $('input[data-insert="fileName"]').val("");
        } else {
            $('input[data-insert="fileName"]').val($(this)[0].files[0].name);
        }

    });
});