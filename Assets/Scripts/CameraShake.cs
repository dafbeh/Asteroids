using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;
    private float duration = 0f;
    private float magnitude = 0.2f;
    private float damping = 1.0f;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (duration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * magnitude;
            duration -= Time.deltaTime * damping;
        }
        else
        {
            duration = 0f;
            transform.localPosition = originalPos;
        }
    }

    public void Shake(float time, float magnitude)
    {
        duration = time;
        duration = magnitude;
    }
}
