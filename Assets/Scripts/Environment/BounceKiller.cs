using UnityEngine;
namespace MyPlatformer
{
    /// <summary>
    /// return to pool all objects when they hits on bounce
    /// </summary>
    public class BounceKiller : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Level"))
            {
                if (other.gameObject.tag=="Player")
                {
                    other.gameObject.GetComponent<DestroybleObject>().Hp = 0;

                }
                ObjectPool.instance.ReturnCharacterToPool(other.gameObject);
            }
        
        }
    }
}

