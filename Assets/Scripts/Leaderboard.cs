using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    private readonly int leaderBoardSize = 5;

    void Awake()
    {
        instance = this;
    }

    public void loadLeaderboard(out int[] scores, out string[] names)
    {
        scores = new int[leaderBoardSize];
        names = new string[leaderBoardSize];

        for (int i = 0; i < leaderBoardSize; i++)
        {
            string scoreKey = "Leaderboard_score_" + i;
            string nameKey = "Leaderboard_name_" + i;

            scores[i] = PlayerPrefs.GetInt(scoreKey, 0);
            names[i] = PlayerPrefs.GetString(nameKey);
        }
    }

    public void updateLeaderboard(int newScore)
    {
        loadLeaderboard(out int[] scores, out string[] names);

        string newName = PlayerPrefs.GetString("ActiveName", "Player");

        for (int i = 0; i < scores.Length; i++)
        {
            if (newScore > scores[i])
            {
                // Shift down array
                for (int k = scores.Length - 1; k > i; k--)
                {
                    scores[k] = scores[k - 1];
                    names[k] = names[k - 1];
                }

                scores[i] = newScore;
                names[i] = newName;

                for (int z = 0; z < scores.Length; z++)
                {
                    PlayerPrefs.SetInt("Leaderboard_score_" + z, scores[z]);
                    PlayerPrefs.SetString("Leaderboard_name_" + z, names[z]);
                }

                PlayerPrefs.Save();
                break;
            }
        }
    }
}