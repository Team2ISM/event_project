﻿(function ($) {
    $.widget("custom.combobox", {
        _create: function () {
            this.wrapper = $("<span>")
              .addClass("custom-combobox")
              .insertAfter(this.element);

            this.element.hide();
            this._createAutocomplete();
            this._createShowAllButton();
        },

        _createAutocomplete: function () {
            var selected = this.element.children(":selected"),
              value = selected.val() ? selected.text() : "";
            locat.val(selected.val() ? selected.val() : "");
            locat.html(selected.val() ? selected.val() : "");
            this.input = $("<input>")
              .appendTo(this.wrapper)
              .val(value)
              .attr("title", "")
              .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
              .autocomplete({
                  delay: 0,
                  minLength: 0,
                  source: $.proxy(this, "_source")
              })
              .tooltip({
                  tooltipClass: "ui-state-highlight"
              });

            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;
                    this._trigger("select", event, {
                        item: ui.item.option
                    });                    
                    //My 
                    locat.val(ui.item.value);
                    //locat.html(ui.item.value);
                },

                autocompletechange: "_removeIfInvalid"
            });
        },

        _createShowAllButton: function () {
            var input = this.input,
              wasOpen = false;

            $("<a>")
              .attr("tabIndex", -1)
              .attr("title", "Show All Items")
              .tooltip()
              .appendTo(this.wrapper)
              .button({
                  icons: {
                      primary: "ui-icon-triangle-1-s"
                  },
                  text: false
              })
              .removeClass("ui-corner-all")
              .addClass("custom-combobox-toggle ui-corner-right")
              .mousedown(function () {
                  wasOpen = input.autocomplete("widget").is(":visible");
              })
              .click(function () {
                  input.focus();

                  // Close if already visible
                  if (wasOpen) {
                      return;
                  }

                  // Pass empty string as value to search for, displaying all results
                  input.autocomplete("search", "");
              });
        },

        _source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
            }));
        },

        _removeIfInvalid: function (event, ui) {

            // Selected an item, nothing to do
            if (ui.item) {
                locat.val(ui.item.value);
                return;
            }

            // Search for a match (case-insensitive)
            var value = this.input.val(),
              valueLowerCase = value.toLowerCase(),
              valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                locat.val(value);
                return;
            }

            // Remove invalid value
            this.input
              .val("")
              .attr("title", value + " отсутствует в списке")
              .tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.autocomplete("instance").term = "";
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });
})(jQuery);
var locat = $("#location"), editor;
window.onload = function () {
    editor = CKEDITOR.replace('Description', { /*magicline_everywhere : true*/ });

    var combobox = $("#combobox"), value;
    if (city) {
        var arr = combobox.children();
        arr.each(function (i, val) {
            if (val.value === city) {
                val.setAttribute('selected', 'selected');
                value = city;
                locat.val(val.value);
                return false;
            }
        });
    }
    combobox.combobox();
    locat.val(combobox.children()[0].innerHTML);
    if (value) $('#LocationId').val(value);

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
