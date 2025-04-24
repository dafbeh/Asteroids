using UnityEngine;
using System.Collections.Generic;

public class AudioService : IAudioSystem
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private GameObject audioSourceHolder;

    public AudioService()
    {
        audioSourceHolder = new GameObject("AudioSources");
        GameObject.DontDestroyOnLoad(audioSourceHolder);
    }

    public void PlaySound(string soundPath)
    {
        if (!audioClips.TryGetValue(soundPath, out AudioClip clip))
        {
            clip = Resources.Load<AudioClip>(soundPath);
            
            if (clip == null)
            {
                Debug.LogWarning($"Audio clip not found at path: {soundPath}");
                return;
            }
            
            audioClips[soundPath] = clip;
        }

        GameObject sourceObj = new GameObject($"Sound_{soundPath}");
        sourceObj.transform.SetParent(audioSourceHolder.transform);
        
        AudioSource source = sourceObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        
        GameObject.Destroy(sourceObj, clip.length + 0.1f);
    }

    public void StopSound(string soundPath)
    {

    }

    public void StopAllSounds()
    {

    }
}
