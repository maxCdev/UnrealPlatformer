using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public class BonusLife : Bonus
    {
        public float addHp = 3;
        public override bool Pickup(GameObject player)
        {
            player.GetComponent<DestroybleObject>().Hp += addHp;
            return true;
        }

    }
}
