function deleteEvent(e) {
    if (confirm("Вы действительно хотите удалить это событие?")) {
        var http = location.protocol;
        var slashes = http.concat("//");
        var host = slashes.concat(window.location.host);
        location.href = host + "/events/delete/" + e;
    }
}