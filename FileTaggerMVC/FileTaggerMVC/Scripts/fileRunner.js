$(function () {
    $("a[data-filepath]").click(function (evt) {
        $(this).next().hide();
        $.ajax({
            type: "GET",
            url: "/File/Run",
            data: "filePath=" + escape($(evt.target).data("filepath"))
        }).done(function (data) {
            if (data == "False") {
                $(evt.target).next().show();
            }
        }).fail(function () {
            $(evt.target).next().show();
        });
    });
    $("a[data-filepath]").next().hide();
});