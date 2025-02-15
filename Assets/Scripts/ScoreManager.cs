using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    void Awake()
    {
        instance = this;
    }

    public void AddScore()
    {
        score += 5;
        scoreText.text = score.ToString();
    }
}
