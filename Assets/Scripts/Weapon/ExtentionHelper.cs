using UnityEngine;
using System.Linq;
using System.Collections.Generic;
namespace MyPlatformer
{
    public static class ExtentionHelper
    {
        public static Bounds OrthographicBounds(this Camera camera)
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = camera.orthographicSize * 2;
            Bounds bounds = new Bounds(
                camera.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            return bounds;
        }
        public static List<Transform> GetChilds(this Transform transform)
        {
            if (transform.childCount!=0)
            {
                List<Transform> childs = new List<Transform>();
                for (int i = 0; i < transform.childCount; i++)
                {                  
                    childs.Add(transform.GetChild(i));
                }
                return childs;
            }
            return null;
            
        }
        public static string GetCloneName(this string name)
        {
            return name + "(Clone)";
        }
        public static Vector3 GetCenterAxisY(this Transform it)
        {
            return new Vector3(0, it.localScale.y/2,0);
        }
        public static Vector3 GetCenterAxisX(this Transform it)
        {
            return new Vector3(it.localScale.x / 2, 0, 0);
        }

    }

}
