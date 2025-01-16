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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject EnemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = EnemyBullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed; // Adjust for firePoint orientation
    }
    
}
