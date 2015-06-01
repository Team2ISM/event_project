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
        this.toogleActive = function (self) {
            $.ajax({
                url: url + "/admin/events/toggleactive",
                type: "POST",
                data: {
                    id: self.id()
                },
                success: function (response) {
                    successHelper(response, function () {
                        self.active(!self.active());
                    })
                },
                error: function (er) {
                    console.dir(er);
                }
            });
        }

        this.loadEvents = function(self) {
            $.ajax({
                url: url + "/admin/events/getall",
                success: function (response) {
                    successHelper(response, function (response) {
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
                                successHelper(response, function () {
                                    var events = self.events();
                                    ko.utils.arrayRemoveItem(events, data);
                                    self.events(events);
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
        if (parseInt(val) < 9) {
            return "0" + val + "";
        }
        return val;
    }

    function responseMessageHelper(response) {
        return "Status: " + response.Status + "\n" + "Message: " + response.Message;
    }

    function successHelper(response, func) {
        if (response.Status) {
            func(response);
        }
        else {
            console.dir(responseMessageHelper(response))
        }
    }

    function EventModel(evnt, model) {
        var self = this;

        this.id = ko.observable(evnt.Id);
        this.title = ko.observable(evnt.Title);
        this.description = ko.observable(evnt.Description != null ? evnt.TextDescription : evnt.Description);
        this.location = ko.observable(evnt.Location);
        this.dateFrom = new Date(turnDate(evnt.FromDate));
        this.dateTo = new Date(turnDate(evnt.ToDate));
        this.active = ko.observable(evnt.Active);
        this.checked = ko.observable(evnt.Checked);
        this.toogleText = ko.observable("");

        self.shortDescription = ko.computed(function () {
            var words = self.description().split(' ');
            if (words.length <= 10) return self.description();
            else 
            {
                var result = "";
                for (var i = 0; i != 10; i++)
                    result += " " + words[i];
                return result + "...";
            }
        }, self);

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
                self.updateAccent();
            }
            else {
                self.toogleText("Активировать");
                self.updateAccent();
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
            adminAjaxHelper.toogleActive(self);
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

    ko.applyBindings(new AdminViewModel(adminAjaxHelper.loadEvents));
})();