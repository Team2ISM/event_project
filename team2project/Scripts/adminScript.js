(function () {
    var url = "http://" + window.location.host;

    function EventModel(evnt, model) {
        var self = this;

        this.id = ko.observable(evnt.Id);
        this.title = ko.observable(evnt.Title);
        this.description = ko.observable(evnt.Description);
        this.location = ko.observable(evnt.Location);
        debugger;
        this.dateFrom = new Date(turnDate(evnt.FromDate));
        this.dateTo = new Date(turnDate(evnt.ToDate));
        this.active = ko.observable(evnt.Active);
        this.toogleText = ko.observable("");

        function turnDate(input) {
            var date = input.slice(6);
            date = date.slice(0, date.length - 2);
            return parseInt(date);
        }

        self.changeToogleText = ko.computed(function () {
            if (self.active()) {
                self.toogleText("Деактивировать");
            }
            else {
                self.toogleText("Активировать");
            }
        });

        self.dateAndTime = ko.computed(function () {
            var date;
            debugger;
            date = self.dateFrom.getDay() + " " + self.dateFrom.getMonth().toString() + " " + self.dateFrom.getFullYear();
            return date;
        });

        self.toogleActive = function () {
            $.ajax({
                url: url + "/ToogleIsActiveEvent/" + self.id(),
                success: function (response) {
                    self.active(!self.active());
                },
                error: function (er) {
                    console.dir(er);
                }
            });
        }

        self.goToDetails = function () {
            window.location.replace(url + "/Details/" + self.id());
        }
    }

    function adminViewModel() {
        var self = this;
        self.events = ko.observableArray([]);
        loadEvents(self);

        self.deleteEvent = function (data) {
            $.ajax({
                url: url + "/DeleteEvent/" + data.id(),
                success: function (response) {
                    var events = self.events();
                    ko.utils.arrayRemoveItem(events, data);
                    self.events(events);
                },
                error: function (er) {
                    console.dir(er);
                }
            });
        }

        function loadEvents(self) {
            $.ajax({
                url: url + "/getEventsToAdminPage",
                success: function (response) {
                    var events = JSON.parse(response);
                    for (var i in events) {
                        self.events.push(new EventModel(events[i], self));
                    }
                },
                error: function (er) {
                    console.dir(er);
                }
            });
        }
    }
    ko.applyBindings(new adminViewModel());
})();