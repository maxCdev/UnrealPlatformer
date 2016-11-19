using System;
using UnityEngine;
using System.Linq;
namespace MyPlatformer
{
    public class CharacterMotor : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private float m_SecondJumpForce = 400f;                  //Second jump force
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        public LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;    
        private bool jumpPressed = false;
        private bool canBurn=true; 
        public Weapon weapon;
        public JetPack jetPack=null;
        public WeaponSwitcher weaponSwitcher = null;
        public AudioSource audioSource;
        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            //m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        public void PickupBonusSound()
        {
            audioSource.Play();
        }
        /// <summary>
        /// Checking if character on ground
        /// </summary>
        /// <param name="groundChek"></param>
        /// <param name="groundLayer">What is the ground?</param>
        /// <returns></returns>
        public bool GroundCheck(Transform groundChek, LayerMask groundLayer)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChek.position, k_GroundedRadius, groundLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    return true;
            }
            return false;
        }
        private void FixedUpdate()
        {
            m_Grounded = GroundCheck(m_GroundCheck, m_WhatIsGround);
            m_Anim.SetBool("Ground", m_Grounded);
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        public void TryFlip(float move)
        {
            // If the input is moving the player right and the player is facing left...
            if ((move > 0 && transform.localScale.x == -1) || (move < 0 && transform.localScale.x == 1))
            {
                // ... flip the player.
                Flip();
            }
        }
        public void Move(float move,float weaponRot, bool crouch, bool jump,bool fire)
        {
            canBurn = true;

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // the Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                //check if need flip
                TryFlip(move);         
            }
            weapon.RotateWeapon(weaponRot);

            // if jetpack is burned and jump already pressed and player or character stay on ground
            if (jetPack != null && jetPack.isBurn && ((jumpPressed && jump) || m_Grounded))
            {
                canBurn = false;
                jetPack.Off();
            }

            //if player should fire and weapon can fire now
            if (fire && weapon.Fire())
            {
                m_Anim.SetTrigger("Fire");             
            }

            // if the player should jump
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                m_Grounded = false;

                //set animators ground value
                m_Anim.SetBool("Ground", false);

                // Add a vertical force to the player.
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

                //jump already pressed
                jumpPressed = true;
            }
            // if the player should jump second time
            else if (!m_Grounded && jump && !m_Anim.GetBool("Ground") && jumpPressed) 
            {
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);

                //if jetpack not available
                if (jetPack!=null&&jetPack.Gas==0)
                {
                    //second jump
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_SecondJumpForce));
                    jumpPressed = false;                 
                }

                //if jetpack available
                else if (!jetPack.isBurn&&canBurn)
                {
                    jetPack.On();
                    jumpPressed = true;
                }
            }
        }
        private void Flip()
        {
            // multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
