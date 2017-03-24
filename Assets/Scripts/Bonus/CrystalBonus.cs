using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace MyPlatformer
{
    public class CrystalBonus : Bonus
    {
        private RectTransform crystalsUi;
        public int addCount=5;
        public bool pickuped = false;
        public override bool Pickup(GameObject player)
        {
            if (!pickuped)
            {
                GameObject ui = GameObject.Find("UI");
                ui.SendMessage("AddCrystal", addCount, SendMessageOptions.DontRequireReceiver);
                crystalsUi = ui.transform.FindChild("Crystals").GetComponent<RectTransform>();
                pickuped = true;
            }          
            return false;
        }
        void Lerp()
        {
            Vector3 uiCrystal = Camera.main.ScreenToWorldPoint(crystalsUi.position);
            if (transform.position == uiCrystal)
            {
                //Destroy(gameObject);
                ObjectPool.instance.ReturnBonusToPool(gameObject);
                return;
            }
          transform.position = Vector3.MoveTowards(transform.position, uiCrystal, 15f * Time.deltaTime);
        }

        void Update()
        {
            if (pickuped)
            {
                Lerp();
            }
        }
    }
}
