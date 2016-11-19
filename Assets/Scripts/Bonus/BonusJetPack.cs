using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public class BonusJetPack : Bonus
    {
        [SerializeField]
        float addGass = 3;
        public override bool Pickup(GameObject player)
        {
            player.GetComponent<CharacterMotor>().jetPack.Gas += addGass;
            return true;
        }
        
    }
}
