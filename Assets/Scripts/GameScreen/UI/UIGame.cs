using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGame : MonoBehaviour 
{
    public GameObject levelTxt;
    public GameObject levelTime;

    /// <summary>
    /// Показать текущий уровень.
    /// </summary>
    public void updateNumLevelTxt(int currLevel, int maxLevel)
    {
        levelTxt.GetComponent<Text>().text = "Level " + currLevel + "/" + maxLevel;
    }

    /// <summary>
    /// Показать время игры.
    /// </summary>
    public void updateTimeLevel(float scaleTimeLine)
    {
        // сюдой должен приходить скейл
        levelTime.transform.localScale = new Vector3(scaleTimeLine, levelTime.transform.localScale.y);
    }

    public float getPositionYLevelTime
    {
        get { return levelTime.transform.position.y; }
    }

    public float getScaleXLevelTime
    {
        get { return levelTime.transform.localScale.x; }
    }
}
