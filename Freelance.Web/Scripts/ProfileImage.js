$(function () {
    $('a.thumbnail').on("click", function (e) {
        e.stopPropagation();
        $("#uploadImage").trigger('click');
    });
    $("input[data-type-file='image']").on("change", function () {

        $("[data-modal-role='ImageEditor']").modal('show');
        var path = $(this).val();
    });

    var $uploadCrop;

    function readFile(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.upload-demo').addClass('ready');
                $uploadCrop.croppie('bind', {
                    url: e.target.result
                }).then(function () {
                    console.log('jQuery bind complete');
                });

            }

            reader.readAsDataURL(input.files[0]);
        }
        else {
            swal("Sorry - you're browser doesn't support the FileReader API");
        }
    }

    $uploadCrop = $('#upload-demo').croppie({
        viewport: {
            width: 360,
            height: 339,
            type: 'square'
        },
        boundary: {
            width: 300,
            height: 300
        },
        enableExif: true
    });

    $('#uploadImage').on('change', function () { readFile(this); });
    $('.upload-result').on('click', function (ev) {
        var result = $uploadCrop.croppie('result', {
            type: 'base64',
            size: 'viewport',
            format: 'png'
        }).then(function (resp) {
            var imgContainer = $("img[data-element-role='imageContainer']");
            //imgContainer.removeAttr('data-src');
            $("img[data-element-role='imageContainer']").attr('src', resp)
            $("input[name='Image']").val(resp);

        });


    });
});