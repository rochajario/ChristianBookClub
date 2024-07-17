function iniciarContador(dataAlvo, elementId) {
    const clockRender = document.getElementById(elementId);

    function atualizarContador() {
        const agora = new Date().getTime();
        const distancia = new Date(dataAlvo).getTime() - agora;

        if (distancia <= 0) {
            clockRender.innerHTML = "Ao Vivo";
            clockRender.className = "d-inline-block mb-2 text-danger lead fw-3 fw-semibold";
            ativaSalaDeReuniao();
            clearInterval(intervalo);
            return;
        }

        const days = Math.floor(distancia / (1000 * 60 * 60 * 24));
        const hours = Math.floor((distancia % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        const minutes = Math.floor((distancia % (1000 * 60 * 60)) / (1000 * 60));
        const seconds = Math.floor((distancia % (1000 * 60)) / 1000);

        clockRender.innerHTML = `Iniciaremos em ${ days }d ${ hours }h ${ minutes }m ${ seconds }s`;
    }

    const intervalo = setInterval(atualizarContador, 1000);
}

function ativaSalaDeReuniao() {
    document.getElementById("enter-meeting").className = "btn btn-success mt-2";
}

function desativaSalaDeReuniao() {
    document.getElementById("enter-meeting").className = "btn btn-outline-dark mt-2 disabled";
}

window.onload = function () {

    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

    if (document.getElementById("next-meeting")) {
        const dataAlvo = new Date(document.getElementById("next-meeting").value);
        const elementoId = 'contador';
        iniciarContador(dataAlvo, elementoId);
    }
};
