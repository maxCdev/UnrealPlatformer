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
        public Dropdown qualityList;
        void Awake()
        {
            qualityList.options.Clear();
            foreach (var name in QualitySettings.names)
            {
                qualityList.options.Add(new Dropdown.OptionData(name));
            }
            qualityList.value = QualitySettings.GetQualityLevel();
            qualityList.onValueChanged.AddListener((value) => { QualitySettings.SetQualityLevel(value); });
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
