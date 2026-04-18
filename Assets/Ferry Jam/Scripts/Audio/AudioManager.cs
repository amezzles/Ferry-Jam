using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip[] clips;
    [Range(0f, 1f)] public float volume = 0.8f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    [Range(0f, 0.5f)] public float randomVolume = 0.1f;
    [Range(0f, 0.5f)] public float randomPitch = 0.1f;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

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
        if (musicTracks.Length > 0) PlayMusic(musicTracks[0].name);
        PlayRandomMusic();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfx, sound => sound.name == name);
        if (s == null || s.clips.Length == 0) return;

        // Pick a random clip
        AudioClip clipToPlay = s.clips[UnityEngine.Random.Range(0, s.clips.Length)];

        // Calculate random volume and pitch
        float finalVolume = s.volume * (1f + UnityEngine.Random.Range(-s.randomVolume / 2f, s.randomVolume / 2f));
        float finalPitch = s.pitch * (1f + UnityEngine.Random.Range(-s.randomPitch / 2f, s.randomPitch / 2f));

        // APPLY AND PLAY
        sfxSource.pitch = finalPitch;
        
        // PlayOneShot is the gold standard for overlapping SFX
        sfxSource.PlayOneShot(clipToPlay, finalVolume);
        
        Debug.Log("Playing OneShot: " + name + " at volume " + finalVolume);
    }

    public void PlayRandomMusic()
    {
        if (musicTracks.Length == 0) return;

        // Pick a random index
        int randomIndex = UnityEngine.Random.Range(0, musicTracks.Length);
        PlayMusic(musicTracks[randomIndex].name);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicTracks, sound => sound.name == name);
        if (s == null || s.clips.Length == 0) return;

        // Stop current music before starting new music
        musicSource.Stop();

        musicSource.clip = s.clips[0];
        musicSource.volume = s.volume;
        musicSource.pitch = s.pitch;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}