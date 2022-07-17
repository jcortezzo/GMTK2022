using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour
{

    public static Jukebox Instance;

    [SerializeField] private Sound[] musics;
    [SerializeField] private Sound[] sfxs;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            musicSource = this.gameObject.AddComponent<AudioSource>();
            sfxSource = this.gameObject.AddComponent<AudioSource>();

        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public AudioSource GetMusicCurrentAudioSource()
    {
        return musicSource;
    }
    public void PlayMusic(string name, float time = 0)
    {
        foreach (Sound s in musics)
        {
            if (s.name.Equals(name))
            {
                musicSource.clip = s.clip;
                musicSource.volume = s.volume;
                musicSource.pitch = s.pitch;
                musicSource.loop = s.loop;
                musicSource.time = time;
                musicSource.Play();
                return;
            }
        }
    }


    public void PlaySFX(string name)
    {
        foreach (Sound s in sfxs)
        {
            if (s.name.Equals(name))
            {
                sfxSource.volume = s.volume;
                sfxSource.pitch = s.pitch;
                sfxSource.PlayOneShot(s.clip);
                return;
            }
        }
    }

    public void PlaySFX(string name, float volume, float pitch)
    {
        foreach (Sound s in sfxs)
        {
            if (s.name.Equals(name))
            {
                sfxSource.volume = volume;
                sfxSource.pitch = pitch;
                sfxSource.PlayOneShot(s.clip);
                return;
            }
        }
    }
}

[System.Serializable]
public struct Sound
{
    public AudioClip clip;

    public string name;

    [Range(0f, 1f)]
    public float volume;

    [Range(-3f, 3f)]
    public float pitch;

    public bool loop;
}