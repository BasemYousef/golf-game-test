
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelSceneName = "Golf Game";

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}
