$(function () {
    // Handle edit ttheme action.
    $(".theme .actions .edit").live('click', function () {
        var themeid = $(this).closest('.theme').data('themeid');
        location.href = "/Home/Edit/" + themeid;
    });

    // Handle delete theme action.
    $(".theme .actions .delete").live('click', function () {
        var $theme = $(this).closest('.theme');
        if (confirm('削除してもよろしいですか?') === false) return false;

        $.ajax({
            url: '/Home/Delete',
            type: 'POST',
            data: { id: $theme.data('themeid') }
        })
        .done(function () {
            $theme.parent().slideUp(function () { $(this).remove(); });
        });

        return false;
    });

    // Handle vote/revert action.
    $(".vote,.revert", ".theme .actions").live('click', function () {
        var $this = $(this);
        var $theme = $this.closest('.theme');
        $.ajax({
            url: '/Home/' + ($this.hasClass('vote') ? 'Vote' : 'Revert'),
            type: 'POST',
            data: { id: $theme.data('themeid') }
        })
        .done(function (data) {
            $theme.parent().html(data);
        });

        return false;

    });
});