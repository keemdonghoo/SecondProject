//document.addEventListener('DOMContentLoaded', function () {
//    var searchBoxInput = document.querySelector('#searchBox input');
//    if (searchBoxInput) {
//        searchBoxInput.addEventListener('keydown', function (e) {
//            if (e.key === 'Enter') {
//                e.preventDefault();
//                const searchQuery = e.target.value.trim();
//                if (searchQuery !== "")
//                {
//                    // Ajax request to server
//                    $.ajax(
//                        {
//                            url: '/Search/SearchMovie',
//                            data: { 'title': searchQuery },
//                            type: 'GET',
//                            dataType: 'json',
//                            success: function (data) {
//                                if (data && data.Title) {
//                                    // Redirect to the Detail page using the returned data
//                                    window.location.href = '/Home/Detail?title=' + encodeURIComponent(data.Title);
//                                }
//                                else {
//                                    // Handle the case where no movie was found.
//                                    // This might involve showing a message to the user, or redirecting to a different page.
//                                }
//                            },
//                            error: function (jqXHR, textStatus, errorThrown) {
//                                // Handle any errors here.
//                                console.log(textStatus, errorThrown);
//                            }
//                        }
//                    );
//                }
//            }
//        }
//    }
//}
document.addEventListener("DOMContentLoaded", function () {
    var searchBox = document.getElementById("searchBox");
    var searchInput = searchBox.querySelector("input[type='text']");

    searchInput.addEventListener("keydown", function (event) {
        if (event.keyCode === 13) {
            var searchValue = searchInput.value.trim();
            if (searchValue !== "") {
                var url = "http://localhost:5142/Home/Detail?title=" + encodeURIComponent(searchValue);
                window.location.href = url;
            }
        }
    });

    var searchIcon = document.querySelector(".search");
    searchIcon.addEventListener("click", function () {
        searchBox.style.width = "200px";
        searchInput.focus();
    });
});
