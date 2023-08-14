using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private Transform projectileSpawnPoint; // The point where projectiles are spawned
    [SerializeField]
    private GameObject projectilePrefab; // Prefab of the projectile object

    private Transform player;
    private bool isAttacking;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within attack range, initiate attack
        if (distanceToPlayer <= enemyData.attackRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        // Instantiate a projectile and set its position and direction
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Vector3 shootDirection = (player.position - projectileSpawnPoint.position).normalized;

        // Get the projectile's rigidbody
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        // Apply force to the projectile in the shoot direction
        projectileRb.AddForce(shootDirection * enemyData.bulletData.speed, ForceMode2D.Impulse);

        // Wait for attack cooldown before allowing another attack
        yield return new WaitForSeconds(enemyData.attackCooldown);
        isAttacking = false;
    }
}
