function deleteEvent(e) {
    var host = location.protocol + '//' + location.host;
    var url = host + "/events/delete";

    $("#dialog-confirm").dialog({
        resizable: false,
        width: 400,
        height: 200,
        modal: true,
        buttons: {
            "Удалить событие": function () {
                $(this).dialog("close");
                $.ajax({
                    url: url + "/admin/events/delete",
                    type: "POST",
                    data: {
                        id: e
                    },
                    success: function (response) {
                        location.reload();
                    },     
                    error: function (er) {
                        console.dir(er);
                    }
                });
            },
            "Отмена": function () {
                $(this).dialog("close");
            }
        }
    });
}