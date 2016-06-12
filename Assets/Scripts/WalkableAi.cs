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
        float horizontal = 1;
        float vertical = 0;
        bool fire = false;
        
        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            renderer = GetComponent<Renderer>();
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
        void GetTarget()
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
                              fire = true;
                           //Debug.Log(string.Format("plYER: transform.right = {0}, locator.up = {1}", transform.right, locator.up));
                           //Debug.Log(string.Format("plYER: direction{0}, locator.up = {1}", (transform.position - hits[j].transform.position).normalized, locator.up));
                           break;

                           
                       }
                    }
                   
                   
                    Debug.DrawLine(ray.origin, new Vector2(locator.position.x, locator.position.y) + ray.direction, Color.red);
                }
        }
        void Patrul(Transform player)
        {
            
            m_Character.Move(horizontal, vertical, false, false, fire);
            if (player!=null)
            {
                GetTarget();
            }
            
          
        }

    }

}
