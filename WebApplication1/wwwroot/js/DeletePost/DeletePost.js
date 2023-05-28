$(function () {
    // 댓글 삭제 버튼
    $(".btnDelComment").click(function () {
        let commentId = $(this).data("comment-id");
        let answer = confirm("정말로 삭제하시겠습니까?");
        if (answer) {
            $("form[name='frmDeleteComment'] input[name='commentId']").val(commentId);
            $("form[name='frmDeleteComment']").submit();
        }
    });

    // 포스트 삭제 버튼
    $("#btnDel").click(function () {
        let answer = confirm("정말로 삭제하시겠습니까?");
        if (answer) {
            $("form[name='frmDelete']").submit();
        }
    });
});
