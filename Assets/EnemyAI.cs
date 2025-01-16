using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;       // Reference to the player
    public float speed;             // Movement speed of the AI
    public float detectionRadius;   // Radius within which the AI detects the player
    public float stoppingDistance;  // Distance at which the AI stops moving

    private float distance;         // Distance between AI and player

    void Update()
    {
        // Calculate distance to the player
        distance = Vector2.Distance(transform.position, player.transform.position);

        // Check if the player is within the detection radius
        if (distance <= detectionRadius)
        {
            // Rotate toward the player
            Vector2 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Move toward the player if outside the stopping distance
            if (distance > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }
}
