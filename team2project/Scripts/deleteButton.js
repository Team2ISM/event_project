function deleteEvent(e, reloadPage) {
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
                    url: host + "/events/delete",
                    type: "POST",
                    data: {
                        id: e
                    },
                    success: function (response) {
                        if (reloadPage) location.reload();
                        else $("#" + e + "_row").remove();
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