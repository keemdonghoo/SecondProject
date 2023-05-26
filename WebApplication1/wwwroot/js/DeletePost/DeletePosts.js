$(document).ready(function () {

    $('#btnDel').click(function () {
        let answer = confirm("정말로 선택한 글들을 삭제시키겠습니까?");
        if (answer) {
            // 선택된 체크박스 값 수집
            var selectedPosts = Array.from($('input[name="selectedPostIds[]"]:checked'))
                .map(function (checkbox) {
                    return checkbox.value;
                });


       
            $('input[name="selectedPostIds[]"]').val(selectedPosts.join(','));


            $("form[name='frmDelete']").submit();
        }
    });
});
