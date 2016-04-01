using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	private LevelBase _level;	//текущий левел
    private int numCurrLevel;

    private LevelManager    _levelManager;
    private DrawManager     _drawManager;
    private FigureManager   _figureManager;
    
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

	void Update () 
	{
	    
	}
}
