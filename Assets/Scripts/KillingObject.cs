using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public class KillingObject : MonoBehaviour
    {
        [SerializeField]
        protected string hostTag = string.Empty;//parent tag
        public bool killGodMode = false;//can kill in godmode
        public bool destructAfterHit = false;// are need destruct after hit
        public float damage;
        public string deathName;
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
        public string HostTag
        {
            get
            {
                return hostTag;
            }
            set
            {
                hostTag = value;
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            FiriebleObject otherDeath = other.gameObject.GetComponent<FiriebleObject>();
            if (otherDeath != null && other.tag!=HostTag)
            {
                //send damage
                otherDeath.ReactionOnFire(this,false);
                if (destructAfterHit)
                {
                    GetComponent<DestroybleObject>().ReactionOnFire(this, false);
                }
            }
        }

        void OnParticleCollision(GameObject other)
        {
            FiriebleObject objDestr = other.GetComponent<FiriebleObject>();
            if (objDestr != null && other.tag != HostTag)
            {
                //send damage
                objDestr.ReactionOnFire(this,true);

            }
        }
    }
}