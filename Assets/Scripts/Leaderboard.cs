using UnityEngine;
using System;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    private readonly int leaderBoardSize = 5;
    
    void Awake()
    {
        instance = this;
    }

    public int[] loadLeaderboard()
    {
        int[] scores = new int[leaderBoardSize];

        for(int i = 0; i < scores.Length; i++) {
            string score = "Leaderboard_score_" + i;

            if(PlayerPrefs.HasKey(score)) {
                scores[i] = PlayerPrefs.GetInt(score);
            } else {
                PlayerPrefs.SetInt("Leaderboard_score_" + i, 0);
                scores[i] = 0;
            }
        }
        return scores;
    }

    public void updateLeaderboard(int newScore)
    {
        int[] scores = loadLeaderboard();

        for(int i = 0; i < scores.Length; i++)
        {
            if(newScore > scores[i]) {
                print("You have beaten place " + (i + 1));
                if(PlayerPrefs.HasKey("Leaderboard_score_" + i)) {

                    // Shift down array
                    for (int k = scores.Length - 1; k > i; k--)
                    {
                        scores[k] = scores[k - 1];
                    }

                    scores[i] = newScore;

                    // Save new leaderboard locations
                    for (int z = 0; z < scores.Length; z++) {
                        PlayerPrefs.SetInt("Leaderboard_score_" + z, scores[z]);
                    }

                    PlayerPrefs.Save();
                    break;
                }
            }
        }
    }
}
