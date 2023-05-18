async function fetchMovieVideos(movieId) {
    const apiKey = '9587124340afc34dae9ecf63d2710f6f';
    const response = await fetch(`https://api.themoviedb.org/3/movie/${movieId}/videos?api_key=${apiKey}&language=en-US`);
    const data = await response.json();
    return data.results;
}

async function getMovieTrailer(movieId) {
    const videos = await fetchMovieVideos(movieId);
    const trailer = videos.find(video => video.type === 'Trailer');
    if (trailer) {
        return `https://www.youtube.com/watch?v=${trailer.key}`;
    }
    return null;
}

async function fetchMovieDetails(title) {
    const apiKey = '9587124340afc34dae9ecf63d2710f6f';
    const response = await fetch(`https://api.themoviedb.org/3/search/movie?api_key=${apiKey}&query=${encodeURIComponent(title)}&language=ko-KR`);
    const data = await response.json();
    if (data.results.length > 0) {
        const movieId = data.results[0].id;
        const detailsResponse = await fetch(`https://api.themoviedb.org/3/movie/${movieId}?api_key=${apiKey}&language=ko-KR`);
        const detailsData = await detailsResponse.json();
        return detailsData;
    }
    return null;
}

async function getMovieDetails(title) {
    const movieDetails = await fetchMovieDetails(title);
    return movieDetails;
}

function displayMovieDetails(movieDetails) {
    if (!movieDetails) return;

    const posterUrl = `https://image.tmdb.org/t/p/w500${movieDetails.poster_path}`;
    document.querySelector('.anime__details__pic').style.backgroundImage = `url('${posterUrl}')`;
    document.querySelector('.anime__details__title h3').innerText = movieDetails.title;
    document.querySelector('.rating').innerText = movieDetails.vote_average.toFixed(1);
    document.querySelector('.vote-count').innerText = `${movieDetails.vote_count} Votes`;
    document.querySelector('.plot').innerText = movieDetails.overview;

    const leftDetailsColumn = document.querySelector('.movie-left-details');
    const rightDetailsColumn = document.querySelector('.movie-right-details');

    // 왼쪽 열에 상세 정보 추가
    leftDetailsColumn.innerHTML = `
        <li><span>Type:</span> Movie</li>
        <li><span>Runtime:</span> ${movieDetails.runtime} min</li>
        <li><span>Release Date:</span> ${movieDetails.release_date}</li>
    `;

    // 오른쪽 열에 상세 정보 추가
    const genres = movieDetails.genres.map(genre => genre.name).join(', ');
    const productionCompanies = movieDetails.production_companies.map(company => company.name).join(', ') || 'Unknown';
    const status = movieDetails.status;

    rightDetailsColumn.innerHTML = `
        <li><span>Genres:</span> ${genres}</li>
        <li><span>Production:</span> ${productionCompanies}</li>
        <li><span>Status:</span> ${status}</li>
    `;
}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    const regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)');
    const results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

async function init() {
    const title = getParameterByName("title");
    if (title) {
        const movieDetails = await getMovieDetails(title);
        displayMovieDetails(movieDetails);

        // 트레일러 링크 가져오기
        const trailerLink = await getMovieTrailer(movieDetails.id);
        if (trailerLink) {
            // 트레일러 버튼에 링크 추가하기
            const trailerBtn = document.querySelector('.anime__details__btn a');
            trailerBtn.href = trailerLink;
            trailerBtn.target = '_blank';
            trailerBtn.innerText = 'Trailer';
            trailerBtn.classList.add('follow-btn');
        }
    }
}

document.addEventListener("DOMContentLoaded", init);