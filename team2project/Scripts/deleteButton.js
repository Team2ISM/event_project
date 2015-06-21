function deleteEvent(e, fromPage) {
    fromPage = fromPage || "admin";
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
                $("body").toggleClass("loaded");
                $.ajax({
                    url: host + "/events/delete",
                    type: "POST",
                    data: {
                        id: e
                    },
                    success: function (response) {
                        switch (fromPage)
                        {
                            case "admin":
                                location.reload();
                                break;
                            case "list":
                                $("#" + e + "_row").remove();
                                break;
                            case "details":
                                window.location.assign(host);
                                break;
                            default:
                                console.dir("ERROR: Unknown source");
                                break;
                        }
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