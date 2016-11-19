using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using MyPlatformer;
namespace MyPlatformer
{
    /// <summary>
    /// Read User Input
    /// </summary>
    public class UserCharacterController : BaseCharacterController
    {
        private bool m_Jump;
        private bool m_Fire;

        private void Update()
        {
            //get input
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            m_Fire = CrossPlatformInputManager.GetButton("Fire1");

            //debug 
            #if UNITY_STANDALONE||UNITY_EDITOR
            if (!m_Jump)
            {
                m_Jump = Input.GetKeyDown(KeyCode.Space);
            }
            m_Fire = Input.GetKey(KeyCode.LeftControl);
            #endif
        }   
        private void FixedUpdate()
        {
            //get input
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            //debug 
            #if UNITY_STANDALONE||UNITY_EDITOR
            if (Input.GetKey(KeyCode.A))
            {
                h = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                h = 1;
            }
            #endif

            // pass all parameters to the character control script.
            m_Character.Move(h,v, false, m_Jump,m_Fire);
            m_Jump = false;
        }
    }
}

