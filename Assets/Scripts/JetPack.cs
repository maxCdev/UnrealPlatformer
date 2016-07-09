using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace MyPlatformer
{
    public class JetPack : MonoBehaviour
    {
        public float gas = 10;
        public float Gas { 
            set 
            {
                if (value <= Constants.jetPackMax)
                {
                    gas = value;
                }
                else
                {
                    gas = 10;
                }
                if (OnUseJetPack != null)
                {
                    OnUseJetPack();
                }
            } 
            get { return gas; } }
        ParticleSystem fire;
        public bool isBurn = false;
        IEnumerator burningCoroutine;
        public Rigidbody2D playerRBody;
        public event UnityAction OnUseJetPack;
        float standartGravityScale;
        void Awake()
        {
            fire = transform.GetChild(0).GetComponent<ParticleSystem>();
            burningCoroutine = Burning();
            standartGravityScale = playerRBody.gravityScale;
        }

        public void On()
        {          
            if (gas>0)
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
                if (playerRBody.velocity.y > 0)
                {
                    playerRBody.gravityScale = 0.2f;
                }
                else if (playerRBody.velocity.y <-7f&&Mathf.Abs(playerRBody.velocity.x)>1f) 
                {
                    playerRBody.gravityScale = Mathf.Clamp(-0.15f * Mathf.Abs(playerRBody.velocity.y *playerRBody.velocity.x),-0.15f,5f);

                }
                else
                {
                    playerRBody.gravityScale = -0.2f;

                }
                yield return new WaitForSeconds(1);
            }
            
        }
        void Update()
        {
            if (isBurn)
            {
                if (gas<=0)
                {
                    Off();
                }
               
            }
        }
    }
}