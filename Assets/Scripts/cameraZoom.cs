using System.Collections;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private float zoom = 5;
    private float velocity = 0f;

    void Awake()
    {
        cam.orthographicSize = 0.5645f;
    }

    void Start()
    {
        StartCoroutine(startZoom());
    }

    private IEnumerator startZoom()
    {
        while (Mathf.Abs(cam.orthographicSize - zoom) > 0.01f)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, 0.25f);
            yield return null;
        }

        cam.orthographicSize = zoom;
    }
}
