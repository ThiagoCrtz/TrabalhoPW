  $(document).ready(function () {
            $('#cep').mask('00000-000', {
                translation: {
                    '0': { pattern: /[0-9]/ }
                },
            });
        });
        const cep = document.querySelector("#cep")
        const adress = document.querySelector("#adress")
        const cidade = document.querySelector("#cidade")

        cep.addEventListener('focusout', async () => {
            const response = await fetch(`https://viacep.com.br/ws/${cep.value}/json/`);
            if (!response.ok) {
                throw await response.json()
            }
            const responseCep = await response.json()

            adress.value = responseCep.logradouro
            cidade.value = responseCep.localidade
})