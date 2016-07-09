using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public enum WeaponType {PlazmaGun,ShutGun,AutomaticGun,FureGun };
    public class Weapon : MonoBehaviour{
        public WeaponType type;
        public AudioSource source;
        [SerializeField]
        protected string bulletName = "bullet";
        public Transform emitter;
        public Transform sight;
        [SerializeField]
        public float refireTime = 1;
        protected float lastFireTime = 0;
        [SerializeField]
        protected float damage = 2;
        void Awake()
        {
            source = GetComponent<AudioSource>();
        }
        protected virtual void FireMethod()
        {
            GameObject shoot = ObjectPool.instance.GetBullet(bulletName + "(Clone)");//Resources.Load<GameObject>(bulletName);
            var script = shoot.GetComponent<Shoot>();
            var course = (sight.position - emitter.position).normalized;
                script.Course = course;
                script.damage = damage;
                script.HostTag = transform.root.tag;
            shoot.transform.position = emitter.position;
            shoot.SetActive(true);           
        }
        public bool Fire()
        {
            if (Time.time>lastFireTime+refireTime)
            {
                if (source != null)
                {
                    source.Play();
                }
            
              FireMethod();
              lastFireTime = Time.time;
                return true;
            }
            return false;
            
        }
    }
}

