using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public string userName;
    [SerializeField] private TextMeshProUGUI scores;   
    [SerializeField] private TextMeshProUGUI textbox;

    void Awake()
    {
        if (PlayerPrefs.HasKey("ActiveName")) {
            userName = PlayerPrefs.GetString("ActiveName");
        } else {
            userName = "P" + UnityEngine.Random.Range(1000, 9999);
        }

        textbox.text = userName;
        setName(userName);
    }

    void Start()
    {
        scores.text = top5();
    }

    private string top5() 
    {
        Leaderboard.instance.loadLeaderboard(out int[] scoreArray, out string[] nameArray);
        string response = "";

        string activeName = PlayerPrefs.GetString("ActiveName", "Player");
        int activeScore = PlayerPrefs.GetInt("ActiveScore", -1);

        for (int i = 0; i < scoreArray.Length; i++) 
        {
            string line = writeLine(i+1, nameArray[i], scoreArray[i], false);

            if (nameArray[i] == activeName && scoreArray[i] == activeScore) 
            {
                line = writeLine(i+1, nameArray[i], scoreArray[i], true);
            }

            response += line;
        }
        return response;
    }

    private string writeLine(int location, string name, int score, bool green) {

        string line = location + ". " + name + " \t" + score + Environment.NewLine;

        if(name.Length <= 1) {
            line = location + ". " + name + " \t\t" + score + Environment.NewLine;
        }

        if(green) {
            line = location + ". " + name + " \t" + score + Environment.NewLine;
            line = $"<color=#00FF00>{line}</color>";
        }

        return line;
    }

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void readName(string name) {
        userName = name;
        setName(name);
    }

    public void setName(string name) {
        PlayerPrefs.SetString("ActiveName", name);
        PlayerPrefs.Save();
    }
}
