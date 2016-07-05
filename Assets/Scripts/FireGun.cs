using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class FireGun : Weapon
    {
        ParticleSystem fire;
        protected override void FireMethod()
        {
            var course = (sight.position - emitter.position).normalized;
            fire.transform.localRotation = Quaternion.Euler(Vector3.forward * course.x * -90);
            fire.Play();
           
        }   
        // Use this for initialization
        void Start()
        {
            fire = emitter.GetComponent<ParticleSystem>();
            var killObj = fire.GetComponent<KillableObject>();
            killObj.HostTag = transform.root.tag;
        }

    }
}
