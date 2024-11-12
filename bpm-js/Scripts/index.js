document.getElementById('novo-fluxo').addEventListener('click', () => {
    window.location.href = '/Diagrama/Create';
});

async function carregarFluxos() {
    try {
        const response = await fetch('/Diagrama/GetFluxos');
        const fluxos = await response.json();

        const lista = document.getElementById('fluxo-lista');
        lista.innerHTML = '';

        fluxos.forEach(fluxo => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${fluxo.nome}</td>
                <td><button onclick="editarFluxo(${fluxo.id})">Abrir</button></td>
            `;
            lista.appendChild(row);
        });
    } catch (err) {
        console.error("Erro ao carregar fluxos:", err);
    }
}

function editarFluxo(id) {
    window.location.href = `/Diagrama/Create?id=${id}`;
}

carregarFluxos();
