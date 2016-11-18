using UnityEngine;
using System.Collections.Generic;
namespace MyPlatformer
{
    public class MoveOnPathObj : MonoBehaviour
    {
        public List<Transform> points; // way points
        public float speed = 1;
        public int course = 1; //direction
        protected int wayIndex = 0; //curent way statement
        protected Transform myTransform;

        // Use this for initialization
        void Start()
        {
            myTransform = transform;
        }
        private void ChangeCourse()
        {
            course = -course;
        }
        public void Move()
        {
            switch (course)
            {
                case 1:
                    {
                        if (myTransform.position == points[wayIndex].position && wayIndex != points.Count - 1)
                        {
                            ++wayIndex;
                        }
                        else if (myTransform.position == points[wayIndex].position && wayIndex == points.Count - 1)
                        {
                            --wayIndex;
                            ChangeCourse();
                        }
                    } break;
                case -1:
                    {
                        if (myTransform.position == points[wayIndex].position && wayIndex != 0)
                        {
                            --wayIndex;
                        }
                        else if (myTransform.position == points[wayIndex].position && wayIndex == 0)
                        {
                            ++wayIndex;
                            ChangeCourse();
                        }
                    } break;
            }

            myTransform.position = Vector2.MoveTowards(myTransform.position, points[wayIndex].position, speed * Time.deltaTime); 
        }
        void Update()
        {
            Move();
        }
    }
}
