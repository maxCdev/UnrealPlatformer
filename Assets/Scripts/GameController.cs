using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace MyPlatformer
{
    public class GameController : MonoBehaviour
    {
        public void Start()
        {
            Application.targetFrameRate = 60;
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
