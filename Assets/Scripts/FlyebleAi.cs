using UnityEngine;
using System.Collections;
using Pathfinding;
namespace MyPlatformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    public class FlyebleAi : MonoBehaviour
    {
        private Transform myTransform;
        private Rigidbody2D rBody;
        private Seeker seeker;
        public Path path;
        public float speed;
        public ForceMode2D forceMode;
        public Transform target;
        bool pathIsEnded = false;
        public float updateDelay = 2f;
        public float nextWayPontDistance = 1f;
        public int currentWayPoint;
        public float maxTargetDistance = 25f;
        public bool active = true;
        void Start()
        {            
            seeker = GetComponent<Seeker>();
            rBody = GetComponent<Rigidbody2D>();
            myTransform = transform;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (target != null)
            {
                //seeker.StartPath(myTransform.position, target.position, OnEndPath);
                StartCoroutine(UpdatePath());
            }

        }
        IEnumerator UpdatePath()
        {
            if (target != null)
            {

                seeker.StartPath(myTransform.position, target.position, OnEndPath);
                yield return new WaitForSeconds(1f / updateDelay);
                StartCoroutine(UpdatePath());
            }

        }
        public void OnEndPath(Path p)
        {
            path = p;
            currentWayPoint = 0;
        }
        void Flip(float side)
        {
            Vector3 theScale = myTransform.localScale;
            theScale.x = side;
            myTransform.localScale = theScale;
        }
        // Update is called once per frame
        void Update()
        {
            if (target == null || path == null || !active|| Vector3.Distance(myTransform.position, target.position) > maxTargetDistance)
            {
                return;
            }
            if (currentWayPoint >= path.vectorPath.Count)
            {
                if (pathIsEnded)
                {
                    return;
                }
                pathIsEnded = true;
                return;
            }
            pathIsEnded = false;
            Vector3 direction = (path.vectorPath[currentWayPoint] - myTransform.position).normalized * speed * Time.deltaTime;
            int pathCount = path.vectorPath.Count - 1;
            if (myTransform.position.x < path.vectorPath[pathCount].x)
            {
                Flip(1);
            }
            else if (myTransform.position.x > path.vectorPath[pathCount].x)
            {
                Flip(-1);
            }
            myTransform.position = Vector3.MoveTowards(myTransform.position, path.vectorPath[currentWayPoint], speed * Time.deltaTime);
            float distance = Vector3.Distance(myTransform.position, path.vectorPath[currentWayPoint]);
            if (distance < nextWayPontDistance)
            {
                currentWayPoint++;
            }
        }
    }
}
