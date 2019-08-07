using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;
    public List<GameObject> stars;

    public static GameManager _instance;
    private Vector3 originPos;

    public GameObject lose;
    public GameObject win;

    private int starNum = 0;
    [HideInInspector]
    public bool ifEnd = false;//是否已经结算(结算时不可暂停)

    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[0].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].ifCtrled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //Next Fly
                Initialized();
            }
            else
            {
                //lose
                ifEnd = true;
                lose.SetActive(true);
            }
        }
        else
        {
            //win
            ifEnd = true;
            win.SetActive(true);
        }
    }
    /// <summary>
    /// 判断滑落、非击杀的猪是否为最后一个
    /// </summary>
    public void IfLastPig()
    {
        if (pigs.Count == 0)
        {
            if (birds[0].ifCtrled)
            {
                print("hualuo");
                ifEnd = true;
                win.SetActive(true);
            }
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for (;starNum <= birds.Count; starNum++)
        {
            if (starNum >= stars.Count)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starNum].SetActive(true);
        }
        print("Num is:" + starNum);
    }

    public void Replay()
    {
        SaveData();
        print("Replay is executed");
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    private void SaveData()
    {
        int preStarNum = PlayerPrefs.GetInt(PlayerPrefs.GetString("curLevel"));
        if (starNum > preStarNum)
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("curLevel"), starNum);

            int totalStarNum = PlayerPrefs.GetInt("TotalStarNum");
            totalStarNum += starNum - preStarNum;
            PlayerPrefs.SetInt("TotalStarNum", totalStarNum);
        }
        print("totoalNum is " + PlayerPrefs.GetInt("TotalStarNum"));
    }
}
