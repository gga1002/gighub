
var GigsController = function (attendanceService, helper) {
    var button;

    
    var fail = helper.fail;

    var done = function () {
        var text = (button.text() === "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var toggleAttendances = function (e) {
        button = $(e.target);
        var gigId = button.attr("id");
        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendances);
    };

    return {
        init: init
    };

}(AttendanceService, Helper);
