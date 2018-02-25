var gigsController = function (attendenceService) {
    var button;
    var init = function (container) {
        $(container).on("click", ".js-toggle-attendence", toggleAttendence);
       
    }


    var fail = function () {
        alert("something failed");
    };
    var done = function () {
        var text = (button).text() === "Going" ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };


    var toggleAttendence = function (e) {
        button = $(e.target);
        gigId = button.attr("data-gig-id");
        if (button.hasClass("btn-default"))
            attendenceService.createAttendence(gigId, done, fail);
        else
            attendenceService.deleteAttendence(gigId, done, fail);

    };


    return {
        init: init
    }
}(AttendenceService);