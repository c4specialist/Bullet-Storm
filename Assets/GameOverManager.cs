using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu"); 
    }
}
