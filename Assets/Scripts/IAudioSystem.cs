using UnityEngine;

public interface IAudioSystem
{
    void PlaySound(string soundPath);
    void StopSound(string soundPath);
    void StopAllSounds();
    AudioSource AdaptiveSound(string source);
}