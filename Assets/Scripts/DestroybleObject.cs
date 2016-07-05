using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
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
        AudioSource audioSource;
        Animator animator;
        float blastWaveRadius = 3;
        float blastForce = 50;
        public event UnityAction OnChangeHp;
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }
        protected void VisualDamage(Vector3 position)
        {
            if (IsOrganic)
            {
                GameObject blood = Resources.Load<GameObject>("Blood");
                blood.transform.position = position + Vector3.back;           
                Instantiate<GameObject>(blood);              
            }
            else
            {
                GameObject sparkDamage = Resources.Load<GameObject>("SparksDamage");
                sparkDamage.transform.position = position + Vector3.back;
                Instantiate<GameObject>(sparkDamage);
            }
            if (audioSource!=null)
            {
                audioSource.Play();
            }
         
        }
        public override void ReactionOnFire(KillableObject objectKiller,bool isParticle)
        {
            if (isParticle)
            {
                VisualDamage(transform.position);
            }
            else
            {
                VisualDamage(transform.position);
            }
            if (SetDamage(objectKiller.damage))
            {
                Death(objectKiller.deathName);
            }
            else
            {
                base.ReactionOnFire(objectKiller,isParticle);
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
        private float hp;
        public float Hp
        {
            get
            {
                return hp;
            }
            set
            {
                if (value < 0)
                {
                    hp = 0;
                }
                else
                {
                    hp = value;
                }
                if (OnChangeHp != null)
                {
                    OnChangeHp();
                }
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
            OnChangeHp = null;
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
                    foreach (var collider in GetComponents<Collider2D>())
                    {
                        collider.isTrigger = true;

                    }
                    PlatformerCharacter2D controller = GetComponent<PlatformerCharacter2D>();
                    GetComponent<Character2DController>().enabled = false; ;

                    if (controller.jetPack != null)
                    {
                        controller.jetPack.Off();
                    }
                    controller.enabled = false;

                    animator.StopPlayback();
                    animator.SetBool("Ground", true);
                    animator.Play(deathName,0);
                    AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
                    return;
                }
                
                
            }
            Destroy(gameObject);
        }

        public bool SetDamage(float damage)
        {        
            Hp -= damage;
            if (Hp<=0)
            {
                return true;
            }
            return false;
        }
    }
}