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

    protected string[] listOfObstacleTags = { Tags.ENEMY, Tags.SOLID_OBSTACLE };



    // Update is called once per frame
    protected virtual void Update()
    {
        if (isMoving)
        {
            transform.Translate(-VectorFromAngle(angle) * velocity);
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

    void CheckIfHitObject(string tag)
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
    private Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta * Mathf.PI / 180), Mathf.Sin(theta * Mathf.PI / 180));
    }
    #endregion



}
