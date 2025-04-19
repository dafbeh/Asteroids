using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AsteroidController asteroidPrefab;
    [SerializeField] public int asteroidCount = 0;
    [SerializeField] private int level = 0;
    [SerializeField] private GameObject[] powerUps;

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
            StartCoroutine(SpawnAsteroidsWithDelay());
        }
    }

    private IEnumerator SpawnAsteroidsWithDelay()
    {
        spawningAsteroids = true;
        while(mainCamera.orthographicSize <= 4.5f) {
            yield return null;
        }

        int asteroids = (2 * level) + 2;
        for (int i = 0; i < asteroids; i++) 
        {
            Instantiate(asteroidPrefab);
            yield return new WaitForSeconds(0.1f);
        }

        spawnPowerup();
        spawningAsteroids = false;
    }

    private void spawnPowerup() {
        int randomNumber = Random.Range(0,powerUps.Length);

        Instantiate(powerUps[randomNumber]);
    }

    public void RespawnPlayer(GameObject player)
    {
        StartCoroutine(respawn(player));
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
        while(insideAsteroid) {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        player.transform.localPosition = new Vector3(0,0,0);
        player.SetActive(true);
    }

    public void GameOver()
    {
        int score = ScoreManager.instance.getScore();
        Leaderboard.instance.updateLeaderboard(score);
        
        FindFirstObjectByType<GameOver>().toggleGameOver();
    }
}
