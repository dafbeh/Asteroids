using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AsteroidController asteroidPrefab;
    [SerializeField] public int asteroidCount = 0;
    [SerializeField] private int level = 0;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(asteroidCount == 0)
        {
            level++;

            int asteroids = 2 + (2 * level);
            for(int i = 0; i < asteroids; i++) {
                SpawnAsteroid();
            }
        }
    }

    private void SpawnAsteroid()
    {
        int edge = Random.Range(0,4);
        float x = 0f;
        float y = 0f;

        if(edge == 0) {

        } else if(edge == 1) {
            x = Random.Range(0f, 1f);
            y = 1f;
        } else if (edge == 2) {
            x = Random.Range(0f, 1f);
            y = 0f;
        } else if (edge == 3) {
            x = 0f;
            y = Random.Range(0f, 1f);
        } else if (edge == 4) {
            x = 1f;
            y = Random.Range(0f, 1f);
        }

        Vector3 spawnPosition = new Vector3(x, y, 0);
        Vector3 viewportToWorldPoint = mainCamera.ViewportToWorldPoint(spawnPosition);
        Vector2 worldSpawnPosition = new Vector2(viewportToWorldPoint.x, viewportToWorldPoint.y);

        AsteroidController asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        asteroid.gameManager = this;
    }

    public void RespawnPlayer(GameObject player)
    {
        StartCoroutine(respawn(player));
    }

    private IEnumerator respawn(GameObject player)
    {
        yield return new WaitForSeconds(1);
        player.transform.localPosition = new Vector3(0,0,0);
        player.SetActive(true);
    }

    public void GameOver()
    {
        
    }
}
