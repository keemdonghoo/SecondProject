document.addEventListener("DOMContentLoaded", function () {
    var searchBox = document.getElementById("searchBox");
    var searchInput = searchBox.querySelector("input[type='text']");

    searchInput.addEventListener("keydown", async function (event) {
        if (event.keyCode === 13) {
            var searchValue = searchInput.value.trim();
            if (searchValue !== "") {
                var searchResults = await requestTMDBSearchAsync(searchValue);
                var container = document.getElementById("movie-container");
                container.innerHTML = ""; // 기존 영화 목록을 지웁니다.

                if (!searchResults || searchResults.results.length === 0) {
                    // 결과가 없으면
                    container.innerHTML = "검색 결과가 없습니다.";
                } else {
                    // 결과가 있으면
                    for (const movie of searchResults.results) {
                        const posterPath = movie["poster_path"];

                        // 포스터 정보가 없으면 건너뜁니다.
                        if (!posterPath) {
                            continue;
                        }
                        const posterBaseUrl = "https://image.tmdb.org/t/p/w500";
                        const posterUrl = posterPath ? `${posterBaseUrl}${posterPath}` : "";

                        const movieItem = `
                        <div class="col-lg-4 col-md-6 col-sm-6">
                            <div class="product__item">
                                <a href="javascript:void(0)" onclick="onMoviePosterClick_2(${movie.id}, '${movie.title}')">
                                    <div class="product__item__pic" style="background-image: url('${posterUrl}');">
                                        <div class="comment"><i class="fa fa-comments"></i>11</div>
                                        <div class="view"><i class="fa fa-eye"></i></div>
                                    </div>
                                </a>
                                <div class="product__item__text">
                                    <ul>
                                        <li>Active</li>
                                        <li>Movie</li>
                                    </ul>
                                    <h5><a href="javascript:void(0)" onclick="onMoviePosterClick_2(${movie.id}, '${movie.title}')">${movie.title}</a></h5>
                                    <input type="hidden" value="${movie.id}" />
                                </div>
                            </div>
                        </div>`;

                        const parser = new DOMParser();
                        const movieItemDOM = parser.parseFromString(movieItem, 'text/html');
                        container.appendChild(movieItemDOM.body.firstChild);
                    }
                }
            }
        }
    });

    var searchIcon = document.querySelector(".search");
    searchIcon.addEventListener("click", function () {
        searchBox.style.width = "200px";
        searchInput.focus();
    });
});

// 이전에 구현한 requestTMDBSearchAsync 함수를 사용합니다.
// requestTMDBSearchAsync 함수는 검색어를 받아 TMDB API로부터 영화 검색 결과를 가져오는 기능을 합니다.
async function requestTMDBSearchAsync(query) {
    const apiKey = "a64af89e63ea55dc53158eca732fee02";
    const apiUrl = `https://api.themoviedb.org/3/search/movie?api_key=${apiKey}&language=ko-KR&query=${encodeURIComponent(query)}`;

    const response = await fetch(apiUrl);

    if (response.ok) {
        const jsonResult = await response.json();
        return jsonResult;
    }

    return null;
}

async function onMoviePosterClick_2(tmdbId, title) {
    console.log(`tmdbId: ${tmdbId}, title: ${title}`);
    const response = await fetch('/Movie/EnsureMovieInDatabase', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ tmdbid: tmdbId, tmdbTitle: title }),
    });
    if (!response.ok) {
        console.error("HTTP Status Code:", response.status, response.statusText);
        try {
            const errorResponse = await response.json();
            console.error("Server Error Response:", errorResponse.message);
        } catch (error) {
            console.error("Failed to parse server error response.");
        }
        console.error("Failed to ensure the movie in the database.");
        return;
    }
    const result = await response.json();
    window.location.href = result.detailUrl;
}