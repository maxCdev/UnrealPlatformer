using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace MyPlatformer
{
    public class MenuController : MonoBehaviour
    {

        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
