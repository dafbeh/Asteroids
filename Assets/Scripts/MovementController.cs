using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class MovementController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float thrustFloat;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Sprite moveSprite;

    private float rotationInput;
    private float thrustInput;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        this.rotationInput = Input.GetAxisRaw("Horizontal");
        this.thrustInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
        ClampSpeed();
    }

    private void Rotate()
    {
        if(this.rotationInput < 0)
            transform.Rotate(Vector3.forward * this.rotationSpeed * Time.deltaTime);

        if (this.rotationInput > 0)
            transform.Rotate(Vector3.back * this.rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (this.thrustInput > 0)
        {
            rb2d.AddForce(transform.up * thrustFloat);
            spriteRenderer.sprite = moveSprite;
        }
        if(this.thrustInput <= 0)
            spriteRenderer.sprite = originalSprite;
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
