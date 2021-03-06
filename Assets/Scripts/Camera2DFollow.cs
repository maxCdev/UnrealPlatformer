using System;
using UnityEngine;

namespace MyPlatformer
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public Transform maxDownPosition;
        public Transform maxTopPosition;
        public Transform maxLeftPosition;
        public Transform maxRightPosition;
        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private Bounds cameraBounds;
        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
            cameraBounds = GetComponent<Camera>().OrthographicBounds();
        }

        private void Update()
        {
            if (target==null||!target.gameObject.activeInHierarchy)
            {
                return;
            }

            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }
            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;

            // clamping 
            aheadTargetPos =new Vector3(
                Mathf.Clamp(aheadTargetPos.x, maxLeftPosition.position.x + cameraBounds.size.x / 2, maxRightPosition.position.x - cameraBounds.size.x / 2),
                Mathf.Clamp(aheadTargetPos.y, maxDownPosition.position.y + cameraBounds.size.y / 2, maxTopPosition.position.y - cameraBounds.size.y / 2), aheadTargetPos.z);            
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}
