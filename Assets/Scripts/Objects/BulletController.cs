using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private BulletData bulletData;

    private void Start()
    {
        Destroy(gameObject, bulletData.lifeTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bulletData.isEnemyBullet && collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        if (!collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
