using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.ImageEffects;
namespace MyPlatformer
{
public class MomentItem 
{
    public Vector3 scale;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Rigidbody2D rBody;
    public Transform transform;
    public ParticleSystem particle;
    public Animator animator;
    public WalkableAi walkableAi;
    public float animTime;
    public int state;
    public float particleTime;
    public float horizontal;
    public MomentItem() { }
    public MomentItem(GameObject gameObject)
    {
        transform = gameObject.transform;
        scale = transform.localScale;
        position = transform.position;
        rotation = transform.rotation;
        rBody = gameObject.GetComponent<Rigidbody2D>();
        particle = gameObject.GetComponent<ParticleSystem>();
        animator = gameObject.GetComponent<Animator>();
        walkableAi = gameObject.GetComponent<WalkableAi>();
        if (rBody != null)
        {
            velocity = rBody.velocity;
        }
        
        if (particle != null)
        {
            particleTime = particle.time;
        }

        if (animator != null)
        {
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            animTime = animationState.normalizedTime;
            state = animationState.fullPathHash;       
        }
        if (walkableAi!=null)
        {
            horizontal = walkableAi.horizontal;
        }
    }
    public void SetFields()
    {
        if (transform!=null)
        {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }

        if (rBody!=null)
        {
            rBody.velocity = velocity;
        }
        if (particle!=null)
        {
            particle.Clear();
            particle.Simulate(particleTime, true, true);
            particle.Play();
        }
        if (animator != null)
        {
            animator.Play(state, 0, animTime);
        }
        if (walkableAi != null)
        {
            walkableAi.horizontal = horizontal;
        }
          
    }
  
}
public enum RewindActionType
{
    Add, Remove
}
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
public class Moment
{
    public List<MomentItem> items = new List<MomentItem>();
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
public class Rewinder : MonoBehaviour 
{
    List<GameObject> gameObjects = new List<GameObject>();
    public string []tags=null;
    public Stack<Moment> moments = new Stack<Moment>();
    bool isRewindOn = false;
    public Moment current = null;
	void Start ()
    {
        #region HardCode
        tags = new string[] { "Player", "Enemie" };//hard code
        Transform items = GameObject.Find("Items").transform;
        for (int i = 0; i < items.childCount; i++)
		{
            gameObjects.Add(items.GetChild(i).gameObject);
        }
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
        isRewindOn = Input.GetKey(KeyCode.R);
    }
    public void ButtonDown()
    {
        isRewindOn = true;
    }
    public void ButtonUp()
    {
        isRewindOn = false;
    }
    IEnumerator RewindUpdate()
    {
        if (tags != null)
        {
            Debug.Log(" moments - " + moments.Count + "; gameObj - " + gameObjects.Count + " time: " + Time.time);
            if (!isRewindOn)
            {

                List<MomentItem> momentItems = new List<MomentItem>();
                Moment currentMoment = new Moment();
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i] != null)
                    {
                        momentItems.Add(new MomentItem(gameObjects[i]));
                    }

                }
                currentMoment.items = momentItems;
                moments.Push(currentMoment);
                //somthing for optimise memory
                //if (moments.Count > 1000)
                //{
                //    List<Moment> optimizedMoments = moments.ToList();
                //    moments = new Stack<Moment>(optimizedMoments.SkipWhile(a => optimizedMoments.IndexOf(a) < 500));
                //    Debug.Log("Repack. rewind moments count : " + optimizedMoments.SkipWhile(a => optimizedMoments.IndexOf(a) < optimizedMoments.Count / 10).Count());
                //}

                Camera.main.GetComponent<MotionBlur>().enabled = false;
            }
           
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(RewindUpdate());
    }
	// Update is called once per frame
    //void Update()
    //{
        
    //    if (isRewindOn)
    //    {
    //        Rewind();
    //    }
    //}
    IEnumerator RewindExecute()
    {
        if (isRewindOn)
        {
            Rewind();
        }

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(RewindExecute());
    }
    private void Rewind()
    {
        if (moments.Count>0)
        {
            Debug.Log("Moments Count " + moments.Count);
            Camera.main.GetComponent<MotionBlur>().enabled = true;

            moments.Pop().Play();

        }
    }

}
}