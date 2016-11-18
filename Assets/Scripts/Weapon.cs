using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    /// <summary>
    /// the type(name) of weapon
    /// </summary>
    public enum WeaponType { PlazmaGun, ShutGun, AutomaticGun, FureGun };
    public class Weapon : MonoBehaviour{
        public WeaponType type;
        public AudioSource audioSource;      
        public Transform emitter;
        public Transform sight;        
        protected float lastFireTime = 0;

        [SerializeField]
        protected string bulletName = "bullet";

        [SerializeField]
        protected float refireTime = 1;//delay between shots

        [SerializeField]
        protected float damage = 2;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        protected virtual void FireMethod()
        {
            //get bullet from pool
            GameObject shoot = ObjectPool.instance.GetBullet(bulletName.GetCloneName());
            
            //get shoot comonent
            var script = shoot.GetComponent<Shoot>();

            //calculate course(direction)
            var course = (sight.position - emitter.position).normalized;
            
            //set course
            script.Course = course;

            //set damage power
            script.damage = damage;

            //set a parent tag
            script.HostTag = transform.root.tag;

            //set bullet to emitter position
            shoot.transform.position = emitter.position;

            //enamable bullet (shooting)
            shoot.SetActive(true);           
        }
        public bool Fire()
        {
            //check if refire delay ended
            if (Time.time > lastFireTime + refireTime)
            {
                //play shoot sound
                if (audioSource != null)
                {
                    audioSource.Play();
                }

                //Execute fire method
                FireMethod();

                //get fire time
                lastFireTime = Time.time;
                return true;
            }

          return false;
            
        }
    }
}

