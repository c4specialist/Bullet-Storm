using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int currentLevel; // Set this per level (1 for Level 1, 2 for Level 2, etc.)

    public void CompleteLevel()
    {
        int highestLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Unlock next level if not already unlocked
        if (currentLevel >= highestLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save(); // Save to disk
            Debug.Log("✅ Level " + (currentLevel + 1) + " unlocked!");
        }

        SceneManager.LoadScene("MainMenu"); // Go back to main menu after completing a level
    }
}
