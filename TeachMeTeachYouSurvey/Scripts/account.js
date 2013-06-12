$(function () {
    $("#signOutLnk").click(function () {
        if (confirm('サインアウトしてもよろしいですか?')) {
            $.ajax("/Account/SignOut", { type: 'POST' })
                .done(function () { location.reload(); });
        }
        return false;
    });
})