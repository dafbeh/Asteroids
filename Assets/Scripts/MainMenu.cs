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
    private IAudioSystem audioSystem;


    void Awake()
    {
        audioSystem = ServiceLocator.Get<IAudioSystem>();

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
        LoadSettings();
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

    public void playGame()
    {
        SceneManager.LoadScene(1);
        audioSystem.PlaySound("Sounds/menu");
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

    private void LoadSettings() {
        float volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        AudioListener.volume = volume;

        bool isFullScreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = isFullScreen;

        int width = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
        int height = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);
        Screen.SetResolution(width, height, Screen.fullScreen);
    }
}
