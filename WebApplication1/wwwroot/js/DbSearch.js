document.addEventListener("DOMContentLoaded", function () {
    var searchBox = document.getElementById("searchBox");
    var searchInput = searchBox.querySelector("input[type='text']");

    searchInput.addEventListener("keydown", function (event) {
        if (event.keyCode === 13) {
            var searchValue = searchInput.value.trim();
            if (searchValue !== "") {
                var url = "http://localhost:5000/Home/Detail?title=" + encodeURIComponent(searchValue);
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
