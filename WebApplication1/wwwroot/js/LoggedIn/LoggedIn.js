$(document).ready(function () {
    $("#btnLike").click(function () {
        var confirmLogin = confirm("로그인이 필요한 서비스입니다. 로그인하시겠습니까?");
        if (confirmLogin) {
            // 로그인 페이지로 이동하는 코드 추가
            window.location.href = "/login/login";
        }
        return false;
    });
});


$(document).ready(function () {
    $("#btnComment").click(function () {
        var confirmLogin = confirm("로그인이 필요한 서비스입니다. 로그인하시겠습니까?");
        if (confirmLogin) {
            // 로그인 페이지로 이동하는 코드 추가
            window.location.href = "/login/login";
        }
        return false;
    });
});

$(document).ready(function () {
    $("#btnCreate").click(function () {
        var confirmLogin = confirm("로그인이 필요한 서비스입니다. 로그인하시겠습니까?");
        if (confirmLogin) {
            // 로그인 페이지로 이동하는 코드 추가
            window.location.href = "/login/login";
        }
        return false;
    });
});

