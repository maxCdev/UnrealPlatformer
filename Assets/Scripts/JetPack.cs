using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace MyPlatformer
{
    public class JetPack : MonoBehaviour
    {
       
        public bool isBurn = false;
        public Rigidbody2D playerRBody;
        public event UnityAction OnUseJetPack;
        private IEnumerator burningCoroutine;
        private float _gas = 10;
        private ParticleSystem fire;
        private float standartGravityScale;
        //public float lookDistance = 1;
        public float Gas { 
            set 
            {
                if (value <= Constants.jetPackMax)
                {
                    _gas = value;
                }
                else
                {
                    _gas = 10;
                }
                if (OnUseJetPack != null)
                {
                    OnUseJetPack();
                }
            } 
            get { return _gas; } }

        void Awake()
        {
            fire = transform.GetChild(0).GetComponent<ParticleSystem>();
            burningCoroutine = Burning();
            standartGravityScale = playerRBody.gravityScale;
        }
        //float? GetGround()
        //{

        //        Ray2D ray = new Ray2D(playerRBody.position, -playerRBody.transform.up);
        //        RaycastHit2D[] hits;
        //        hits = Physics2D.RaycastAll(ray.origin, ray.direction, lookDistance);
        //        for (int j = 0; j < hits.Length; j++)
        //        {
        //            if (hits[j].collider.gameObject.layer == LayerMask.NameToLayer("Level"))
        //            {

        //                return hits[j].distance;
        //            }
                  
        //        }            
        //    return null;
        //}
        public void On()
        {          
            if (_gas>0)
            {
                StartCoroutine(burningCoroutine);
                isBurn = true;
                fire.Play(true);
            }
           
        }

        public void Off()
        {
            playerRBody.gravityScale = standartGravityScale;
            StopCoroutine(burningCoroutine);
            isBurn = false;
            fire.Stop(true);
        }
        IEnumerator Burning()
        {
            for (;; --Gas)
            {
                yield return new WaitForSeconds(1);
            }
            
        }
        void Update()
        {
            if (isBurn)
            {
                transform.GetChild(0).localScale = playerRBody.transform.localScale;

                if (playerRBody.velocity.y > 0)
                {
                    playerRBody.gravityScale = 0.2f;
                }
                else if (playerRBody.velocity.y < -7f)// && Mathf.Abs(playerRBody.velocity.x) > 1f)
                {
                    playerRBody.gravityScale = Mathf.Lerp(playerRBody.gravityScale, -8f, Time.deltaTime * Mathf.Abs(playerRBody.velocity.y)); //Mathf.Clamp(-0.2f * Mathf.Abs(playerRBody.velocity.y * playerRBody.velocity.x), -8f, 0f);
                    Debug.Log(playerRBody.gravityScale);
                }
                else
                {
                    //float range = Random.RandomRange(1, 2);
                   // playerRBody.gravityScale = -0.2f;
                    playerRBody.gravityScale = Mathf.Lerp(playerRBody.gravityScale, playerRBody.gravityScale > -7 ? -1f : -7f, Time.deltaTime * Mathf.Abs(playerRBody.gravityScale*0.1f));
                }
                if (_gas<=0)
                {
                    Off();
                }
               
            }
        }
    }
}