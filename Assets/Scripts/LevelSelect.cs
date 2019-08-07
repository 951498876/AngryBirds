using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 关卡选择界面单元格的脚本，管理当前关卡的显示与星星数据显示
/// </summary>
public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false;

    public Sprite levelBG;
    public Sprite star_0;
    public Sprite star_1;
    public Sprite star_2;
    public Sprite star_3;

    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else
        {
            //获取前一关卡，判断当前关卡是否可以选择
            string curLvlName = gameObject.name;
            string formLvlName = curLvlName.Substring(0, curLvlName.Length - 1) + (curLvlName[curLvlName.Length - 1] - '0' - 1);
            //print("curLvlName:" + curLvlName);
            //print("formLvlName:" + formLvlName);
            if (PlayerPrefs.GetInt("level" + formLvlName) > 0)
            {
                isSelect = true;
            }
        }

        if (isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("Star").gameObject.SetActive(true);
            transform.Find("Num").gameObject.SetActive(true);

            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            //print("count="+count);
            switch (count)
            {
                case 0:
                    transform.Find("Star").GetComponent<Image>().overrideSprite = star_0;
                    break;
                case 1:
                    transform.Find("Star").GetComponent<Image>().overrideSprite = star_1;
                    break;
                case 2:
                    transform.Find("Star").GetComponent<Image>().overrideSprite = star_2;
                    break;
                case 3:
                    transform.Find("Star").GetComponent<Image>().overrideSprite = star_3;
                    break;
            }
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("curLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
        }
    }

}