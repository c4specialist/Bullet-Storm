using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 15; // Set default bullet damage

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet hit an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Deal damage to enemy
            }
        }

        Destroy(gameObject); // Destroy bullet after collision
    }
}
