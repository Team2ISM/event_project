function bindButtons() {
    buttons = $(".subscribe.unbinded");

    $.each(buttons, function (index, value) {
        $(value).toggleClass("disabled");
        $.post("/issubscribed", { id: $(value).attr("id").split("_")[0] }, function (data) {
            if (data) {
                $(value).addClass("unsubscribe-button");
                $(value).find("i").addClass("mdi-action-exit-to-app");
            }
            else {
                $(value).addClass("subscribe-button");
                $(value).find("i").addClass("mdi-social-person-add");
            }
            $(value).toggleClass("disabled");
            bindButton(value);
            $(value).removeClass("unbinded");
        });
    });
}

function bindButton(value) {
    $(value).unbind("click");
    if ($(value).hasClass("subscribe-button")) $(value).click(subscribe);
    else $(value).click(unsubscribe);
}

function reloadCount(id) {
    $.post("/subscribers/getcount", { id: id }, function (data) {
        $("#" + id + "-count").children().html(data);
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

$(document).ready(function () {
    setTimeout(function () { bindButtons() }, 300);
});