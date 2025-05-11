/* global Audio */
// Javascript for audio player.
window.audioPlayer = {
    audio: null,

    play: function (url) {
        // If already playing the same audio just continues playing.
        if (this.audio && this.audio.src === url) {
            this.audio.play() // If paused it continues playing.
            return
        }
        // If a different audio is playing stop it.
        this.audio = new Audio(url)
        this.audio.play()
        // Add event listener to update progress bar.
        this.audio.addEventListener("timeupdate", () => {
            this.updateProgress();
        });
    },

    pause: function () {
        if (this.audio) {
            this.audio.pause() // Pauses without resetting position.
    }
  },

  stop: function () {
    if (this.audio) {
        this.audio.pause() // Pauses without resetting position.
        this.audio.currentTime = 0 // Resets position to start.
        this.updateProgress() // Update progress bar.
    }
  },

    // Event listeners for play, pause, and stop buttons to implement progress bar.
    updateProgress: function () {
        const progressBar = document.getElementById("audioProgressBar")
        if (!progressBar || !this.audio?.duration) return
        const percent = (this.audio.currentTime / this.audio.duration) * 100
        progressBar.style.width = `${percent}%`
    },

    seek: function (event) {
        if (!this.audio || !this.audio.duration) return
        const container = event.currentTarget
        const rect = container.getBoundingClientRect()
        const x = event.clientX - rect.left
        const percent = x / rect.width
        this.audio.currentTime = percent * this.audio.duration
    }
}