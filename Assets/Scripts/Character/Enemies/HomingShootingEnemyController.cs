using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShootingEnemyController : EnemyController
{


    float fireRate;

    float nextFire;



    
    protected override void Start()
    {
        fireRate = 1f;
        nextFire = 0;
    }


    protected override void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(Pool.HOMING_ENEMY_BULLET, transform.position, Quaternion.identity);
            bullet.GetComponent<TrackingEnemyBullet>().OnObjectSpawn();
           // Debug.Log("Spawn Bullet");
            nextFire = Time.time + fireRate;
        }
    }
}
