using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultySlider : MonoBehaviour
{
    public Slider difficultySlider; 
    public TMP_Text difficultyText; 

    void Start()
    {
        if (DifficultyManager.Instance == null)
        {
            Debug.LogError("❌ DifficultyManager is missing! Make sure it's in the scene.");
        }
        else
        {
            UpdateDifficulty();
        }

        difficultySlider.minValue = 0;
        difficultySlider.maxValue = 2;
        difficultySlider.wholeNumbers = true;

        difficultySlider.onValueChanged.AddListener(delegate { UpdateDifficultyText(); });
        UpdateDifficultyText();
    }

    public void UpdateDifficulty()
    {
        if (DifficultyManager.Instance != null)
        {
            DifficultyManager.Instance.SetDifficulty((DifficultyLevel)(int)difficultySlider.value);
            Debug.Log("🎯 Difficulty Updated: " + DifficultyManager.Instance.currentDifficulty);
            Debug.Log("💙 Enemy Health Updated: " + DifficultyManager.Instance.enemyHealth);
        }
    }

    public void UpdateDifficultyText()
    {
        switch ((int)difficultySlider.value)
        {
            case 0:
                difficultyText.text = "Easy";
                break;
            case 1:
                difficultyText.text = "Medium";
                break;
            case 2:
                difficultyText.text = "Hard";
                break;
            default:
                difficultyText.text = "Unknown";
                break;
        }
    }


}

public enum DifficultyLevel { Easy = 0, Medium = 1, Hard = 2 }

