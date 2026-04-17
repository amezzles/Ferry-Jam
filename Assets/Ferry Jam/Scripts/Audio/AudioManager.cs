using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip[] clips;

    [Range(0f, 1f)]
    public float volume = 0.8f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;
    
    public Sound[] musicTracks;
    public Sound[] sfx;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (musicTracks.Length > 0)
        {
            PlayMusic(musicTracks[0].name);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicTracks, sound => sound.name == name);
        if (s == null || s.clips.Length == 0)
        {
            Debug.LogWarning("Music track: " + name + " not found!");
            return;
        }

        musicSource.clip = s.clips[0];
        musicSource.volume = s.volume;
        musicSource.pitch = s.pitch;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfx, sound => sound.name == name);
        if (s == null || s.clips.Length == 0)
        {
            Debug.LogWarning("SFX: " + name + " not found!");
            return;
        }

        // Pick a random clip from the array
        AudioClip clipToPlay = s.clips[UnityEngine.Random.Range(0, s.clips.Length)];

        // Create a temporary GameObject to play the sound
        GameObject soundGameObject = new GameObject("SFX_" + name);
        AudioSource tempAudioSource = soundGameObject.AddComponent<AudioSource>();

        // Apply settings with randomization
        tempAudioSource.clip = clipToPlay;
        tempAudioSource.volume = s.volume * (1f + UnityEngine.Random.Range(-s.randomVolume / 2f, s.randomVolume / 2f));
        tempAudioSource.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.randomPitch / 2f, s.randomPitch / 2f));

        tempAudioSource.Play();

        // Destroy the temporary object after the clip has finished playing
        Destroy(soundGameObject, clipToPlay.length);
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
