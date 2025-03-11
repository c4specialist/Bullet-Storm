using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultySlider : MonoBehaviour
{
    public Slider difficultySlider; 
    public TMP_Text difficultyText; 

    void Start()
    {
        
        difficultySlider.minValue = 0;
        difficultySlider.maxValue = 2;
        difficultySlider.wholeNumbers = true; 
        UpdateDifficultyText(); 
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
