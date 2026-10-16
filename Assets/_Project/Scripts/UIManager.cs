
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Victory UI")]
    [SerializeField] private GameObject victoryPanel;
  

    [Header("Victory Stars")]
    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite filledStar;
    [SerializeField] private Sprite emptyStar;

    [Header("UI Panels")]
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private GameObject menuButton;

    private bool isPaused;

    private void Start()
    {
        victoryPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (GameManager.Instance.IsLevelCompleted)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (GameManager.Instance.IsLevelCompleted)
            return;

        isPaused = true;
        pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (GameManager.Instance.IsLevelCompleted)
            return;

        isPaused = false;
        pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }


    public void ShowVictory(int stars)
    {
        isPaused = false;
        pausePanel.SetActive(false);
        menuButton.SetActive(false);

        victoryPanel.SetActive(true);

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite =
                i < stars ? filledStar : emptyStar;
        }

        Time.timeScale = 0f;
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

}
