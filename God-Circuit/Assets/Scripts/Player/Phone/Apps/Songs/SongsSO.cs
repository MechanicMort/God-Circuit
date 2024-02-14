using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SongSO", order = 3)]

public class SongsSO : ScriptableObject
{
 
    public Sprite SongCover;
    public string SongName;
    public string ArtistName;
    public float SongLength;
    public AudioClip Song;
}
