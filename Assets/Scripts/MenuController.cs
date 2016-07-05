using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace MyPlatformer
{
    public class MenuController : MonoBehaviour
    {
        public GameObject settingsPanel;
        public Slider sliderVolume;
        void Awake()
        {
            sliderVolume.value = AudioListener.volume;
            sliderVolume.onValueChanged.AddListener((vol) => { AudioListener.volume = vol; });
        }
        public void StartGame()
        {
            SceneManager.LoadScene("Game");
            
        }
        public void CloseOpenSettingsPanel()
        {
            settingsPanel.GetComponent<Animator>().SetTrigger("Move");
        }
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit(); 
            }
              
        }

    }
}
