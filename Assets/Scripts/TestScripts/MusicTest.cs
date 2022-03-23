using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GIKIT;

public class MusicTest : MonoBehaviour
{
    [SerializeField] AudioClip music1 = null;
    [SerializeField] AudioClip music2 = null;

    public void Play1()
    {
        MusicManager.Instance.PlayMusic(music1,1);
    }

    public void Play2()
    {
        MusicManager.Instance.PlayMusic(music2,1);
    }

   
}
