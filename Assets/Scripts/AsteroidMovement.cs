using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    private WrapAroundController wrapAroundController;
    private Rigidbody2D rb2d;

    void Start()
    {
        wrapAroundController = GetComponent<WrapAroundController>();
        rb2d = GetComponent<Rigidbody2D>();

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    void FixedUpdate()
    {
        Move();
        ClampSpeed();
    }

    private void Move()
    {
        rb2d.AddForce(transform.up * speed);

        if(wrapAroundController.isOutOfBounds)
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(20, 70));
        }
    }

    private void ClampSpeed()
    {
        Vector2 velocity = rb2d.linearVelocity;

        if (velocity.magnitude > maxSpeed)
        {
            rb2d.linearVelocity = velocity.normalized * maxSpeed;
        }
    }
}
