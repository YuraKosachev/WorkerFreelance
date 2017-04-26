$(function () {

    $('button[data-source="file"]').on("click", function (e) {
        $("input[type='file']").trigger('click');
    });

    $("input[type='file']").on("change", function (data) {

        console.log($(this));
        if ($(this)[0].files.length == 0) {
            $('input[data-insert="fileName"]').val("");
        } else {
            $('input[data-insert="fileName"]').val($(this)[0].files[0].name);
        }

    });
});