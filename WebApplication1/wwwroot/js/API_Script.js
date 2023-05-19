//한국영화진흥위원회 API를 사용하여 최대한 현재 상영중인 영화 받아오기
async function getNowPlayingMoviesInKorea() {
    const apiKey = "b2bebdb778a72426b1ed38c376061f23";
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
    const apiKey = "9587124340afc34dae9ecf63d2710f6f";
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
    
    //영화 개수에 따라 동적으로 실행
    for (const movie of nowPlayingMovies) {
        // 영문 제목이 비어있는 경우 한글 제목을 사용
        const searchQuery = movie.movieNmEn || movie.movieNm;
        const searchResult = await requestTMDBSearchAsync(searchQuery, movie.prdtYear);

        if (!searchResult) {
            continue;
        }

        const movieData = searchResult["results"];

        if (movieData.length === 0) {
            continue;
        }
        
        //영화 제목과 포스터의 정확도를 높임
        const bestMatch = findBestMatch(movie.movieNm, movieData);

        // 성인물(에로) 장르를 제외
        if (bestMatch.adult) {
            continue;
        }

        const posterPath = bestMatch["poster_path"];

        //포스터
        const posterBaseUrl = "https://image.tmdb.org/t/p/w500";
        const posterUrl = posterPath ? `${posterBaseUrl}${posterPath}` : "";

        const movieItem = `
            <div class="col-lg-4 col-md-6 col-sm-6">
                <div class="product__item">
                    <a href="/Home/Detail?title=${movie.movieNm}">
                        <div class="product__item__pic" style="background-image: url('${posterUrl}');">
                            <div class="comment"><i class="fa fa-comments"></i> 11</div>
                            <div class="view"><i class="fa fa-eye"></i> 9141</div>
                        </div>
                    </a>
                    <div class="product__item__text">
                        <ul>
                            <li>Active</li>
                            <li>Movie</li>
                        </ul>
                        <h5><a href="/Home/Detail?title=${movie.movieNm}">${movie.movieNm}</a></h5>
                    </div>
                </div>
            </div>`;
            const parser = new DOMParser();
            const movieItemDOM = parser.parseFromString(movieItem, 'text/html');
            container.appendChild(movieItemDOM.body.firstChild);
    }
}

window.onload = renderMovies;