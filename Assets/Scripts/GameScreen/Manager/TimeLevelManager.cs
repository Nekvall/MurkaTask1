using UnityEngine;
using System.Collections;

public class TimeLevelManager : MonoBehaviour 
{
    private Vector3 _minStaticPos;
    private Vector3 _maxStaticPos;

    private float _time;
    private bool _drawLine;

    private Vector3 _currPos;

    void Start ()
    {
        _minStaticPos = new Vector3(0, GetComponent<UIGame>().getPositionYLevelTime, 0);
        _maxStaticPos = new Vector3(Screen.width, GetComponent<UIGame>().getPositionYLevelTime, 0);

        _drawLine = true;

        _currPos = _maxStaticPos;
    }

    /// <summary>
    /// Задать время, за которое нужно решить уровень.
    /// </summary>
    public void setTime(float time)
    {
        _drawLine = true;
        _time = time;
        _currPos = _maxStaticPos;
    }
	
	void Update () 
    {
        if (!_drawLine){
            return;
        }

        float currScale = GetComponent<UIGame>().getScaleXLevelTime;
        currScale -= _time;

        if (currScale > 0)
        {
            GetComponent<UIGame>().updateTimeLevel(currScale);
        }
        else
        {
            GetComponent<UIGame>().updateTimeLevel(0);
            // Время вышло.
            _drawLine = false;
            GetComponent<Game>().timeLevelEnded();
        }
	}
}
