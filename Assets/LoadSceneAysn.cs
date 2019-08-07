using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAysn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1366, 768, false);
        Invoke("Load", 2);
    }

    // Update is called once per frame
    private void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
