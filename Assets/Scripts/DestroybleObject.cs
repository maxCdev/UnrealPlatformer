using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace MyPlatformer
{
    interface IDestroyble
    {
        float Hp { set; get; }
        void Death(string deathName);
        bool SetDamage(float damage);
    }
    public class DestroybleObject : FiriebleObject,IDestroyble
    {

        float blastWaveRadius = 6;
        float blastForce = 50;
        public void ReactionOnFire(KillableObject objectKiller)
        {
            if (SetDamage(objectKiller.damage))
            {
                Death(objectKiller.deathName);
            }
        }
        public override void ReactionOnFire(Shoot bullet)
        {
            if (SetDamage(bullet.damage))
            {
                Death(bullet.deathName);
            }
            else
            {
                base.ReactionOnFire(bullet);
            }
            
        }
        [SerializeField]
        public float hp;
        public float Hp
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
            }
        }
        void Blast()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastWaveRadius);
            List<FiriebleObject> fireableObjs = colliders.Select(a => a.GetComponent<FiriebleObject>()).ToList();
            Debug.Log(" colliders : " + fireableObjs.Count);
            for (int i = 0; i < fireableObjs.Count; i++)
            {
                if (fireableObjs[i] != null&&fireableObjs[i].gameObject != gameObject)
                {
                var course = (fireableObjs[i].transform.position - transform.position).normalized;
                    fireableObjs[i].ReactionOnFire(course, blastForce, transform.position);
                }
                    
            }
        }
        public void Death(string deathName)
        {
            GameObject exp = transform.GetChild(0).gameObject;
            //on death animation for this bullet(weapon)
            exp.SetActive(true);
            exp.transform.parent = null;           
            Blast();
            Destroy(exp,0.5f);
            Destroy(gameObject);
        }

        public bool SetDamage(float damage)
        {
            Hp -= damage;
            if (Hp<0)
            {
                return true;
            }
            return false;
        }
    }
}