using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public bool ifClone = true;
    private void Awake()
    {
        if (ifClone)
        {
            print("Loading:" + PlayerPrefs.GetString("curLevel"));
            Instantiate(Resources.Load(PlayerPrefs.GetString("curLevel")));
        }
    }
}
