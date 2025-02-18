using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AsteroidController asteroidPrefab;
    [SerializeField] public int asteroidCount = 0;
    [SerializeField] private int level = 0;

    private Camera mainCamera;
    private bool spawningAsteroids;
    private bool insideAsteroid = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(asteroidCount == 0 && !spawningAsteroids)
        {
            level++;
            spawningAsteroids = true;
            StartCoroutine(SpawnAsteroidsWithDelay());
        }
    }

    private IEnumerator SpawnAsteroidsWithDelay()
    {
        yield return new WaitForSeconds(1f);  

        int asteroids = (2 * level) + 2;
        for (int i = 0; i < asteroids; i++) 
        {
            Instantiate(asteroidPrefab);
        }

        spawningAsteroids = false;
    }

    public void RespawnPlayer(GameObject player)
    {
        if(insideAsteroid) {
            StartCoroutine(respawn(player));
        } else {
            player.transform.localPosition = new Vector3(0,0,0);
            player.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid"))
        {
            insideAsteroid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid"))
        {
            insideAsteroid = false;
        }
    }

    private IEnumerator respawn(GameObject player)
    {
        yield return new WaitForSeconds(1f);
        RespawnPlayer(player);
    }

    public void GameOver()
    {
        int score = ScoreManager.instance.getScore();
        Leaderboard.instance.updateLeaderboard(score);
        StartCoroutine(endGameMenu());
    }

    private IEnumerator endGameMenu()
    {
        yield return new WaitForSeconds(1);
    }
}
