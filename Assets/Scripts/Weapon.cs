using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class Weapon : MonoBehaviour{
        [SerializeField]
        string bulletName = "bullet";
        public Transform emitter;
        public Transform sight;
        [SerializeField]
        float refireTime = 1;
        float lastFireTime = 0;
        [SerializeField]
        float damage = 2;
        public void Fire()
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
                    shoot.transform.localRotation = Quaternion.Euler(Vector3.forward * course.x * -90);
                }                
                shoot.transform.position = emitter.position;
                Instantiate<GameObject>(shoot);
                lastFireTime = Time.time;
            }
            
            
        }
    }
}

