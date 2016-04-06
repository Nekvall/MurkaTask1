using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
    public static event EventManager.MethodContainer mouseButtonDownEvent;      // Игрок нажал левую кнопку мыши.

	private LevelBase _level;	//текущий левел
    private int numCurrLevel;

    private LevelManager        _levelManager;
    private DrawManager         _drawManager;       // менеджер отрисовки.
    private FigureManager       _figureManager;     
    private SimileManager       _simileManager;     // менеджер сравнения фигур.
    private TimeLevelManager    _timeLevelManager;  // менеджер времени уровня.
	private EventManager		_eventManager;
    private UIGame              _uiGame;            // gui.

    private float hardLevel;                        // текущая сложность уровня.
    private float addHardLevel = 0.0005f;             // добавляем сложность.      

	void Start () 
	{
        initialize();

		numCurrLevel = 0;
        hardLevel = 0;

        createNextLevel();
	}

    /// <summary>
    /// Инициализация классов.
    /// </summary>
	private void initialize()
	{
        _uiGame             = GetComponent<UIGame>();
        _levelManager       = GetComponent<LevelManager>();
        _drawManager        = GetComponent<DrawManager>();
        _figureManager      = GetComponent<FigureManager>();
        _simileManager      = GetComponent<SimileManager>();
        _timeLevelManager   = GetComponent<TimeLevelManager>();
		_eventManager 		= GetComponent<EventManager> ();   
	}

    /// <summary>
    /// Создать следующий уровень.
    /// </summary>
    private void createNextLevel()
    {
        numCurrLevel++;

        _uiGame.updateNumLevelTxt(numCurrLevel, 15);
        PlayerPrefs.SetInt("Player Level", numCurrLevel);

        GetComponent<UIGame>().updateTimeLevel(1);

        // создаем новый уровень.
        _level = _levelManager.createLevel(numCurrLevel);    

        // менеджер отрисует фигуру на сцене.
        _figureManager.show(_level.numForm);

        // усложняем уровень.
        hardLevel += addHardLevel;

        _timeLevelManager.setTime(hardLevel);
    }

    /// <summary>
    /// Сравнить нарисованую игроком фигуру, с заданой фигурой. 
    /// </summary>
    public void simileFigure()
    {
        bool simile = _simileManager.compare(_figureManager.currFigure, _drawManager.pointsList);

        if (simile)
        {
            _figureManager.destroyCurrFigure();
            createNextLevel();
        }

        _drawManager.clean();
        _simileManager.clean();
    }

    /// <summary>
    /// Время уровня закончилось.
    /// </summary>
    public void timeLevelEnded()
    {
		_eventManager.removeEvent ();
		_drawManager.destroy();
        Application.LoadLevel(2);
    }
}
