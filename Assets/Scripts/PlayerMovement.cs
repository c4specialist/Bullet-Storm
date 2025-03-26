using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    
    [Header("Components")]
    public Rigidbody2D rb;
    
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private PlayerStamina playerStamina;

    void Start()
    {
        playerStamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        Vector3 rawMousePosition = Input.mousePosition;

        rawMousePosition.x = Mathf.Clamp(rawMousePosition.x, 0, Screen.width);
        rawMousePosition.y = Mathf.Clamp(rawMousePosition.y, 0, Screen.height);

        rawMousePosition.z = Mathf.Abs(Camera.main.transform.position.z);
        mousePosition = Camera.main.ScreenToWorldPoint(rawMousePosition);
    }

    private void Move()
    {
        float currentSpeed = moveSpeed;
        
        if (Input.GetKey(KeyCode.LeftShift) && playerStamina.HasStamina())
        {
            currentSpeed = sprintSpeed;
        }

        rb.velocity = moveDirection * currentSpeed;
    }

    private void Rotate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 180f;
        rb.rotation = aimAngle;
    }

    

}