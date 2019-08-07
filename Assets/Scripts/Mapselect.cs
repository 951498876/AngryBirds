using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 地图选择界面单元格的脚本，管理当前地图的显示与锁、星星数据显示
/// </summary>

public class Mapselect : MonoBehaviour
{
    public int starsNum = 0;
    public bool isSelect = false;

    public GameObject lockFlag;
    public GameObject starFlag;
    public GameObject panel;
    public GameObject map;

    public Text starsText;
    public int startNum = 1;
    public int endNum = 8;

    private string[] map1 =
        { "level1-1", "level1-2", "level1-3", "level1-4", "level1-5", "level1-6", "level1-7", "level1-8",
    "level2-1", "level2-2", "level2-3", "level2-4", "level2-5", "level2-6", "level2-7", "level2-8",
    "level3-1", "level3-2", "level3-3", "level3-4", "level3-5", "level3-6", "level3-7", "level3-8"};
    private void Start()
    {
        //PlayerPrefs.DeleteAll();//测试用，清零
        if (PlayerPrefs.GetInt("TotalStarNum", 0) >= starsNum)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            lockFlag.SetActive(false);
            starFlag.SetActive(true);

            //TODO:Text显示
            int count = 0;
            for (int i = startNum; i <= endNum; i++)
            {
                count += PlayerPrefs.GetInt(map1[i - 1],0);
            }
            starsText.text = count.ToString() + "/24";
        }

    }
    /// <summary>
    /// 鼠标点击
    /// </summary>
    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }
    public void Home()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }
}
