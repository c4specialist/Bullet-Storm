using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerStamina))]
[RequireComponent(typeof(PlayerCombat))]
public class PlayerController : MonoBehaviour
{
    // This script serves as the main controller that coordinates between different player components
    private PlayerHealth health;
    private PlayerMovement movement;
    private PlayerStamina stamina;
    private PlayerCombat combat;

    void Awake()
    {
        // Get references to all required components
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        stamina = GetComponent<PlayerStamina>();
        combat = GetComponent<PlayerCombat>();
    }
}
