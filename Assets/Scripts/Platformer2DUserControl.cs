using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using MyPlatformer;
namespace MyPlatformer
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Character2DController : MonoBehaviour
    {
        protected PlatformerCharacter2D m_Character;
        protected void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();

        }
    }
    public class Platformer2DUserControl : Character2DController
    {
        private bool m_Jump;
        private bool m_Fire;
        //private void Awake()
        //{
        //    base.Awake();
        //}


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump")||Input.GetKeyDown(KeyCode.Space);
            }
            m_Fire = CrossPlatformInputManager.GetButton("Fire1") || Input.GetKey(KeyCode.LeftControl);
        }   
        private void FixedUpdate()
        {
            // Read the inputs.
            //bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            //debug version
            if (Input.GetKey(KeyCode.A))
            {
                h = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                h = 1;
            }
            //
            // Pass all parameters to the character control script.
            m_Character.Move(h,v, false, m_Jump,m_Fire);
            m_Jump = false;
        }
    }
}

