using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    private Transform player;
    private NavMeshAgent enemyAgent;

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.updateRotation = false;
        enemyAgent.updateUpAxis = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the distance is greater than the stop distance, move the enemy towards the player
        if (distanceToPlayer > enemyData.stopDistance)
        {
            // Set the agent's speed to the enemy's movement speed
            enemyAgent.speed = enemyData.speed;

            // Set the destination to the player's position
            enemyAgent.SetDestination(player.position);
        }
        else
        {
            // Stop the enemy if it's close enough to the player
            enemyAgent.ResetPath();
        }

        // Make the enemy always face the player
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyData.rotationSpeed * Time.deltaTime);
    }
}
