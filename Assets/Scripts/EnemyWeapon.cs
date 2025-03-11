using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;    
    public float fireRate = 1f;    
    public float bulletSpeed = 5f;
    public float shootRange = 5f; 

    private float nextFireTime = 0f;
    private Transform player;
    private EnemyAI enemyAI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAI = GetComponentInParent<EnemyAI>();

        if (enemyAI == null)
        {
            Debug.LogError("EnemyAI NOT FOUND!");
        }
        else
        {
            Debug.Log("EnemyAI FOUND: " + enemyAI.gameObject.name);
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        Debug.Log("Trying to shoot: " + (enemyAI.CanShoot() ? "YES" : "NO"));

        // 🔹 Only shoot when the enemy sees the player
        if (distanceToPlayer <= shootRange && enemyAI != null && enemyAI.CanShoot() && Time.time >= nextFireTime)
        {
            Debug.Log("Shooting!");
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPrefab NOT Assigned!");
            return;
        }

        GameObject enemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Bullet instantiated!");

        Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;  // ✅ Ensure bullet moves toward player
            Debug.Log("Bullet moving!");
        }
        else
        {
            Debug.LogError("Bullet has no Rigidbody2D!");
        }
    }
}
