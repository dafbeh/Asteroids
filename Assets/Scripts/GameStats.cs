using System;

public class GameStats {
    public string dateTime;
    public float matchLength;
    public string name;
    public int score;
    public int level;
    public float bulletsShot;
    public float bulletsHit;
    public int iteration;

    public GameStats(string dateTime, float matchLength, string name, int score, int level, float bulletsShot, float bulletsHit) {
        this.dateTime = dateTime;
        this.matchLength = matchLength;
        this.name = name;
        this.score = score;
        this.level = level;
        this.bulletsShot = bulletsShot;
        this.bulletsHit = bulletsHit;
    }
}