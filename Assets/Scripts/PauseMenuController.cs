using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuCanvas;

    private bool isPaused = false;

    private void Start()
    {
        // Ensure the pause menu is inactive at the start
        pauseMenuCanvas.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1; // Pause or resume the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Ensure the game is running
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1; // Ensure the game is running
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}
