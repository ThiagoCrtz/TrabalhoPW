$(document).ready(function () {
    $('#placa').mask('SSS0S00', {
        translation: {
            'S': { pattern: /[A-Za-z]/ }, // Letras
            '0': { pattern: /[0-9]/ } // Números
        },
        placeholder: "ABC1D23" // Exemplo do formato da placa Mercosul
    });
});
