using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WrapAroundController : MonoBehaviour
{
    [SerializeField] private float teleportOffest;

    public bool isOutOfBounds { get; private set; }
    private Camera mainCamera;
    private Rigidbody2D rb2d;

    void Start()
    {
        mainCamera = Camera.main;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        WrapAround();
    }

    private void WrapAround()
    {
        Vector3 position = mainCamera.WorldToViewportPoint(transform.position);
        Vector3 newPosition = transform.position;
        isOutOfBounds = false;

        if (position.x > (1 + teleportOffest))
        {
            newPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(0 - teleportOffest, 0, 0)).x;
            isOutOfBounds = true;
        }
        else if (position.x < (0 - teleportOffest))
        {
            newPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(1 + teleportOffest, 0, 0)).x;
            isOutOfBounds = true;
        }

        if (position.y > (1 + teleportOffest))
        {
            newPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(0, 0 - teleportOffest, 0)).y;
            isOutOfBounds = true;
        }
        else if (position.y < (0 - teleportOffest))
        {
            newPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(0, 1 + teleportOffest, 0)).y;
            isOutOfBounds = true;
        }
            transform.position = newPosition;
    }
}
