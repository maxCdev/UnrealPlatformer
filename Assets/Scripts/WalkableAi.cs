using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
namespace MyPlatformer
{  

    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class WalkableAi : MonoBehaviour
    {

        private UnityAction<Transform> currentBehavior;
        private PlatformerCharacter2D m_Character;
        Transform player;
        public Transform locator;
        Renderer renderer;
        public float lookDistance = 10;
        public bool active = true;
        public float horizontal = 1;
        float vertical = 0;
        public bool canRotateWeapon = false;
        public Transform rightGroundCheck;
        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            renderer = GetComponentInChildren<Renderer>();
        }
        // Use this for initialization
        void Start()
        {
           currentBehavior = Patrul;
           
        }
        //Ai seeing a player
        void SeeTargetBehavior(Transform player)
        {

        }
        // Update is called once per frame
        void Update()
        {
            if (active)
            {
                currentBehavior(player);
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
                          
                           horizontal =Mathf.RoundToInt(ray.direction.normalized.x);
                           if (canRotateWeapon)
                           {
                               vertical =-Mathf.RoundToInt(ray.direction.normalized.y);  
                           }                   
                              return true;                          
                       }
                    }
                    Debug.DrawLine(ray.origin, new Vector2(locator.position.x, locator.position.y) + ray.direction, Color.red); 
                }
                return false;
        }
        void Patrul(Transform player)
        {
            
            
            if (player!=null)
            {
               if (GetTarget())
               {
                   m_Character.TryFlip(horizontal);
                   m_Character.Move(0, vertical, false, false, true);
               }
               else
               {
                   if (!m_Character.GroundCheck(rightGroundCheck)||m_Character.GroundCheck(m_Character.weapon.emitter))
                   {
                       horizontal *= -1;
                   }
                   m_Character.Move(horizontal, vertical, false, false, false);
               }
            }
            
          
        }

    }

}
