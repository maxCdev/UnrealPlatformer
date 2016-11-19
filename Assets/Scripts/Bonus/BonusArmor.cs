using UnityEngine;
using System.Collections;
using System.Linq;
namespace MyPlatformer
{
    public class BonusArmor : TimeStopBonus
    {
        /// <summary>
        /// On armor
        /// </summary>
        /// <param name="flag"></param>
        protected override void Action(bool flag)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player!=null)
            {
                Debug.Log(player.name+" god mode = "+!flag);
                player.GetComponent<DestroybleObject>().godMode = !flag;
                Transform armor = player.transform.GetChild(0); 
                //todo: this is bad, need to fix
                if (armor.name=="Armor")
                {
                    armor.gameObject.SetActive(!flag);
                }
            }
        }
    }
}

