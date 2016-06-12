using UnityEngine;
using System.Collections;
using System.Linq;
namespace MyPlatformer
{
    public interface IShoot
    {
        void Move();
        float GetDistance(Transform other);
        bool ImOnPath(Collider other);
        Vector3 Course { set; get; }
        float Speed { set; get; }
    }
    public class Shoot : KillableObject, IShoot
    {

        Transform thisTransform;
        [SerializeField]
        float speed;
        [SerializeField]
        Vector3 course;
        Renderer renderer;
        public Vector3 Course
        {
            get
            {
                return course;
            }
            set
            {
                course = value;
            }
        }
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        
        void Start()
        {
            Debug.Log("Start shoot from x: " + Course.x + " y: " + Course.y);
            renderer = GetComponent<Renderer>();
            thisTransform = GetComponent<Transform>();
        }
        void Update()
        {
                Move();            
        }
        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        public void Move()
        {
            transform.position = Vector3.Lerp(thisTransform.position, thisTransform.position + Course, Time.deltaTime * Speed);
        }

        public float GetDistance(Transform other)
        {
            return Vector3.Distance(thisTransform.position, other.position);
        }

        public bool ImOnPath(Collider other)
        {
            return Physics2D.RaycastAll(thisTransform.position, other.transform.position).Any(a => a.collider == other);
        }

      void OnTriggerEnter2D(Collider2D other)
      {
          if (other.gameObject.layer == LayerMask.NameToLayer("Reboundiable"))
          {
              course = -course;
              return;
          }
          FiriebleObject script = other.GetComponent<FiriebleObject>();
          if (script!=null)
          {
              script.ReactionOnFire(this);
              Destroy(gameObject);
          }
      }
    }
}
