using System;
using System.IO;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public GameStats currentStats;
    private string path;

    public void saveGame(DateTime dateTime, float matchLength, string name, int score, int level, float bulletsShot, float bulletsHit)
    {
        string dateTimeS = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        GameStats newStats = new GameStats(dateTimeS, matchLength, name, score, level, bulletsShot, bulletsHit);
        currentStats = newStats;
        saveStats();
    }

    public void saveStats() {
        path = Path.Combine(Application.persistentDataPath, "gamestats_" 
                + dateTimeConvert() + ".json");
        string jsonData = JsonUtility.ToJson(currentStats, true);
        File.WriteAllText(path, jsonData);
    }

    private string dateTimeConvert() {
        string[] parts = currentStats.dateTime.Split(new char[] {'/', ':', ' '});
        string result = "";

        for(int i = 0; i < parts.Length; i++) {
            result += parts[i];
        }

        return result;
    }
}