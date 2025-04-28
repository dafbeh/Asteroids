using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class StatsLoader : MonoBehaviour
{
    [SerializeField] public GameObject statsScreen;

    [SerializeField] Button button;
    [SerializeField] GameObject list;
    private int iterations = 1;
    public List<GameStats> previousGames = new List<GameStats>();

    public void createButtons() {
        // Destroy children
        foreach(Transform child in list.transform)
        {
            Destroy(child.gameObject);
        }

        // Load stats
        loadAllStats();

        // Create buttons for each stat
        foreach (GameStats stat in previousGames) {
            Button newButton = Instantiate(button, list.transform);     
            newButton.GetComponent<PreviousGameButton>().buttonStats = stat;
        }
    }

    public List<GameStats> loadAllStats()
    {
        previousGames.Clear();
        string filePath = Application.persistentDataPath;
        
        string[] gameStats = Directory.GetFiles(filePath, "*gamestats*.json");
        
        if (gameStats.Length > 0)
        {
            foreach (string file in gameStats)
            {
                string jsonData = File.ReadAllText(file);
                GameStats loadedStats = JsonUtility.FromJson<GameStats>(jsonData);
                loadedStats.iteration = iterations++;
                previousGames.Add(loadedStats);
            }            
            return previousGames;
        }
        else
        {
            Debug.LogWarning("No saves found");
            return previousGames;
        }
    }

}
