using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviousGameButton : MonoBehaviour
{
    private GameObject previousScreen;
    private GameObject statsScreen;
    public GameStats buttonStats;
    private Button button;
    private TMP_Text buttonText;

    void Start()
    {
        previousScreen = transform.parent.parent.parent.gameObject;
        statsScreen = transform.parent.parent.parent.gameObject.GetComponent<StatsLoader>().statsScreen;

        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        button.onClick.AddListener(clicked);
    }

    void Update()
    {
        if (buttonStats != null && buttonText != null)
        {
            string name = buttonStats.name + " - " + buttonStats.score + " - " + buttonStats.dateTime;
            buttonText.text = name;
            enabled = false;
        }
    }

    private void clicked()
    {
            previousScreen.SetActive(false);
            statsScreen.SetActive(true);

            GameStatsManager.Instance.currentStats = buttonStats;
    }
}