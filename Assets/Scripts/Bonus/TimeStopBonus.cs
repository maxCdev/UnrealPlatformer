using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class TimeStopBonus : Bonus
    {
        public float stopTime = 5;
        GameObject[] enemies;
        public override bool Pickup(GameObject player)
        {
            Action(false);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Renderer>().enabled = false;
            StartCoroutine("OnAllAfterTime");
            return false;
          
        }
        protected virtual void Action(bool flag)
        {
            OnOffAi(flag);
        }
        void OnOffAi(bool active)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemie");
            if (enemies != null)
            {
                foreach (var enemie in enemies)
                {
                    WalkableAi ai = enemie.GetComponent<WalkableAi>();
                    if (ai != null)
                    {
                        ai.active = active;
                    }
                    else
                    {
                        FlyebleAi aiFly = enemie.GetComponent<FlyebleAi>();
                        if (aiFly != null)
                        {
                            aiFly.active = active;
                        }
                    }

                }
            }
        }
        IEnumerator OnAllAfterTime()
        {
            yield return new WaitForSeconds(stopTime);        
            Action(true);
            Destroy(gameObject);
            
        }
    }
}