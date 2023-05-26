$(document).ready(function () {
    // 회원탈퇴 버튼 클릭 시
    $('#btnDel').click(function () {
        let answer = confirm("정말로 탈퇴시키겠습니까?");
        if (answer) {
            // 선택된 체크박스 값 수집
            var selectedUsers = Array.from($('input[name="selectedUserIds[]"]:checked'))
                .map(function (checkbox) {
                    return checkbox.value;
                });

        
            // selectedUserIds 값을 폼 데이터에 설정
            $('input[name="selectedUserIds[]"]').val(selectedUsers.join(','));


            $("form[name='frmDelete']").submit();
        }
    });
});
