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
        Animator animator;
        float blastWaveRadius = 3;
        float blastForce = 50;
        void Start()
        {
            animator = GetComponent<Animator>();
        }
        protected void VisualDamage(Vector3 position)
        {
            if (IsOrganic)
            {
                GameObject blood = Resources.Load<GameObject>("Blood");
                blood.transform.position = position;
                Instantiate<GameObject>(blood);
            }
            else
            {
                GameObject sparkDamage = Resources.Load<GameObject>("SparksDamage");
                sparkDamage.transform.position = position;
                Instantiate<GameObject>(sparkDamage);
            }
        }
        public override void ReactionOnFire(KillableObject objectKiller)
        {
            VisualDamage(objectKiller.transform.position);
            if (SetDamage(objectKiller.damage))
            {
                Death(objectKiller.deathName);
            }
        }
        public override void ReactionOnFire(Shoot bullet)
        {
            VisualDamage(bullet.transform.position);
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
            if (!IsOrganic)
            {
                GameObject exp = transform.GetChild(0).gameObject;
                //on death animation for this bullet(weapon)
                exp.SetActive(true);
                exp.transform.parent = null;
                Blast();
                Destroy(exp, 0.5f);
                
            }          
            else
            {
                if (animator != null)
                {
                
                    animator.Play(deathName,0);
                    AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
                    //fix them
                }
                
                Destroy(gameObject,animator.GetCurrentAnimatorStateInfo(0).length);
                
            }
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