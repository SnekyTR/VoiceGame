using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField]private List<AudioClip> musicList = new List<AudioClip>();

    [SerializeField] AudioSource audioSource;
    public void ChangeCombatMusic()
    {
        audioSource.clip = musicList[1];
        audioSource.Play();
    }
}
