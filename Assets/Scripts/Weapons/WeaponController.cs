using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bulletPrefab;
    
    public WeaponData weaponData;

    private bool canShoot = true; // Flag to control shooting cadence
    private float timeSinceLastShot = 0f; // Track time since the last shot

    private void Start()
    {
        bulletPrefab = weaponData.bulletPrefab;
    }

    private void Update()
    {
        // Increment time since the last shot
        timeSinceLastShot += Time.deltaTime;
    }

    private void Shoot()
    {
        // Check if enough time has passed to shoot again
        if (timeSinceLastShot >= 1f / weaponData.fireRate)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRb = bulletObject.GetComponent<Rigidbody2D>();
            Vector2 bulletDirection = firePoint.right; // Fire in the direction of the firePoint's right
            bulletRb.AddForce(bulletDirection * weaponData.bulletData.speed, ForceMode2D.Impulse);

            // Reset the time since the last shot and prevent shooting until cadence time passes again
            timeSinceLastShot = 0f;
            canShoot = false;

            // Start a coroutine to enable shooting after the cadence time
            StartCoroutine(EnableShooting());
        }
    }

    private IEnumerator EnableShooting()
    {
        // Wait for the weapon's cadence time
        yield return new WaitForSeconds(1f / weaponData.fireRate);

        // Enable shooting again
        canShoot = true;
    }

    public void HandleShootInput(bool shouldShoot)
    {
        // Check if the player wants to shoot and if enough time has passed
        if (shouldShoot && canShoot)
        {
            Shoot();
        }
    }
}
