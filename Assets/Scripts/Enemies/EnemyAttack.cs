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

        instantiatedWeapon = Instantiate(weaponData.weaponPrefab, weaponHolder);
        instantiatedWeapon.tag = "Untagged";
        weaponController = instantiatedWeapon.GetComponent<WeaponController>(); // Get the WeaponController component
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Set up raycast parameters
        Vector3 shootDirection = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shootDirection, distanceToPlayer, LayerMask.GetMask("Obstacle"));

        // If the player is within attack range, there's no obstacle in the line of sight, initiate attack
        if (distanceToPlayer <= enemyData.attackRange && hit.collider == null)
            Attack();

        // Update weapon direction to point towards the player
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        instantiatedWeapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // Invert Y scale based on enemy rotation
        Vector3 weaponScale = weaponHolder.localScale;
        weaponScale.y = (angle >= 100 && angle <= 180) ? -0.75f : 0.75f; // CHANGE FOT THE ORIGINAL SCALE
        weaponHolder.localScale = weaponScale;
    }

    private void Attack()
    {
        // Call the Shoot function of the WeaponController component
        weaponController.HandleShootInput(true);
    }

    public void TakeWeapon(GameObject newWeapon)
    {
        // Remove the instantiated weapon
        Destroy(instantiatedWeapon);

        // Instantiate the weapon and set it as a child of this object
        instantiatedWeapon = Instantiate(newWeapon.GetComponent<WeaponController>().weaponData.weaponPrefab, weaponHolder);
        weaponController = instantiatedWeapon.GetComponent<WeaponController>(); // Get the WeaponController component
        instantiatedWeapon.tag = "Untagged";
    }

    public GameObject FindNearestWeapon()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        GameObject nearestWeapon = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject weapon in weapons)
        {
            float distance = Vector3.Distance(transform.position, weapon.transform.position);
            if (distance < shortestDistance)
            {
                nearestWeapon = weapon;
                shortestDistance = distance;
            }
        }

        return nearestWeapon;
    }
}
