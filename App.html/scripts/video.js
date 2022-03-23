/* Este es el video.js */
(function (cibertec) {
    cibertec.video = {
        videoElement: document.getElementById("video"),
        play: function () {
            if (this.videoElement.paused) {
                this.videoElement.play();
            }
        },
        pause: function () {
            if (this.videoElement.played) {
                this.videoElement.pause();
            }
        },
        stop: function () {
            this.videoElement.currentTime = 0;
            this.videoElement.pause();
        }
    }
})(window.cibertec = window.cibertec || {})