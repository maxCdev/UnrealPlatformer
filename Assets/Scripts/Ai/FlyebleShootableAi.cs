using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
[RequireComponent(typeof(TargetLocator))]
public class FlyebleShootableAi : FlyebleAi
{
    public Weapon weapon;
    private TargetLocator locator;
	void Start () {
        base.Start();
        locator = GetComponent<TargetLocator>();
	}
    void Update()
    {
        base.Update();
        if (locator.targetVisible)
        {
           weapon.RotateWeapon(Mathf.RoundToInt(locator.targetDirection.normalized.y));
           weapon.Fire();
        }
    }
}
}