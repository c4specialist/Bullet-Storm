using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;       
    public float speed = 2f;             
    public float detectionRadius = 5f;   
    public float stoppingDistance = 1.5f; 
    public LayerMask obstacleLayer; 

    private Rigidbody2D rb;  
    private float distance;         
    private bool canShoot = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // ✅ Use Kinematic Rigidbody
    }

    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= detectionRadius && HasLineOfSight())
        {
            Debug.Log("✅ Player detected! Moving...");

            Vector2 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

            if (distance > stoppingDistance)
            {
                Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;

                // ✅ Check for walls before moving
                if (!IsBlocked(newPosition))
                {
                    rb.MovePosition(newPosition);
                }
            }

            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
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
        return hit != null;
    }

    public bool CanShoot()
    {
        return canShoot;
    }
}
