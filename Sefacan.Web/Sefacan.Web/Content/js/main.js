$(function () {
    $("#searchForm").submit(function (e) {
        var input = $("#term");
        if (input.val()) {
            if (input.val().length > 2)
                location.href = "/Search/" + $("#term").val();
        }
        e.preventDefault();
    });

    if ($(window).scrollTop() > 200)
        $(".btn-top").fadeIn();
    else
        $(".btn-top").fadeOut();

    $(".btn-top").click(function (e) {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        e.preventDefault();
    });

    $(document).on("click", ".reply", function (e) {
        $("#commentRow").appendTo($(this).parent(".comment-item"));
        $("input[name='parentId']").val($(this).attr("data-id"));
        $(".cancel").removeClass("hidden");
        e.preventDefault();
    });

    $(document).on("click", ".cancel", function (e) {
        $("#commentRow").appendTo("#comments");
        $("input[name='parentId']").val(0);
        $(".cancel").addClass("hidden");
        e.preventDefault();
    });

    $("#commentForm").submit(function (e) {
        e.preventDefault();
        if ($("[name='postId']").val() != "" &&
            $("[name='postId']").val() != "0" &&
            $("[name='name']").val() != "" &&
            $("[name='email']").val() != "" &&
            $("[name='message']").val() != "") {

            var form = $(this);
            $.post(form.attr("action"), form.serialize(), function (response) {
                if (response.errorMessage &&
                    response.errorMessage != null) {
                    alert(response.errorMessage);
                } else {
                    if ($("#commentRow").parent().hasClass("post-comments")) {//comment
                        var item = $(".comment-item:first").clone();
                        prepareElement(item, response);
                        $(item).find(".reply").attr("data-id", response.Id);
                        $("#comments").find("h4").after($(item));
                        gotoElement("comments");

                    } else if ($("#commentRow").parent().hasClass("comment-item")) {//child comment
                        var item = $(".comment-subitem:first").clone();
                        prepareElement(item, response);
                        $("#commentRow").parent().append($(item));
                        $(".cancel").click();
                    }
                    $("[name='name']").val("");
                    $("[name='email']").val("");
                    $("[name='message']").val("");
                }
            });
        }
    });
});

$(window).on("scroll", function (e) {
    if ($(window).scrollTop() > 200)
        $(".btn-top").fadeIn();
    else
        $(".btn-top").fadeOut();
});

function prepareElement(item, response) {
    $(item).find(".name").html(response.FullName);
    $(item).find(".date").html(response.CreateDate);
    $(item).find("p").html(response.Content);
    $(item).find("img").attr({ title: response.FullName, alt: response.FullName });
}

function gotoElement(id) {
    // Remove "link" from the ID
    id = id.replace("link", "");
    // Scroll
    $('html, body').animate({
        scrollTop: $("#" + id).offset().top
    }, 'slow');
}