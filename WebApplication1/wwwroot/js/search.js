document.querySelector('.search').addEventListener('click', function (e) {
    e.preventDefault();
    var searchBox = document.getElementById('searchBox');
    searchBox.style.width = searchBox.style.width === '0px' ? '150px' : '0px';
});
