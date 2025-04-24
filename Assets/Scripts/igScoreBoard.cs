using TMPro;
using UnityEngine;
using System;

public class igScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scores;
    [SerializeField] private GameObject confetti;
    [SerializeField] private Transform confettiPoint;

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

        if (boardLocation > 1 && currentScore > scoresArray[boardLocation - 1])
        {
            UpdateScoreboard();
        }

        if(currentScore > previousScore && currentScore > scoresArray[4]) {
            updateLiveScore();
            previousScore = currentScore;
        }
    }

    private void UpdateScoreboard()
    {
        string activeName = PlayerPrefs.GetString("ActiveName", "Player");

        if(activeName == null) {
            activeName  = "P" + UnityEngine.Random.Range(1000, 9999);
            PlayerPrefs.SetString("ActiveName", activeName);
            PlayerPrefs.Save();
        }

        Leaderboard.instance.loadLeaderboard(out int[] scoreArray, out string[] nameArray);

        if (scoreArray == null || nameArray == null || scoreArray.Length != nameArray.Length)
        {
            scores.text = "Leaderboard data error.";
            return;
        }

        string response = "";
        bool inserted = false;

        int count = 0;

        for (int i = 0; count < 5 && i < scoreArray.Length; i++)
        {
            scoresArray[count] = scoreArray[i];

            if (!inserted && currentScore > scoreArray[i])
            {
                response += writeLine(count + 1, activeName, currentScore, true);
                boardLocation = count;
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
            scoresArray[count] = currentScore;
        }

        if(boardLocation !=5) {
            Instantiate(confetti, confettiPoint.position, confettiPoint.rotation);
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
            if(lineCount == (boardLocation + 1)) {
                print("board Location = " + boardLocation);            
                result += writeLine(boardLocation + 1, activeName, currentScore, true);
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

        if(name.Length <= 3) {
            line = location + ". " + name + " \t\t" + score + Environment.NewLine;
        }

        if(green) {
            line = location + ". " + name + " \t" + score + Environment.NewLine;
            line = $"<color=#00FF00>{line}</color>";

            if(name.Length <= 3) {
                line = location + ". " + name + " \t\t" + score + Environment.NewLine;
            }
        }

        return line;
    }
}