using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour, IPooledObject
{
    [SerializeField] float velocity = 1f;
    [SerializeField] protected float timeToLive;
    [SerializeField] protected int damage;
    protected float angle;
    protected float timeSpawned; //currentDuration

    protected string[] listOfObstacleTags = {Tags.SOLID_OBSTACLE , "Ground"};

    public virtual void OnObjectSpawn(){
        timeSpawned = Time.time;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log(currentTimeToLive);

        transform.Translate(-VectorFromAngle(angle) * velocity);
        if (Time.time - timeSpawned > timeToLive)
        {
            OnMovementTimeToLiveStopped();
        }
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        CheckIfHitObject(col.tag);
    }

    protected void CheckIfHitObject(string tag)
    {
        for (int i = 0; i < listOfObstacleTags.Length; i++)
        {
            if (tag == listOfObstacleTags[i])
            {
                OnHitObject();
            }
        }
    }

    protected abstract void OnHitObject();

    protected abstract void OnMovementTimeToLiveStopped();


    #region Tools
    protected Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta * Mathf.PI / 180), Mathf.Sin(theta * Mathf.PI / 180));
    }
    #endregion



}
