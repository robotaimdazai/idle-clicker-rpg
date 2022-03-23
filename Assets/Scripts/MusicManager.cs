using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GIKIT
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get { return instance; } }
        static MusicManager instance = null;

        public bool IsMuted { get { return isMuted; } }
        public AudioClip[] PlayList { get { return playList; } }

        [SerializeField] AudioClip[] playList = null;

        AudioSource musicPlayer = null;
        AudioSource sfxPlayer = null;
        bool isMuted = false;
        AudioClip currentlyPlaying = null;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance!=this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            AudioSource[] audioSources = GetComponents<AudioSource>();
            // this source plays music
            musicPlayer = audioSources[0];
            // this source plays sfx in game
            sfxPlayer = audioSources[1];
        }

        private void Start()
        {
            AudioClip bg = playList[0];
            PlayMusic(bg);
        }

        private  IEnumerator FadeOut(float FadeTime)
        {
            float startVolume = musicPlayer.volume;
            while (musicPlayer.volume > 0)
            {
                musicPlayer.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            musicPlayer.Stop();
        }

        private  IEnumerator FadeIn(AudioClip clip, float FadeTime)
        {
            musicPlayer.clip = clip;

            musicPlayer.Play();
            musicPlayer.volume = 0f;
            while (musicPlayer.volume < 1)
            {
                musicPlayer.volume += Time.deltaTime / FadeTime;
                yield return null;
            }
        }

        private IEnumerator PlayMusicCoroutine(AudioClip clip, float fadeOutTime, float fadeInTime)
        {
            StartCoroutine(FadeOut(fadeOutTime));
            yield return new WaitForSeconds(fadeOutTime);
            StartCoroutine(FadeIn(clip,fadeInTime));
        }


        public void PlayMusic(AudioClip clip, float fadeOutTime=1f, float fadeInTime=1f)
        {
            if (clip==null) { Debug.Log("No music clip"); return; }

            if (musicPlayer.clip == null)
            {
                StartCoroutine(FadeIn(clip, fadeInTime));
            }
            else
            {
                StartCoroutine(PlayMusicCoroutine(clip, fadeOutTime, fadeInTime));
            }

           
        }
        public void PlaySFX(AudioClip sfx)
        {
            if (sfx==null) { Debug.Log("No sfx file"); return; }
            sfxPlayer.PlayOneShot(sfx);
        }

        public void Mute()
        {
            musicPlayer.mute = true;
            sfxPlayer.mute = true;
            isMuted = true;
        }
        public void UnMute()
        {
            musicPlayer.mute = false;
            sfxPlayer.mute = false;
            isMuted = false;
        }
        public void ToggleMute()
        {
            isMuted = !isMuted;
            if (isMuted)
            {
                Mute();
            }
            else
            {
                UnMute();
            }
        }

    }
}

