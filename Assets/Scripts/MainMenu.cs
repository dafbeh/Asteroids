using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scores;   
    private int[] scoreArray;
    void Awake()
    {
        
    }

    void Start()
    {
        scores.text = top5();
    }

    private string top5() 
    {
        scoreArray = Leaderboard.instance.loadLeaderboard();
        string response = "error";

        for(int i = 0; i < scoreArray.Length; i++) {
            if(i == 0) {response = "";}
            response += (i + 1) + ". " + scoreArray[i] + Environment.NewLine;
        }

        return response;
    }

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}
