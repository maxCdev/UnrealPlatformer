using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace MyPlatformer
{
    public class Paralax : MonoBehaviour
    {

        public List<Transform> backgrounds=new List<Transform>();
        private List<float> parallaxScales= new List<float>();
        public float smouthing;

        private Transform camera;
        private Vector3 previusCameraPos;
        void Start()
        {
            camera = Camera.main.transform;
            previusCameraPos = camera.transform.position;
            parallaxScales.AddRange(backgrounds.Select(a => a.transform.position.z * -1));
        }

        // Update is called once per frame
        void LateUpdate()
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                float parallax = (previusCameraPos.x - camera.position.x) * parallaxScales[i];
                float bgTargetPosX = backgrounds[i].position.x + parallax;
                Vector3 bgTargetPos = new Vector3(bgTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bgTargetPos, smouthing * Time.deltaTime);
            }
            previusCameraPos = camera.position;
        }
    }
}
