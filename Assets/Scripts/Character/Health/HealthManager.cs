using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int MaxHealth;

    protected int CurrentHealth;

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // This needs to be changed when we get a prefab thingy manager thing
    protected virtual void Update()
    {
        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);

        }
    }

    public virtual void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
