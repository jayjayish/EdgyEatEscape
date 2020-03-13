using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] protected float timeToLive;
    [SerializeField] protected float damage;
    protected float angle;
    protected float currentTimeToLive; //currentDuration
    protected bool isMoving = true;

    protected string[] listOfObstacleTags = {Tags.SOLID_OBSTACLE };



    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log(currentTimeToLive);
        if (isMoving)
        {
            transform.Translate(-VectorFromAngle(angle) * velocity * Time.fixedDeltaTime);
            currentTimeToLive += Time.fixedDeltaTime;
            if (currentTimeToLive > timeToLive)
            {
                OnMovementTimeToLiveStopped();
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        CheckIfHitObject(col.tag);
    }

    protected virtual void CheckIfHitObject(string tag)
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
