using UnityEngine;
using System.Collections.Generic;

public class SawController : MonoBehaviour {

    public List<Transform> points;
    public float speed=3;
    public int course = 1;
    private int wayIndex = 0;
    private Transform myTransform;
	// Update is called once per frame
    void Start()
    {
        myTransform = transform;
    }
	void Update () {    
        switch(course)
        {
            case 1:{
             if (myTransform.position==points[wayIndex].position&&wayIndex!=points.Count-1)
                {
                    ++wayIndex;
                }
             else if (myTransform.position==points[wayIndex].position&&wayIndex==points.Count-1)
             {
                 --wayIndex;
                 course=-1;
             }
            }break;
            case -1:{
               if (myTransform.position==points[wayIndex].position&&wayIndex!=0)
                {
                    --wayIndex;
                }
             else if (myTransform.position==points[wayIndex].position&&wayIndex==0)
             {
                 ++wayIndex;
                 course=1;
             }
            }break;
        }
        myTransform.Rotate(Vector3.forward*10);
       myTransform.position = Vector2.MoveTowards(myTransform.position, points[wayIndex].position, speed * Time.deltaTime);
	}
}
