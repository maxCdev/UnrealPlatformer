using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
namespace MyPlatformer
{
    [RequireComponent(typeof(TargetLocator))]
    public class WalkableAi : BaseCharacterController
    {
        private UnityAction<Transform> currentBehavior;
        private Transform target;
        private Ticker flipTicker;
        public bool active = true;
        public bool canRotateWeapon = false;
        public Transform rightGroundCheck;
        private TargetLocator locator;  
        private void Awake()
        {
            //execute CharacterController Awake
            base.Awake();
            //find player
            target = GameObject.FindGameObjectWithTag("Player").transform;
            //init flipticker
            flipTicker = new Ticker(2,3f);
        }
        // Use this for initialization
        void Start()
        {
            //set patrul behavior
           currentBehavior = Patrul;
            //init locator
           locator = GetComponent<TargetLocator>();           
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
        void AttackTarget()
        {
            m_Character.TryFlip(locator.targetDirection.x);
            m_Character.Move(0, canRotateWeapon ? locator.targetDirection.y : 0f, false, false, true);
        }
        void Move()
        {
            m_Character.Move(locator.targetDirection.x, locator.targetDirection.y, false, false, false);
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
               if (locator.targetVisible)
               {
                   AttackTarget();
               }
               else
               {
                   if (CantMoveForward())
                   {
                       flipTicker.Tick();
                       locator.targetDirection.x *= -1;
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
