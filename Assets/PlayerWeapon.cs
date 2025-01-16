using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;



    public void Fire()
    {
        GameObject PlayerBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        PlayerBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
    

}

