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
    /// <summary>
    /// This object can be destroyed
    /// </summary>
    public class DestroybleObject : FiriebleObject,IDestroyble
    {
        private Animator animator;
        public float blastWaveRadius = 3;
        public float blastForce = 50;//power of blast wave
        public event UnityAction OnChangeHp;//hp change event
        public bool godMode = false;//no destroyble mode

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
        void Start()
        {
            animator = GetComponent<Animator>();
        }        
        /// <summary>
        /// Visual effects as response on damage
        /// </summary>
        /// <param name="position">effects position</param>
        protected void VisualDamage(Vector3 position)
        {
            if (IsOrganic)
            {
                GameObject blood = ObjectPool.instance.GetEffect("Blood(Clone)");
                #if UNITY_EDITOR
                if (blood == null)
                {
                    throw new UnityException("pool return null!!!");
                }
                #endif
                blood.transform.position = position;   
                blood.SetActive(true);          
            }
            else
            {               
                GameObject sparkDamage = ObjectPool.instance.GetEffect("SparksDamage(Clone)");
                #if UNITY_EDITOR
                if (sparkDamage==null)
                {
                    throw new UnityException("pool return null!!!");
                }
                #endif
                sparkDamage.transform.position = position;
                sparkDamage.SetActive(true);
            }
        }
        /// <summary>
        /// The response on damage
        /// </summary>
        /// <param name="objectKiller">Object who make damage</param>
        /// <param name="isParticle"> Flag if is particle</param>
        public override void ReactionOnFire(KillingObject objectKiller,bool isParticle)
        {
            //undo reaction(response) if godmode or rewind on
            if ((godMode&&!objectKiller.killGodMode)||Rewinder.instance.isRewindOn)
            {
                return;
            }
            if (isParticle)
            {
                VisualDamage(transform.position);
            }
            else
            {
                VisualDamage(transform.position);
            }
            //set damage and if is deadly this object dead
            if (SetDamage(objectKiller.damage))
            {
                Death(objectKiller.deathName);
            }
            else
            {
                base.ReactionOnFire(objectKiller,isParticle);
            }
        }
        //public void ReactionOnFire(Shoot bullet, Collision2D collision)
        //{
        //    if (godMode)
        //    {
        //        return;
        //    }
        //    VisualDamage(collision.contacts.First().point);
        //    if (SetDamage(bullet.damage))
        //    {
        //        Death(bullet.deathName);
        //    }
        //    else
        //    {
        //        base.ReactionOnFire(bullet);
        //    }

        //}
        public override void ReactionOnFire(Shoot bullet)
        {
            if (godMode || Rewinder.instance.isRewindOn)
            {
                return;
            }
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
        /// <summary>
        /// Blast wave reaction
        /// </summary>
        void Blast()
        {
            //get all colliders in blast radius
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastWaveRadius);
            List<FiriebleObject> fireableObjs = colliders.Select(a => a.GetComponent<FiriebleObject>()).ToList();   
 
            //send to all fireable objects reaction
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

            //if this object inanimate
            if (!IsOrganic)
            {
                GameObject exp = ObjectPool.instance.GetEffect("ExplosionMobile(Clone)");
                #if UNITY_EDITOR
                if (exp == null)
                {
                    throw new UnityException("pool return null!!!");
                }
                #endif              
                exp.transform.position = transform.position;               
                exp.SetActive(true);
                Blast();
            }          
            //if organic object
            else
            {
                if (animator != null)
                {
                    //disable colliders to collise
                    foreach (var collider in GetComponents<Collider2D>())
                    {
                        collider.isTrigger = true;
                    }             
     
                    //undo constrains
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                    //disable controllers
                    GetComponent<BaseCharacterController>().enabled = false;
                    CharacterMotor controller = GetComponent<CharacterMotor>();

                    // off jetpack
                    if (controller.jetPack != null)
                    {
                        controller.jetPack.Off();
                    }
                    controller.enabled = false;

                    //stop current animation
                    animator.StopPlayback();

                    //set grounded
                    animator.SetBool("Ground", true);

                    //play death animation
                    animator.Play(deathName,0);
                    return;
                }  
            }
            ObjectPool.instance.ReturnCharacterToPool(gameObject);
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