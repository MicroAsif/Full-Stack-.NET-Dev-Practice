var gigsController = function() {
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
        if (button.hasClass("btn-default")) {
            $.post("/api/attendences", { GigId: button.attr("data-gig-id") })
                .done(done)
                .fail(fail);
        }
        else {
            $.ajax({
                    url: "/api/attendences/" + button.attr("data-gig-id"),
                    method: "DELETE"
                }).done(done)
                .fail(fail);
        }

    };
    return {
        init : init
    }
}(); 