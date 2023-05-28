async function fetchMovieVideos(movieId) {
    const apiKey = 'a64af89e63ea55dc53158eca732fee02';
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
    const apiKey = 'a64af89e63ea55dc53158eca732fee02';
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

function createReviewItem(review) {
    // div 생성
    const reviewDiv = document.createElement('div');
    reviewDiv.className = 'anime__review__item';

    // 내부 HTML 삽입
    const userName = review.userNickname || 'Unknown User'; // 사용자 닉네임 가져오기
    reviewDiv.innerHTML = `
    <div class="anime__review__item__text">
      <h6>${userName} <span> - </span> <span>${review.date}</span></h6>
      <p>${review.content}</p>
    </div>
  `;

    return reviewDiv;
}



//리뷰 항목 생성
// ajax로 post 요청을 보내는 함수
function sendPost(url, data) {
    return new Promise((resolve, reject) => {
        const xhr = new XMLHttpRequest();
        xhr.open("POST", url);
        xhr.setRequestHeader('Content-type', 'application/json');
        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                resolve(xhr.responseText);
            } else {
                reject(xhr.statusText);
            }
        };
        xhr.onerror = () => reject(xhr.statusText);
        xhr.send(JSON.stringify(data));
    });
}

async function postReview(event) {
    event.preventDefault();
    event.stopPropagation();

    const form = event.target;
    const formData = new FormData(form);

    // 폼 잠금
    form.classList.add('disabled');
    form.querySelectorAll('button').forEach(button => {
        button.disabled = true;
    });

    try {
        const data = {
            movieUid: formData.get('movieId'),
            review: formData.get('review'),
            rating: formData.get('rating'),
        };

        // 사용자 정보를 함께 전달
        const userId = sessionStorage.getItem('UserId');
        data.userId = userId;

        const response = await fetch('/Home/ReviewAdd', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (response.ok) {
            const review = await response.json();

            // 새로운 리뷰 항목 생성
            const reviewItem = createReviewItem(review);

            // 리뷰 섹션에 새 리뷰 항목 추가
            const reviewSection = document.querySelector('.anime__details__review');
            reviewSection.prepend(reviewItem);

            // 폼 필드 초기화
            form.reset();
        } else {
            throw new Error('Failed to post review');
        }
    } catch (error) {
        console.error(error);
    } finally {
        // 폼 잠금 해제
        form.classList.remove('disabled');
        form.querySelectorAll('button').forEach(button => {
            button.disabled = false;
        });
    }
}



document.addEventListener('DOMContentLoaded', async function () {
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

    const reviewForm = document.querySelector('.anime__review__form');
    if (reviewForm) {
        reviewForm.removeEventListener('submit', postReview); // 기존 이벤트 리스너 제거
        reviewForm.addEventListener('submit', postReview); // 수정된 이벤트 리스너 등록
    } else {
        console.log("Cannot find element with class '.anime__review__form'");
    }
});
