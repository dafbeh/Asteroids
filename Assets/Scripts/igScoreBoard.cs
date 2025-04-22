using TMPro;
using UnityEngine;
using System;

public class igScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scores;

    private int currentScore;
    private int previousScore;
    private int boardLocation = 5;
    private int[] scoresArray = new int[5];

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
            Debug.Log("Top 5 updated. Current Score: " + currentScore + " location: " + boardLocation + " next score: " + scoresArray[boardLocation - 1]);
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
        bool inserted = false;
        boardLocation = 5;

        int count = 0;

        for (int i = 0; count < 5 && i < scoreArray.Length; i++)
        {
            scoresArray[count] = scoreArray[i];

            if (!inserted && currentScore > scoreArray[i])
            {
                response += writeLine(count + 1, activeName, currentScore, true);
                boardLocation = count + 1;
                inserted = true;
                scoresArray[count] = currentScore;
                count++;

                if (count < 5)
                {
                    response += writeLine(count + 1, nameArray[i], scoreArray[i], false);
                    scoresArray[count] = scoreArray[i];
                    count++;
                }
            }
            else
            {
                response += writeLine(count + 1, nameArray[i], scoreArray[i], false);
                count++;
            }
        }

        if (!inserted && count < 5)
        {
            response += writeLine(count + 1, activeName, currentScore, true);
            boardLocation = count + 1;
            scoresArray[count] = currentScore;
        }

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
                if(lineCount < 6) {
                    result += line + Environment.NewLine;
                }
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
            line = location + ". " + name + " \t" + currentScore + Environment.NewLine;
            line = $"<color=#00FF00>{line}</color>";
        }

        return line;
    }
}