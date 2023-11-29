using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> playlist;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

        if (playlist.Count == 0)
        {
            Debug.LogError("No audio clips in the playlist. Please add audio clips in the Inspector.");
            enabled = false; // Disable the script
        }

        // Play random track from the playlist
        PlayRandomTrack(); 
    }

    void PlayRandomTrack()
    {
        int randomIndex = Random.Range(0, playlist.Count); // Choose a random index
        audioSource.clip = playlist[randomIndex]; // Set the chosen clip to the AudioSource
        audioSource.Play(); // Play the audio clip

        // Subscribe to the AudioSource's "ended" event to play the next random track when the current one ends
        audioSource.loop = false; // Disable the built-in loop
        audioSource.PlayOneShot(playlist[randomIndex]);
    }

    void Update()
    {
        // Check if the current track has ended
        if (!audioSource.isPlaying)
        {
            PlayRandomTrack();
        }
    }
}
