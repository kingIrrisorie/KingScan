// URL da API para detalhes do mangá
const API_URL_MANGA_BY_ID = 'https://localhost:5215/api/Mangas/';
const API_URL_CHAPTERS = 'https://localhost:5215/api/Chapters';

// Função para carregar detalhes de um mangá
async function fetchMangaDetails(mangaId) {
    try {
        const response = await fetch(`${API_URL_MANGA_BY_ID}${mangaId}`);
        if (!response.ok) throw new Error(`Erro ${response.status}: ${response.statusText}`);
        const manga = await response.json();
        displayMangaDetails(manga);
    } catch (error) {
        console.error('Erro ao carregar detalhes do mangá:', error);
        alert('Erro ao carregar detalhes do mangá.');
    }
}

// Função para carregar capítulos de um mangá
async function fetchChapters(mangaId) {
    try {
        const response = await fetch(`${API_URL_CHAPTERS}/${mangaId}`);
        if (!response.ok) throw new Error(`Erro ${response.status}: ${response.statusText}`);
        const chapters = await response.json();
        displayChapters(chapters);
    } catch (error) {
        console.error('Erro ao obter capítulos:', error);
        alert('Erro ao carregar os capítulos.');
    }
}

// Exibir detalhes do mangá na página
function displayMangaDetails(manga) {
    const thumbnail = document.querySelector('.manga-thumbnail');
    const title = document.querySelector('.manga-title');
    const description = document.querySelector('.manga-description');
    const genres = document.querySelector('.manga-genres');

    thumbnail.src = manga.thumbnailUrl || 'https://via.placeholder.com/300x400';
    thumbnail.alt = manga.title || 'Sem título';
    title.textContent = manga.title || 'Sem título';
    description.textContent = manga.description || 'Sem descrição';
    genres.textContent = manga.genreNames ? manga.genreNames.join(', ') : 'Sem gênero';

    const backButton = document.querySelector('.back-button');
    if (backButton) {
        backButton.addEventListener('click', () => window.history.back());
    }
}

// Exibir capítulos na página
function displayChapters(chapters) {
    const chapterList = document.querySelector('.chapter-list');
    if (!chapterList) return;
    chapterList.innerHTML = '';

    if (!Array.isArray(chapters) || chapters.length === 0) {
        const li = document.createElement('li');
        li.textContent = 'Nenhum capítulo disponível.';
        chapterList.appendChild(li);
        return;
    }

    chapters.forEach((chapter, index) => {
        const li = document.createElement('li');
        li.textContent = chapter.title || `Capítulo ${index + 1} - Em Breve`;
        chapterList.appendChild(li);
    });
}

// Toggle do menu hamburguer
function toggleMenu() {
    const nav = document.querySelector('nav');
    if (nav) nav.classList.toggle('active');
}

// Fechar o menu ao clicar fora
document.addEventListener('click', (e) => {
    const nav = document.querySelector('nav');
    const menuToggle = document.querySelector('.menu-toggle');
    if (nav && menuToggle && !nav.contains(e.target) && e.target !== menuToggle && nav.classList.contains('active')) {
        nav.classList.remove('active');
    }
});

// Configurar tudo ao carregar a página
document.addEventListener('DOMContentLoaded', () => {
    const urlParams = new URLSearchParams(window.location.search);
    const mangaId = urlParams.get('id');
    if (mangaId) {
        fetchMangaDetails(mangaId);
        fetchChapters(mangaId);
    }

    const menuToggle = document.querySelector('.menu-toggle');
    if (menuToggle) {
        menuToggle.addEventListener('click', toggleMenu);
    }
});