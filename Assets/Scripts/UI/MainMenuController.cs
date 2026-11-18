
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameScene = "Level01";

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Spiel wird beendet.");
    }
}
