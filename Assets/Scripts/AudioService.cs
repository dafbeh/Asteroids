using UnityEngine;
using System.Collections.Generic;

public class AudioService : IAudioSystem
{
    private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    private GameObject audioHolder;

    public AudioService()
    {
        audioHolder = new GameObject("AudioSources");
        GameObject.DontDestroyOnLoad(audioHolder);
    }

    public void PlaySound(string soundPath)
    {
        if (!sounds.TryGetValue(soundPath, out AudioClip clip))
        {
            clip = Resources.Load<AudioClip>(soundPath);
            
            if (clip == null)
            {
                Debug.LogWarning("Audio not found at: " + soundPath);
                return;
            }
            
            sounds[soundPath] = clip;
        }

        GameObject sourceObj = new GameObject("Sound_" + soundPath);
        sourceObj.transform.SetParent(audioHolder.transform);
        
        AudioSource source = sourceObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        
        GameObject.Destroy(sourceObj, clip.length + 0.1f);
    }

    public void StopSound(string soundPath)
    {
        foreach (Transform child in audioHolder.transform) {
            if(child.gameObject.name == "Sound_" + soundPath) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void StopAllSounds()
    {
        foreach (Transform child in audioHolder.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public AudioSource AdaptiveSound(string soundPath)
    {
        GameObject sourceObj = new GameObject("layerdAudio_" + soundPath);
        sourceObj.transform.SetParent(audioHolder.transform);
        AudioSource audioSource = sourceObj.AddComponent<AudioSource>();
        return audioSource;
    }
}
