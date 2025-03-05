// URL da API para detalhes do mangá
const API_URL_MANGA_BY_ID = 'http://localhost:5215/api/Mangas/';

// Função para carregar detalhes de um mangá
async function fetchMangaDetails(mangaId) {
    try {
        const response = await fetch(`${API_URL_MANGA_BY_ID}${mangaId}`);
        if (!response.ok) throw new Error(`Erro ${response.status}`);
        const manga = await response.json();
        displayMangaDetails(manga);
    } catch (error) {
        console.error('Erro ao carregar detalhes do mangá:', error);
        alert('Erro ao carregar detalhes do mangá.');
    }
}

// Exibir detalhes do mangá na página
function displayMangaDetails(manga) {
    const thumbnail = document.querySelector('.manga-thumbnail');
    const title = document.querySelector('.manga-title');
    const description = document.querySelector('.manga-description');
    const genres = document.querySelector('.manga-genres');
    const chapterList = document.querySelector('.chapter-list');

    thumbnail.src = manga.thumbnailUrl || 'https://via.placeholder.com/300x400';
    thumbnail.alt = manga.title;
    title.textContent = manga.title || 'Sem título';
    description.textContent = manga.description || 'Sem descrição';
    genres.textContent = manga.genreNames ? manga.genreNames.join(', ') : 'Sem gênero';

    // Placeholder para capítulos (pode ser expandido com uma API futura)
    chapterList.innerHTML = '';
    for (let i = 1; i <= 5; i++) {
        const li = document.createElement('li');
        li.textContent = `Capítulo ${i} - Em Breve`;
        chapterList.appendChild(li);
    }

    // Botão de voltar
    const backButton = document.querySelector('.back-button');
    backButton.addEventListener('click', () => {
        window.history.back();
    });
}

// Toggle do menu hamburguer
function toggleMenu() {
    const nav = document.querySelector('nav');
    nav.classList.toggle('active');
}

// Fechar o menu ao clicar fora
document.addEventListener('click', (e) => {
    const nav = document.querySelector('nav');
    const menuToggle = document.querySelector('.menu-toggle');
    if (!nav.contains(e.target) && e.target !== menuToggle && nav.classList.contains('active')) {
        nav.classList.remove('active');
    }
});

// Configurar tudo ao carregar a página
document.addEventListener('DOMContentLoaded', () => {
    const urlParams = new URLSearchParams(window.location.search);
    const mangaId = urlParams.get('id');
    if (mangaId) {
        fetchMangaDetails(mangaId);
    }

    // Adicionar evento ao botão do menu
    const menuToggle = document.querySelector('.menu-toggle');
    menuToggle.addEventListener('click', toggleMenu);
});