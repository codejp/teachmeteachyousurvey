$(function () {

    var $textarea = $('textarea');
    $textarea.focus();
    var maxchar = $textarea.data('val-length-max');

    var $charcounter = $('#charcounter');
    var updatecharcounter = function () {
        $charcounter.text($textarea.val().length + "/" + maxchar + '文字');
    };
    updatecharcounter();

    $textarea
        .keydown(updatecharcounter)
        .keyup(updatecharcounter)
        .change(updatecharcounter);
});