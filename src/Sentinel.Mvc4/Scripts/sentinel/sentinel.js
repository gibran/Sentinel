var TestResult = function (testData) {
    var self = this;

    self.name = testData.name;
    self.description = testData.description;
    self.state = testData.eventTypeName;
    self.message = testData.message;
};

var TestViewModel = function () {
    var self = this;

    self.testResults = ko.observableArray();

    self.Init = function () {
        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: '/api/sentinel',
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.testResults.push(new TestResult(data[i]));
                }
            },
            fail: function (error) {
            }
        });
    }

    this.Init();
};

ko.applyBindings(new TestViewModel());