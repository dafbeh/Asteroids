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
    private IAudioSystem audioSystem;


    void Awake()
    {
        audioSystem = ServiceLocator.Get<IAudioSystem>();
        UpdateScoreboard();
    }

    void FixedUpdate()
    {
        currentScore = ScoreManager.instance.getScore();

        if (boardLocation > 0 && currentScore > scoresArray[boardLocation - 1])
        {
            UpdateScoreboard();
        }

        if(currentScore > previousScore && boardLocation < 5) {
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
            boardLocation = count;
            scoresArray[count] = currentScore;
        }
        else if (!inserted)
        {
            boardLocation = 5;
        }

        if(boardLocation < 5) {
            Instantiate(confetti, confettiPoint.position, confettiPoint.rotation);
            audioSystem.PlaySound("Sounds/achievement");
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
        string locationStr = location + ". ";
        string nameStr = name;
        string scoreStr = score.ToString();
        
        string result = string.Format("{0}{1,-8}{2}", locationStr, nameStr, scoreStr) + Environment.NewLine;
        
        if(green) {
            result = $"<color=#00FF00>{result}</color>";
        }
        
        return result;
    }
}