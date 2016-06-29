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
        static Queue<GameObject> automatBullets = new Queue<GameObject>();
        static Queue<GameObject> plazmaBullets = new Queue<GameObject>();

        void CreteBulletInPool(string name,int count)
        {
            for (int i = 0; i < count; i++)
            {
               
                GameObject shoot = Resources.Load<GameObject>(name);
              
                shoot = Instantiate<GameObject>(shoot);
                ReturnBulletToPool(shoot);
            }
        }
        void Start()
        {
            myTransform = transform;

            CreteBulletInPool("bullet2", automatBulletsCount);
            CreteBulletInPool("bullet", plazmaBulletsCount);
           
        }
        static Queue<GameObject> GetObjectsList(string nameObj)
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
        void ReturnBulletToPool(GameObject bullet)
    {
        bullet.transform.position = myTransform.position; 
        bullet.transform.parent = myTransform;
        Shoot script = bullet.GetComponent<Shoot>();
        script.Course = Vector3.zero;
        script.enabled = false;
        Debug.Log(bullet.name);
        GetObjectsList(bullet.name).Enqueue(bullet);
    }
        static GameObject GetBullet(string name)
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
