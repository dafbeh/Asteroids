using System.Collections;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private float zoom = 5;
    private float velocity = 0f;
    private bool wantZoom = true;

    public static cameraZoom instance;

    void Awake()
    {
        instance = this;
        cam.orthographicSize = 0.5645f;
    }

    void Start()
    {
        if(wantZoom) {
            StartCoroutine(startZoom());
        } else {
            cam.orthographicSize = 5;
        }
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

    public void setWantZoom(bool value)
    {
        wantZoom = value;
    }
}
