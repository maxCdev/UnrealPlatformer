using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class Bonus : MonoBehaviour
    {
        //if true then destroy
        /// <summary>
        /// Action after pickup bonus
        /// </summary>
        /// <param name="player"></param>
        /// <returns>true if destroy this object after hit with player</returns>
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
