using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class Weapon : MonoBehaviour{
        [SerializeField]
        protected string bulletName = "bullet";
        public Transform emitter;
        public Transform sight;
        [SerializeField]
        public float refireTime = 1;
        protected float lastFireTime = 0;
        [SerializeField]
        protected float damage = 2;
        public virtual void Fire()
        {
            if (Time.time>lastFireTime+refireTime)
            {
                GameObject shoot = Resources.Load<GameObject>(bulletName);
                var script = shoot.GetComponent<Shoot>();
                var course = (sight.position - emitter.position).normalized;
                if (script!=null)
                {
                    script.Course = course;
                    script.damage = damage;
                    script.HostTag = transform.root.tag;
                }
                else//if particle
                {
                    var killObj = shoot.GetComponent<KillableObject>();
                    killObj.HostTag = transform.root.tag;
                    shoot.transform.localRotation = Quaternion.Euler(Vector3.forward * course.x * -90);
                }                
                shoot.transform.position = emitter.position;
                Instantiate<GameObject>(shoot);
                lastFireTime = Time.time;
            }
            
            
        }
    }
}

