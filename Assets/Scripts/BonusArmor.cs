using UnityEngine;
using System.Collections;
using System.Linq;
namespace MyPlatformer
{
    public class BonusArmor : TimeStopBonus
    {
        protected override void Action(bool flag)
        {
           GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player!=null)
            {
                Debug.Log(player.name+" god mode = "+!flag);
                player.GetComponent<DestroybleObject>().godMode = !flag;
               Transform armor = player.transform.GetChild(0); 
               //Debug.Log(armor.gameObject.name + " god mode = " + !flag);
                if (armor.name=="Armor")
                {
                    armor.gameObject.SetActive(!flag);
                }

                
            }
        }
    }
}

