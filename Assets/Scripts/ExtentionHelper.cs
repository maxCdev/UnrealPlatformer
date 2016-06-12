using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    public static class ExtentionHelper
    {

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
