using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/BlowUp")]
public class BulletBlowUpAbility : Ability
{
    public GameObject bulletPrefab;
    public int numberOfBullets;
    public float bulletsSpeed = 15f;
    public float explosionRadius = 1f;
    public float forceFieldStrength = 50f;

    public override void Activate(PlayerController player)
    {
        float angleStep = 360f / numberOfBullets;
        float angle = 0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angleInRadians = angle * Mathf.Deg2Rad;

            // Calculate the bullet spawn position on the circle using the radius
            Vector3 bulletSpawnPosition = player.transform.position + new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f) * 1;

            // Calculate the direction of the bullet away from the center of the circle
            Vector3 bulletDirection = (bulletSpawnPosition - player.transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(bulletDirection * bulletsSpeed, ForceMode2D.Impulse);

            angle += angleStep;
        }

        // Apply the force field effect to nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (!collider.CompareTag("Bullet"))
            {
                Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();
                if (rigidbody != null)
                {
                    Vector3 forceDirection = (collider.transform.position - player.transform.position).normalized;
                    rigidbody.AddForce(forceDirection * forceFieldStrength, ForceMode2D.Impulse);
                }
            }
        }
    }

    public override void BeginCooldown(PlayerController player)
    {
        activeTime = 0f;
    }
}
