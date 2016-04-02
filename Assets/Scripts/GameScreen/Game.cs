using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
    public static event EventManager.MethodContainer mouseButtonDownEvent;      // Игрок нажал левую кнопку мыши.

	private LevelBase _level;	//текущий левел
    private int numCurrLevel;

    private LevelManager    _levelManager;
    private DrawManager     _drawManager;       // менеджер отрисовки.
    private FigureManager   _figureManager;     
    private SimileManager   _simileManager;     // менеджер сравнения фигур.
    
	void Start () 
	{
        initialize();

		numCurrLevel = 0;

        createNextLevel();
	}

    /// <summary>
    /// Инициализация классов.
    /// </summary>
	private void initialize()
	{
        _levelManager   = GetComponent<LevelManager>();
        _drawManager    = GetComponent<DrawManager>();
        _figureManager  = GetComponent<FigureManager>();
        _simileManager  = GetComponent<SimileManager>();
	}

    /// <summary>
    /// Создать следующий уровень.
    /// </summary>
    private void createNextLevel()
    {
        numCurrLevel++;

        // создаем новый уровень.
        _level = _levelManager.createLevel(numCurrLevel);    

        // менеджер отрисует фигуру на сцене.
        _figureManager.show(_level.numForm);
    }

    /// <summary>
    /// Сравнить нарисованую игроком фигуру, с заданой фигурой. 
    /// </summary>
    public void simileFigure()
    {
        _simileManager.compare(_figureManager.currFigure, _drawManager.pointsList);
    }

	void Update () 
	{
	    
	}
}
