var locat = $("#location"), editor;
window.onload = function () {
    editor = CKEDITOR.replace('Description', { /*magicline_everywhere : true*/ });

    var cityVa
    $("#LocationId").on("change", function () {
        var arr = $("#LocationId").children();
        var value = $("#LocationId").val();
        $('#LocationId option[selected="selected"]').removeAttr('selected');
        arr.each(function (i, val) {
            if (value == $(val).val()) $(val).attr('selected', 'selected');
        });
    });

    var content, label = $('label[for="Description"]'),
        textDescr = $('#TextDescription'),
        description = $('#Description');

    editor.on('blur', function () {
        if (editor.getData().length > 0) return;
        label.removeClass('active');
    });

    editor.on('focus', function () {
        if (!content) content = $(editor._.editable.$);
        label.addClass('active');
    });

    editor.on('change', function () {
        textDescr.val(content.children().text());
        description.html(editor.getData());
    });

    $('#form input').on('keypress', function(e) {
        if (e.key == '<' || e.key == '>') return false;
    });

    if (fDate) $('label[for="FromDate"]').addClass('active');
    if (tDate) $('label[for="ToDate"]').addClass('active');
    var pick1 = $('#datetimepicker1'), pick2 = $('#datetimepicker2');
        if (fDate) pick1.val(fDate);
        if (tDate) pick2.val(tDate);
        pick1.datetimepicker({
        minDate: '-1970/01/01',
        startDate: fDate ? fDate : '',
        minTime: 0,
        lang: 'ru',
        mask: true
    });
    pick2.datetimepicker({
        minDate: 0,
        sstartDate: tDate ? tDate : '',
        lang: 'ru',
        mask: true
    });
   
};
