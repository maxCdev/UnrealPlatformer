using UnityEngine;
using System.Collections;
using System;
namespace MyPlatformer
{
interface IFirieble
{
    void ReactionOnFire(Shoot bullet);
    float ReactionForce { set; get; }
}

public class FiriebleObject : MonoBehaviour,IFirieble {

    Rigidbody2D rBody;
    public bool IsOrganic = false;
    [SerializeField]
    float reactionForce=1;
    public float ReactionForce
    {
        get
        {
            return reactionForce;
        }
        set
        {
            reactionForce = value;
        }
    }
        void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    public virtual void ReactionOnFire(Shoot bullet)
    {
        rBody.AddForceAtPosition(bullet.Course * reactionForce, bullet.transform.position);
    }
    public virtual void ReactionOnFire(Vector2 course, float speed, Vector2 position)
    {
        rBody.AddForceAtPosition(course * reactionForce, position);
    }
    public virtual void ReactionOnFire(KillableObject objectKiller)
    {}

 
}
}

