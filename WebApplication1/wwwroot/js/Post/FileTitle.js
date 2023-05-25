$(document).ready(function () {
    $('form').submit(function (e) {
        var fileTitle = $('input[name="fileTitle"]').val();
        var file = $('input[name="Attachments"]').get(0).files[0];

        if (!fileTitle && file) {
            e.preventDefault(); // 폼 제출 중지
            alert("파일 이름을 입력해주세요."); // 알림 창을 띄웁니다.
        }
    });
});
