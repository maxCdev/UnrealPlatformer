using UnityEngine;
using System.Collections;

public class NotDestroySceneObj : MonoBehaviour {

    public static NotDestroySceneObj instance;
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

}
