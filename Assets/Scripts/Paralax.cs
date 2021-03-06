﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace MyPlatformer
{
    /// <summary>
    /// Move background and create paralax effect
    /// </summary>
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
            for (int i = 1; i < backgrounds.Count + 1; i++)
            {
                parallaxScales.Add(i * -10);
            }               
        }
        void LateUpdate()
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                float parallax = (previusCameraPos.x - camera.position.x) * parallaxScales[i];
                float bgTargetPosX = backgrounds[i].position.x + parallax;
                parallax = (previusCameraPos.y - camera.position.y) * parallaxScales[i];
                float bgTargetPosY = backgrounds[i].position.y + parallax;
                Vector3 bgTargetPos = new Vector3(bgTargetPosX, bgTargetPosY, backgrounds[i].position.z);
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bgTargetPos, smouthing * Time.deltaTime);
            }
            previusCameraPos = camera.position;
        }
    }
}
