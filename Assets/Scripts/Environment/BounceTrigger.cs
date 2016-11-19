using UnityEngine;
using System.Collections;
/// <summary>
/// It changes the bounce of the game when player hit this trigger
/// </summary>
public class BounceTrigger : MonoBehaviour {

    public GameObject left;
    public GameObject down;
    public GameObject top;
    public Transform leftNew;
    public Transform topNew;
    public Transform downNew;
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
             }
             if (top!=null)
             {
                 top.transform.position = topNew.position;
             }
             Destroy(gameObject);
         }
    }
}
