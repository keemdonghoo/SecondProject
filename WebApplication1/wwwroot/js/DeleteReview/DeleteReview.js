$(function () {
    $(".btnDelReview").click(function () {
        let reviewId = $(this).data("review-id");
        let answer = confirm("정말로 삭제하시겠습니까?");
        if (answer) {
            $("form[name='frmDeleteReview'] input[name='reviewId']").val(reviewId);
            $("form[name='frmDeleteReview']").submit();
        }
    });
});
