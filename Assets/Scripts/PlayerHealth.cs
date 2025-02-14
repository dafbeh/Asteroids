using System.Numerics;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            if(Health > 1)
            {
                Health--;
                gameObject.transform.localPosition = new UnityEngine.Vector3(0,0,0);
            } else {
                Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
