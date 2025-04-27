window.audioPlayer = {
    audio: null,

    play: function (url) {
        if (this.audio) {
            this.audio.play(); // If paused it continues playing
            return;
        }
        this.audio = new Audio(url);
        this.audio.play();
    },

    pause: function () {
        if (this.audio) {
            this.audio.pause(); // Pauses without resetting position
        }
    },

    stop: function () {
        if (this.audio) {
            this.audio.currentTime = 0; 	// Set to start from position 0
            this.audio.pause();          	// Pauses at the beginning
        }
    }
}
