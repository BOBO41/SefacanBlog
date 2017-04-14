$(function () {
    var defaults = {
        color: '#3bafda',
        secondaryColor: '#ddd',
        size: 'small'
    };
    var inputs = Array.prototype.slice.call(document.querySelectorAll("input[type='checkbox']"));
    inputs.forEach(function (input) {
        new Switchery(input, defaults);
    });

    $(".submenu > a").click(function (e) {
        e.preventDefault();
        var $li = $(this).parent("li");
        var $ul = $(this).next("ul");

        if ($li.hasClass("open")) {
            $ul.slideUp(350);
            $li.removeClass("open");
        } else {
            $(".nav > li > ul").slideUp(350);
            $(".nav > li").removeClass("open");
            $ul.slideDown(350);
            $li.addClass("open");
        }
    });

    $(".btn-delete").click(function (e) {
        var row = $(this).parent("td").parent("tr");
        var postUrl = $(this).attr("href");

        if (postUrl) {
            e.preventDefault();
            if (confirm("Kayıt silinsin mi?")) {
                $.post(postUrl, function (response) {
                    row.remove();
                });
            }
        }
    });
});