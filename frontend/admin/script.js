const API_URL = "http://localhost:5215/api/Mangas/"; // Atualize para Heroku depois
const ADMIN_API_KEY = "admin-secret-key";

async function fetchMangas() {
    try {
        const response = await fetch(`${API_URL}`, {
            headers: { "X-API-Key": ADMIN_API_KEY }
        });
        if (!response.ok) throw new Error('Erro na API');
        return await response.json();
    } catch (error) {
        console.error('Erro:', error);
        document.getElementById("mangaList").innerHTML = "<p>Erro ao carregar mangás.</p>";
        return [];
    }
}

async function createManga(mangaData) {
    try {
        const response = await fetch(API_URL, {
            method: "POST",
            headers: {
                "X-API-Key": ADMIN_API_KEY,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(mangaData)
        });
        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.error);
        }
        return await response.json();
    } catch (error) {
        alert(`Erro: ${error.message}`);
    }
}

function displayMangas(mangaArray) {
    const mangaList = document.getElementById("mangaList");
    mangaList.innerHTML = "";
    if (mangaArray.length === 0) {
        mangaList.innerHTML = "<p>Nenhum mangá encontrado.</p>";
        return;
    }
    mangaArray.forEach(manga => {
        const mangaCard = document.createElement("div");
        mangaCard.classList.add("manga-card");
        mangaCard.innerHTML = `
            ${manga.thumbnailUrl ? `<img src="${manga.thumbnailUrl}" alt="${manga.title}">` : ''}
            <h2>${manga.title}</h2>
            <p>${manga.description || 'Sem descrição'}</p>
        `;
        mangaList.appendChild(mangaCard);
    });
}

window.onload = async () => {
    const mangas = await fetchMangas();
    displayMangas(mangas);

    document.getElementById("createForm")?.addEventListener("submit", async (e) => {
        e.preventDefault();
        const mangaData = {
            title: document.getElementById("title").value,
            status: document.getElementById("status").value,
            description: document.getElementById("description").value,
            releaseDate: document.getElementById("releaseDate").value,
            thumbnailUrl: document.getElementById("thumbnailUrl").value,
            authorNames: [document.getElementById("author").value],
            genreNames: document.getElementById("genres").value.split(",")
        };
        await createManga(mangaData);
        const updatedMangas = await fetchMangas();
        displayMangas(updatedMangas);
    });
};