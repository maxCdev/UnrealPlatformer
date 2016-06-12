using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {

public void Restart()
    {
        SceneManager.LoadScene("1");
    }
}
