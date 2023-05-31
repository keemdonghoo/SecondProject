$(function () {
    $("#btnLogout").click(function () {
        $("form[name='frmLogout']").submit();
        alert("로그아웃되었습니다.");
    });
});
