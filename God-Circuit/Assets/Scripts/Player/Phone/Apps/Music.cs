using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Music : MonoBehaviour
{

    public GameObject phone;
    public GameObject earbuds;
    private AudioSource AudioSource;
    private bool earBudsIn;
    public List<SongsSO> songs = new List<SongsSO>();
    private int currentSong =0;
    public SongsSO[] songsArray;

    public Image PlayPauseImage;
    public Sprite PlayImage;
    public Sprite PauseImage;

    public Image songImage;
    public bool isPaused;
    public float percentThrough;
    // Start is called before the first frame update
    void Start()
    {
        if (phone != null)
        {
            AudioSource = phone.GetComponent<AudioSource>();
        }
    }


    private void LoadSongs()
    {
        songs.Clear();
        songs.AddRange(songsArray);
        StartNewSong(songs[currentSong]);
    }

    public void PlayPause()
    {

        if (AudioSource.isPlaying)
        {
            AudioSource.Stop();
            PlayPauseImage.sprite = PauseImage;

        }
        else
        {
            AudioSource.Play();
            PlayPauseImage.sprite = PlayImage;
        }
    }

    public void StartNewSong(SongsSO songToPlay)
    {
        AudioSource.clip = songToPlay.Song;
        songImage.sprite = songToPlay.SongCover;

    }
    public void SkipForward()
    {
        try { currentSong++; StartNewSong(songs[currentSong]); } catch { currentSong = 0; StartNewSong(songs[currentSong]) ; }
    }

    public void SkipBackward() { try { currentSong--; StartNewSong(songs[currentSong]); } catch { currentSong = songs.Count -1; StartNewSong(songs[currentSong]); } }

    public void Forward()
    {

    }
    public void Backward()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
