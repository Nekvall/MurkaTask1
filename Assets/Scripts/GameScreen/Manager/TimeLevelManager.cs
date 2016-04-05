using UnityEngine;
using System.Collections;

public class TimeLevelManager : MonoBehaviour 
{
    public GameObject game;
    public GameObject minTimeObject;
    public GameObject maxTimeObject;

    private Vector3 _minStaticPos;
    private Vector3 _maxStaticPos;

    private int _time;
    private bool _drawLine;
    private LineRenderer _line;

    private Vector3 _currPos;

    void Awake ()
    {
        _minStaticPos = new Vector3(minTimeObject.transform.position.x, minTimeObject.transform.position.y, 0);
        _maxStaticPos = new Vector3(maxTimeObject.transform.position.x, maxTimeObject.transform.position.y, 0);
        _drawLine = true;

        _currPos = _maxStaticPos;

        _line = gameObject.AddComponent<LineRenderer>();
        _line.SetVertexCount(0);
        _line.SetWidth(0.5f, 0.5f);
        _line.material = new Material(Shader.Find("Particles/Additive"));
        _line.SetColors(Color.blue, Color.blue);
        _line.useWorldSpace = true;
    }

    /// <summary>
    /// Задать время, за которое нужно решить уровень.
    /// </summary>
    public void setTime(int time)
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

        float num = (float)_time / 100f;
              num /= (float)Screen.width;
        
        _currPos.x -= num;            // скорость отчета времени.

        _line.SetVertexCount(2);
        _line.SetPosition(0, _minStaticPos);
        _line.SetPosition(1, _currPos);

        if (_currPos.x < _minStaticPos.x)
        {
            // Время вышло.
            _drawLine = false;
            game.GetComponent<Game>().timeLevelEnded();
        }
	}
}
