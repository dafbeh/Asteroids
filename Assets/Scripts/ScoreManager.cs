using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] public int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    void Awake()
    {
        instance = this;
    }

    public int getScore() 
    {
        return score;
    }

    public void AddScore()
    {
        score += 5;
        scoreText.text = score.ToString();
    }
}
