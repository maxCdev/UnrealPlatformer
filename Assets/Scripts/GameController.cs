using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityStandardAssets._2D;
namespace MyPlatformer
{
    public class GameController : MonoBehaviour
    {
        public Image hpImage;
        public Scrollbar jetPackSlider;
        public GameObject player;
        public Text fps;
        public Text crystals;
        public GameObject GameOverPanel;
        public Text crystalsResult;
        bool pause = false;
        void Start()
        {
            pause = false;
            DestroybleObject playerFireObj = player.GetComponent<DestroybleObject>();
            hpImage.fillAmount = playerFireObj.Hp / 10;
            playerFireObj.OnChangeHp += () => { hpImage.fillAmount = playerFireObj.Hp / 10; };
            JetPack jetPack = player.GetComponent<PlatformerCharacter2D>().jetPack;
            jetPackSlider.size = jetPack.gas / 10;
            jetPack.OnUseJetPack+=()=>
            {
                    jetPackSlider.handleRect.gameObject.SetActive(jetPack.gas!=0);
                    jetPackSlider.size = jetPack.gas / 10;
            };
            playerFireObj.OnChangeHp += () => 
            {
                GameOverPanel.SetActive(playerFireObj.Hp <= 0);
                GameOverPanel.GetComponent<Animator>().SetTrigger("Move");
                crystalsResult.text = crystals.text;
            };
            Application.targetFrameRate = 60;
            
        }
        IEnumerator FpsShow()
        {
            yield return new WaitForSeconds(2);
            fps.text = ((int)(1f / Time.unscaledDeltaTime)).ToString();
        }
        void AddCrystal(int addCount)
        {
           crystals.text = (int.Parse(crystals.text) + addCount).ToString();
        }
        void Update()
        {
            StartCoroutine(FpsShow());
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToMenu();
            }
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void ToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        public void Pause()
        {
            if (!pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            pause = !pause;
            
        }
        public void PauseOff()
        {
            
        }
    }
}
