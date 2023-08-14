using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public WeaponType weaponType;
    public float fireRate;
    public float accuracy;
    public BulletData bulletData;
    public GameObject weaponPrefab;
    // Add more stats as needed

    public enum WeaponType
    {
        Pistol,
        SubmachineGun,
        AssaultRifle,
        Sniper,
        // Add more weapon types
    }
}
