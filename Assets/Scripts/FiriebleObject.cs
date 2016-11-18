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

/// <summary>
/// This object response reaction on fire, but cant have damage
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
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
     void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    public virtual void ReactionOnFire(Shoot bullet)
    {
        rBody.AddForceAtPosition(bullet.Course * reactionForce, bullet.transform.position);
    }
    public virtual void ReactionOnFire(Vector2 course, float speed, Vector2 position)
    {
        rBody.AddForceAtPosition(course * reactionForce * (speed/10), position);
    }
    public virtual void ReactionOnFire(KillingObject objectKiller, bool isParticle)
    {
        var course = (objectKiller.transform.position - transform.position).normalized;
        rBody.AddForceAtPosition(-course * reactionForce, transform.position-course);
    }

 
}
}

