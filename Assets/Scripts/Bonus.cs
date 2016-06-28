using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class Bonus : MonoBehaviour
    {

        public virtual bool Pickup(GameObject player)
        { return true; }
        public void OnTriggerEnter2D(Collider2D other)
        {

            if (other.CompareTag("Player"))
            {
                if (Pickup(other.gameObject))
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }
}
