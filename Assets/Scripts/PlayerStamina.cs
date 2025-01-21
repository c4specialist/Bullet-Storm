using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 10f;

    [Header("UI References")]
    public Slider staminaBar;
    private Image staminaBarFill;

    private float currentStamina;

    void Start()
    {
        currentStamina = maxStamina;
        InitializeStaminaBar();
    }

    void Update()
    {
        HandleStamina();
        UpdateStaminaUI();
    }

    private void InitializeStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = currentStamina;
            staminaBarFill = staminaBar.fillRect.GetComponent<Image>();
        }
    }

    private void HandleStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            DrainStamina();
        }
        else
        {
            RegenStamina();
        }
    }

    private void DrainStamina()
    {
        currentStamina -= staminaDrainRate * Time.deltaTime;
        currentStamina = Mathf.Max(currentStamina, 0);
    }

    private void RegenStamina()
    {
        currentStamina += staminaRegenRate * Time.deltaTime;
        currentStamina = Mathf.Min(currentStamina, maxStamina);
    }

    public bool HasStamina()
    {
        return currentStamina > 0;
    }

    private void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
            UpdateStaminaBarColor();
        }
    }

    private void UpdateStaminaBarColor()
    {
        if (staminaBarFill != null)
        {
            if (currentStamina > maxStamina * 0.5f)
            {
                staminaBarFill.color = Color.white;
            }
            else if (currentStamina > maxStamina * 0.25f)
            {
                staminaBarFill.color = Color.yellow;
            }
            else
            {
                staminaBarFill.color = Color.red;
            }
        }
    }
}