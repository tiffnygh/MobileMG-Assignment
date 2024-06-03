using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void Start()
    {
        // Ensure the game over canvas is inactive at the start
        gameOverCanvas.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game before returning to the main menu
        SceneManager.LoadScene("MainMenu");
    }
}
