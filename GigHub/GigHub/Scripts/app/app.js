var AttendenceService = function() {

    var createAttendence = function(gigId, done, fail) {
        $.post("/api/attendences", { GigId: gigId })
            .done(done)
            .fail(fail);

    };
    var deleteAttendence = function(gigId, done, fail) {
        $.ajax({
                url: "/api/attendences/" + gigId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);

    }

    return {
        createAttendence: createAttendence,
        deleteAttendence: deleteAttendence
    }
}();

var gigsController = function (attendenceService) {
    var button;
    var init = function() {
        $(".js-toggle-attendence").click(toggleAttendence);
    }


    var fail = function() {
        alert("something failed");
    };
    var done = function () {
        var text = (button).text() === "Going" ? "Going?" : "Going"; 
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    
    var toggleAttendence = function(e) {
        button = $(e.target);
        gigId = button.attr("data-gig-id");
        if (button.hasClass("btn-default"))
            attendenceService.createAttendence(gigId, done, fail);
        else
            attendenceService.deleteAttendence(gigId, done, fail);

    };

   
    return {
        init : init
    }
}(AttendenceService);