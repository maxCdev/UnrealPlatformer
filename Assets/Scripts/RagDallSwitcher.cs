using UnityEngine;
using System.Collections;

namespace MyPlatformer
{
    public class RagDallSwitcher : FiriebleObject {
        public override void ReactionOnFire(Shoot bullet)
        {
            foreach (Rigidbody2D body in transform.parent.GetComponentsInChildren<Rigidbody2D>())
            {
                body.isKinematic = false;
            }
            transform.parent.GetComponent<Rigidbody2D>().isKinematic = false;
            foreach (HingeJoint2D hinge in transform.parent.GetComponentsInChildren<HingeJoint2D>())
            {
                hinge.enabled = true;
            }
            foreach (FiriebleObject hinge in transform.parent.GetComponentsInChildren<FiriebleObject>())
            {
                hinge.enabled = true;
            }
            base.ReactionOnFire(bullet);
        }
}
}

