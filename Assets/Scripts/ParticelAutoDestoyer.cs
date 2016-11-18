using UnityEngine;
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
                    
                    if (gameObject.transform.parent != null)
                    {
                        ObjectPool.instance.ReturnParticleToPool(transform.parent.gameObject);
                    }
                    else
                    {
                        ObjectPool.instance.ReturnParticleToPool(gameObject);
                    }                   
                }
            }
        }
    }
}
