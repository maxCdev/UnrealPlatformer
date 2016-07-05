using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public class CrystalBonus : Bonus
    {
        public int addCount=5;
        public override bool Pickup(GameObject player)
        {
            GameObject.Find("UI").SendMessage("AddCrystal", addCount, SendMessageOptions.DontRequireReceiver);
            return true;
        }
    }
}
