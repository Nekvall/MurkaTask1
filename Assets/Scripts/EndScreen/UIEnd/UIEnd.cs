using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIEnd : MonoBehaviour 
{
    public GameObject levelTxt;

    public void setLevelTxt(int currLevel, int maxLevel)
    {
        levelTxt.GetComponent<Text>().text = "Level " + currLevel + "/" + maxLevel;
    }
}
