const modal = document.getElementById('review-modal');

if (modal) {
    modal.addEventListener('show.bs.modal', event => {
        const textArea = document.getElementById('post-content');
        const content = textArea.value;

        const newTextArea = document.querySelector('#review-modal #conteudo');
        newTextArea.value = content;
    })
}