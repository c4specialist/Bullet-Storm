using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;       
    public float speed = 2f;             
    public float detectionRadius = 3f;   
    public float stoppingDistance = 1.5f; 
    public LayerMask obstacleLayer; 

    private Rigidbody2D rb;  
    private float distance;         
    private bool canShoot = false;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // ✅ Use Kinematic Rigidbody
    }

    void FixedUpdate()
{
    distance = Vector2.Distance(transform.position, player.transform.position);

    if (distance <= detectionRadius)
    {
        Debug.Log("✅ Player detected! Moving...");

        moveDirection = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        if (distance > stoppingDistance)
        {
            Vector2 newPosition = transform.position + (Vector3)(moveDirection * speed * Time.fixedDeltaTime); // Use transform.position here

            if (!IsBlocked(newPosition))
            {
                transform.position = newPosition; // Directly change position
            }
            else
            {
                moveDirection *= -1; // Reverse direction when hitting a wall
                Debug.Log("🔄 Reversing direction!");
            }
        }

        canShoot = true;
    }
    else
    {
        canShoot = false;
    }

    Debug.Log("Move Direction: " + moveDirection); // Debugging movement direction
    Debug.Log("Detection Radius: " + detectionRadius);
    Debug.Log("Has Line of Sight: " + HasLineOfSight());
}


    bool HasLineOfSight()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, obstacleLayer);

        Debug.DrawRay(transform.position, direction * detectionRadius, Color.red);

        return hit.collider == null || hit.collider.gameObject == player;
    }

    bool IsBlocked(Vector2 targetPosition)
    {
        Collider2D hit = Physics2D.OverlapCircle(targetPosition, 0.1f, obstacleLayer);
        
        if (hit != null)
        {
            Debug.Log("🚧 Enemy blocked by: " + hit.gameObject.name);
            return true;
        }
        
        return false;
    }

    public bool CanShoot()
    {
        return canShoot;
    }
} 
