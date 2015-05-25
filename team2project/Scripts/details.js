function reloadCount() {
    $.post("/getCount", { id: id }, function (data) {
        link.html(data);
    });
}
function subscribe() {
    button.html('Подождите');
    button.on('click', function(){});
    $.post("/Subscribe", { id: id }, function (data) {
        if (data) {
            button.html('Покинуть');
            button.on('click', unsubscribe);
        }
        else {
            location.assign('/User/Login');
        }
        reloadCount();
    });
}
function unsubscribe() {
    button.html('Подождите');
    button.on('click', function () { });
    $.post("/Unsubscribe", { id: id }, function (data) {
        if (data) {
            button.html('Присоедениться');
            button.on('click', subscribe);
        }
        else {
            location.assign('/User/Login');
        }
        reloadCount();
    });
}
var button, id, link;
$(function () {
    button = $('#submit');
    link = $('#subscribers a');
    id = location.pathname.split('/').pop();
    $.post("/IsSubscribed", {id:id}, function (data) {
        if (data) {
            button.html('Покинуть');
            button.on('click', unsubscribe);
        }
        else {
            button.html('Присоедениться');
            button.on('click', subscribe);
        }
    });
});