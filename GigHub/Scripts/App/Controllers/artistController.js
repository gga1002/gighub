var ArtistController = function (artistService, helper) {
    
    var button;
    var fail = helper.fail;

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollowing);
    };

    var done = function () {
        var text = (button.text() == "Following") ? "Follow" : "Following";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var toggleFollowing = function (e) {

        button = $(e.target);
        var artistId = button.attr("data-user-id");

        if (!button.text().includes("Following"))
            artistService.follow(artistId, done, fail);
        else
            artistService.unfollow(artistId, done, fail);
    };


    return {
        init: init
    };

}(ArtistService, Helper);