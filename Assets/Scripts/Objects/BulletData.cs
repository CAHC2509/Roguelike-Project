using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Bullet Data")]
public class BulletData : ScriptableObject
{
    public float damage = 2f;
    public int health = 2;
    public float lifeTime = 4f;
    public bool isEnemyBullet;
    public float speed = 5f;
}
