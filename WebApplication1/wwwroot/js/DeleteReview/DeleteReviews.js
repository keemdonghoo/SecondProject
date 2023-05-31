$(document).ready(function () {

    $('#btnDel').click(function () {
        let answer = confirm("정말로 선택한 리뷰들을 삭제시키겠습니까?");
        if (answer) {
            // 선택된 체크박스 값 수집
            var selectedReviews = Array.from($('input[name="selectedReviewIds[]"]:checked'))
                .map(function (checkbox) {
                    return checkbox.value;
                });



            $('input[name="selectedReviewIds[]"]').val(selectedReviews.join(','));


            $("form[name='frmDelete']").submit();
        }
    });
});
