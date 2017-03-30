using UnityEngine;
using System.Collections.Generic;
namespace MyPlatformer
{
    public class ObjectPool : MonoBehaviour
    {
        private Transform myTransform;
        public int automatBulletsCount = 100;
        public int plazmaBulletsCount = 50;
        public int bloodsCount = 30;
        public int sparksCount = 30;
        public int exploadsCount = 20;
        Queue<GameObject> automatBullets = new Queue<GameObject>();
        Queue<GameObject> plazmaBullets = new Queue<GameObject>();
        Queue<GameObject> bloods = new Queue<GameObject>();
        Queue<GameObject> exploeds = new Queue<GameObject>();
        Queue<GameObject> sparks = new Queue<GameObject>();
        Queue<GameObject> characters = new Queue<GameObject>();
        Queue<GameObject> bonuses = new Queue<GameObject>();
        public static ObjectPool instance;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// Create bullet in pool
        /// </summary>
        /// <param name="name">bullet name</param>
        /// <param name="count">bullet count</param>
        void CreteBulletInPool(string name,int count)
        {
            for (int i = 0; i < count; i++)
            {              
                GameObject shoot = Resources.Load<GameObject>(name);
                shoot = Instantiate<GameObject>(shoot);
                ReturnBulletToPool(shoot);
            }
        }
        /// <summary>
        /// Create particle in pool
        /// </summary>
        /// <param name="name">particle name</param>
        /// <param name="count">particle count</param>
        void CreateParticleInPool(string name,int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject particle = Resources.Load<GameObject>(name);
                particle = Instantiate<GameObject>(particle);
                ReturnParticleToPool(particle);
            }
        }
        void Start()
        {
            //cashing transform component
            myTransform = transform;

            //create objects in pool
            CreteBulletInPool("bullet2", automatBulletsCount);
            CreteBulletInPool("bullet", plazmaBulletsCount);
            CreateParticleInPool("Blood", bloodsCount);
            CreateParticleInPool("SparksDamage", bloodsCount);
            CreateParticleInPool("ExplosionMobile", bloodsCount);
           
        }
        /// <summary>
        /// Return a collection objects by name
        /// </summary>
        /// <param name="nameObj">name of object in pool</param>
        /// <returns></returns>
        Queue<GameObject> GetObjectsList(string nameObj)
        {
            switch (nameObj)
            {
                case "bullet(Clone)":
                    {
                        return automatBullets;
                    }
                case "bullet2(Clone)":
                    {
                        return plazmaBullets;
                    }
                case "Blood(Clone)":
                    {
                        return bloods;
                    }
                case "SparksDamage(Clone)":
                    {

                        return sparks;
                    } 
                case "ExplosionMobile(Clone)":
                    {
                        return exploeds;
                    }
                default: return null;
            }
        }
        /// <summary>
        /// poots a particle system to pool
        /// </summary>
        /// <param name="effect"></param>
        public void ReturnParticleToPool(GameObject effect)
        {
            if (effect == null)
            {
                Debug.LogError(effect.name);
            }
            var particleDestr = effect.GetComponent<ParticelAutoDestoyer>();
            if (particleDestr!=null)
            {
                particleDestr.enabled = false;
            }
            effect.SetActive(false);
            effect.transform.position = myTransform.position;
            effect.transform.parent = myTransform;
            ParticleSystem script = effect.GetComponent<ParticleSystem>();
            if (script!=null)
            {
                script.Stop();
            }
            //var storage = GetObjectsList(effect.name);
            //if (!storage.Contains(effect))
            //{               
            //    storage.Enqueue(effect);
            //}
            //else
            //{
            //    Debug.Log(effect.name + "already exist in pool");
            //}
            
        }
        public void ReturnBonusToPool(GameObject bonus)
        {
            bonus.SetActive(false);
            bonus.transform.position = myTransform.position;
            bonus.transform.parent = myTransform;
            bonuses.Enqueue(bonus);
        }
        /// <summary>
        /// puts any object who have DestroybleObject to pool
        /// </summary>
        /// <param name="character"></param>
        public void ReturnCharacterToPool(GameObject character)
        {
            character.SetActive(false);
            DestroybleObject destrObj = character.GetComponent<DestroybleObject>();
            if (destrObj)
            {
                destrObj.enabled = false;
            }
            
            character.transform.position = myTransform.position;
            character.transform.parent = myTransform;
            characters.Enqueue(character);

        }
        /// <summary>
        /// poots a bullet to pool
        /// </summary>
        /// <param name="bullet"></param>
        public void ReturnBulletToPool(GameObject bullet)
        {
            bullet.SetActive(false);
            bullet.transform.position = myTransform.position; 
            bullet.transform.parent = myTransform;
            Shoot script = bullet.GetComponent<Shoot>();
            script.Course = Vector3.zero;
            script.enabled = false;
            GetObjectsList(bullet.name).Enqueue(bullet);
        }
        /// <summary>
        /// return particle effects from pool
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
       public GameObject GetEffect(string name)
       {
          // Queue<GameObject> listObj = GetObjectsList(name);
         //  if (listObj.Count > 0)
           {
               GameObject effect = transform.FindChild(name).gameObject; //listObj.Dequeue();
               effect.transform.parent = null;               
           
               var particleDestr = effect.GetComponent<ParticelAutoDestoyer>();
               if (particleDestr != null)
               {
                   particleDestr.enabled = true;
               }

             return effect;
           }
           return null;
       }
        /// <summary>
        /// return character from pool
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
       //public GameObject GetCharacter(string name)
       //{
       //    Queue<GameObject> listObj = GetObjectsList(name);
       //    if (listObj.Count > 0)
       //    {
       //        GameObject character = listObj.Dequeue();
       //        character.transform.parent = null;
       //        return character;
       //    }
       //    return null;
       //}
        /// <summary>
        /// return bullet from pool
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
       public GameObject GetBullet(string name)
        {
            Queue<GameObject> listObj = GetObjectsList(name);
            if (listObj.Count>0)
            {
                GameObject bullet = listObj.Dequeue();
                bullet.transform.parent=null;
                bullet.GetComponent<Shoot>().enabled = true;
                return bullet;
            }
            return null;
        }
    }
}
