$(function () {
    $("#signOutLnk").click(function () {
        if (confirm('Sure?')) {
            $.ajax("/Account/SignOut", { type: 'POST' })
                .done(function () { location.reload(); });
        }
        return false;
    });
})