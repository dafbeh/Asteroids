using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class AsteroidFinder : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject asteroid;
    private int previousLevel = -1;
    private bool hasPushed = false;
    [SerializeField] private Camera myCamera;

    private float offscreenTimer = 0f;
    private float pushTime = 1.5f;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        myCamera = Camera.main;
    }

    void Update()
    {
        levelCheck();

        if (gameManager.asteroidCount == 1 && !hasPushed 
            && !gameManager.spawningAsteroids)
        {
            asteroid = findLastAsteroid();
            if (asteroid != null)
            {
                if (!coordinateChecker(asteroid))
                {
                    offscreenTimer += Time.deltaTime;

                    if (offscreenTimer >= pushTime)
                    {
                        asteroid.GetComponent<AsteroidController>().CenterPush();
                        hasPushed = true;
                    }
                }
                else
                {
                    offscreenTimer = 0f;
                }
            }
        }
    }

    private bool coordinateChecker(GameObject singleObject) {
        Vector3 viewportPoint = myCamera.WorldToViewportPoint(singleObject.transform.position);

        if (viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
            viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
            viewportPoint.z > 0) {
            return true;
        } else {
            return false;
        }
    }

    private GameObject findLastAsteroid() {
        asteroid = GameObject.FindGameObjectWithTag("Asteroid");
        return asteroid;
    }

    private void levelCheck()
    {
        if (gameManager.level != previousLevel)
        {
            previousLevel = gameManager.level;
            hasPushed = false;
        }
    }
}
