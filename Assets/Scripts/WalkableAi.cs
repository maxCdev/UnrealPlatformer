using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityStandardAssets._2D;
using System.Linq;
namespace MyPlatformer
{  

    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class WalkableAi : MonoBehaviour
    {
        [SerializeField]
        private UnityAction<Transform> currentBehavior;
        private PlatformerCharacter2D m_Character;
        Transform player;
        public Transform locator;
        Renderer renderer;
        [SerializeField]
        bool active {
            get
            {
                return renderer.isVisible;
            }
        }
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
            //if (active)
            //{
                currentBehavior(player);
            //}
               
        }
        Vector2? GetTarget()
        {
                for (int i = 0; i < 360; i+=45)
                {
                    locator.Rotate(Vector3.forward * 45);

                    Ray2D ray = new Ray2D(locator.position, locator.up);
                    RaycastHit2D[] hits;
                    hits = Physics2D.RaycastAll(ray.origin, ray.direction,40f);
                    for (int j = 0; j < hits.Length; j++)
                    {
                       if (hits[j].collider.gameObject.layer == LayerMask.NameToLayer("Level"))
                       {
                           break;

                       }
                       if (hits[j].collider.gameObject.CompareTag("Player"))
                       {                        
                              if (player.position.x>transform.position.x)
                              {
                                  horizontal = 1;
                              }
                              else if (player.position.x<transform.position.x)
                              {
                                  horizontal = -1;
                              }
                              else
                              {
                                  horizontal = 0;
                              }
                           if (canRotateWeapon)
                           {
                               if (player.position.y > transform.position.y)
                               {
                                   vertical = 1;
                               }
                               else if (player.position.y < transform.position.y)
                               {
                                   vertical = -1;
                               }
                               else
                               {
                                   vertical = 0;
                               }     
                           }                   
                              return new Vector2(horizontal, vertical);                          
                       }
                    }
                    Debug.DrawLine(ray.origin, new Vector2(locator.position.x, locator.position.y) + ray.direction, Color.red); 
                }
                return null;
        }
        void Patrul(Transform player)
        {
            
            
            if (player!=null)
            {
               if (GetTarget()!=null)
               {
                   m_Character.TryFlip(horizontal);
                   m_Character.Move(0, vertical, false, false, true);
               }
               else
               {
                   if (m_Character.GroundCheck(rightGroundCheck)&&!m_Character.GroundCheck(m_Character.weapon.emitter))
                   {
                       Debug.Log("Im Go!" + horizontal);
                   }
                   else
                   {
                       Debug.Log("Flip[]" + horizontal);
                       horizontal *= -1;
                   }

                   m_Character.Move(horizontal, vertical, false, false, false);
               }
            }
            
          
        }

    }

}
