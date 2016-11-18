using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
namespace MyPlatformer
{
    public class WalkableAi : CharacterController
    {

        private UnityAction<Transform> currentBehavior;
        private Transform target;
        private Ticker flipTicker;
        public Transform locator;
        public float lookDistance = 10;
        public bool active = true;
        public float horizontal = 1;
        public float vertical = 0;
        public bool canRotateWeapon = false;
        public Transform rightGroundCheck;
        public float updadeTargetDelay = 1f;
        bool targetVisible = false;       
        private void Awake()
        {
            //execute CharacterController Awake
            base.Awake();
            //find player
            target = GameObject.FindGameObjectWithTag("Player").transform;
            //set flipticker
            flipTicker = new Ticker(2,3f);
        }
        // Use this for initialization
        void Start()
        {
            //set patrul behavior
           currentBehavior = Patrul;
            //start locator
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
        
        /// <summary>
        /// if target visible then change move direction 
        /// </summary>
        /// <returns>true if target visible</returns>
        /// 
        //todo: move this method and locator verible to separate class
        bool GetTarget()
        {
                for (int i = 0; i < 360; i+=45)
                {
                    locator.Rotate(Vector3.forward * 45);
                    Ray2D ray = new Ray2D(locator.position, locator.up);
                    RaycastHit2D[] hits;
                    hits = Physics2D.RaycastAll(ray.origin, ray.direction, lookDistance);
                    for (int j = 0; j < hits.Length; j++)
                    {
                        //all out of range of level items are invisible
                       if (hits[j].collider.gameObject.layer == LayerMask.NameToLayer("Level"))
                       {
                           break;
                       }
                        // if player visible
                       if (hits[j].collider.gameObject.CompareTag("Player"))
                       {                    
                           //set direction
                           horizontal = Mathf.RoundToInt(ray.direction.normalized.x);
                           // if character can rotate weapon
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
        /// <summary>
        /// Check if can chracter moving forward or not
        /// </summary>
        /// <returns></returns>
        protected bool CantMoveForward()
        {
            return !m_Character.GroundCheck(rightGroundCheck, m_Character.m_WhatIsGround) || m_Character.GroundCheck(m_Character.weapon.emitter, m_Character.m_WhatIsGround);
        }
        public IEnumerator RestartPatrul(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            active = true;
        }     
        //Patrul ai behavior
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
    /// <summary>
    /// to be considered a repeates
    /// </summary>
    class Ticker
    {
        public int tick;
        public int start;
        public float secondsWait;
        public Ticker(int start, float secondsWait)
        {
            tick = this.start = start;
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

}
