using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Retry()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void Home()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 播放暂停动画
    /// </summary>
    public void Pause()
    {
        print("ifEnd:"+ GameManager._instance.ifEnd);
        if (GameManager._instance.ifEnd)
        {
            print("End");
            return;
        }
        anim.SetBool("isPause", true);
        button.SetActive(false);

        if (GameManager._instance.birds.Count > 0)
        {
            if (!GameManager._instance.birds[0].GetIfFly())
            {
                GameManager._instance.birds[0].ifCtrled = false;
            }

        }
    }

    /// <summary>
    /// 暂停动画播放完
    /// </summary>
    public void PauseAnimEnd()
    {
        //1、播放动画
        //2、暂停

        Time.timeScale = 0;
    }


    /// <summary>
    /// 播放继续动画
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);

        if (GameManager._instance.birds.Count > 0)
        {
            if (!GameManager._instance.birds[0].GetIfFly())
            {
                GameManager._instance.birds[0].ifCtrled = true;
            }

        }
    }

    /// <summary>
    /// 继续动画播放完
    /// </summary>
    public void RestartAnimEnd()
    {
        button.SetActive(true);
    }

}