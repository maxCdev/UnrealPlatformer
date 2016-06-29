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
        void Start()
        {
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
            Application.targetFrameRate = 60;
            
        }
        IEnumerator FpsShow()
        {
            yield return new WaitForSeconds(2);
            fps.text = ((int)(1f / Time.unscaledDeltaTime)).ToString();
        }
        void Update()
        {
            StartCoroutine(FpsShow());
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
