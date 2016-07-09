using UnityEngine;
using System.Collections.Generic;
namespace MyPlatformer
{
    public class ObjectPool : MonoBehaviour
    {
        private Transform myTransform;
        public int automatBulletsCount = 100;
        public int plazmaBulletsCount = 50;
        // Use this for initialization
         Queue<GameObject> automatBullets = new Queue<GameObject>();
         Queue<GameObject> plazmaBullets = new Queue<GameObject>();
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

        void CreteBulletInPool(string name,int count)
        {
            for (int i = 0; i < count; i++)
            {
               
                GameObject shoot = Resources.Load<GameObject>(name);
                shoot = Instantiate<GameObject>(shoot);
                //Debug.LogError(shoot);
                ReturnBulletToPool(shoot);
            }
        }
        void Start()
        {
            myTransform = transform;

            CreteBulletInPool("bullet2", automatBulletsCount);
            CreteBulletInPool("bullet", plazmaBulletsCount);
           
        }
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
                default: return null;
            }
        }
       public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = myTransform.position; 
        bullet.transform.parent = myTransform;
        Shoot script = bullet.GetComponent<Shoot>();
        script.Course = Vector3.zero;
        script.enabled = false;
        Debug.Log(bullet.name);
        GetObjectsList(bullet.name).Enqueue(bullet);
     
    }
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
        // Update is called once per fram
    }
}
