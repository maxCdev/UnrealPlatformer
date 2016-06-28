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
                Debug.Log(enemies.Length);
                foreach (var enemie in enemies)
                {
                    WalkableAi ai = enemie.GetComponent<WalkableAi>();
                    if (ai!=null)
                    {
                        ai.active = false;
                    }
                    
                    
                }
                //gameObject.SetActive(false);
                StartCoroutine("OnAllAfterTime");
                return false;
            }
            return true;
          
        }
        IEnumerator OnAllAfterTime()
        {
            yield return new WaitForSeconds(5);        
            enemies = GameObject.FindGameObjectsWithTag("Enemie");
            if (enemies != null)
            {
                foreach (var enemie in enemies)
                {
                    WalkableAi ai = enemie.GetComponent<WalkableAi>();
                    if (ai != null)
                    {
                        ai.active = true;
                    }

                }
            }
            //Destroy(gameObject);
        }
    }
}