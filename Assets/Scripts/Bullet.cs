using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float travelTime = 0.80f;
    private Camera mainCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate() 
    {
        StartCoroutine(DestroyBullet());

        if (IsOffScreen())
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(travelTime);
        Destroy(gameObject);
    }

    private bool IsOffScreen()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        float offset = 0.1f;
        bool isOffScreen = viewportPosition.x < -offset || viewportPosition.x > 1 + offset ||
                           viewportPosition.y < -offset || viewportPosition.y > 1 + offset;

        return isOffScreen;
    }
}
