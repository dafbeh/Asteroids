using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float thrustFloat;
    [SerializeField] private sprite moveSprite;

    private float rotationInput;
    private float thrustInput;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
        if(this.thrustInput > 0)
            rb2d.AddForce(transform.up * thrustFloat);
    }
}
