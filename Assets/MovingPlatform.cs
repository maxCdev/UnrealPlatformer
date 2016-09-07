using UnityEngine;
using System.Collections;

public class MovingPlatform:MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.parent = transform;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        other.transform.parent = null;
    }
}
