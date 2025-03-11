using UnityEngine;

public interface Audio
{
    void PlaySound(string soundPath);
    void StopSound(string soundPath);
    void StopAllSounds();
}