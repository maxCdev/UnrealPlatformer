using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class Bonus : MonoBehaviour
    {
        //if true then destroy
        public virtual bool Pickup(GameObject player)
        { 
            return true; 
        }
        public void OnTriggerEnter2D(Collider2D other)
        {

            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlatformerCharacter2D>().PickupBonusSound();
              
                if (Pickup(other.gameObject))
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }
}
