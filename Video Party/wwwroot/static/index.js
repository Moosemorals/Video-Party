
document.getElementById("subs")
    .addEventListener("click", e => {
        const label = e.target.innerText;

        const player = document.getElementById("player");

        const pos = player.currentTime;

        const setPos = () => {
            player.fastSeek(pos);
            player.play();
            player.removeEventListener("canplay", setPos);
        }

        if (label == "on") {
            e.target.innerText = "off";
            player.src = "/video/1"
        } else {
            e.target.innerText = "on";
            player.src = "/video/0"
        }

        player.addEventListener("canplay", setPos);
    });