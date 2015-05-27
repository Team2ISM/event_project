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
    $.post("/Unsubscribe", { id: id }, function (data) {
        if (data) {
            button.html('Присоедениться');
            button.get()[0].onclick = subscribe;
        }
        else {
            location.assign('/User/Login');
        }
        reloadCount();
    });
}
var button, id, link;
window.onload = function () {
    button = $('#submit');
    link = $('#subscribers a');
    id = location.pathname.split('/').pop();
    $.post("/IsSubscribed", {id:id}, function (data) {
        if (data) {
            button.html('Покинуть');
            button.get()[0].onclick = unsubscribe;
        }
        else {
            button.html('Присоедениться');
            button.get()[0].onclick = subscribe;
        }
    });
}
