using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Менеджер, который отрисовывает фигуру, которую в настоящее время рисует игрок.
/// </summary>
public class DrawManager : MonoBehaviour 
{
    private bool _drawLine;
    private List<Vector3> _pointsList;
    
    private LineRenderer _line;
    private Vector3 mousePos;

    void Start() 
	{
        _drawLine = false;
        _pointsList = new List<Vector3>();
        _line = gameObject.AddComponent<LineRenderer>();
        _line.SetVertexCount(0);
        _line.SetWidth(0.1f, 0.1f);
        _line.material = new Material(Shader.Find("Particles/Additive"));
        _line.SetColors(Color.white, Color.white);
        _line.useWorldSpace = true;
  	}
	
	void Update () 
	{
        if (!_drawLine) return;
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);         // позиция мышки.
        mousePos.z = 0;
        
        _pointsList.Add(mousePos);

        _line.SetVertexCount(_pointsList.Count);
        _line.SetPosition(_pointsList.Count - 1, (Vector3)_pointsList[_pointsList.Count - 1]);

        if (checkLineOnCollideStartFinish())
        {
            // Остановить рисование фигуры.
            stopDraw();
        }
	}

    /// <summary>
    /// Проверка, на пересечение линии старт/финиш.
    /// </summary>
    private bool checkLineOnCollideStartFinish()
    {
        if (_pointsList.Count > 100)
        {
            Vector3 posStartMouse = _pointsList[0];
            Vector3 posCurrentMouse = _pointsList[_pointsList.Count-1];
            float distanceAB = Vector3.Distance(posStartMouse, posCurrentMouse);
           
            if (distanceAB < 0.1)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Игрок начинает отрисовывать фигуру.
    /// </summary>
    public void startDraw()
    {
        clean();
        _drawLine = true;
    }

    /// <summary>
    /// Игрок закончил отрисовывать фигуру.
    /// </summary>
    public void stopDraw()
    {
        if (!_drawLine){
            return;
        }

        _drawLine = false;
        GetComponent<Game>().simileFigure();
    }

    public List<Vector3> pointsList
    {
        get { return _pointsList; }
    }

    /// <summary>
    /// Очистить что нарисовал игрок.
    /// </summary>
    public void clean()
    {
        _drawLine = false;
        if (_pointsList != null)
        {
			_pointsList.RemoveRange (0, _pointsList.Count);
            _line.SetVertexCount(0);
        }
    }

	public void destroy()
	{
		clean ();
		Destroy (_line);
	}
}
