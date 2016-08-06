using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
public class BaseAi : FlyebleAi
{


    public bool canRotateWeapon = false;
    protected Vector2? targetDirection;
    public float updateTarget;
    public Transform locator;
    public Weapon weapon;
    bool fire = false;
	void Start () {
        base.Start();
        StartCoroutine(CheckTarget());
	}
    IEnumerator CheckTarget()
    {
        yield return new WaitForSeconds(updateTarget);
        GetTarget();
        StartCoroutine(CheckTarget());
        
    }
    void Update()
    {
        base.Update();
        if (targetDirection!=null&&fire)
        {
            RotateWeapon(Mathf.RoundToInt(targetDirection.Value.normalized.y));
            weapon.Fire();
        }
    }
    protected void GetTarget()
    {
        for (int i = 0; i < 360; i += 45)
        {
            locator.Rotate(Vector3.forward * 45);
            Ray2D ray = new Ray2D(locator.position, locator.up);         
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(ray.origin, ray.direction, loockDistance);
            for (int j = 0; j < hits.Length; j++)
            {
                if (hits[j].collider.gameObject.layer == LayerMask.NameToLayer("Level"))
                {
                    break;

                }
                if (hits[j].collider.gameObject.CompareTag("Player"))
                {
                    targetDirection = ray.direction.normalized;
                    fire = true;
                    return;
                }
              

            }
        }
        fire = false;
    }
    private void RotateWeapon(float rotate)
    {
        if (rotate < 0)
        {
            weapon.transform.rotation = Quaternion.Euler(Vector3.back * 45f);
        }
        else if (rotate > 0)
        {
            weapon.transform.rotation = Quaternion.Euler(Vector3.forward * 45f);
        }
        else
        {
            weapon.transform.rotation = Quaternion.identity;
        }

    }
}
}