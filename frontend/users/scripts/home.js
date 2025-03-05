// URLs da API
const API_URL_MANGAS = 'http://localhost:5215/api/Mangas';
//const API_URL_CHAPTERS = 'http://localhost:5000/api/capitulos'; // Exemplo
//const API_URL_REVIEWS = 'http://localhost:5000/api/resenhas';   // Exemplo

// Placeholders para simular capítulos e resenhas
const chapterData = [
    { id: 1, capitulo: "Capítulo 1 - O Início" },
    { id: 2, capitulo: "Capítulo 2 - A Jornada" },
    { id: 3, capitulo: "Capítulo 3 - O Confronto" },
    { id: 4, capitulo: "Capítulo 4 - Revelações" }
];

const reviewData = [
    { id: 1, resenha: "Resenha: Uma obra incrível!" },
    { id: 2, resenha: "Resenha: História cativante." },
    { id: 3, resenha: "Resenha: Arte impressionante." },
    { id: 4, resenha: "Resenha: Final emocionante." }
];

// Função para carregar dados da API
async function fetchData(url, containerId) {
    try {
        const response = await fetch(url);
        if (!response.ok) throw new Error(`Erro ${response.status}`);
        const data = await response.json();
        displayItems(data, containerId);
    } catch (error) {
        console.error(`Erro ao carregar ${containerId}:`, error);
        document.getElementById(containerId).innerHTML = '<div>Erro ao carregar conteúdo.</div>';
    }
}

// Exibir itens no carrossel como cards
function displayItems(items, containerId) {
    const container = document.getElementById(containerId);
    container.innerHTML = ''; // Limpa antes de adicionar

    items.forEach(item => {
        const div = document.createElement('div');
        if (containerId === 'manga-list') {
            // Cria um card para mangás com thumbnail, título e gênero
            div.innerHTML = `
                <img src="${item.thumbnailUrl || 'https://via.placeholder.com/200x150'}" alt="${item.title}">
                <div class="card-title">${item.title || 'Sem título'}</div>
                <div class="card-genre">${item.genreNames ? item.genreNames.join(', ') : 'Sem gênero'}</div>
            `;
            div.dataset.id = item.id;
            div.addEventListener('click', () => {
                window.location.href = `manga/manga.html?id=${item.id}`; // Ajuste para a nova pasta
            });
        } else if (containerId === 'chapter-list') {
            div.textContent = item.capitulo;
        } else if (containerId === 'review-list') {
            div.textContent = item.resenha;
        }
        div.dataset.id = item.id;
        container.appendChild(div);
    });

    // Atualiza os botões após carregar os itens
    updateCarouselButtons(containerId);
    setupSwipe(containerId); // Adiciona suporte a swipe
}

// Controle do carrossel
function setupCarousel(containerId) {
    const container = document.getElementById(containerId);
    const prevBtn = document.querySelector(`.prev[data-target="${containerId}"]`);
    const nextBtn = document.querySelector(`.next[data-target="${containerId}"]`);
    const itemWidth = 220; // Largura do item (ajustável)

    let scrollPosition = 0;

    nextBtn.addEventListener('click', () => {
        if (!nextBtn.classList.contains('disabled')) {
            scrollPosition += itemWidth;
            container.scrollTo({ left: scrollPosition, behavior: 'smooth' });
            updateCarouselButtons(containerId);
        }
    });

    prevBtn.addEventListener('click', () => {
        if (!prevBtn.classList.contains('disabled')) {
            scrollPosition -= itemWidth;
            if (scrollPosition < 0) scrollPosition = 0;
            container.scrollTo({ left: scrollPosition, behavior: 'smooth' });
            updateCarouselButtons(containerId);
        }
    });

    // Adiciona suporte a swipe
    setupSwipe(containerId);
}

// Atualiza o estado dos botões do carrossel
function updateCarouselButtons(containerId) {
    const container = document.getElementById(containerId);
    const prevBtn = document.querySelector(`.prev[data-target="${containerId}"]`);
    const nextBtn = document.querySelector(`.next[data-target="${containerId}"]`);

    const maxScroll = container.scrollWidth - container.clientWidth;
    prevBtn.classList.toggle('disabled', container.scrollLeft <= 0);
    nextBtn.classList.toggle('disabled', container.scrollLeft >= maxScroll);
}

// Adiciona suporte a swipe nos carrosséis
function setupSwipe(containerId) {
    const container = document.getElementById(containerId);
    let touchStartX = 0;
    let touchEndX = 0;

    container.addEventListener('touchstart', (e) => {
        touchStartX = e.changedTouches[0].screenX;
    });

    container.addEventListener('touchmove', (e) => {
        touchEndX = e.changedTouches[0].screenX;
    });

    container.addEventListener('touchend', () => {
        const deltaX = touchEndX - touchStartX;
        const itemWidth = 220; // Mesmo valor usado no setupCarousel

        if (deltaX > 50) { // Swipe para a esquerda
            let scrollPosition = container.scrollLeft - itemWidth;
            if (scrollPosition < 0) scrollPosition = 0;
            container.scrollTo({ left: scrollPosition, behavior: 'smooth' });
        } else if (deltaX < -50) { // Swipe para a direita
            let scrollPosition = container.scrollLeft + itemWidth;
            const maxScroll = container.scrollWidth - container.clientWidth;
            if (scrollPosition > maxScroll) scrollPosition = maxScroll;
            container.scrollTo({ left: scrollPosition, behavior: 'smooth' });
        }
        updateCarouselButtons(containerId);
    });
}

// Manipular envio do formulário de contato
function handleContactForm() {
    const form = document.getElementById('contact-form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const name = document.getElementById('name').value;
        const email = document.getElementById('email').value;
        const message = document.getElementById('message').value;

        console.log('Formulário enviado:', { name, email, message });
        alert('Mensagem enviada com sucesso!');
        form.reset();
    });
}

// Exibir "Em Breve" ao clicar nos links
function showComingSoon(sectionId) {
    console.log(`Tentando mostrar 'Em Breve' na seção: ${sectionId}`);
    const section = document.getElementById(sectionId);
    if (section) {
        const comingSoon = section.querySelector('.coming-soon');
        if (comingSoon) {
            comingSoon.style.display = 'block';
            console.log(`'Em Breve' exibido em ${sectionId}`);
            setTimeout(() => {
                comingSoon.style.display = 'none';
                console.log(`'Em Breve' oculto em ${sectionId}`);
            }, 3000); // Exibe por 3 segundos
        } else {
            console.error(`Elemento .coming-soon não encontrado em ${sectionId}`);
        }
    } else {
        console.error(`Seção ${sectionId} não encontrada`);
    }
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

// Fechar o menu ao clicar em um link e exibir "Em Breve"
document.querySelectorAll('.nav-menu a').forEach(link => {
    link.addEventListener('click', (e) => {
        const nav = document.querySelector('nav');
        if (nav.classList.contains('active')) {
            nav.classList.remove('active');
        }
        const sectionId = link.getAttribute('href').substring(1); // Remove o '#'
        if (sectionId !== 'home') {
            e.preventDefault(); // Impede a navegação
            showComingSoon(sectionId);
        }
    });
});

// Configurar tudo ao carregar a página
document.addEventListener('DOMContentLoaded', () => {
    // Carregar mangás da API para "Destaques"
    fetchData(API_URL_MANGAS, 'manga-list');

    // Carregar placeholders para "Capítulos Recentes" e "Resenhas"
    displayItems(chapterData, 'chapter-list');
    displayItems(reviewData, 'review-list');

    // Configurar carrosséis
    setupCarousel('manga-list');
    setupCarousel('chapter-list');
    setupCarousel('review-list');

    // Configurar formulário
    handleContactForm();

    // Adicionar evento ao botão do menu
    const menuToggle = document.querySelector('.menu-toggle');
    menuToggle.addEventListener('click', toggleMenu);
});
// sugestoes para o back-end (apenas como referência para o futuro):
/*
// Para "Capítulos Recentes", você poderia criar um endpoint como este:
// [Route("api/[controller]")]
// [ApiController]
// public class CapitulosController : ControllerBase {
//     private readonly MangaService _mangaService;
//     public CapitulosController(MangaService mangaService) {
//         _mangaService = mangaService;
//     }
//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<CapituloDTO>>> GetRecentChapters() {
//         var chapters = await _mangaService.GetRecentChaptersAsync();
//         return Ok(chapters);
//     }
// }
// public class CapituloDTO {
//     public int Id { get; set; }
//     public string Capitulo { get; set; } // Ex.: "Capítulo 1 - O Início"
//     public int MangaId { get; set; }
//     public DateTime ReleaseDate { get; set; }
// }
// Então, substituir: displayItems(chapterData, 'chapter-list') por fetchData('http://localhost:5000/api/Capitulos', 'chapter-list');

// Para "Resenhas", algo assim:
// [Route("api/[controller]")]
// [ApiController]
// public class ResenhasController : ControllerBase {
//     private readonly MangaService _mangaService;
//     public ResenhasController(MangaService mangaService) {
//         _mangaService = mangaService;
//     }
//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<ResenhaDTO>>> GetReviews() {
//         var reviews = await _mangaService.GetReviewsAsync();
//         return Ok(reviews);
//     }
// }
// public class ResenhaDTO {
//     public int Id { get; set; }
//     public string Resenha { get; set; } // Ex.: "Uma obra incrível!"
//     public int MangaId { get; set; }
//     public DateTime CreatedAt { get; set; }
// }
// Então, substituir: displayItems(reviewData, 'review-list') por fetchData('http://localhost:5000/api/Resenhas', 'review-list');
*/