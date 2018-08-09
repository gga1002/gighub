var Helper = function () {

    var fail = function (err) {
        $('#ErrorMessage').text('Error: ' + err);
        $('#myModal').modal('show');
    };

    return {
        fail: fail
    };

}();