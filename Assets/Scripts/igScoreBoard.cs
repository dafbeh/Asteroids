using TMPro;
using UnityEngine;
using System;
using NUnit.Framework.Interfaces;
using UnityEngine.UIElements;

public class igScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scores;

    private int currentScore;
    private int previousScore;
    private int boardLocation = 5;
    private bool locationVerified = false;
    private int[] scoresArray = new int[5]; // Default top 5 scores

    void Awake()
    {
        UpdateScoreboard();
    }

    void FixedUpdate()
    {
        currentScore = ScoreManager.instance.getScore();

        if(currentScore > previousScore) {
            updateLiveScore();
            previousScore = currentScore;
        }

        if (boardLocation > 1 && currentScore > scoresArray[boardLocation - 1])
        {
            UpdateScoreboard();
            Debug.Log("Top 5 updated. Current Score: " + currentScore + " location: " + boardLocation);
        }
    }

    private void UpdateScoreboard()
    {
        Leaderboard.instance.loadLeaderboard(out int[] scoreArray, out string[] nameArray);

        if (scoreArray == null || nameArray == null || scoreArray.Length != nameArray.Length)
        {
            scores.text = "Leaderboard data error.";
            return;
        }

        string activeName = PlayerPrefs.GetString("ActiveName", "Player");
        int activeScore = PlayerPrefs.GetInt("ActiveScore", -1);

        string response = "";
        boardLocation = 5;

        for (int i = 0; i < scoreArray.Length; i++)
        {
            scoresArray[i] = scoreArray[i];
            string name = nameArray[i];
            int score = scoreArray[i];

            string line = "";

            if (currentScore > score && !locationVerified)
            {
                line = writeLine(i + 1, activeName, score, true);
                boardLocation = i + 1;
                locationVerified = true;
            }
            else
            {
                line = writeLine(i + 1, name, score, false);
            }

            response += line;
        }

        locationVerified = false;
        scores.text = response;
    }

    private void updateLiveScore() {
        int lineCount = 0;
        string result = "";
        string activeName = PlayerPrefs.GetString("ActiveName", "Player");

        string[] lines = scores.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        foreach (string line in lines)
        {
            lineCount++;
            if(lineCount == boardLocation) {                
                result += writeLine(boardLocation, activeName, currentScore, true);
            } else {
                result += line + Environment.NewLine;
            }
        }
        scores.text = result;
    }

    private string writeLine(int location, string name, int score, bool green) {

        string line = location + ". " + name + " \t" + score + Environment.NewLine;

        if(name.Length <= 1) {
            line = location + ". " + name + " \t\t" + score + Environment.NewLine;
        }

        if(green) {
            line = $"<color=#00FF00>{line}</color>";
        }

        return line;
    }
}