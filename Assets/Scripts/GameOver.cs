using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private CanvasGroup canvas;
    private IAudioSystem audioSystem;

    void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        audioSystem = ServiceLocator.Get<IAudioSystem>();

        canvas.interactable = false;
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
    }

    public void playAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void mainMenu()
    {
        audioSystem.StopAllSounds();
        SceneManager.LoadScene(0);
    }

    public void toggleGameOver()
    {
        canvas.interactable = true;
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
    }
}
