using UnityEngine;
using System.Collections;

namespace MyPlatformer
{

    public class ParticleReciever : MonoBehaviour
    {
        public string deathName = "Fire balls particle";
        // Use this for initialization
        void Start()
        {

        }
        void OnParticleCollision(GameObject other)
        {
            DestroybleObject objDestr = other.GetComponent<DestroybleObject>();
            if (objDestr!=null)
            {
                objDestr.ReactionOnFire(new KillableObject() { damage = 3, deathName = deathName });
             
            }
        }
            // Update is called once per frame
        void Update()
        {

        }

    }

}

