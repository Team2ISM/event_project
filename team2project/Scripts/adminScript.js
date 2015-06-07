(function () {
    var url = "http://" + window.location.host;
    var monthNames = ["января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря"];
    var adminAjaxHelper = new AdminAjaxHelper();

    $("body").addClass("loaded");
    $(document).ajaxStart(function () {
        $("body").toggleClass("loaded");
    }).ajaxStop(function () {
        $("body").toggleClass("loaded");
    })

    function AdminAjaxHelper() {
        this.toogleActive = function (self, urlPart, errorActivateText) {
            $.ajax({
                url: url + "/admin/events/" + urlPart,
                type: "POST",
                data: {
                    id: self.id()
                },
                success: function (response) {
                    successHelper(response, function () {
                        self.active(!self.active());
                    },
                    function (response) {
                        $("#text-toggleStatus").html(response.Message);
                        $("#dialog-toggleStatus-error").dialog({
                            resizable: false,
                            width: 400,
                            height: 200,
                            modal: true
                        });
                        if (response.Message === "Событие было удалено") {
                            viewModel.deleteEventLocal(self);
                        }
                        else {
                            self.active(!self.active());
                        }
                    }
                    )
                },
                error: function (er) {
                    console.dir(er);
                }
            });
        }

        this.loadEvents = function (self) {
            $.ajax({
                url: url + "/admin/events/getall",
                success: function (response) {
                    successHelper(response,
                        function (response) {
                            var events = response.Data;
                            for (var i in events) {
                                self.events.push(new EventModel(events[i], self));
                            }
                        })


                },
                error: function (er) {
                    console.dir(er);
                }
            });
        }

        this.deleteEvent = function (data, self) {
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
                                id: data.id()
                            },
                            success: function (response) {
                                successHelper(response,
                                    function () {
                                        var events = self.events();
                                        ko.utils.arrayRemoveItem(events, data);
                                        self.events(events);
                                    },
                                function () {
                                    $("#dialog-was-deleted").dialog({
                                        resizable: false,
                                        width: 400,
                                        height: 200,
                                        modal: true
                                    });
                                    self.deleteEventLocal(data);
                                });
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
    }

    function turnDate(input) {
        var date = input.slice(6);
        date = date.slice(0, date.length - 2);
        return parseInt(date);
    }

    function getDoubleCharacterValue(val) {
        if (parseInt(val) < 10) {
            return "0" + val + "";
        }
        return val;
    }

    function responseMessageHelper(response) {
        return "Status: " + response.Status + "\n" + "Message: " + response.Message;
    }

    function successHelper(response, funcSuccess, funcError) {
        if (response.Status) {
            if (funcSuccess) {
                funcSuccess(response);
            }
        }
        else {
            if (funcError) {
                funcError(response);
            }
        }
    }

    function EventModel(evnt, model) {
        var self = this;

        self.id = ko.observable(evnt.Id);
        self.title = ko.observable(evnt.Title);
        self.description = ko.observable(evnt.Description != null ? evnt.TextDescription : evnt.Description);
        self.location = ko.observable(evnt.Location);
        self.dateFrom = new Date(turnDate(evnt.FromDate));
        self.dateTo = new Date(turnDate(evnt.ToDate));
        self.active = ko.observable(evnt.Active);
        self.checked = ko.observable(evnt.Checked);
        self.toogleText = ko.observable("");
        self.urlPart = ko.observable("");
        self.errorActivateText = ko.observable("");



        self.shortDescription = ko.computed(function () {
            if (self.description()) {
                var words = self.description().split(' ');
                if (words.length <= 10) return self.description();
                else {
                    var result = "";
                    for (var i = 0; i != 10; i++)
                        result += " " + words[i];
                    return result + "...";
                }
            }

            return "";
        });

        self.isSeen = ko.computed(function () {
            var status = this.checked();
            return status ? "seen" : "not-seen";
        }, self);

        self.updateAccent = ko.computed(function () {
            return this.active() ? "lighten" : "accent";
        }, self);

        self.changeToogleText = ko.computed(function () {
            if (self.active()) {
                self.toogleText("Деактивировать");
                self.urlPart("deactivate");
                self.errorActivateText("активировано");
            }
            else {
                self.toogleText("Активировать");
                self.urlPart("activate");
                self.errorActivateText("деактивировано");
            }
        });

        self.dateAndTime = ko.computed(function () {
            var date;
            date = self.dateFrom.getUTCDate() + " " + monthNames[self.dateFrom.getMonth()] + " " + self.dateFrom.getUTCFullYear() + ", " +
            getDoubleCharacterValue(self.dateFrom.getHours()) + ":" + getDoubleCharacterValue(self.dateFrom.getMinutes()) + " - ";

            if (self.dateFrom.getDay() != self.dateTo.getDay()) {
                date += self.dateTo.getUTCDate() + " " + monthNames[self.dateTo.getMonth()] + " " + self.dateTo.getUTCFullYear() + ", ";
            }
            date += getDoubleCharacterValue(self.dateTo.getUTCHours()) + ":" + getDoubleCharacterValue(self.dateTo.getUTCMinutes());

            return date;
        });

        self.toogleActive = function () {
            adminAjaxHelper.toogleActive(self, self.urlPart(), self.errorActivateText());
        }

        self.goToDetails = function () {
            window.location = url + "/events/details/" + self.id();
        }


    }

    function AdminViewModel(initFunc) {
        var self = this;
        self.events = ko.observableArray([]);
        initFunc(self);

        self.deleteEvent = function (data) {
            adminAjaxHelper.deleteEvent(data, self);
        }

        self.deleteEventLocal = function (data) {
            var events = self.events();
            ko.utils.arrayRemoveItem(events, data);
            self.events(events);
        }
    }

    ko.bindingHandlers.descriminateActive = {
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var isActive = bindingContext.$data.active();
            if (isActive) {
                $(element).removeClass("disabled");
            }
            else {
                $(element).addClass("disabled");
            }
        }
    };
    var viewModel = new AdminViewModel(adminAjaxHelper.loadEvents);
    ko.applyBindings(viewModel);
})();