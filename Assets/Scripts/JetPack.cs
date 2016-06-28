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
                if (value <= 10)
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
        void Awake()
        {
            fire = transform.GetChild(0).GetComponent<ParticleSystem>();
            burningCoroutine = Burning();
        }

        public void On()
        {
            if (gas>0)
            {
                playerRBody.gravityScale = 0.2f;
                StartCoroutine(burningCoroutine);
                isBurn = true;
                fire.Play(true);
            }
           
        }
        public void Off()
        {
            playerRBody.gravityScale = 3f;
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
                if (gas<=0)
                {
                    Off();
                }
               
            }
        }
    }
}