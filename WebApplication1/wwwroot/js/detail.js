//TMDB API KEY
const apiKey = 'a64af89e63ea55dc53158eca732fee02';
//받아온 영화제목 데이터로 해당 영화 상세정보 받아오기
async function fetchMovieDetails(title) {
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
//해당 영화 트레일러 받아오기
async function fetchMovieVideos(movieId) {
    const response = await fetch(`https://api.themoviedb.org/3/movie/${movieId}/videos?api_key=${apiKey}&language=en-US`);
    const data = await response.json();
    const trailer = data.results.find(video => video.type === 'Trailer');
    if (trailer) {
        return `https://www.youtube.com/watch?v=${trailer.key}`;
    }
    return null;
}
//포스터 기본 URL
function createImageUrl(path) {
    return `https://image.tmdb.org/t/p/w500${path}`;
}
//상세정보 화면에 출력
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
//시작
async function init() {
    const title = getParameterByName("title");
    if (title) {
        const movieDetails = await fetchMovieDetails(title);
        displayMovieDetails(movieDetails);

        const trailerLink = await fetchMovieVideos(movieDetails.id);
        if (trailerLink) {
            const trailerBtn = document.querySelector('.anime__details__btn a');
            trailerBtn.href = trailerLink;
            trailerBtn.target = '_blank';
            trailerBtn.innerText = 'Trailer';
            trailerBtn.classList.add('follow-btn');
        }
    }
}


///////////////////////////////////////////////////////////////////////////////////////
//화면에 리뷰 출력
function createReviewItem(review) {
    Console.WriteLine("createReviewItem() 호출");
    // div 생성
    const reviewDiv = document.createElement('div');
    reviewDiv.className = 'anime__review__item';

    // 내부 HTML 삽입
    const userName = review.userNickname; // 사용자 닉네임 가져오기
    reviewDiv.innerHTML = `
    <div class="anime__review__item__text">
      <h6>${userName} <span> - </span> <span>${review.date}</span></h6>
      <p>${review.content}</p>
    </div>
  `;

    return reviewDiv;
}

// AJAX를 사용하여 HTTP POST 요청을 보내는 역할
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

//폼 제출 이벤트가 발생했을 때 호출
//폼 데이터를 가져와서 DB에 저장
async function postReview(event) {
    Console.WriteLine("postReview() 호출");
    event.preventDefault();
    event.stopPropagation();

    const form = event.target;
    const formData = new FormData(form);


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
            

            var confirmLogin = confirm("로그인이 필요한 서비스입니다. 로그인하시겠습니까?");
            if (confirmLogin) {
                // 로그인 페이지로 이동하는 코드 추가
                window.location.href = "/login/login";
            }
            const textarea = document.querySelector('#reviewtext');
            textarea.focus();
            textarea.value = '';
            return false;
        }
    } catch (error) {
        console.error(error);
    } finally {
        //const textarea = document.querySelector('#reviewtext');
        //textarea.focus();
        //textarea.value = '';

        //location.reload();
        

    }
}

document.addEventListener('DOMContentLoaded', init , async function () {
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
