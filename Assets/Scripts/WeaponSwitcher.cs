using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace MyPlatformer
{
    /// <summary>
    /// Swith weapons
    /// </summary>
    public class WeaponSwitcher : MonoBehaviour
    {
        private PlatformerCharacter2D controller;
        [SerializeField]
        private Weapon currentWeapon;
        public Weapon CurrentWeapon { 
            set 
            { 
                //check if its not same weapon
                if (value.type!=currentWeapon.type)
                {
                    //hide previus weapon
                    currentWeapon.gameObject.SetActive(false);

                    //show new weapon
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
            weapons.AddRange(transform.GetComponentsInChildren<Weapon>(true));
        }
    }
}
