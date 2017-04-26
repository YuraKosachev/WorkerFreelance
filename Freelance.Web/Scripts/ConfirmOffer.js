$(function () {
    var DataConfirm = {
        Id: null,
        Date: null,
        DateOfCreate: null,
        Description: null,
        ProfileId: null,
        UserId:null
    };
    $('[data-offer-confim="#ConfirmOffer"]').on('click', function () {
        DataConfirm.Id = $(this).attr("data-offer-id");
        DataConfirm.Date = $(this).attr("data-confirm-date");
        DataConfirm.ProfileId = $(this).attr("data-confirm-ProfileId");
        DataConfirm.UserId = $(this).attr("data-confirm-UserId");
        DataConfirm.DateOfCreate = $(this).attr("data-confirm-DateOfCreate");
        DataConfirm.Description=$(this).attr("data-confirm-description");

        $('#ConfirmOffer').modal('show');

    });
    

    $('[data-bt-role="confirm"]').on("click", function () {
        
        $.post("Confirm", DataConfirm)
            .success(function (data) {
                var button = $('[data-offer-id="' + data.OfferId + '"]');
                button.removeClass("btn-primary");
                button.addClass("btn-success disabled");
                button.text("Подтвержден!");

            $("#ConfirmOffer").modal('hide');
        })
            .error(function (error) {
                //
                console.log(error);
            });

    });
});