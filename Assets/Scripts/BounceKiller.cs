using UnityEngine;
using System.Collections;

public class BounceKiller : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.transform.gameObject);
    }
}
