using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartGame()
    {
        string currentScene = SceneManager.GetActiveScene().name; // Get current level name
        SceneManager.LoadScene(currentScene);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu"); 
    }
}
