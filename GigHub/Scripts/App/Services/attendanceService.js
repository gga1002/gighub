var AttendanceService = function () {

    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
        .done(done)
        .fail(function (jqXHR, textStatus, err) {
            fail(err);
        });
    };
    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"
        })
        .done(done)
        .fail(function (jqXHR, textStatus, err) {
            fail(rr);
        });
    }

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }

}();