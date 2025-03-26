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

    private int health; // Enemy Health

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    rb.isKinematic = true;

    StartCoroutine(WaitForDifficultyManager());
}

IEnumerator WaitForDifficultyManager()
{
    while (DifficultyManager.Instance == null)
    {
        Debug.LogWarning("⏳ Waiting for DifficultyManager...");
        yield return null; // Waits until the next frame
    }

    Debug.Log("🎯 DifficultyManager found, setting enemy health.");
    health = DifficultyManager.Instance.enemyHealth;
}

    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= detectionRadius)
        {
            moveDirection = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

            if (distance > stoppingDistance)
            {
                Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;

                // Prevent movement through walls using Raycast
                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, speed * Time.fixedDeltaTime, obstacleLayer);
                if (hit.collider == null)
                {
                    rb.MovePosition(newPosition);
                }
                else
                {
                    Debug.Log("🚧 Enemy is blocked by: " + hit.collider.gameObject.name);
                    moveDirection = Vector2.zero; 
                }
            }

            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }

    // Take Damage Function
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage! Remaining Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Destroy Enemy
    private void Die()
    {
        Debug.Log("💀 Enemy Defeated!");
        Destroy(gameObject);
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
