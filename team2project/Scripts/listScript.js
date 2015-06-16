var period, city;
$(function () {
    var path = decodeURI(location.pathname).split('/');

    var select = $("#City");

    select.prepend('<option value="-1">Все</option>');

    period = path[2];
    if (path[3]) {
        var arr = select.children();
        if (cityValue) {
            arr.each(function (i, val) {
                if ($(val).val() === cityValue) { val.setAttribute('selected', 'selected'); city = val; return false }
            });
        }
    }
    else $("#City :first-child").attr("selected", "selected");

    select.on("change", function () {
        var url = location.protocol + '//' + location.host + '/', path = location.pathname.split('/');
        url += path[1];

        if (period && period !== '-1') {
            url += '/' + period;
        }

        if ($("#City option:selected" ).text() !== "Все") {
            url += '/' + $(this).val();
        }
        document.location.href = url;
        setTimeout(function () {
            document.location.href = url;
        }, 1000);
    });

    $('.date-filters a').each(function () {
        var location = window.location.href;
        var link = this.href;
        if (location == link) {
            $(this).addClass('active');
        }
    });

    $(".filter-link").each(function () {
        $(this).attr("href", $(this).attr("href") + "/" + cityValue);
    });
});
