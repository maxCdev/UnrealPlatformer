using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.ImageEffects;
namespace MyPlatformer
{
#region MomentItems
    //todo: try serialize all fields to json
    public class ParticleItem : MovingMomentItem
{
    public ParticleSystem particle;
    public List<ParticleSystem> particles;
    public List<float> particleTimeChilds;
    public float particleTime;
    public ParticleItem(GameObject gameObject)
        : base(gameObject)
    {
       
        particle = gameObject.GetComponent<ParticleSystem>();
        if (particle != null)
        {
            particleTime = particle.time;
        }
    }
    public override void SetFields()
    {

         base.SetFields();
         
        #region Particles
        if (particle!=null)
        {
            particle.Clear();
            particle.Simulate(particleTime, true, true);
            particle.Play(true);
        }
        else
        {
            if (particles != null && particles.Count > 0)
            {

                for (int i = 0; i < particles.Count; i++)
                {
                    if (particles[i]==null)
                    {
                        continue;
                    }
                    particles[i].Clear();
                    particles[i].Simulate(particleTimeChilds[i], true, true);
                    particles[i].Play(true);
                }

            }
        }
        #endregion
 	
}
}
public class FireableItem : MovingMomentItem
{
    public Vector3 velocity;
    public Rigidbody2D rBody;
    public FireableItem(GameObject gameObject)
        : base(gameObject)
    {
        rBody = gameObject.GetComponent<Rigidbody2D>();
        if (rBody != null)
        {
            velocity = rBody.velocity;
        }
    }
    public override void SetFields()
    {
        base.SetFields();

        if (rBody != null)
        {
            rBody.velocity = velocity;
        }

    }
}
public class DestroybleItem : FireableItem
{
    public Transform parent;
    public bool destroyObj;
    public float hp;
    public DestroybleObject destroybleObject;
    public DestroybleItem(GameObject gameObject):base(gameObject)
    {
       
        destroybleObject = gameObject.GetComponent<DestroybleObject>();
        parent = gameObject.transform.parent;

        if (destroybleObject != null)
        {
            destroyObj = destroybleObject.enabled;
            hp = destroybleObject.Hp;
        }
    }
    public override void SetFields()
    {
        base.SetFields();

        transform.parent = parent;

        if (destroybleObject != null)
        {
            destroybleObject.enabled = destroyObj;
            destroybleObject.Hp = hp;
        }
    }
}
public class PlayerItem : CharacteItem
{
    public PlayerItem(GameObject gameObject)
        : base(gameObject)
    {

    }
}
public class ShootItem : MovingMomentItem
{
    Shoot shoot;
    Vector2 course;
    Transform parent;
    public ShootItem(GameObject gameObject)
        : base(gameObject)
    {
        shoot = gameObject.GetComponent<Shoot>();
        course = shoot.Course;
        parent = gameObject.transform.parent;
    }
    public override void SetFields()
    {
        base.SetFields();
        shoot.Course = course;
        transform.parent = parent;
    }
}
public class EnemieItem : CharacteItem
{
    public WalkableAi walkableAi;
    public float horizontal;

    public EnemieItem(GameObject gameObject)
        : base(gameObject)
    {
        walkableAi = gameObject.GetComponent<WalkableAi>();
        if (walkableAi != null)
        {
            horizontal = walkableAi.horizontal;
        }
    }
    public override void SetFields()
    {
        base.SetFields();

        if (walkableAi != null)
        {
            walkableAi.horizontal = horizontal;
        }
    }

}
public class CharacteItem : DestroybleItem
{
   
    public Animator animator;
    public float animTime;
  
    public int state;
    
    public CharacteItem(GameObject gameObject)
        : base(gameObject)
    {
        animator = gameObject.GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            animTime = animationState.normalizedTime;
            state = animationState.fullPathHash;
        }

        //collider = gameObject.GetComponent<Collider2D>();
        //if (collider!=null)
        //{
        //    colliderValue = collider.isTrigger;
        //}


        //else
        //{
        //    particles = new List<ParticleSystem>(gameObject.GetComponentsInChildren<ParticleSystem>());
        //    if (particles.Count>0)
        //    {
        //        particleTimeChilds = new List<float>(particles.Select(a => a.time));
        //    }

        //   // particle = gameObject.GetComponentInChildren<ParticleSystem>();
        //}
    }
    public override void SetFields()
    {
        base.SetFields();

       
        if (animator != null)
        {
            animator.Play(state, 0, animTime);
        }
         //if (collider != null)
        //{
        //    foreach (var coll in transform.GetComponents<Collider2D>())
        //    {
        //        coll.isTrigger = colliderValue;
        //    }
        //    colliderValue = collider.enabled;
        //}
 	     
    }
}

public class MovingMomentItem 
{
    public Vector3 scale;
    public Vector3 position;
    public Quaternion rotation;
    public Transform transform;
    public GameObject gameObject;
    //public Collider2D collider;
    //public bool colliderValue;
    public bool active;
    public MovingMomentItem(GameObject gameObject)
    {
        transform = gameObject.transform;
        scale = transform.localScale;
        position = transform.position;
        rotation = transform.rotation;  
        active = gameObject.activeInHierarchy;
    }
    public virtual void SetFields()
    {     
        if (transform!=null)
        {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
            transform.gameObject.SetActive(active);
        }
    }
    public static MovingMomentItem GetItem(GameObject gameObject)
    {

        if (gameObject.GetComponent<PlatformerCharacter2D>()!=null)
        {
            return new CharacteItem(gameObject);
        }
        else if (gameObject.GetComponent<Shoot>() != null)
        {
            return new ShootItem(gameObject);
        }
        else if (gameObject.GetComponent<WalkableAi>() != null)
        {
            return new EnemieItem(gameObject);
        }
        else if (gameObject.GetComponent<DestroybleObject>()!=null)
        {
            return new DestroybleItem(gameObject);
        }
        else if (gameObject.GetComponent<FiriebleObject>() != null)
        {
            return new FireableItem(gameObject);//add this
        }       
        else if (gameObject.GetComponent<ParticleSystem>() != null)
        {
            return new ParticleItem(gameObject);
        }
        else if (gameObject.GetComponent<Transform>() != null)
        {
            return new MovingMomentItem(gameObject);
        }
        return null;
    }
  
}
    #endregion
#region notRealized
public enum RewindActionType
{
    Add, Remove
}
/// <summary>
/// not realized
/// </summary>
public class RewindAction
{
    public GameObject obj;
    public RewindActionType type;

    public void Execute()
    {
        if (type == RewindActionType.Add)
        {
            GameObject.Destroy(obj);
        }
        else if (type == RewindActionType.Remove)
        {
            GameObject.Instantiate<GameObject>(obj);
        }
    }
}
#endregion
/// <summary>
/// A moment of time
/// </summary>
public class Moment
{
    public List<MovingMomentItem> items = new List<MovingMomentItem>();
    public List<RewindAction> actions = new List<RewindAction>();

    public void Play()
    {
        foreach (var item in items)
        {
            item.SetFields();
        }
        foreach (var action in actions)
        {
            action.Execute();
        }
    }

}
    /// <summary>
    /// Rewind time
    /// </summary>
public class Rewinder : MonoBehaviour 
{
    List<GameObject> gameObjects = new List<GameObject>(); //all object they can be rewined
    public string []tags=null; //object tags whos should be rewined
    public Stack<Moment> moments = new Stack<Moment>();//All saved moments
    public bool isRewindOn = false;
    public static Rewinder instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;        
        }
        else
        {
            Destroy(gameObject);
        }
    }

	void Start ()
    {
        #region HardCode 
        tags = new string[] { "Player", "Enemie" };//hard code - fix them!!!
        Transform items = GameObject.Find("Items").transform;
        gameObjects.AddRange(items.GetChilds().Select(a=>a.gameObject));
        Transform pool = GameObject.Find("ObjectPool").transform;
        gameObjects.AddRange(pool.GetChilds().Select(a => a.gameObject));
        #endregion

        if (tags!=null)
        {
            foreach (string tag in tags)
            {
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag(tag));
            }
        }
        StartCoroutine(RewindUpdate());
        StartCoroutine(RewindExecute());

	}
    void FixedUpdate()
    {
       // isRewindOn = Input.GetKey(KeyCode.R);
    }
    public void ButtonDown()
    {
        isRewindOn = true;
    }
    public void ButtonUp()
    {
        isRewindOn = false;
    }
    /// <summary>
    /// if !isRewind then save moments
    /// </summary>
    /// <returns></returns>
    IEnumerator RewindUpdate()
    {
        if (tags != null)
        {
            //Debug.Log(" moments - " + moments.Count + "; gameObj - " + gameObjects.Count + " time: " + Time.time);
            if (!isRewindOn)
            {
                Time.timeScale = 1f;
                List<MovingMomentItem> momentItems = new List<MovingMomentItem>();
                Moment currentMoment = new Moment();
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i] != null)
                    {
                        momentItems.Add(MovingMomentItem.GetItem(gameObjects[i]));
                    }
                }
                currentMoment.items = momentItems;
                moments.Push(currentMoment);

                Camera.main.GetComponent<MotionBlur>().enabled = false;
            }
           
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(RewindUpdate());
    }
    /// <summary>
    /// Execute rewind
    /// </summary>
    /// <returns></returns>
    IEnumerator RewindExecute()
    {
        if (isRewindOn)
        {
            Rewind();
        }

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(RewindExecute());
    }
    private void Rewind()
    {
        if (moments.Count>0)
        {
            Time.timeScale = 0.1f;
            Camera.main.GetComponent<MotionBlur>().enabled = true;
            moments.Pop().Play();

        }
    }

}
}