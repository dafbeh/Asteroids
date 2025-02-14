using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float speed;

    private WrapAroundController wrapAroundController;
    private Rigidbody2D rb2d;

    void Start()
    {
        wrapAroundController = GetComponent<WrapAroundController>();
        rb2d = GetComponent<Rigidbody2D>();

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        Move();
    }

    void FixedUpdate()
    {
        if(wrapAroundController.isOutOfBounds)
        {
            Vector2 newDir = Random.insideUnitCircle.normalized;

            rb2d.linearVelocity = newDir;        
        }
    }

    private void Move()
    {
        rb2d.AddForce(transform.up * speed);
    }
}
