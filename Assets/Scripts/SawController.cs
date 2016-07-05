using UnityEngine;
using System.Collections.Generic;
namespace MyPlatformer
{
    [RequireComponent(typeof(KillableObject))]
public class SawController : MoveOnPathObj {


       public float rotateSpeed = 10;
    void Start()
    {
        myTransform = transform;
    }
	void Update () {    
        myTransform.Rotate(Vector3.forward*rotateSpeed);
        Move();
	}
}
}
