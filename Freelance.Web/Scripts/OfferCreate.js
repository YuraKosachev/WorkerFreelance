$(function () {
    //DatePickers
    var OfferTime = $('.time').datetimepicker({
        locale: 'ru',
        format: 'LT',
    });
    $('.dayofweek').datetimepicker({
        minDate: moment(),//disabled dates before current
        locale: 'ru',
        format: 'DD/MM/YYYY',

    });
    var FilterTimeTo = $('.timeFilterTo').datetimepicker({
        locale: 'ru',
        format: 'LT',
    });
    var FilterTimeFrom = $('.timeFilterFrom').datetimepicker({
        locale: 'ru',
        format: 'LT',
    });

    //-------------------------
    //--clear filter
    $(".remove").on("click", function (e) {
        e.stopImmediatePropagation();
        var button = $(this);
        var attrValue = button.attr('data-remove-content');
        console.log(attrValue);
        $('input[name="' + attrValue + '"]').val("");

    });
    //----------------------------
    //FilterTimeTo.on('dp.change', function (selected) {
    //    FilterTimeFrom.data("DateTimePicker").maxDate(selected.date);
    //});
    //FilterTimeFrom.on('dp.change', function (selected) {

    //    FilterTimeTo.data("DateTimePicker").minDate(selected.date);
    //});

    //-------------------------------
    var urls = {
        OfferCreateUrl: "Offer/Create"
       
    };
    $('[data-toggle="popover"]').popover();
    var profileId;
    var OfferModal = {
        profileId: null,
        time: null,
        date: null,
        description: null,
        GetdData: function () {


        }

    };





    $('[data-offer-modal]').on('click', function () {
        var button = $(this);
        profileId = button.attr("data-profile-id");
        var timeFromH = button.attr("data-time-from-h");
        var timeFromM = button.attr("data-time-from-m");
        var timeToH = button.attr("data-time-to-h");
        var timeToM = button.attr("data-time-to-m");

        OfferTime.data("DateTimePicker").minDate(moment({ h: timeFromH, m: timeFromM }));
        OfferTime.data("DateTimePicker").maxDate(moment({ h: timeToH, m: timeToM }));
        $('#OfferCreate').modal('show');

    });

   

    $('#OfferCreate').on('hidden.bs.modal', function (e) {
            e.stopImmediatePropagation();
            $('[name="Time"]').val("");
            $('[name="Date"]').val(""),
            $('[name="Description"]').val("");
        });


    $('[data-bt-role="submit"]').on('click', function () {
        var data = {
            ProfileId:profileId,
            Time:$('[name="Time"]').val(),
            Date:$('[name="Date"]').val(),
            Description:$('[name="Description"]').val()
        };
       
        
        $.post(urls.OfferCreateUrl, data)
            .success(function (suc) {

                $('#OfferCreate').modal('hide');
            })
            .error(function (error) {

            
        });
       

    });
});