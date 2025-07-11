using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class AdaptiveMusicController : MonoBehaviour
{
    private IAudioSystem audioSystem;
    private GameManager gameManager;
    
    [SerializeField] private string low = "Sounds/AdaptiveMusic/Ambient";
    [SerializeField] private string medium = "Sounds/AdaptiveMusic/Guitar";
    [SerializeField] private string high = "Sounds/AdaptiveMusic/AllLayers";
    [SerializeField] private float fadeSpeed = 3f;
    [SerializeField] private bool musicOn;
    
    private Dictionary<string, AudioSource> tracks = new Dictionary<string, AudioSource>();

    void Awake()
    {
        audioSystem = ServiceLocator.Get<IAudioSystem>();
        gameManager = GetComponent<GameManager>();
        
        setup(low);
        setup(medium);
        setup(high);

        musicOn = PlayerPrefs.GetInt("music") == 1;
    }
    
    private void setup(string path)
    {
        AudioSource source = audioSystem.AdaptiveSound(path);
        AudioClip clip = Resources.Load<AudioClip>(path);
        
        if (clip != null)
        {
            source.clip = clip;
            source.loop = true;
            source.volume = 0f;
            source.Play();
            tracks[path] = source;
        }
    }

    void Update()
    {
        if(musicOn) {
            updateVol();
        } else {
            mute();
        }
    }
    
    private void updateVol()
    {
        float lowTarget = 0f;
        float mediumTarget = 0f;
        float highTarget = 0f;

        if (gameManager.asteroidCount < (gameManager.level * 2) && !gameManager.spawningAsteroids)
        {
            lowTarget = 1f;
        }
        else if (gameManager.asteroidCount < gameManager.level * 3 && !gameManager.spawningAsteroids)
        {
            mediumTarget = 1f;
        }
        else
        {
            highTarget = 1f;
        }

        lerp(low, lowTarget);
        lerp(medium, mediumTarget);
        lerp(high, highTarget);
    }
    
    private void lerp(string trackPath, float targetVolume)
    {
        if (tracks.TryGetValue(trackPath, out AudioSource source))
        {
            source.volume = Mathf.Lerp(source.volume, targetVolume, fadeSpeed * Time.deltaTime);
        }
    }

    private void mute()
    {
        foreach (var track in tracks.Values) {
            track.volume = 0f;
        }
    }
}