var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

function reOrderSteps() {
    $('#intermediate-steps li').each(function (index) {
        var newOrder = index + 1;
        $(this).find('label').attr('for', function (i, txt) { return txt.replace(/\d+/, newOrder); });
        $(this).find('label').text(function (i, txt) { return txt.replace(/\d+/, newOrder); });
        $(this).find('input[type="text"]').attr('id', function (i, txt) { return txt.replace(/\d+/, newOrder); });
        $(this).find('input[type="text"]').attr('name', function (i, txt) { return txt.replace(/\d+/, newOrder); });
    });
}
var directionsService;
function initGMap() {

    directionsService = new google.maps.DirectionsService();

}

function getGMapDirections() {
    var start = $('#start').val();
    var end = $('#end').val();
    var waypoints = [];
    $('#intermediate-steps li').each(function () {
        waypoints.push({
            location: $(this).find('input[type="text"]').val(),
            stopover: true
        });
    });
    var request = {
        origin: start,
        destination: end,
        waypoints: waypoints,
        travelMode: google.maps.TravelMode.DRIVING
    };
    directionsService.route(request, function (result, status) {
        if (status == google.maps.DirectionsStatus.OK && result.routes.length) {
            var distanceText = result.routes[0].legs[0].distance.text;
            var distanceValue = result.routes[0].legs[0].distance.value;
            var durationText = result.routes[0].legs[0].duration.text;
            var durationValue = result.routes[0].legs[0].duration.value;
            $('#gmap-data-distance').text(distanceText);
            $('#gmap-data-duration').text(durationText);
            $('#duration').val(durationValue);
            $('#gmap-data-title, #gmap-data-distance, #gmap-data-duration').show();
        } else {
            $('#gmap-data-distance').text('');
            $('#gmap-data-duration').text('');
            $('#duration').val('');
            $('#gmap-data-title, #gmap-data-distance, #gmap-data-duration').hide();
        }
    });
}

function putGeolocationTo() {
    if (navigator.geolocation)
        navigator.geolocation.getCurrentPosition(showLocation);
}

function showLocation(position) {
    $.ajax({
        url: 'https://maps.googleapis.com/maps/api/geocode/json?latlng=' + position.coords.latitude + ',' + position.coords.longitude + '&key=AIzaSyCta3Rfy6fLAfzzb8OrqQKNfISBSefH8Rk',
        success: function (result) {
            $('#start').val(result.results[0].formatted_address).parent().addClass('is-dirty');
        },
        complete: function () {
            componentHandler.upgradeDom();
        }
    });
}

$(function () {

    putGeolocationTo($);

    $('#add-step').click(function (e) {
        var stepsCount = $('#intermediate-steps li').length;
        if (stepsCount < 23) {
            var stepNum = stepsCount + 1;
            var dataStepLabel = $('#intermediate-steps').data('step-label');
            var $element = '<li>'
                + '<div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">'
                + '<input type="text" class="mdl-textfield__input" id="step-' + stepNum + '" name="steps[' + stepNum + ']" />'
                + '<label class="mdl-textfield__label" for="step-' + stepNum + '">'
                + dataStepLabel + ' ' + stepNum
                + '</label>'
                + '</div>'
                + '<button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent float-right detele-button">'
                + 'Supprimer'
                + '</button>'
                + '<div class="clear"></div>'
                + '</li>'
                + '<div class="clear"></div>';
            $('#intermediate-steps').append($element);
            componentHandler.upgradeDom();
            $('#intermediate-steps li').find('.detele-button').click(function () {
                $(this).parents('li').remove();
                reOrderSteps();
            });
        } else
            alert('Vous avez atteind le nombre maximum d\'étapes');
        event.preventDefault();
    });

    $('#submit-route').click(function () {
        $(this).parents('form').submit();
    });

    $('#start').keyup(function () {
        $input = $(this);
        delay(function () {
            if ($('#start').val() != '' && $('#start').val() && $('#end').val() != '' && $('#end').val())
                getGMapDirections();
        });
    });

    $('#end').keyup(function () {
        $input = $(this);
        delay(function () {
            if ($input.val() != '' && $input.val()) {
                if ($('#start').val() != '' && $('#start').val() && $('#end').val() != '' && $('#end').val())
                    getGMapDirections();
                $.ajax({
                    url: '/Created/VerifyIsCompany?address=' + $input.val(),
                    type: 'GET',
                    beforeSend: function () {
                        $('#check-address-list-loader').show();
                        $('#check-address-list').hide();
                    },
                    success: function (result) {
                        if (!result)
                            $('#check-address-list-title').hide();
                        else {
                            $('#check-address-list').html('');
                            $.each(result.Items, function (index, value) {
                                var $listElement = '<li data-id-tiers="' + result.Items[index].TiersId + '">'
                                + '<div class="logo-container">';
                                if (result.Items[index].Logo)
                                    $listElement += '<img src="http://webfiles.luxweb.com/logos/' + result.Items[index].Logo.Path + '" />'
                                $listElement += '</div>'
                                + '<div class="right-container">'
                                + '<div class="name-container">'
                                + result.Items[index].Nom
                                + '</div>'
                                + '<div class="address-container">'
                                + result.Items[index].Adresse + result.Items[index].LocaliteFr
                                + '</div>'
                                + '</div>'
                                + '</li>';
                                $('#check-address-list').append($listElement);
                            });
                            $('#check-address-list-title').show();
                        }
                    },
                    complete: function () {
                        $('#check-address-list-loader').hide();
                        $('#check-address-list').show();
                        $('#check-address-list li').unbind('click').bind('click', function () {
                            $('#id-tiers').val($(this).attr('data-id-tiers'));
                            $('#end').val($(this).find('.address-container').text());
                            $('#check-address-list').html('');
                            $('#check-address-list-title').hide();
                            if ($('#start').val() != '' && $('#start').val() && $('#end').val() != '' && $('#end').val())
                                getGMapDirections();
                        });
                    }
                });
            }
        }, 1000);

    });

    $("#date").pickadate({
        today: '',
        clear: '',
        close: 'Fermer',
        onClose: function () {
            if ($('#date').val() != '')
                $('#date').parent().addClass('is-dirty');
            else
                $('#date').parent().removeClass('is-dirty');
        },
        min: [2016,4,9],
        max: [2018,4,9]
    });

    $("#hour").pickatime({
        clear: '',
        format: 'HH:i',
        onClose: function () {
            if ($('#hour').val() != '')
                $('#hour').parent().addClass('is-dirty');
            else
                $('#hour').parent().removeClass('is-dirty');
        },
    });

    $("#comeback-hour").pickatime({
        clear: '',
        format: 'HH:i',
        onClose: function () {
            if ($('#comeback-hour').val() != '')
                $('#comeback-hour').parent().addClass('is-dirty');
            else
                $('#comeback-hour').parent().removeClass('is-dirty');
        },
    });

    $('#round-trip').change(function () {
        if ($(this).is(':checked'))
            $('#comeback-hour-block').show();
        else
            $('#comeback-hour-block').hide();
    });

    (function () {
        window.setTimeout(function () {
            return componentHandler.upgradeDom();
        }, 500);
    }.call(this));

});