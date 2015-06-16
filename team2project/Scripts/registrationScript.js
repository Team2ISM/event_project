var locat = $("#location");
window.onload = function () {
    $("#LocationId").on("change", function () {
        var arr = $("#LocationId").children();
        var value = $("#LocationId").val();
        $('#LocationId option[selected="selected"]').removeAttr('selected');
        arr.each(function (i, val) {
            if (value == $(val).val()) $(val).attr('selected', 'selected');
        });
    });

    document.forms[0].onsubmit = function () {
        if ($(this).valid()) $('#loading-img').show();
    }
    
};