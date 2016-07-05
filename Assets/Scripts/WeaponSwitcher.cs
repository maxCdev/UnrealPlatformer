using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace MyPlatformer
{

    public class WeaponSwitcher : MonoBehaviour
    {
        private PlatformerCharacter2D controller;
        [SerializeField]
        private Weapon currentWeapon;
        public Weapon CurrentWeapon { 
            set 
            { 
                if (value.type!=currentWeapon.type)
                {
                    currentWeapon.gameObject.SetActive(false);
                    value.gameObject.SetActive(true);
                    currentWeapon = value;
                    controller.weapon = currentWeapon;
                    
                 }
            
            } get { return currentWeapon; } }
        private List<Weapon> weapons = new List<Weapon>();
        public void SwitchWeapon(WeaponType typeWeapon)
        {
            CurrentWeapon = weapons.First(a => a.type == typeWeapon);
        }
        // Use this for initialization
        void Start()
        {
            controller = transform.root.GetComponent<PlatformerCharacter2D>();
            currentWeapon = controller.weapon;
            for (int i = 0; i < transform.childCount; i++)
            {
                weapons.Add(transform.GetChild(i).GetComponent<Weapon>());
            }
        }
    }
}
