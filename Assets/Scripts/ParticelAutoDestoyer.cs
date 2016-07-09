using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public class ParticelAutoDestoyer : MonoBehaviour
    {

        private ParticleSystem ps;


        public void Start()
        {
            ps = GetComponent<ParticleSystem>();
        }

        public void Update()
        {
            if (ps)
            {
                if (!ps.IsAlive()||ps.time==ps.duration)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
