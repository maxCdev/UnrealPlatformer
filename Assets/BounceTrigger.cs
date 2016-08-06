using UnityEngine;
using System.Collections;

public class BounceTrigger : MonoBehaviour {

    public GameObject left;
    public Transform leftNew;
    public GameObject top;
    public Transform topNew;
    public GameObject down;
    public Transform downNew;
   // public GameObject deadFall;
 public void OnTriggerEnter2D(Collider2D other)
    {
     if (other.tag=="Player")
     {
         if (left!=null)
         {
             left.transform.position = leftNew.position;
         }
        if (down!=null)
        {
            down.transform.position = downNew.position;
          //  deadFall.transform.position = downPos - Vector2.down;
        }
         if (top!=null)
         {
             top.transform.position = topNew.position;
         }
         Destroy(gameObject);
     }
    }
}
