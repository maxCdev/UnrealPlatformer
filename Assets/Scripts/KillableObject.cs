using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public class KillableObject : MonoBehaviour
    {

        public float damage;
        public float Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }
        public string deathName;
        public void OnTriggerEnter2D(Collider2D other)
        {
            DestroybleObject otherDeath = other.gameObject.GetComponent<DestroybleObject>();
           if (otherDeath!=null)
           {
               otherDeath.ReactionOnFire(this);
           }
        }
        void OnParticleCollision(GameObject other)
        {
            DestroybleObject objDestr = other.GetComponent<DestroybleObject>();
            if (objDestr != null)
            {
                objDestr.ReactionOnFire(this);

            }
        }
    }
}