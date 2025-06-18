using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        if (MusicManager.Instance != null)
            MusicManager.Instance.ResumeMusic();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (MusicManager.Instance != null)
            MusicManager.Instance.PauseMusic();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        if (GameTimerManager.Instance != null)
        {
            GameTimerManager.Instance.ResetTimer();
            GameTimerManager.Instance.StartTimer();
        }

        if (MusicManager.Instance != null)
            MusicManager.Instance.RestartMusic();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Thực tế
#endif
    }
}
