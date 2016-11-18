using UnityEngine;
using System.Collections.Generic;
namespace MyPlatformer
{
    [RequireComponent(typeof(KillingObject))]
    public class SawController : MoveOnPathObj 
    {
        public float rotateSpeed = 10;//the speed of saw rotation
        void Start()
        {
            myTransform = transform;
        }
	    void Update () {    

            //rotate the saw
            myTransform.Rotate(Vector3.forward * rotateSpeed);

            //if first point exist
            if (points[0]!=null)
            {
                Move();
            }
        
	    }
    }
}
