using UnityEngine;
namespace MyPlatformer
{
    [RequireComponent(typeof(CharacterMotor))]
    public class BaseCharacterController : MonoBehaviour
    {
        protected CharacterMotor m_Character;
        protected void Awake()
        {
            m_Character = GetComponent<CharacterMotor>();
        }
    }
}
