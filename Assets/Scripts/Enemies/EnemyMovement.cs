using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private EnemyAttack enemyAttack;
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private float searchWeaponChance = 0.5f;

    private Transform player;
    private NavMeshAgent enemyAgent;
    [SerializeField]
    private Transform target; // Store the chosen target (player or weapon)

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.updateRotation = false;
        enemyAgent.updateUpAxis = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Define if the enemy will search a weapon or the player
        if (Random.value < searchWeaponChance)
        {
            Transform nearestWeapon = enemyAttack.FindNearestWeapon()?.transform;
            target = nearestWeapon != null ? nearestWeapon : player;
        }
        else
        {
            target = player;
        }

        // Set the agent's destination to the chosen target's position
        enemyAgent.SetDestination(target.position);
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // If the distance is greater than the stop distance, move the enemy towards the target
        if (distanceToTarget > enemyData.stopDistance)
        {
            // Set the agent's speed to the enemy's movement speed
            enemyAgent.speed = enemyData.speed;

            // Set the destination to the target's position
            enemyAgent.SetDestination(target.position);
        }
        else
        {
            // Stop the enemy if it's close enough to the target
            enemyAgent.ResetPath();

            // If the target is an weapon, take it
            if (target.CompareTag("Weapon"))
            {
                enemyAttack.TakeWeapon(target.gameObject);
                Destroy(target.gameObject);
                target = player; // Set the target back to player after taking the weapon
            }
        }

        // Make the enemy always face the target
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyData.rotationSpeed * Time.deltaTime);
    }
}
