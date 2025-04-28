using UnityEngine;
using TMPro;
using System;

public class GameStatsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gameNumber; 
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text matchLength;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text bs;
    [SerializeField] private TMP_Text bh;
    [SerializeField] private TMP_Text accuracy;

    public GameStats currentStats;
    public static GameStatsManager Instance;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        float bulletsHit = currentStats.bulletsHit;
        float bulletsShot = currentStats.bulletsShot;

        gameNumber.text = "Game #" + currentStats.iteration.ToString();
        playerName.text = "Name : " + wrapGreen(currentStats.name);
        score.text = "Score : " + wrapGreen(currentStats.score.ToString());
        matchLength.text = "Length : " + wrapGreen(Convert.ToInt32(currentStats.matchLength) + " seconds");
        level.text = "Level : " + wrapGreen(currentStats.level.ToString());
        bs.text = "Bullets Shot : " + wrapGreenLong(bulletsShot.ToString());
        bh.text = "Bullets Hit : " + wrapGreenLong(bulletsHit.ToString());
        accuracy.text = "Accuracy" + " ~ " + wrapGreenLong(Convert.ToInt32(bulletsHit / bulletsShot * 100) + "%");
    }

    private string wrapGreen(string text) {
        return $"<color=#89F336>\t\t{text}</color>";
    }

    // Quick fix for columns :)
    private string wrapGreenLong(string text) {
        return $"<color=#89F336>\t{text}</color>";
    }
}