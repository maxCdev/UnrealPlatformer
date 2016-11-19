using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    /// <summary>
    /// help ai detect player
    /// </summary>
    public class TargetLocator : MonoBehaviour
    {
        public Vector2 targetDirection;
        public float updateTargetDelay;
        public Transform locator;
        public float lookDistance;
        public bool targetVisible = false;
        void Start()
        {
            StartCoroutine(CheckTarget());
        }
        IEnumerator CheckTarget()
        {
            yield return new WaitForSeconds(updateTargetDelay);
            targetVisible = GetTarget();
            StartCoroutine(CheckTarget());

        }
        /// <summary>
        /// if target visible then change move direction 
        /// </summary>
        /// <returns>true if target visible</returns>
        protected bool GetTarget()
        {
            for (int i = 0; i < 360; i += 45)
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
                        targetDirection = ray.direction.normalized;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
