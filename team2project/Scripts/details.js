function reloadCount() {
    $.post("subscribers/getcount", { id: id }, function (data) {
        link.html(data);
    });
}
function subscribe() {
    button.html('Подождите');
    button.on('click', function(){});
    $.post("/subscribe", { id: id }, function (data) {
        if (data) {
            button.html('Покинуть');
            button.get()[0].onclick = unsubscribe;
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
    $.post("/unsubscribe", { id: id }, function (data) {
        if (data) {
            button.html('Присоединиться');
            button.get()[0].onclick = subscribe;
        }
        else {
            location.assign('/user/login');
        }
        reloadCount();
    });
}
var button, id, link;
window.onload = function () {
    button = $('#submit');
    link = $('#subscribers a');
    id = location.pathname.split('/').pop();
    ReloadSubscr($('#subscribers_onhover'));
    $.post("/issubscribed", {id:id}, function (data) {
        if (data) {
            button.html('Покинуть');
            button.get()[0].onclick = unsubscribe;
        }
        else {
            button.html('Присоединиться');
            button.get()[0].onclick = subscribe;
        }
    });
}
