using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource soundsAudioSource;
    public AudioSource uiAudioSource;

    void Start()
    {	
        Events.OnMusic += OnMusic;
		Events.OnSoundFX += OnSoundFX;
        Events.OnUIFX += OnUIFX;
	}
    void OnSoundFX(string soundName)
    {
        AudioClip ac = Resources.Load("Sounds/" + soundName) as AudioClip;
        soundsAudioSource.clip = ac;
        soundsAudioSource.Play();
        soundsAudioSource.loop = false;
    }
     void OnUIFX(string soundName)
    {
        AudioClip ac = Resources.Load("Sounds/" + soundName) as AudioClip;
        uiAudioSource.clip = ac;
        uiAudioSource.Play();
        uiAudioSource.loop = false;
    }
    void OnMusic(string soundName)
    {
        if(soundName == "")
        {
            musicAudioSource.Stop();
        }
        AudioClip ac = Resources.Load("Music/" + soundName) as AudioClip;
        musicAudioSource.clip = ac;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
}
