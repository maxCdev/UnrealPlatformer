using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
namespace MyPlatformer
{
    class Ticker
    {
       public int tick;
       public int start;
       public float secondsWait;
       public Ticker(int start,float secondsWait)
       {
           tick = this.start=start;
           this.secondsWait = secondsWait;
       }
       public bool IsStop { get { return tick <= 0; } }
       public bool Tick()
        {          
            if (tick >= 0)
            {
                --tick;
                
                return true;
            }
            return false;
        }
       public void Restart()
       {
           tick = start;
       }
    }
    public class WalkableAi : Character2DController
    {

        private UnityAction<Transform> currentBehavior;
        Transform target;
        public Transform locator;
        public float lookDistance = 10;
        public bool active = true;
        public float horizontal = 1;
        float vertical = 0;
        public bool canRotateWeapon = false;
        public Transform rightGroundCheck;
        public float updadeTargetDelay = 1f;
        bool targetVisible = false;
        Ticker flipTicker;
        private void Awake()
        {
            base.Awake();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            flipTicker = new Ticker(2,3f);
        }
        // Use this for initialization
        void Start()
        {
           currentBehavior = Patrul;
           StartCoroutine(CheckTarget());
           
        }
        // Update is called once per frame
        void Update()
        {
            if (active)
            {
                currentBehavior(target);
            }
            else
            {
                m_Character.Move(0, 0, false, false, false);
            }
               
        }
        bool GetTarget()
        {
                for (int i = 0; i < 360; i+=45)
                {
                    locator.Rotate(Vector3.forward * 45);

                    Ray2D ray = new Ray2D(locator.position, locator.up);
                    RaycastHit2D[] hits;
                    hits = Physics2D.RaycastAll(ray.origin, ray.direction,lookDistance);
                    for (int j = 0; j < hits.Length; j++)
                    {
                       if (hits[j].collider.gameObject.layer == LayerMask.NameToLayer("Level"))
                       {
                           break;

                       }
                       if (hits[j].collider.gameObject.CompareTag("Player"))
                       {
                          
                           horizontal = Mathf.RoundToInt(ray.direction.normalized.x);
                           if (canRotateWeapon)
                           {
                               vertical =-Mathf.RoundToInt(ray.direction.normalized.y);  
                           }                   
                              return true;                          
                       }
                    }
                }
                return false;
        }
        IEnumerator CheckTarget()
        {
             yield return new WaitForSeconds(updadeTargetDelay);
             targetVisible = GetTarget();
             StartCoroutine(CheckTarget());
        }
        void AttackTarget()
        {
            m_Character.TryFlip(horizontal);
            m_Character.Move(0, vertical, false, false, true);
        }
        void Move()
        {            
            m_Character.Move(horizontal, vertical, false, false, false);
        }
        bool CantMoveForward()
        {
            return !m_Character.GroundCheck(rightGroundCheck, m_Character.m_WhatIsGround) || m_Character.GroundCheck(m_Character.weapon.emitter, m_Character.m_WhatIsGround);
        }
        public IEnumerator RestartPatrul(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            active = true;
        }     
        void Patrul(Transform player)
        {
            
            
            if (player!=null)
            {
               if (targetVisible)
               {
                   AttackTarget();
               }
               else
               {
                   if (CantMoveForward())
                   {
                       flipTicker.Tick();
                       Debug.Log(flipTicker.tick);
                       horizontal *= -1;
                   }
                   else
                   {
                       flipTicker.Restart();
                   }
                   if (flipTicker.IsStop)
                   {
                       active = false;
                       StartCoroutine(RestartPatrul(0.5f));
                       AttackTarget();
                       flipTicker.Restart();

                   }
                   
                       Move();
                   
                   
               }
            }
            
          
        }

    }

}
