using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float fireRate = 0.5f;
    public Transform firePoint;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        // Логика стрельбы. Например, создание пули или применение урона к цели.
        Debug.Log("Bang! Bang!");
    }
}
