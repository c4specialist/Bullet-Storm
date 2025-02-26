using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultySlider : MonoBehaviour
{
    [Header("UI Components")]
    public Slider difficultySlider; // Reference to the slider
    public TMP_Text difficultyText; // Reference to the TextMeshPro display

    void Start()
    {
        // Ensure the slider's value is within bounds and update text at the start
        difficultySlider.minValue = 0;
        difficultySlider.maxValue = 2;
        difficultySlider.wholeNumbers = true; // Slider snaps to whole numbers
        UpdateDifficultyText(); // Initialize the text
    }

    public void UpdateDifficultyText()
    {
        // Update the text based on the slider's value
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
