$(function () {

    /*Pagination vers le bas*******************************************************/
    $(window).scroll(function () {
        if ($(window).scrollTop() >= ($(document).height() - $(window).height() - 150)) {
            if ($("#settings").attr("data-tempo-paginate") == 0 && $("#settings").attr("data-statut-paginate") == "true") {
                // Requete ajax
                $("#loader-results").css("display", "block");



                $.post("/Find/Next",
                    function (data) {
                        //$(".result").html(data);

                        //Si plus de résultats on désactive la pagination
                        var dataTestEmpty = data.replace(/\s+/g, '');
                        if (dataTestEmpty == "") {
                            $("#settings").attr("data-statut-paginate", "false");
                            $("#loader-results").css("display", "none");
                            return false;
                        }

                        $("#settings").attr("data-tempo-paginate", 0);

                        //Affiche la liste de résultats
                        $('#results-list > ul').append(data);

                        $("#loader-results").css("display", "none");

                    }
                );

            }
            $("#settings").attr("data-tempo-paginate", 1);
        }
    });
});


