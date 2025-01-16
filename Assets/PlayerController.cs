using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import for UI components

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Normal movement speed
    public float sprintSpeed = 8f; // Sprinting speed
    public float maxStamina = 100f; // Maximum stamina
    public float staminaDrainRate = 20f; // Stamina drain per second while sprinting
    public float staminaRegenRate = 10f; // Stamina regeneration per second when not sprinting

    private float currentStamina; // Player's current stamina
    public Rigidbody2D rb;
    public Weapon weapon;

    public Slider staminaBar; // Reference to the stamina slider


    Vector2 moveDirection;
    Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina; // Initialize stamina

        // Initialize the stamina bar UI
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = currentStamina;


        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Check for shooting input
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        // Normalize movement direction
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Get mouse position in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Regenerate stamina if not sprinting or stamina is depleted
        if (!Input.GetKey(KeyCode.LeftShift) || currentStamina <= 0)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina); // Clamp stamina to max
        }

        // Update the stamina bar
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;

        }
    }

    // FixedUpdate is called at fixed intervals for physics updates
    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        // Check if sprint key is held and stamina is above 0
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            currentSpeed = sprintSpeed;

            // Drain stamina while sprinting
            currentStamina -= staminaDrainRate * Time.fixedDeltaTime;
            currentStamina = Mathf.Max(currentStamina, 0); // Clamp stamina to zero
        }

        // Stop sprinting if stamina is 0
        if (currentStamina <= 0)
        {
            currentSpeed = moveSpeed;
        }

        // Set player velocity based on movement direction and speed
        rb.velocity = new Vector2(moveDirection.x * currentSpeed, moveDirection.y * currentSpeed);

        // Handle player rotation to face the mouse cursor
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
