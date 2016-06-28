﻿using UnityEngine;
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
        [SerializeField]
        string hostTag=string.Empty;
        public void OnTriggerEnter2D(Collider2D other)
        {
            DestroybleObject otherDeath = other.gameObject.GetComponent<DestroybleObject>();
            if (otherDeath != null && other.tag!=HostTag)
           {
               otherDeath.ReactionOnFire(this,false);
           }
        }
        void OnParticleCollision(GameObject other)
        {
            FiriebleObject objDestr = other.GetComponent<FiriebleObject>();
            if (objDestr != null && other.tag != HostTag)
            {
                objDestr.ReactionOnFire(this,true);

            }
        }
    }
}