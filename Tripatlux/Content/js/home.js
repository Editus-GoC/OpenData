function putGeolocationTo() {
    if (navigator.geolocation)
        navigator.geolocation.getCurrentPosition(showLocation);
}

function showLocation(position) {
    $.ajax({
        url: 'https://maps.googleapis.com/maps/api/geocode/json?latlng=' + position.coords.latitude + ',' + position.coords.longitude + '&key=AIzaSyCta3Rfy6fLAfzzb8OrqQKNfISBSefH8Rk',
        success: function (result) {
            $('#from').val(result.results[0].formatted_address).parent().addClass('is-dirty');
        },
        complete: function () {
            componentHandler.upgradeDom();
        }
    });
}

$(function () {

    //putGeolocationTo();

    $("#when").pickadate({
        today: '',
        clear: '',
        close: 'Fermer',
        onClose: function () {
            if ($('#when').val() != '')
                $('#when').parent().addClass('is-dirty');
            else
                $('#when').parent().removeClass('is-dirty');
        },
        min: [2016, 4, 9],
        max: [2018, 4, 9]
    });

});