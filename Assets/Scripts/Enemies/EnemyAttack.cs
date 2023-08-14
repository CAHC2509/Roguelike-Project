using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private WeaponData weaponData;
    [SerializeField]
    private Transform weaponHolder;

    private Transform player;
    private GameObject instantiatedWeapon; // Reference to the instantiated weapon
    private WeaponController weaponController; // Reference to the WeaponController component

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Instantiate the weapon and set it as a child of this object
        instantiatedWeapon = Instantiate(weaponData.weaponPrefab, weaponHolder);
        weaponController = instantiatedWeapon.GetComponent<WeaponController>(); // Get the WeaponController component
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within attack range and the enemy's attack type allows projectile attacks, initiate attack
        if (distanceToPlayer <= enemyData.attackRange)
        {
            Attack();
        }

        // Update weapon direction to point towards the player
        Vector3 shootDirection = (player.position - instantiatedWeapon.transform.position).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        instantiatedWeapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // Invert Y scale based on enemy rotation
        Vector3 weaponScale = weaponHolder.localScale;
        weaponScale.y = (angle >= 100 && angle <= 180) ? -0.75f :0.75f; // CHANGE FOT THE ORIGINAL SCALE
        weaponHolder.localScale = weaponScale;
    }

    private void Attack()
    {
        // Call the Shoot function of the WeaponController component
        weaponController.HandleShootInput(true);
    }
}
