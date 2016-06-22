using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class JetPack : MonoBehaviour
    {
        [SerializeField]
        float gas = 10;
        ParticleSystem fire;
        public bool isBurn = false;
        IEnumerator burningCoroutine;
        public Rigidbody2D playerRBody;
        void Awake()
        {
            fire = transform.GetChild(0).GetComponent<ParticleSystem>();
            burningCoroutine = Burning();
        }

        public void On()
        {
            if (gas>0)
            {
                Debug.Log("Start corutine");
                playerRBody.gravityScale = 0.2f;
                StartCoroutine(burningCoroutine);
                isBurn = true;
                fire.Play(true);
            }
           
        }
        public void Off()
        {
            playerRBody.gravityScale = 3f;
            Debug.Log("Stop corutine");
            StopCoroutine(burningCoroutine);
            isBurn = false;
            fire.Stop(true);
        }
        IEnumerator Burning()
        {
            for (;; --gas)
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