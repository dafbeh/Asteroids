using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
