using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{

    public void HurtEnemy(int damageToGive)
    {
        CurrentHealth -= damageToGive;
        Debug.Log(damageToGive);
    }

}