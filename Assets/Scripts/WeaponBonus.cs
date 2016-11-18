using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class WeaponBonus : Bonus
    {
        public WeaponType weaponType;
        public override bool Pickup(GameObject player)
        {
           player.GetComponent<PlatformerCharacter2D>().weaponSwitcher.SwitchWeapon(weaponType);
           return true;
        }
    }
}
