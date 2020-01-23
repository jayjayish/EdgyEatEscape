using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingEnemy : MonoBehaviour
{


    float fireRate;

    float nextFire;



    
    void Start()
    {
        fireRate = 1f;
        nextFire = 0;
    }

    
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(Pool.HORIZONTAL_ENEMY_BULLET, transform.position, Quaternion.identity);
            bullet.GetComponent<HorizontalEnemyBullet>().OnObjectSpawn();
            nextFire = Time.time + fireRate;
        }
    }
}
