using UnityEngine;
using System.Collections;

public class BounceKiller : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Level"))
        {
            Destroy(other.transform.gameObject);
        }
        
    }
}
