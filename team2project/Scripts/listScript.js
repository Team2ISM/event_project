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
function bindButton(value) {
    $(value).unbind("click");
    if ($(value).hasClass("subscribe-button")) $(value).click(subscribe);
    else $(value).click(unsubscribe);
}
function reloadCount(id) {
    $.post("/subscribers/getcount", { id: id }, function (data) {
        $("#"+id+"-count").children().html(data);
    });
}
function subscribe() {
    button = $(this);
    id = button.attr("id").split("_")[0];
    button.toggleClass("disabled");
    button.unbind("click");
    $.post("/subscribe", { id: id }, function (data) {
        button = $("#" + this.data.split("=")[1] + "_sub");
        if (data) {
            button.toggleClass("unsubscribe-button");
            button.toggleClass("subscribe-button");
            button.click(unsubscribe);
            button.find('i').toggleClass('mdi-action-exit-to-app');
            button.find('i').toggleClass('mdi-social-person-add');
        }
        else {
            location.assign('/user/login?returnURL=' + returnURL);
        }
        reloadCount(id);
        button.toggleClass("disabled");
    });
}
function unsubscribe() {
    button = $(this);
    id = button.attr("id").split("_")[0];
    button.toggleClass("disabled");
    button.unbind("click");
    $.post("/unsubscribe", { id: id }, function (data) {
        button = $("#" + this.data.split("=")[1] + "_sub");
        if (data) {
            button.toggleClass("unsubscribe-button");
            button.toggleClass("subscribe-button");
            button.click(subscribe);
            button.find('i').toggleClass('mdi-social-person-add');
            button.find('i').toggleClass('mdi-action-exit-to-app');
        }
        else {
            location.assign('/user/login?returnURL=' + returnURL);
        }
        reloadCount(id);
        button.toggleClass("disabled");
    });
}