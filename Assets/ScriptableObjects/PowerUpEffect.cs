using System.Collections;
using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
    [SerializeField] public float duration = 10f;
    protected GameObject player;
    protected GameObject weapon;
    protected MonoBehaviour coroutineRunner;
    protected IAudioSystem audioSystem;
    protected Vector3 originalScale;

    public virtual void Apply(GameObject gameObject)
    {
        player = gameObject;
        weapon = player.transform.GetChild(0).gameObject;
        coroutineRunner = player.GetComponent<MonoBehaviour>();
        audioSystem = ServiceLocator.Get<IAudioSystem>();
        originalScale = player.transform.localScale;
        
        PlayerHealth.OnPlayerDeath += ResetOnDeath;
        
        ApplyPower();
        
        if (duration > 0)
        {
            coroutineRunner.StartCoroutine(ResetAfterDuration());
        }
    }

    protected abstract void ApplyPower();
    
    protected abstract void RemovePower();

    protected void SetColor(Color color)
    {
        if (player != null)
        {
            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
        }
    }

    protected void Resize(Vector3 scale)
    {
        player.transform.localScale = scale;
    }
    
    protected void SetAutoFire(bool enabled)
    {
        weapon.GetComponent<Weapon>().autoFire = enabled;
    }
    
    protected void AddHealth()
    {
        player.GetComponent<PlayerHealth>().addHealth();
    }
    
    protected void SetTime(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * scale;
    }
    
    protected void PlaySound(string soundPath)
    {
        audioSystem.PlaySound(soundPath);
    }
    
    private IEnumerator ResetAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        RemovePower();
        PlayerHealth.OnPlayerDeath -= ResetOnDeath;
    }
    
    private void ResetOnDeath()
    {
        RemovePower();
        PlayerHealth.OnPlayerDeath -= ResetOnDeath;
    }
}
