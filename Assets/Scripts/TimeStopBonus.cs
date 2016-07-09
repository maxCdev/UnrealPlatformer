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

                enemies = GameObject.FindGameObjectsWithTag("Enemie");
            if (enemies != null)
            {
                OnOffAi(false);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Renderer>().enabled = false;
                StartCoroutine("OnAllAfterTime");
                return false;
            }
            return true;
          
        }
        void OnOffAi(bool active)
        {
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
            yield return new WaitForSeconds(5);        
            enemies = GameObject.FindGameObjectsWithTag("Enemie");
            OnOffAi(true);
            Destroy(gameObject);
            
        }
    }
}