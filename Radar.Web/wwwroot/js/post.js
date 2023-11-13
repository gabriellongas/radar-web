function like(pessoaId, postId, url, token) {
    function updateUI(postId) {
        $(`#${postId} .post-like .post-like-icon`).removeClass("fa-regular");
        $(`#${postId} .post-like .post-like-icon`).addClass("fa-solid");

        let likesTxt = $(`#${postId} .post-like .post-like-counter`).text();
        let likes = parseInt(likesTxt);
        likes++;
        $(`#${postId} .post-like .post-like-counter`).text(likes);

        $(`#${postId} .post-reaction .post-like`).attr("onclick", `dislike(${pessoaId}, ${postId}, '${url}', '${token}')`);
    }

    let request = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({
            "curtidaId": -1,
            "pessoaIdCurtindo": pessoaId,
            "postIdCurtido": postId
        })
    };
    try {
        fetch(`${url}`, request)
            .then(response => { if (!response.ok) throw new Error(response.status); else updateUI(postId); })

    } catch (error) {
        console.error("Erro ao curtir postagem", error)
    }
}

function dislike(pessoaId, postId, url, token) {
    function updateUI(postId) {
        $(`#${postId} .post-like .post-like-icon`).removeClass("fa-solid");
        $(`#${postId} .post-like .post-like-icon`).addClass("fa-regular");

        let likesTxt = $(`#${postId} .post-like .post-like-counter`).text();
        let likes = parseInt(likesTxt);
        likes--;
        $(`#${postId} .post-like .post-like-counter`).text(likes);

        $(`#${postId} .post-reaction .post-like`).attr("onclick", `like(${pessoaId}, ${postId}, '${url}', '${token}')`);
    }

    let request = {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`,
        }
    };
    try {
        fetch(`${url}/${pessoaId}/${postId}`, request)
            .then(response => { if (!response.ok) throw new Error(response.status); else updateUI(postId); })

    } catch (error) {
        console.error("Erro ao curtir postagem", error)
    }
}