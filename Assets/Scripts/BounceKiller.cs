using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public class BounceKiller : MonoBehaviour
    {


        // Use this for initialization
        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Level"))
        {
            if (other.gameObject.tag=="Player")
            {
                other.gameObject.GetComponent<DestroybleObject>().Hp = 0;

            }
            Destroy(other.transform.gameObject);
        }
        
    }
    }
}

