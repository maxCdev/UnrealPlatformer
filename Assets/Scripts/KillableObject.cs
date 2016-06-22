using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public class KillableObject : MonoBehaviour
    {

        public float damage;
        public float Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }
        public string deathName;
    }
}