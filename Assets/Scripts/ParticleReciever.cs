using UnityEngine;
using System.Collections;

namespace MyPlatformer
{

    public class ParticleReciever : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }
        void OnParticleCollision(GameObject other)
        {
            DestroybleObject objDestr = other.GetComponent<DestroybleObject>();
            if (objDestr!=null)
            {
                objDestr.ReactionOnFire(new KillableObject(){damage=3,deathName="Fire balls particle"});
             
            }
        }
            // Update is called once per frame
        void Update()
        {

        }

    }

}

