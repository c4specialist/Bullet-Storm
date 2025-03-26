using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;
    public DifficultyLevel currentDifficulty = DifficultyLevel.Medium;
    public int enemyHealth;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ✅ Persist across scenes
            Debug.Log("✅ DifficultyManager Initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        currentDifficulty = difficulty;

        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                enemyHealth = 50;
                break;
            case DifficultyLevel.Medium:
                enemyHealth = 100;
                break;
            case DifficultyLevel.Hard:
                enemyHealth = 150;
                break;
        }

        Debug.Log("🎯 Difficulty Set To: " + currentDifficulty);
        Debug.Log("💙 Enemy Health Updated To: " + enemyHealth);
    }
}
