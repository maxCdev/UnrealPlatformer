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
        public Text crystalsResultWin;
        public Transform Enemies;
        public GameObject WinPanel;
        public GameObject boss;
        bool missionComplete = false;
        bool pause = false;
        void Start()
        {
            pause = false;
            DestroybleObject playerFireObj = player.GetComponent<DestroybleObject>();
            hpImage.fillAmount = playerFireObj.Hp / Constants.playerLifeMax;
            playerFireObj.OnChangeHp += () => { hpImage.fillAmount = playerFireObj.Hp / Constants.playerLifeMax; };
            JetPack jetPack = player.GetComponent<PlatformerCharacter2D>().jetPack;
            jetPackSlider.size = jetPack.gas / Constants.jetPackMax;
            jetPack.OnUseJetPack+=()=>
            {
                    jetPackSlider.handleRect.gameObject.SetActive(jetPack.gas!=0);
                    jetPackSlider.size = jetPack.gas / Constants.jetPackMax;
            };
            playerFireObj.OnChangeHp += () => 
            {
                if (playerFireObj.Hp <= 0 && !missionComplete)
                {
                    GameOverPanel.GetComponent<Animator>().SetTrigger("Move");
                }
               
                crystalsResult.text = crystals.text;
            };
            DestroybleObject bossDestr =boss.GetComponent<DestroybleObject>();
            bossDestr.OnChangeHp += () =>
                {
                    if (playerFireObj.Hp != 0 && bossDestr.Hp <= 0)
                    {
                        missionComplete = true;
                        WinPanel.GetComponent<Animator>().SetTrigger("Move");
                        crystalsResultWin.text = crystals.text;
                    }
                };
            Application.targetFrameRate = 60;
            StartCoroutine(UpdateEnemiesCount());
            
        }
        IEnumerator UpdateEnemiesCount()
        {
            yield return new WaitForSeconds(2);
            if (Enemies.childCount==0)
            {
                WinPanel.GetComponent<Animator>().SetTrigger("Move");
            }
            else
            {
                StartCoroutine(UpdateEnemiesCount());
            }
            
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
          public void NextLevel()
        {
              int levelIndex = SceneManager.GetActiveScene().buildIndex+1;
              if (levelIndex<Constants.levelCount)
              {
                  SceneManager.LoadScene(levelIndex);
              }
              else
              {
                  ToMenu();
              }
            
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
    }
}
