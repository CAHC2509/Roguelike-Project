using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public int maxHealth;
    public float rotationSpeed;
    public AttackType attackType;
    public float attackRange;
    public BulletData bulletData;
    public float attackCooldown;
    public float stopDistance = 1.25f;
    // More stats

    public enum AttackType
    {
        Projectile,
        Melee,
        ProjectileAndMelee
        // More attack types
    }
}
