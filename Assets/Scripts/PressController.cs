using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class PressController : MonoBehaviour
    {
        public Transform downPad;
        public Transform endPoint;
        public Transform edges;
        public float speed;
        public float interval;
        public GameObject killableObj;
        Vector3 startPosition;
        Vector3 endPosition;
        float lastTime=0;
        
        // Use this for initialization
        void Start()
        {
            startPosition = downPad.position;
            endPosition = endPoint.position;
        }
        IEnumerator Wait()
        {
           yield return new WaitForSeconds(interval);
        }
        // Update is called once per frame
        void Update()
        {
            if (downPad.position != endPosition)
            {
                downPad.position = Vector3.MoveTowards(downPad.position, endPosition, speed * Time.deltaTime);
            }
            else
            {
                if (lastTime+interval<Time.time)
                {                   
                    lastTime = Time.time;
                    Vector3 temp = endPosition;
                    endPosition = startPosition;
                    startPosition = temp;
                    //if course down then on killer effect
                    killableObj.SetActive(endPosition == endPoint.position);
                }
                
            }
            
        }
    }
}
