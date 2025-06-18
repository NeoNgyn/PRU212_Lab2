using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Nhấn ESC để bật/tắt Pause
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
        Time.timeScale = 1f;  // Khôi phục tốc độ game
        isPaused = false;
        if (MusicManager.Instance != null)
            MusicManager.Instance.ResumeMusic();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Dừng game
        isPaused = true;

        if (MusicManager.Instance != null)
            MusicManager.Instance.PauseMusic();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Khôi phục tốc độ trước khi reload
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
        Time.timeScale = 1f;  // Tránh bị đứng nếu quay lại menu
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dùng khi test trong Editor
#else
        Application.Quit(); // Thực tế
#endif
    }
}
