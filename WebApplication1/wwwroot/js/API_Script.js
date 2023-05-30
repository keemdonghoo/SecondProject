async function onMoviePosterClick(tmdbId, title) {
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



//한국영화진흥위원회 API를 사용하여 최대한 현재 상영중인 영화 받아오기
async function getNowPlayingMoviesInKorea() {
    const apiKey = "295ad79fa848cceace936f1e2c005bf7";
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const date = String(today.getDate()).padStart(2, '0');
    const targetDt = `${year}${month}${date}`;
    const openStartDt = `${year}`;

    let allMovies = [];

    for (let curPage = 1; curPage <= 10; curPage++) {
        const url = `http://www.kobis.or.kr/kobisopenapi/webservice/rest/movie/searchMovieList.json?key=${apiKey}&openStartDt=${openStartDt}&itemPerPage=10&curPage=${curPage}`;

        const response = await fetch(url);
        const data = await response.json();
        const movies = data.movieListResult?.movieList || [];

        allMovies = allMovies.concat(movies);
    }

    return allMovies.filter(movie => {
        const movieOpenDt = movie.openDt.replace(/-/g, ''); // 날짜 형식을 YYYYMMDD로 변환합니다.
        return movieOpenDt <= targetDt && movie.typeNm === "장편";
    });
}

//한국영화진흥위원회 API를 사용하여 TMDB API로 영화 포스터 및 기타 상세정보 받아오기
async function requestTMDBSearchAsync(query, prdtYear) {
    const apiKey = "a64af89e63ea55dc53158eca732fee02";
    const apiUrl = `https://api.themoviedb.org/3/search/movie?api_key=${apiKey}&language=ko-KR&query=${encodeURIComponent(query)}&year=${prdtYear}`;

    const response = await fetch(apiUrl);

    if (response.ok) {
        const jsonResult = await response.json();
        return jsonResult;
    }

    return null;
}

//두 문자열 간의 유사성을 측정하는 함수
function levenshteinDistance(a, b) {
    const matrix = [];

    for (let i = 0; i <= b.length; i++) {
        matrix[i] = [i];
    }

    for (let i = 0; i <= a.length; i++) {
        matrix[0][i] = i;
    }

    for (let i = 1; i <= b.length; i++) {
        for (let j = 1; j <= a.length; j++) {
            if (b.charAt(i - 1) === a.charAt(j - 1)) {
                matrix[i][j] = matrix[i - 1][j - 1];
            } else {
                matrix[i][j] = Math.min(matrix[i - 1][j - 1] + 1, matrix[i][j - 1] + 1, matrix[i - 1][j] + 1);
            }
        }
    }

    return matrix[b.length][a.length];
}

//가장 적합한 영화를 선택하는 함수
function findBestMatch(originalTitle, searchResults) {
    let bestMatch = null;
    let bestMatchDistance = Infinity;

    for (const result of searchResults) {
        const distance = levenshteinDistance(originalTitle.toLowerCase(), result.title.toLowerCase());

        if (distance < bestMatchDistance) {
            bestMatch = result;
            bestMatchDistance = distance;
        }
    }

    return bestMatch;
}


//영화 제목 및 포스터 받아오기
async function renderMovies() {
    const nowPlayingMovies = await getNowPlayingMoviesInKorea();
    const container = document.getElementById("movie-container");

    for (const movie of nowPlayingMovies) {
        const searchQuery = movie.movieNmEn || movie.movieNm;
        const searchResult = await requestTMDBSearchAsync(searchQuery, movie.prdtYear);

        if (!searchResult) {
            continue;
        }

        const movieData = searchResult["results"];

        if (movieData.length === 0) {
            continue;
        }

        const bestMatch = findBestMatch(movie.movieNm, movieData);

        if (bestMatch.adult) {
            continue;
        }

        const posterPath = bestMatch["poster_path"];
        const posterBaseUrl = "https://image.tmdb.org/t/p/w500";
        const posterUrl = posterPath ? `${posterBaseUrl}${posterPath}` : "";
        const detailUrlBase = document.body.dataset.detailUrlBase;

        const movieItem = `
        <div class="col-lg-4 col-md-6 col-sm-6">
            <div class="product__item">

                <a href="javascript:void(0)" onclick="onMoviePosterClick(${bestMatch.id}, '${movie.movieNm}')">
                    <div class="product__item__pic" style="background-image: url('${posterUrl}');">
                        <div class="comment"><i class="fa fa-comments"></i> ${commentCount}</div>
                        <div class="view"><i class="fa fa-eye"></i> 9141</div>
                    </div>
                </a>

                <div class="product__item__text">
                    <ul>
                        <li>Active</li>
                        <li>Movie</li>
                    </ul>
                    <h5><a href="javascript:void(0)" onclick="onMoviePosterClick(${bestMatch.id}, '${movie.movieNm}')">${movie.movieNm}</a></h5>
                    <input type="hidden" value="${bestMatch.id}" />
                </div>
            </div>
        </div>`;


        const parser = new DOMParser();
        const movieItemDOM = parser.parseFromString(movieItem, 'text/html');
        container.appendChild(movieItemDOM.body.firstChild);
    }
}

window.onload = renderMovies;
